using System;
using System.Collections.Generic;

namespace ElectricBillWebApp.Models.Entities
{
    public partial class TblCustomer
    {
        public TblCustomer()
        {
            CustomerMeters = new HashSet<CustomerMeter>();
            TblBillHistories = new HashSet<TblBillHistory>();
            TblPayHistories = new HashSet<TblPayHistory>();
            TblTransactionHistories = new HashSet<TblTransactionHistory>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string Barcode { get; set; } = null!;
        public string MeterNo { get; set; } = null!;
        public string Township { get; set; } = null!;
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual TblUser? CreatedByNavigation { get; set; }
        public virtual ICollection<CustomerMeter> CustomerMeters { get; set; }
        public virtual ICollection<TblBillHistory> TblBillHistories { get; set; }
        public virtual ICollection<TblPayHistory> TblPayHistories { get; set; }
        public virtual ICollection<TblTransactionHistory> TblTransactionHistories { get; set; }
    }
}
