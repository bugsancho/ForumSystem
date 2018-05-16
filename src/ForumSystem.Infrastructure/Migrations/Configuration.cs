namespace ForumSystem.Infrastructure.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using ForumSystem.Core.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<ForumSystem.Infrastructure.Data.ForumSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ForumSystem.Infrastructure.Data.ForumSystemDbContext context)
        {
            User admin = new User { Username = "admin@admin.com", CreatedOn = DateTime.UtcNow };
            context.Users.Add(admin);

            List<ForumThread> threads = GetThreads(admin);

            foreach (ForumThread forumThread in threads)
            {
                context.Threads.Add(forumThread);
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
                                                        ForumPosts =
                                                            new List<ForumPost>()
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "Post post post",
                                                                                User
                                                                                    = user
                                                                            },
                                                                    new
                                                                        ForumPost()
                                                                            {
                                                                                Content
                                                                                    = "Excelent post"
                                                                            },
                                                                    new
                                                                        ForumPost()
                                                                            {
                                                                                Content
                                                                                    = "Thank you!",
                                                                                User
                                                                                    = user
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        User = user,
                                                        Title = "Very clever question2",
                                                        ForumPosts =
                                                            new List<ForumPost>()
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "Post post post 2",
                                                                                User
                                                                                    = user
                                                                            },
                                                                    new
                                                                        ForumPost()
                                                                            {
                                                                                Content
                                                                                    = "Excelent post 2"
                                                                            },
                                                                    new
                                                                        ForumPost()
                                                                            {
                                                                                Content
                                                                                    = "Thank you! 2",
                                                                                User
                                                                                    = user
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        User = user,
                                                        Title = "Not so clever question",
                                                        ForumPosts =
                                                            new List<ForumPost>
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "Pica pica",
                                                                                User
                                                                                    = user
                                                                            },
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "Choo Choo"
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        Title = "Anonymous post",
                                                        ForumPosts =
                                                            new List<ForumPost>
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "anon anon",
                                                                            },
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "non anon response",
                                                                                User
                                                                                    = user
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        Title = "Getting short on ideas",
                                                        ForumPosts =
                                                            new List<ForumPost>
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "idea idea",
                                                                            },
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "non anon response",
                                                                                User
                                                                                    = user
                                                                            }
                                                                }
                                                    },
                                                new ForumThread
                                                    {
                                                        Title = "Sixt thread!",
                                                        ForumPosts =
                                                            new List<ForumPost>
                                                                {
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "66",
                                                                            },
                                                                    new
                                                                        ForumPost
                                                                            {
                                                                                Content
                                                                                    = "one 6 short of the devil",
                                                                                User
                                                                                    = user
                                                                            }
                                                                }
                                                    },
                                            };
        }
    }
}
