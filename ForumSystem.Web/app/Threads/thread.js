(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .component('threadComponent',
            {
                controller: controller,
                templateUrl: '/thread.html',
                bindings: {
                    thread: '<'
                }
            });


    function controller(authService, $state) {
        const ctrl = this;

        ctrl.canEdit = authService.isAuthenticated();
        ctrl.newPost = function (newPost) {
            ctrl.thread.posts.push(newPost);
        }

        ctrl.editPost = function(post) {
            $state.go('editPost', { postId: post.id});
        }
    }

})();