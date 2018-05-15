(function () {
    'use strict';

    angular
        .module('forumSystem.auth')
        .component('loginInfo',
            {
                controller: controller,
                templateUrl:'/login-info.html'
            });


    function controller(authService, usersService, $state, $rootScope, events) {
        const ctrl = this;
        console.log('loginzz');

        ctrl.logout = function() {
            authService.logout().then(function () {
                ctrl.user = {};
                ctrl.isAuthenticated = false;
                $state.go('default');
            });
        };

        const isAuthenticated = authService.isAuthenticated();
        ctrl.isAuthenticated = isAuthenticated;

        $rootScope.$on(events.userLoggedIn, getUser);

        if (isAuthenticated) {
            console.log('initial logged in');
            getUser();
        }

        function getUser() {
            usersService.getCurrentUser().then(function (user) {
                console.log('user logged in');
                ctrl.user = user;
                ctrl.isAuthenticated = true;
            });
        }



    }
})();