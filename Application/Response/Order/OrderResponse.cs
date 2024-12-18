﻿using Application.Response.BoxAllocation;
using Application.Response.Fish;
using Application.Response.OrderItem;
using Application.Response.RouteStop;
using Application.Response.TransportService;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Order
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public double Distance { get; set; }
        public string ReceiverPhone { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Notes { get; set; } = string.Empty;
        public PaymentMethodEnum PaymentMethod { get; set; }
       /* public string FromProvince { get; set; } = string.Empty;
        public string ToProvince { get; set; } = string.Empty;*/
        public float FeedbackStars { get; set; }
        public string FeedbackContent { get; set; } = string.Empty;
        public string ReasonToCancel { get; set; } = string.Empty;
        public TransportServiceResponse TransportService { get; set; }
        //public List<OrderFishResponse> OrderFishes { get; set; }
        public List<RouteStopResponse> RouteStops { get; set; }
        public List<BoxAllocationResponse> BoxAllocations { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; }
    }
}
