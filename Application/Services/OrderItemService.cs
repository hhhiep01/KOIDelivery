using Application.Interface;
using Application.Request.OrderItem;
using Application.Response;
using AutoMapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public OrderItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> AddOrderItemAsync(OrderItemRequest orderItemRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                int totalSlotsUsed = 0;
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == orderItemRequest.OrderId);
                if (order == null)
                {
                    return new ApiResponse().SetBadRequest("Not Found Order Id " + orderItemRequest.OrderId);
                }
                var existingOrderItems = await _unitOfWork.OrderItems.GetAllAsync(x => x.OrderId == orderItemRequest.OrderId);


                var newKoiSizeIds = orderItemRequest.OrderItemDetails.Select(x => x.KoiSizeId).ToList();


                foreach (var existingItem in existingOrderItems)
                {
                    if (!newKoiSizeIds.Contains(existingItem.KoiSizeId))
                    {
                        await _unitOfWork.OrderItems.RemoveByIdAsync(existingItem.Id);

                    }
                }
                //await _unitOfWork.SaveChangeAsync();

                foreach (var itemDetail in orderItemRequest.OrderItemDetails)
                {
                    var koiSize = await _unitOfWork.KoiSizes.GetAsync(x => x.Id == itemDetail.KoiSizeId);
                    if (koiSize == null)
                    {
                        return new ApiResponse().SetBadRequest("Not Found Koi size Id " + itemDetail.KoiSizeId);
                    }
                    var existingOrderItem = await _unitOfWork.OrderItems.GetAsync(oi => oi.OrderId == orderItemRequest.OrderId && oi.KoiSizeId == itemDetail.KoiSizeId);
                    if (existingOrderItem != null)
                    {
                        existingOrderItem.Quantity = itemDetail.Quantity;
                    }
                    else
                    {
                        var newOrderItem = new OrderItem
                        {
                            OrderId = orderItemRequest.OrderId,
                            KoiSizeId = itemDetail.KoiSizeId,
                            Quantity = itemDetail.Quantity
                        };
                        await _unitOfWork.OrderItems.AddAsync(newOrderItem);
                    }
                }
                await _unitOfWork.SaveChangeAsync();
                var orderItems = await _unitOfWork.OrderItems.GetAllAsync(x => x.OrderId == orderItemRequest.OrderId);
                foreach (var item in orderItems)
                {
                    var koSize = await _unitOfWork.KoiSizes.GetAsync(x => x.Id == item.KoiSizeId);
                    var space = koSize.SpaceRequired;
                    var totalSpace = item.Quantity * space;
                    totalSlotsUsed += totalSpace;
                }

                // 5. Phân bổ thùng ban đầu


                var boxTypes = await _unitOfWork.BoxTypes.GetAllAsync(null);
                int remainingSlots = totalSlotsUsed;
                var boxAllocationsDict = new Dictionary<string, int>();

                foreach (var box in boxTypes.OrderByDescending(b => b.Capacity))
                {
                    int boxCount = remainingSlots / box.Capacity;
                    if (boxCount > 0)
                    {
                        if (boxAllocationsDict.ContainsKey(box.BoxName))
                        {
                            boxAllocationsDict[box.BoxName] += boxCount;
                        }
                        else
                        {
                            boxAllocationsDict[box.BoxName] = boxCount;
                        }
                        remainingSlots %= box.Capacity;
                    }
                }

                // Xử lý phần dư trong thùng cuối cùng
                int lastBoxRemainingSlots = 0;
                if (remainingSlots > 0)
                {
                    var smallestBox = boxTypes.OrderBy(b => b.Capacity).First();
                    if (boxAllocationsDict.ContainsKey(smallestBox.BoxName))
                    {
                        boxAllocationsDict[smallestBox.BoxName] += 1;
                    }
                    else
                    {
                        boxAllocationsDict[smallestBox.BoxName] = 1;
                    }
                    lastBoxRemainingSlots = smallestBox.Capacity - remainingSlots;
                }

                // 6. Tối ưu hóa thùng
                OptimizeBoxAllocation(boxTypes, boxAllocationsDict);
                decimal totalBoxCost = 0;
                foreach (var allocation in boxAllocationsDict)
                {
                    var boxType = boxTypes.FirstOrDefault(b => b.BoxName == allocation.Key);
                    if (boxType != null)
                    {
                        totalBoxCost += boxType.ShippingCost * allocation.Value;
                    }
                }

                // 7. Gợi ý số lượng cá có thể thêm vào
                var koiSizes = await _unitOfWork.KoiSizes.GetAllAsync(null);
                var suggestions = koiSizes.Select(koiSize =>
                {
                    // Nếu không gian dư là 0, tất cả các gợi ý phải là 0
                    int maxQuantity = lastBoxRemainingSlots == 0 ? 0 : lastBoxRemainingSlots / koiSize.SpaceRequired;
                    return $"{maxQuantity} of {koiSize.SizeCmMax}cm({koiSize.SizeInchMax})";
                }).ToList();
                decimal totalPriceTransportService = 0;
                var transportService = await _unitOfWork.TransportServices.GetAsync(x => x.Id == order.TransportServiceId);
                if (transportService.TransportType == TransportType.Local)
                {
                    totalPriceTransportService +=
                                                (transportService.PricePerKm ?? 0) * (decimal)order.Distance;
                }
                else
                {
                    totalPriceTransportService += (transportService.TransportPrice ?? 0);
                }
                var totalPrice = totalBoxCost + totalPriceTransportService;
                order.TotalPrice = totalPrice;
                await _unitOfWork.SaveChangeAsync();
                // 8. Trả về kết quả
                return apiResponse.SetOk(new
                {
                    TotalSlotsUsed = totalSlotsUsed,
                    BoxAllocations = boxAllocationsDict.Select(b => $"{b.Value} {b.Key}").ToList(),
                    TotalBoxCost = totalBoxCost,
                    TotalTransportCost = totalPriceTransportService,
                    TotalPrice = totalPrice,
                    LastBoxRemainingSlots = lastBoxRemainingSlots,
                    Suggestions = suggestions
                });
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        private void OptimizeBoxAllocation(List<BoxType> boxTypes, Dictionary<string, int> boxAllocations)
        {
            var boxList = boxTypes.OrderBy(b => b.Capacity).ToList(); // Sắp xếp thùng theo sức chứa tăng dần

            for (int i = 0; i < boxList.Count - 1; i++)
            {
                var currentBox = boxList[i];
                var nextBox = boxList[i + 1];

                if (boxAllocations.ContainsKey(currentBox.BoxName) && boxAllocations[currentBox.BoxName] >= 2)
                {
                    int currentBoxCount = boxAllocations[currentBox.BoxName];
                    int pairCount = currentBoxCount / 2; // Số cặp có thể thay thế

                    // Thay thế bằng thùng lớn hơn nếu phù hợp
                    if (nextBox.Capacity <= currentBox.Capacity * 2)
                    {
                        if (boxAllocations.ContainsKey(nextBox.BoxName))
                        {
                            boxAllocations[nextBox.BoxName] += pairCount;
                        }
                        else
                        {
                            boxAllocations[nextBox.BoxName] = pairCount;
                        }

                        boxAllocations[currentBox.BoxName] -= pairCount * 2;

                        // Xóa thùng nhỏ nếu không còn
                        if (boxAllocations[currentBox.BoxName] == 0)
                        {
                            boxAllocations.Remove(currentBox.BoxName);
                        }
                    }
                }
            }
        }


    }
}
