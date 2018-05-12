namespace ForumSystem.Core.Entities.Base
{
    using System;

    public interface IAuditedEntity
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}