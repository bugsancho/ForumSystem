namespace ForumSystem.Core.Tests.Posts
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Logging;
    using ForumSystem.Core.Posts;
    using ForumSystem.Core.Shared;
    using ForumSystem.Core.Threads;
    using ForumSystem.Core.Users;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ThreadsServiceTests
    {
        private ForumThreadsService _threadsService;

        private Mock<IUnitOfWork> _uowMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IPostsService> _postsServiceMock;
        private Mock<ILogger> _loggerMock;
        private Mock<IRepository<ForumThread>> _threadsRepositoryMock;

        [SetUp]
        public void Initialize()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _userServiceMock = new Mock<IUserService>();
            _postsServiceMock = new Mock<IPostsService>();
            _loggerMock = new Mock<ILogger>();
            _threadsRepositoryMock = new Mock<IRepository<ForumThread>>();

            _uowMock.SetupGet(x => x.ForumThreads).Returns(_threadsRepositoryMock.Object);

            _threadsService = new ForumThreadsService(_uowMock.Object, _userServiceMock.Object, _postsServiceMock.Object, _loggerMock.Object);
        }



        [Test]
        public async Task GetById_ReturnsCorrectPost_WhenProvidedValidId()
        {
            // Arrange
            ForumThread thread = new ForumThread { Id = 11 };
            _threadsRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(thread);

            // Act
            ThreadDetailsModel result = await _threadsService.GetById(thread.Id);

            // Assert
            Assert.That(result.Id, Is.EqualTo(thread.Id));
        }

        [Test]
        public async Task GetById_CallsRepositoryWithCorrectId_WhenProvidedValidId()
        {
            // Arrange
            ForumThread thread = new ForumThread { Id = 11 };
            _threadsRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(thread);

            // Act
            ThreadDetailsModel result = await _threadsService.GetById(thread.Id);

            // Assert
            _threadsRepositoryMock.Verify(x => x.GetById(thread.Id));

        }

        [Test]
        public async Task Create_RetrievesUser_WhenUsernameIsProvided()
        {
            // Arrange
            _uowMock.Setup(x => x.BeginTransaction()).Returns(new Mock<ITransaction>().Object);
            CreateThreadModel createModel = new CreateThreadModel
            {
                Username = "fancy@user.name",
                InitialPost = new CreatePostModel() { Content = "contzntz" }
            };
            User user = new User { Username = "fancy@user.name", Id = 123 };

            _userServiceMock.Setup(x => x.GetByUsername(createModel.Username)).ReturnsAsync(user);

            // Act
            await _threadsService.Create(createModel);

            _userServiceMock.Verify(x => x.GetByUsername(createModel.Username));
        }

        [Test]
        public async Task Create_PersistsUserInfo_WhenUsernameIsProvided()
        {
            // Arrange
            _uowMock.Setup(x => x.BeginTransaction()).Returns(new Mock<ITransaction>().Object);
            CreateThreadModel createModel = new CreateThreadModel
            {
                Username = "fancy@user.name",
                InitialPost = new CreatePostModel() { Content = "contzntz" }
            };
            User user = new User { Username = "fancy@user.name", Id = 123 };

            _userServiceMock.Setup(x => x.GetByUsername(createModel.Username)).ReturnsAsync(user);

            // Act
            await _threadsService.Create(createModel);

            _threadsRepositoryMock.Verify(x => x.Add(It.Is<ForumThread>(thread => thread.UserId == user.Id)));
        }

        [Test]
        public async Task Create_DoesntContainUserInformation_WhenUsernameIsNotProvided()
        {
            // Arrange
            _uowMock.Setup(x => x.BeginTransaction()).Returns(new Mock<ITransaction>().Object);
            CreateThreadModel createModel = new CreateThreadModel
            {
                InitialPost = new CreatePostModel { Content = "contzntz" }
            };


            // Act
            await _threadsService.Create(createModel);

            _threadsRepositoryMock.Verify(x => x.Add(It.Is<ForumThread>(thread => thread.UserId == null)));

        }

        [Test]
        public async Task Create_ProvidesPostsServiceWithCorrectThreadId_WhenThreadWasCreatedSuccessfully()
        {
            // Arrange
            _uowMock.Setup(x => x.BeginTransaction()).Returns(new Mock<ITransaction>().Object);
            CreateThreadModel createModel = new CreateThreadModel
            {
                InitialPost = new CreatePostModel { Content = "contzntz" }
            };

            _threadsRepositoryMock.Setup(x => x.Add(It.IsAny<ForumThread>()))
                                  .Callback<ForumThread>(thread => thread.Id = 12);

            // Act
            await _threadsService.Create(createModel);

            _postsServiceMock.Verify(x => x.CreatePost(It.Is<CreatePostModel>(post => post.ThreadId == 12)));
        }

        [Test]
        public async Task Create_ReturnsCorrectThreadId_WhenThreadWasCreatedSuccessfully()
        {
            // Arrange
            _uowMock.Setup(x => x.BeginTransaction()).Returns(new Mock<ITransaction>().Object);
            CreateThreadModel createModel = new CreateThreadModel
            {
                InitialPost = new CreatePostModel { Content = "contzntz" }
            };

            _threadsRepositoryMock.Setup(x => x.Add(It.IsAny<ForumThread>()))
                                  .Callback<ForumThread>(thread => thread.Id = 12);

            // Act
            EntityCreatedResult result = await _threadsService.Create(createModel);

            Assert.That(result.Id, Is.EqualTo(12));
        }
    }
}
