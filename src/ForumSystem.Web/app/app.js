(function () {
    'use strict';

    angular.module('forumSystem.shared', []);
    angular.module('forumSystem.auth', ['forumSystem.shared']);
    angular.module('forumSystem.threads', ['forumSystem.shared']);
    angular.module('forumSystem.statistics', ['forumSystem.shared', 'chart.js']);
    angular.module('forumSystem.posts', ['forumSystem.shared', 'angularMoment']);

// ReSharper disable once UndeclaredGlobalVariableUsing
    angular.module('forumSystem', ['ui.bootstrap', 'ngTouch', 'ngAnimate', 'ui.router', 'forumSystem.auth', 'forumSystem.threads', 'forumSystem.posts', 'forumSystem.statistics'])
       
        .run(function (authService) {
            authService.processRedirectInfo();
        });

    angular.module('forumSystem').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });
})();