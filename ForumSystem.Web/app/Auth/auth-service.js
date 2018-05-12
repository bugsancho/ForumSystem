(function () {
    'use strict';

    const tokenCacheKey = 'access_token';

    angular
        .module('forumSystem.auth')
        .factory('authService', authService);


    function authService($http, $window, $location, urlHelper) {
        const service = {
            ensureAuthenticated: ensureAuthenticated,
            getAccessToken: getAccessToken
        };

        return service;

        function ensureAuthenticated() {
            // Check if we're currently redirected from the auth server and the auth token is in the URL
            const hash = $location.hash();
            if (hash) {
                const accessTokenData = urlHelper.parseQueryString(hash);
                if (accessTokenData['access_token']) {
                    setAccessToken(accessTokenData['access_token']);
                    $location.hash('');
                }
            }

            const accessToken = getAccessToken();
            if (!accessToken) {
                $window.location.href = '/Account/Authorize?client_id=web&response_type=token&state=';
            }
        }

        function getAccessToken() {
            const sessionStorage = $window.sessionStorage;
            const accessToken = sessionStorage.getItem(tokenCacheKey);
            return accessToken;
        }

        function setAccessToken(accessToken) {
            const sessionStorage = $window.sessionStorage;
            sessionStorage.setItem(tokenCacheKey, accessToken);
        }
    }
})();