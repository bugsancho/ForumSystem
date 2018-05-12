(function () {
    'use strict';

    angular.module('forumSystem.shared', []);
    angular.module('forumSystem.auth', ['forumSystem.shared']);

// ReSharper disable once UndeclaredGlobalVariableUsing
    angular.module('forumSystem', ['ui.bootstrap', 'ngTouch', 'ngAnimate', 'forumSystem.auth'])
        //TODO move to route guard only for required components
        .run(['authService',function (authService) {
            authService.ensureAuthenticated();
        }]);
    
})();