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


    function controller(authService, $state, postsService) {
        const ctrl = this;

        ctrl.canEdit = authService.isAuthenticated();
        ctrl.newPost = function(newPost) {
            ctrl.thread.posts.push(newPost);
        };

        ctrl.editPost = function(post) {
            $state.go('editPost', { postId: post.id });
        };

        ctrl.canDeletePost = function () {
            //Can't delete the last post for the thread
            return ctrl.thread.posts.length > 1;
        };

        ctrl.deletePost = function(postId) {
            postsService.deletePost(postId).then(function() {
                for (let index = 0; index < ctrl.thread.posts.length; index++) {
                    const currentPost = ctrl.thread.posts[index];
                    // Remove the newly deleted post from the current thread
                    if (currentPost.id === postId) {
                        ctrl.thread.posts.splice(index, 1);
                        break;
                    }
                }
            });
        };
    }

})();