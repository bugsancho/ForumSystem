(function () {
    'use strict';

    angular.module('forumSystem.shared', []);
    angular.module('forumSystem.auth', ['forumSystem.shared']);
    angular.module('forumSystem.threads', ['forumSystem.shared']);
    angular.module('forumSystem.posts', ['forumSystem.shared', 'angularMoment']);

// ReSharper disable once UndeclaredGlobalVariableUsing
    angular.module('forumSystem', ['ui.bootstrap', 'ngTouch', 'ngAnimate', 'ui.router','forumSystem.auth', 'forumSystem.threads', 'forumSystem.posts'])
        //TODO move to route guard only for required components
        .run(['authService', function (authService) {

            authService.processRedirectInfo();
        }]);


    angular.module('forumSystem').config(function ($httpProvider) {

        $httpProvider.interceptors.push('authInterceptor');
    });
})();