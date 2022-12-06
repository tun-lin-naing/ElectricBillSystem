using System;
using System.Collections.Generic;

namespace ElectricBillWebApp.Models.Entities
{
    public partial class TblPayHistory
    {
        public TblPayHistory()
        {
            TblTransactionHistories = new HashSet<TblTransactionHistory>();
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long UserId { get; set; }
        public long BillId { get; set; }
        public decimal PayAmount { get; set; }
        public DateTime PayDate { get; set; }
        public long Method { get; set; }
        public string Code { get; set; } = null!;
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual TblBillHistory Bill { get; set; } = null!;
        public virtual TblCustomer Customer { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;
        public virtual ICollection<TblTransactionHistory> TblTransactionHistories { get; set; }
    }
}
