angular.module('forumSystem.shared')
    .constant('urls',
        {
            getThreads: 'api/forumthreads/',
            getThread: 'api/forumthreads/',
            createThread: 'api/forumthreads/',
            posts: 'api/posts/',
            logout: 'account/logoff',
            threadStatistics: 'api/threadstatistics'
    });
