namespace ForumSystem.Core.Entities.Base
{
    using System;

    public class BaseEntity : ISoftDeletableEntity, IAuditedEntity, IEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int Id { get; set; }
    }
}
