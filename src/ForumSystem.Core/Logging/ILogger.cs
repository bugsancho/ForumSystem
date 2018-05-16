namespace ForumSystem.Core.Logging
{
    using System;

    public interface ILogger
    {
        void Debug(string message, object data = null);

        void Info(string message, object data = null);

        void Warning(string message, object data = null);

        void Error(Exception exception, object data = null);
    }
}
