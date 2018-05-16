namespace ForumSystem.Infrastructure.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ForumSystem.Core.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<ForumSystem.Infrastructure.Data.ForumSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ForumSystem.Infrastructure.Data.ForumSystemDbContext context)
        {
            if (!context.Users.Any())
            {
                User admin = new User { Username = "admin@admin.com", CreatedOn = DateTime.UtcNow };
                context.Users.Add(admin);

                List<ForumThread> threads = GetThreads(admin);

                foreach (ForumThread forumThread in threads)
                {
                    context.Threads.Add(forumThread);
                }
            }


        }

        private static List<ForumThread> GetThreads(User user)
        {
            return new List<ForumThread>
                                            {
                                                new ForumThread
                                                    {
                                                        User = user,
                                                        Title = "Very clever question",
                                                        CreatedOn = DateTime.UtcNow,
                                                        ForumPosts =
                                                            new List<ForumPost>()
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "Post post post",
                                                                                User= user,
                                                                                CreatedOn = DateTime.UtcNow
                                                                            },
                                                                    new
                                                                        ForumPost()
                                                                            {
                                                                                Content
                                                                                    = "Excelent post",
                                                                                CreatedOn = DateTime.UtcNow
                                                                            },

                                                                    new
                                                                        ForumPost()
                                                                            {
                                                                                Content
                                                                                    = "Thank you!",
                                                                                User
                                                                                    = user,
                                                                                CreatedOn = DateTime.UtcNow
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        User = user,
                                                        Title = "Very clever question2",
                                                        CreatedOn = DateTime.UtcNow,
                                                        ForumPosts =
                                                            new List<ForumPost>()
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "Post post post 2",
                                                                                User
                                                                                    = user,
                                                                                CreatedOn = DateTime.UtcNow
                                                                            },
                                                                    new
                                                                        ForumPost()
                                                                            {
                                                                                Content
                                                                                    = "Excelent post 2",
                                                                                CreatedOn = DateTime.UtcNow
                                                                            },
                                                                    new
                                                                        ForumPost()
                                                                            {
                                                                                Content
                                                                                    = "Thank you! 2",
                                                                                User
                                                                                    = user,
                                                                                CreatedOn = DateTime.UtcNow
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        User = user,
                                                        Title = "Not so clever question",
                                                        CreatedOn = DateTime.UtcNow,
                                                        ForumPosts =
                                                            new List<ForumPost>
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "Pica pica",
                                                                                User
                                                                                    = user,
                                                                                CreatedOn = DateTime.UtcNow
                                                                            },
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "Choo Choo",
                                                                                CreatedOn = DateTime.UtcNow
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        Title = "Anonymous post",
                                                        CreatedOn = DateTime.UtcNow,
                                                        ForumPosts =
                                                            new List<ForumPost>
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "anon anon",
                                                                                CreatedOn = DateTime.UtcNow
                                                                            },
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "non anon response",
                                                                                User
                                                                                    = user,
                                                                                CreatedOn = DateTime.UtcNow
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        Title = "Getting short on ideas",
                                                        CreatedOn = DateTime.UtcNow,
                                                        ForumPosts =
                                                            new List<ForumPost>
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "idea idea",
                                                                                CreatedOn = DateTime.UtcNow
                                                                            },
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "non anon response",
                                                                                User
                                                                                    = user,
                                                                                CreatedOn = DateTime.UtcNow
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        Title = "Sixt thread!",
                                                        CreatedOn = DateTime.UtcNow,
                                                        ForumPosts =
                                                            new List<ForumPost>
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "66",
                                                        CreatedOn = DateTime.UtcNow
                                                                            },
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "one 6 short of the devil",
                                                                                User
                                                                                    = user,
                                                                                CreatedOn = DateTime.UtcNow
                                                                            }
                                                                }
                                                    },
                                            };
        }
    }
}
