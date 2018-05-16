using System;

namespace ForumSystem.Core.IntegrationTests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.IntegrationTests.Base;
    using ForumSystem.Infrastructure.Data;

    using Moq;

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


        [Test]
        public async Task GetByThreadId_FiltersPostsByThreadId_WhenProvidedValidThreadId()
        {
            // Arrange
            ForumThread thread = new ForumThread
            {
                Title = "123",
                ForumPosts =
                                             new List<ForumPost>
                                                 {
                                                     new ForumPost { Content = "test" },
                                                     new ForumPost { Content = "123432" }
                                                 }
            };

            ForumThread thread2 = new ForumThread
            {
                Title = "123",
                ForumPosts =
                     new List<ForumPost> { new ForumPost { Content = "test" } }
            };

            await SeedData(
                context =>
                    {
                        context.Threads.Add(thread);
                        context.Threads.Add(thread2);
                    });

            // Act
            IReadOnlyCollection<ForumPost> posts = await _postsRepository.GetByThread(thread.Id);

            // Assert
            Assert.That(posts, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetByThreadId_ReturnsUserInformation_WhenUserInformationIsAvailable()
        {
            // Arrange
            ForumThread thread = new ForumThread
            {
                Title = "123",
                ForumPosts =
                                             new List<ForumPost>
                                                 {
                                                     new ForumPost
                                                         {
                                                             Content = "test",
                                                             User = new User
                                                                        {
                                                                            Username
                                                                                = "test@test.test"
                                                                        }
                                                         }
                                                 }
            };


            await SeedData(x => x.Threads.Add(thread));

            // Act
            IReadOnlyCollection<ForumPost> posts = await _postsRepository.GetByThread(thread.Id);

            // Assert
            Assert.That(posts.Single().User, Is.Not.Null);
        }


        [Test]
        public async Task GetByThreadId_OrdersPostByDate_WhenMultiplePostsAreAvailable()
        {
            // The CreatedOn property is handled by the DBContext, so we cannot manually change that.
            // For this test we rely on the clock, so we wait a few milliseconds to ensure different timestamps.

            // Arrange
            ForumThread thread = new ForumThread
            {
                Title = "123",
                ForumPosts =
                                             new List<ForumPost>
                                                 {
                                                     new ForumPost
                                                         {
                                                             Content = "test"
                                                         }
                                                 }
            };


            await SeedData(x => x.Threads.Add(thread));

            await Task.Delay(50);

            ForumPost secondPost = new ForumPost { Content = "second", ThreadId = thread.Id };
            await SeedData(x => x.Posts.Add(secondPost));

            // Act
            IReadOnlyCollection<ForumPost> posts = await _postsRepository.GetByThread(thread.Id);

            // Assert
            Assert.That(posts.Count, Is.EqualTo(2));
            Assert.That(posts.Last().Content, Is.EqualTo(secondPost.Content));
        }

    }
}
