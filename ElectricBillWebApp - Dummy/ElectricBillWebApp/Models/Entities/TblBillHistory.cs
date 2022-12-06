using System;
using System.Collections.Generic;

namespace ElectricBillWebApp.Models.Entities
{
    public partial class TblBillHistory
    {
        public TblBillHistory()
        {
            TblPayHistories = new HashSet<TblPayHistory>();
            TblTransactionHistories = new HashSet<TblTransactionHistory>();
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long UserId { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime DueDate { get; set; }
        public long? LastUnit { get; set; }
        public long CurrentUnit { get; set; }
        public long UsedUnit { get; set; }
        public string Currency { get; set; } = null!;
        public decimal Amount { get; set; }
        public string BillPeriod { get; set; } = null!;
        public decimal? PrepaidAmount { get; set; }
        public decimal? RemainAmount { get; set; }
        public decimal? FineAmount { get; set; }

        public virtual TblCustomer Customer { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;
        public virtual ICollection<TblPayHistory> TblPayHistories { get; set; }
        public virtual ICollection<TblTransactionHistory> TblTransactionHistories { get; set; }
    }
}
