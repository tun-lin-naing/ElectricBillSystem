using System;
using System.Collections.Generic;

namespace ElectricBillWebApp.Models.Entities
{
    public partial class CustomerMeter
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long UserId { get; set; }
        public long MeterId { get; set; }
        public string? Status { get; set; }

        public virtual TblCustomer Customer { get; set; } = null!;
        public virtual TblMeterType Meter { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;
    }
}
