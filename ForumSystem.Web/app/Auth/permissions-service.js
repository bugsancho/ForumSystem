(function () {
    'use strict';
    
    angular
        .module('forumSystem.auth')
        .factory('permissionsService', permissionsService);


    function permissionsService($window, $location, urlHelper) {
        const service = {
            getUserPermissions: getUserPermissions
        };

        return service;

        function getUserPermissions() {
           
        }
    }
})();