(function () {
    'use strict';

    angular
        .module('forumSystem.posts')
        .component('editPost',
            {
                controller: controller,
                templateUrl: '/edit-post.html',
                bindings: {
                    onNewPost: '&',
                    threadId: '@'
                }
            });


    function controller(postsService) {
        const ctrl = this;
        // If there is no thread passed, we're creating a new thread
        //ctrl.isCreateThread = !ctrl.thread;

        //ctrl.pageTitle = ctrl.isCreateThread ? 'Create new thread' : 'Edit thread';
        ctrl.submitButtonText = 'Send';

        ctrl.post = {
            threadId: ctrl.threadId
        };


        ctrl.submit = function (form) {
            if (form.$valid) {
                ctrl.post["threadId"] = ctrl.threadId;
                ctrl.loading = true;

                postsService.addPost(ctrl.post).then(function (post) {
                    console.log('postserv new post', post);
                    ctrl.post.content = '';
                    ctrl.onNewPost({ post: post });
                    ctrl.loading = false;

                });
            }
        }
    }

})();