using System;
using System.Collections.Generic;

namespace ElectricBillWebApp.Models.Entities
{
    public partial class TblMeterType
    {
        public TblMeterType()
        {
            CustomerMeters = new HashSet<CustomerMeter>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual TblUser? CreatedByNavigation { get; set; }
        public virtual ICollection<CustomerMeter> CustomerMeters { get; set; }
    }
}
