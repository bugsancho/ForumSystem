(function () {
    'use strict';

    const tokenCacheKey = 'access_token';

    angular
        .module('forumSystem.auth')
        .factory('tokenService', tokenService);


    function tokenService($window) {
        const service = {
            getAccessToken: getAccessToken,
            setAccessToken: setAccessToken,
            clearAccessToken: clearAccessToken
        };

        return service;


        function getAccessToken() {
            const sessionStorage = $window.sessionStorage;
            const accessToken = sessionStorage.getItem(tokenCacheKey);
            return accessToken;
        }

        function clearAccessToken() {
            const sessionStorage = $window.sessionStorage;
            sessionStorage.removeItem(tokenCacheKey);
        }

        function setAccessToken(accessToken) {
            const sessionStorage = $window.sessionStorage;
            sessionStorage.setItem(tokenCacheKey, accessToken);
        }
    }
})();