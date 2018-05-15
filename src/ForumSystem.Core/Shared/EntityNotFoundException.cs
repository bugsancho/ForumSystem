namespace ForumSystem.Core.Shared
{
    using System;

    using ForumSystem.Core.Entities.Base;

    public class EntityNotFoundException<T> : Exception where T : IEntity
    {
        public EntityNotFoundException(int id) : base($"Entity of type: {typeof(T)} with id: {id} could not be found")
        {
        }
    }
}
