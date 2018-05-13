(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .component('editPost',
            {
                controller: controller,
                templateUrl: '/edit-post.html',
                bindings: {
                    post: '<',
                    onNewPost: '&',
                    threadId: '@'
                }
            });


    function controller(postsService, $state) {
        const ctrl = this;
        // If there is no thread passed, we're creating a new thread


        ctrl.$onInit = function() {
            ctrl.isNewPost = !ctrl.post;
            ctrl.isEditPost = !ctrl.isNewPost;
            console.log('controlelr edit post', ctrl.post);
            ctrl.submitButtonText = 'Send';
        }



        ctrl.submit = function (form) {
            if (form.$valid) {
                ctrl.loading = true;

                if (ctrl.isNewPost) {
                    ctrl.post["threadId"] = ctrl.threadId;

                    postsService.addPost(ctrl.post).then(function (post) {
                        console.log('postserv new post', post);
                        ctrl.post.content = '';
                        ctrl.onNewPost({ post: post });
                        ctrl.loading = false;

                    });
                } else {
                    postsService.editPost(ctrl.post.id, ctrl.post).then(function (post) {
                        console.log('postserv edited post', post);
                        $state.go('thread', { threadId: post.threadId });

                    });
                }
            }
        }
    }

})();