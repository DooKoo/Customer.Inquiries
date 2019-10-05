using System;
namespace Customer.Inquiries.DataAccess.Base
{
    public abstract class BaseEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }//For soft delete

        public BaseEntity()
        {
        }
    }
}
