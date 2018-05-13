(function () {
    'use strict';
    
    angular
        .module('forumSystem.auth')
        .factory('authInterceptor', authInterceptor);


    function authInterceptor(tokenService) {
        return {
            request: function ($config) {
                let token = tokenService.getAccessToken();
                if (token) {
                    $config.headers['Authorization'] = 'Bearer ' + token;
                }

                return $config;
            }
        };
    }
})();