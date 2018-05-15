(function () {
    'use strict';
    
    angular
        .module('forumSystem.auth')
        .factory('usersService', usersService);


    function usersService($http, $q, urls) {
        const service = {
            getCurrentUser: getCurrentUser
        };

        return service;

      

        function getCurrentUser() {

            const deferred = $q.defer();
            $http.get(urls.user).then(function (result) {
                    const user = result.data;
                    console.log('received user', user);
                    deferred.resolve(user);
                },
                function (error) {
                    deferred.reject(error);
                });

            return $q.when(deferred.promise);
        }

       
    }
})();