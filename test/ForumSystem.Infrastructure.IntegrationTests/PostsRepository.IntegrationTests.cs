using System;

namespace ForumSystem.Core.IntegrationTests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.IntegrationTests.Base;
    using ForumSystem.Infrastructure.Data;

    using NUnit.Framework;

    [TestFixture]
    public class PostsRepositoryIntegrationTests : IntegrationTestBase
    {
        private PostsRepository _postsRepository;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _postsRepository = new PostsRepository(DbContext);

        }

        [Test]
        public async Task GetByThreadId_ReturnsPost_WhenProvidedValidThreadId()
        {
            // Arrange
            ForumThread thread = new ForumThread
            {
                Title = "123",
                ForumPosts =
                     new List<ForumPost> { new ForumPost { Content = "test" } }
            };

            DbContext.Threads.Add(thread);
            await DbContext.SaveChangesAsync();

            // Act
            IReadOnlyCollection<ForumPost> posts = await _postsRepository.GetByThread(thread.Id);

            // Assert
            Assert.That(posts, Has.Count.EqualTo(1));
        }

    }
}
