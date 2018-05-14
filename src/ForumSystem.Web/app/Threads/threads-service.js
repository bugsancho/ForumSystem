(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .factory('threadsService', threadsService);


    function threadsService($http, $q, urls) {
        const service = {
            getThreads: getThreads,
            getThread: getThread,
            createThread: createThread
        };

        return service;

        function createThread(thread) {
            const deferred = $q.defer();
            $http.post(urls.createThread, thread).then(function (result) {
                    const posts = result.data;
                    console.log('created post', posts);
                    deferred.resolve(posts);
                },
                function (error) {
                    deferred.reject(error);
                });

            return $q.when(deferred.promise);
        }

        function getThreads(page) {
            const deferred = $q.defer();
            let params = {
                page: '',
                pageSize: ''
            };

            if (page) {
                params['page'] = page;
            }

            $http.get(urls.getThreads, { params: params }).then(function (result) {
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