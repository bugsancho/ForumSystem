(function () {
    'use strict';
    
    angular
        .module('forumSystem.auth')
        .factory('usersService', usersService);


    function usersService($window, $location, urlHelper) {
        const service = {
            getCurrentUser: getCurrentUser
        };

        return service;

        function getCurrentUser() {
           
        }
    }
})();