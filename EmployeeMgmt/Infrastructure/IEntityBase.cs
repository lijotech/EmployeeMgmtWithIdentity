namespace EmployeeMgmt.Infrastructure
{
    /// <summary>
    /// Base class for Entities, provide some common fields and use by repositories
    /// </summary>
    public interface IEntityBase<T>
    {
        T Id { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }

    public interface IEntityBaseLocalDate<T> : IEntityBase<T>
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
    public interface IEntityBaseGlobalDate<T> : IEntityBase<T>
    {
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset? UpdatedDate { get; set; }
    }

    public interface IEntityBaseGlobalDate : IEntityBaseGlobalDate<string>
    {

    }
    public interface IEntityBaseLocalDate : IEntityBaseLocalDate<string>
    {
    }

    public interface IEntityBase : IEntityBase<string>
    {
    }


    public class EntityBase<T> : IEntityBaseLocalDate<T>
    {
        public virtual T Id { get; set; }

        public virtual DateTime CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }

        public virtual DateTime? UpdatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
    }

    public class EntityBaseGlobal<T> : IEntityBaseGlobalDate<T>
    {
        public virtual T Id { get; set; }

        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }

        public virtual DateTimeOffset? UpdatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
    }

    public class EntityBaseGlobal : EntityBaseGlobal<string>, IEntityBaseGlobalDate
    {

    }
    public class EntityBase : EntityBase<string>, IEntityBase
    {
    }
}
