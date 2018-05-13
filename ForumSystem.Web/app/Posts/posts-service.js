(function () {
    'use strict';

    angular
        .module('forumSystem.posts')
        .factory('postsService', postsService);

    function postsService($http, $q, urls) {
        var service = {
            addPost: addPost,
            getPost: getPost,
            editPost: editPost,
            deletePost: deletePost
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


        function editPost(postId, editPostModel) {
            const deferred = $q.defer();
            $http.put(urls.posts + postId, editPostModel).then(function (result) {
                const post = result.data;
                console.log('updated post', post);
                deferred.resolve(post);
            },
                function (error) {
                    deferred.reject(error);
                });

            return $q.when(deferred.promise);
        }

        function getPost(postId) {
            const deferred = $q.defer();
            $http.get(urls.posts + postId).then(function (result) {
                const post = result.data;
                console.log('received post', post);
                deferred.resolve(post);
            },
                function (error) {
                    deferred.reject(error);
                });

            return $q.when(deferred.promise);
        }


        function deletePost(postId) {
            return $http.delete(urls.posts + postId);
        }
    }
})();