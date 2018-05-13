namespace ForumSystem.Core.Users
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Shared;

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetByUsername(string username)
        {
            User user = await _unitOfWork.Users.GetByUsername(username);

            if (user == null)
            {
                throw new UserNotFoundException(username);
            }

            return user;
        }

        public async Task<EntityCreatedResult> Create(User user)
        {
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChanges();

            return new EntityCreatedResult { Id = user.Id };

        }
    }
}
