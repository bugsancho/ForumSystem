﻿(function () {
    angular.module('forumSystem').config(function ($stateProvider) {
        const threads = {
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
                component: 'threadComponent',
                resolve: {
                    thread: function (threadsService, $transition$) {
                        "ngInject";
                        return threadsService.getThread($transition$.params().threadId);
                    }
                }
            };

        $stateProvider.state(thread);
        $stateProvider.state(threads);
    });
})();