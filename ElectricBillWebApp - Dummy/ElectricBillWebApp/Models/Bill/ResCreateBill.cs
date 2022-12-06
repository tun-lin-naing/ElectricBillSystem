namespace ElectricBillWebApp.Models.Bill
{
    public class ResCreateBill
    {
        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string MeterNo { get; set; }
        public string Township { get; set; }
        public long LastUnit { get; set; }
        public long CurrentUnit { get; set; }
        public long UsedUnit { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime DueDate { get; set; }
        public string BillPeriod { get; set; }

    }
}
