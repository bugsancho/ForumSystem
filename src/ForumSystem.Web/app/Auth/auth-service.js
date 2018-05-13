(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .factory('authService', authService);


    function authService($window, $location, $http, $state, urlHelper, tokenService, urls) {
        const service = {
            ensureAuthenticated: ensureAuthenticated,
            processRedirectInfo: processRedirectInfo,
            isAuthenticated: isAuthenticated,
            logout: logout
        };

        return service;

        function logout() {
            tokenService.clearAccessToken();
            $http.post(urls.logout).then(function () {
                $state.go('threads');
            });
        }

        function ensureAuthenticated(targetUrl) {
            // Check if we're currently redirected from the auth server and the auth token is in the URL
            processRedirectInfo();

            const accessToken = tokenService.getAccessToken();
            if (!accessToken) {
                $window.location.href = '/Account/Authorize?client_id=web&response_type=token&state=' + encodeURIComponent(targetUrl);
            }

            //Indicate success of the operation
            return true;
        }

        function isAuthenticated() {
            const accessToken = tokenService.getAccessToken();
            return !!accessToken;
        }

        function processRedirectInfo() {
            const hash = $location.hash();
            if (hash) {
                const accessTokenData = urlHelper.parseQueryString(hash);
                if (accessTokenData['access_token']) {
                    tokenService.setAccessToken(accessTokenData['access_token']);
                    const state = accessTokenData['state'];
                    if (state) {
                        const decodedState = decodeURIComponent(state);
                        $window.location.href = decodedState;

                    }
                }
            }
        }
    }
})();