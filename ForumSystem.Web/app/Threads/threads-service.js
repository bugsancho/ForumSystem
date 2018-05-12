(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .factory('threadsService', threadsService);


    function threadsService($http, $q, urls) {
        const service = {
            getThreads: getThreads
        };

        return service;

        function getThreads() {
            const deferred = $q.defer();
            $http.get(urls.getThreads).then(function (result) {
                const posts = result.data;
                console.log('received postsss', posts);
                deferred.resolve(posts);
            },
            function (error) {
                deferred.reject(error);
            });

            return $q.when(deferred.promise);
        }
    }
})();