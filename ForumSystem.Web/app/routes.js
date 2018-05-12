(function () {
    angular.module('forumSystem').config(function($stateProvider) {
        var threads = {
                name: 'threads',
                url: '/threads',
            component: 'threadsComponent',
            resolve: {
                threads: function (threadsService) {
                    "ngInject";
                   return threadsService.getThreads();
                }
            }
            },
            thread = {
                name: 'thread',
                url: '/thread/{threadId}',
                component: 'threadComponent'};

        $stateProvider.state(thread);
        $stateProvider.state(threads);
    });
})();