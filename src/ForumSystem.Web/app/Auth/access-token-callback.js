(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .component('accessTokenCallback',
            {
                controller: controller
            });


    function controller(authService, $rootScope) {
        const ctrl = this;
        console.log('loginzz');
        authService.processAccessTokenCallback();
    }
})();