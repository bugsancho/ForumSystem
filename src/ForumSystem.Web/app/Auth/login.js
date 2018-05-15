(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .component('login',
            {
                controller: controller
            });


    function controller(authService) {
        const ctrl = this;
        console.log('loginzz');
        authService.processRedirectInfo();


    }
})();