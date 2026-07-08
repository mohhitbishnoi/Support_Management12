namespace Domain.Commons;

public class BaseAuditableEntity:IBaseAuditableEntity
{
    public int Id { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; } 
}

