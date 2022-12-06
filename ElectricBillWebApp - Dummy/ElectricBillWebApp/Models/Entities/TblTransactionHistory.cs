using System;
using System.Collections.Generic;

namespace ElectricBillWebApp.Models.Entities
{
    public partial class TblTransactionHistory
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long BillId { get; set; }
        public long PayId { get; set; }
        public string MeterNo { get; set; } = null!;
        public decimal? PrepaidAmount { get; set; }
        public decimal? RemainAmount { get; set; }
        public decimal? FineAmount { get; set; }
        public byte? LateMonthCount { get; set; }
        public string BillPeriod { get; set; } = null!;
        public byte? Status { get; set; }

        public virtual TblBillHistory Bill { get; set; } = null!;
        public virtual TblCustomer Customer { get; set; } = null!;
        public virtual TblPayHistory Pay { get; set; } = null!;
    }
}
