(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .component('loginInfo',
            {
                controller: controller,
                templateUrl:'/login-info.html'
            });


    function controller(authService, usersService) {
        const ctrl = this;
        console.log('loginzz');

        ctrl.logout = authService.logout;

        //usersService.getCurrentUser().then();


    }
})();