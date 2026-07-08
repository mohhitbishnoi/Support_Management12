using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commons
{
    public interface IBaseAuditableEntity
    {
        public int Id {  get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        bool IsDeleted { get; set; }

    }
}
