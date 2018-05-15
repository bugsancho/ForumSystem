(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .component('login',
            {
                controller: controller
            });


    function controller(authService, $rootScope) {
        const ctrl = this;
        console.log('loginzz');
        authService.processLoginCallback();
    }
})();