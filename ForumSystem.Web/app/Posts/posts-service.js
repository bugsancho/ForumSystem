(function () {
    'use strict';

    angular
        .module('forumSystem.posts')
        .factory('postsService', postsService);

    function postsService($http, $q, urls) {
        var service = {
            addPost: addPost
        };

        return service;

        function addPost(createPostModel) {
            const deferred = $q.defer();
            $http.post(urls.posts, createPostModel).then(function (result) {
                const post = result.data;
                console.log('created post', post);
                deferred.resolve(post);
            },
                function (error) {
                    deferred.reject(error);
                });

            return $q.when(deferred.promise);
        }
    }
})();