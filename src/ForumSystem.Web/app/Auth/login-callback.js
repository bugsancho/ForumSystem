(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .component('loginCallback',
            {
                controller: controller
            });


    function controller(authService) {
        const ctrl = this;

        authService.ensureAuthenticated();
    }
})();