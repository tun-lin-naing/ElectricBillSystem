using ElectricBillWebApp.Models.Pay;

namespace ElectricBillWebApp.Models.Bill
{
    public class ResPayBill
    {
        public long CustomerId { get; set; }
        public long BillId { get; set; }
        public string Name { get; set; }
        public string MeterNo { get; set; }
        public string Township { get; set; }
        public long LastUnit { get; set; }
        public long CurrentUnit { get; set; }
        public long UsedUnit { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime DueDate { get; set; }
        public string BillPeriod { get; set; }
        public decimal Amount { get; set; }
        public decimal PrepaidAmount { get; set; }
        public decimal RemainAmount { get; set; }
        public decimal FineAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<PayMethod> PayMethod { get; set; }
        public string? PayMethodString { get; set; }


    }
}
