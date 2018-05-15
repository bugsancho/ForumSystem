angular.module('forumSystem.shared')
    .constant('urls',
        {
            getThreads: 'api/forumthreads/',
            getThread: 'api/forumthreads/',
            createThread: 'api/forumthreads/',
            posts: 'api/posts/',
            logout: 'account/logoff',
            threadStatistics: 'api/threadstatistics',
            user: '/api/user'
    })
    // Provide Underscore for angular components
    .constant('_', _)
    .constant('events',
        {
            userLoggedIn: 'userLoggedIn'
        });


