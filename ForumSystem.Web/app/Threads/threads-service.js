(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .factory('threadsService', threadsService);


    function threadsService($http, $q, urls) {
        const service = {
            getThreads: getThreads,
            getThread: getThread
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

        function getThread(threadId) {
            const deferred = $q.defer();
            $http.get(urls.getThread + threadId).then(function (result) {
                const thread = result.data;
                console.log('received thread', thread);
                deferred.resolve(thread);
            },
            function (error) {
                deferred.reject(error);
            });

            return $q.when(deferred.promise);
        }
    }
})();