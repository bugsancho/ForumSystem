(function () {
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
            },
            createThread = {
                name: 'threadCreate',
                url: '/thread/create',

                component: 'editThreadComponent'

            }, editPost = {
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

        $stateProvider.state(createThread);
        $stateProvider.state(thread);
        $stateProvider.state(threads);
        $stateProvider.state(editPost);

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