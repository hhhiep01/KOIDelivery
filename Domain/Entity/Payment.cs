using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Payment :Base
    {
        public int Id { get; set; }
        public StatusPayment StatusPayment { get; set; }
        public double Amount { get; set; }
       /* public string OrderType { get; set; }
        //public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }*/
        //
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
    public enum StatusPayment
    {
        NotCompleted,
        Success,
        Failed,
        Pending,
        Paid        
    }
    
}
