(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .factory('authService', authService);


    function authService($window, $location, $http, $state, $rootScope, urlHelper, tokenService, urls, events) {
        const service = {
            ensureAuthenticated: ensureAuthenticated,
            processAccessTokenCallback: processAccessTokenCallback,
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
                let redirectUrl = '/Account/Authorize?client_id=web&response_type=token&state=';
                if (targetUrl) {
                    redirectUrl += encodeURIComponent(targetUrl);
                }

                $window.location.href = redirectUrl;
                return false;
            }

            //Indicate success of the operation
            return true;
        }

        function isAuthenticated() {
            const accessToken = tokenService.getAccessToken();
            return !!accessToken;
        }

        function processAccessTokenCallback() {
            const hash = $location.hash();
            if (hash) {
                const accessTokenData = urlHelper.parseQueryString(hash);
                if (accessTokenData['access_token']) {
                    tokenService.setAccessToken(accessTokenData['access_token']);

                    const state = accessTokenData['state'];
                    $rootScope.$emit(events.userLoggedIn);

                    if (state) {

                        const decodedState = decodeURIComponent(state);
                        $window.location.href = decodedState;
                    }
                    else {
                        $state.go('default');
                    }
                }
            }
        }
    }
})();