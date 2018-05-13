angular.module('forumSystem.shared')
    .constant('urls',
        {
            getThreads: 'api/forumthreads/',
            getThread: 'api/forumthreads/',
            createThread: 'api/forumthreads/',
            logout: 'account/logoff'
        })