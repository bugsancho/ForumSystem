﻿namespace ForumSystem.Core.Data
{
    using System;

    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
