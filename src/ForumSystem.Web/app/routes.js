﻿(function () {
    angular.module('forumSystem').config(function ($stateProvider, $urlRouterProvider) {
        const threads = {
            name: 'threads',
            url: '/threads/{page:int}',
            params: {
                page: { value: null, squash: false }
            },
            component: 'threadsComponent',
            resolve: {
                threads: function (threadsService, $transition$) {
                    "ngInject";
                    return threadsService.getThreads($transition$.params().page);
                }
            }
        },
            createThread = {
                name: 'threadCreate',
                url: '/thread/create',

                component: 'editThreadComponent'

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
            },
            threadStatistics = {
                name: 'threadStatistics',
                url: '/thread/{threadId}/stats',
                component: 'threadStatistics'
            },
            editPost = {
                name: 'editPost',
                url: '/post/{postId}/edit',
                resolve: {
                    post: function (postsService, $transition$) {
                        "ngInject";
                        return postsService.getPost($transition$.params().postId);
                    },
                    authenticate: authenticate

                },
                component: 'editPost'
            };

        $stateProvider.state(threads);
        $stateProvider.state(createThread);
        $stateProvider.state(thread);
        $stateProvider.state(threadStatistics);
        $stateProvider.state(editPost);

        $urlRouterProvider.otherwise('/threads');

        function authenticate(authService, $transition$) {
            "ngInject";

            const returnRoute = getRouteFromTransition($transition$);
            return authService.ensureAuthenticated(returnRoute);

        }

        function getRouteFromTransition(transition) {
            const toState = transition.to();
            const toStateName = toState.name;
            const toStateParams = transition.params(); // by default "to"
            const toHref = transition.router.stateService.href(toStateName, toStateParams, { absolute: true });
            return toHref;
        }
    });
})();