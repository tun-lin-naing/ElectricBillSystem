using System;
using System.Collections.Generic;

namespace ElectricBillWebApp.Models.Entities
{
    public partial class TblUser
    {
        public TblUser()
        {
            CustomerMeters = new HashSet<CustomerMeter>();
            TblBillHistories = new HashSet<TblBillHistory>();
            TblCustomers = new HashSet<TblCustomer>();
            TblMeterTypes = new HashSet<TblMeterType>();
            TblPayHistories = new HashSet<TblPayHistory>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CustomerMeter> CustomerMeters { get; set; }
        public virtual ICollection<TblBillHistory> TblBillHistories { get; set; }
        public virtual ICollection<TblCustomer> TblCustomers { get; set; }
        public virtual ICollection<TblMeterType> TblMeterTypes { get; set; }
        public virtual ICollection<TblPayHistory> TblPayHistories { get; set; }
    }
}
