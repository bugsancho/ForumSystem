(function () {
    'use strict';

    angular.module('forumSystem.shared', []);
    angular.module('forumSystem.auth', ['forumSystem.shared']);
    angular.module('forumSystem.threads', ['forumSystem.shared']);

// ReSharper disable once UndeclaredGlobalVariableUsing
    angular.module('forumSystem', ['ui.bootstrap', 'ngTouch', 'ngAnimate', 'ui.router','forumSystem.auth', 'forumSystem.threads'])
        //TODO move to route guard only for required components
        .run(['authService', function (authService) {

            //authService.ensureAuthenticated();
        }]);


    angular.module('forumSystem').config(function ($httpProvider) {

        $httpProvider.interceptors.push('authInterceptor');
    });
})();