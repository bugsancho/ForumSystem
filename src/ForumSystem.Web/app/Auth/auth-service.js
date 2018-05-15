(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .factory('authService', authService);


    function authService($window, $location, $http, $state, $rootScope, urlHelper, tokenService, urls, events) {
        const service = {
            ensureAuthenticated: ensureAuthenticated,
            processLoginCallback: processLoginCallback,
            isAuthenticated: isAuthenticated,
            logout: logout
        };

        return service;

        function logout() {
            tokenService.clearAccessToken();

            return $http.post(urls.logout);
        }

        function ensureAuthenticated(targetUrl) {
            // Check if we're currently redirected from the auth server and the auth token is in the URL
            //processRedirectInfo();

            const accessToken = tokenService.getAccessToken();
            if (!accessToken) {
                $window.location.href = '/Account/Authorize?client_id=web&response_type=token&state=' + encodeURIComponent(targetUrl);
                return false;
            }

            //Indicate success of the operation
            return true;
        }

        function isAuthenticated() {
            const accessToken = tokenService.getAccessToken();
            return !!accessToken;
        }

        function processLoginCallback() {
            const hash = $location.hash();
            if (hash) {
                const accessTokenData = urlHelper.parseQueryString(hash);
                if (accessTokenData['access_token']) {
                    tokenService.setAccessToken(accessTokenData['access_token']);
                    const state = accessTokenData['state'];
                    if (state) {
                        const decodedState = decodeURIComponent(state);
                        console.log('before redirect');

                        $window.location.href = decodedState;
                        $rootScope.$emit(events.userLoggedIn);

                        console.log('after redirect');
                    }
                }
            }
        }
    }
})();