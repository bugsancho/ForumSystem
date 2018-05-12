(function () {
    'use strict';
    
    angular
        .module('forumSystem.auth')
        .factory('authInterceptor', authInterceptor);


    function authInterceptor(authService) {
        return {
            request: function ($config) {
                let token = authService.getAccessToken();
                if (token) {
                    $config.headers['Authorization'] = 'Bearer ' + token;
                }

                return $config;
            }
        };
    }
})();