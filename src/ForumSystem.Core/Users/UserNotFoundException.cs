namespace ForumSystem.Core.Users
{
    using System;

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username) : base($"User with username: {username} could not be found.")
        {
            Username = username;
        }

        public string Username { get; }
    }
}
