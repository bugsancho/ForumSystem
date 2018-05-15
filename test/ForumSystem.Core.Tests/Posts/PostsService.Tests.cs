namespace ForumSystem.Core.Tests.Posts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Posts;
    using ForumSystem.Core.Users;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PostsServiceTests
    {
        private PostsService _postsService;
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IPostsRepository> _postsRepositoryMock;

        [SetUp]
        public void Initialize()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _userServiceMock = new Mock<IUserService>();
            _postsRepositoryMock = new Mock<IPostsRepository>();
            _uowMock.SetupGet(x => x.ForumPosts).Returns(_postsRepositoryMock.Object);
            _postsService = new PostsService(_uowMock.Object, _userServiceMock.Object);
        }

        [Test]
        public async Task GetById_ReturnsCorrectPost_WhenProvidedValidId()
        {
            // Arrange
            ForumPost post = new ForumPost { Content = "test", Id = 11 };
            _postsRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(post);

            // Act
            PostDetailsModel result = await _postsService.GetById(post.Id);

            // Assert
            Assert.That(result.Id, Is.EqualTo(post.Id));
        }

  

        [Test]
        public async Task GetById_CallsRepositoryWithCorrectId_WhenProvidedValidId()
        {
            // Arrange
            ForumPost post = new ForumPost { Content = "test", Id = 11 };
            _postsRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(post);

            // Act
            await _postsService.GetById(post.Id);

            // Assert
            _postsRepositoryMock.Verify(x => x.GetById(post.Id));
        }

        [Test]
        public async Task GetByThread_ReturnsAllPosts_WhenProvidedValidId()
        {
            // Arrange
            var posts = new List<ForumPost>
                            {
                                new ForumPost { Content = "test", Id = 11, ThreadId = 2},
                                new ForumPost { Content = "test", Id = 22, ThreadId = 2}
                            };
            _postsRepositoryMock.Setup(x => x.GetByThread(It.IsAny<int>())).ReturnsAsync(posts);

            // Act
            IReadOnlyCollection<PostDetailsModel> result = await _postsService.GetByThread(2);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.IsTrue(result.Any(x => x.Id == 11));
            Assert.IsTrue(result.Any(x => x.Id == 22));
        }

        [Test]
        public async Task GetByThread_CallsRepositoryWithCorrectId_WhenProvidedValidId()
        {
            // Arrange
            var posts = new List<ForumPost>
                            {
                                new ForumPost { Content = "test", Id = 11, ThreadId = 2},
                                new ForumPost { Content = "test", Id = 22, ThreadId = 2}
                            };
            _postsRepositoryMock.Setup(x => x.GetByThread(It.IsAny<int>())).ReturnsAsync(posts);

            // Act
            await _postsService.GetByThread(2);
            // Assert
            _postsRepositoryMock.Verify(x => x.GetByThread(2));
        }

    }
}
