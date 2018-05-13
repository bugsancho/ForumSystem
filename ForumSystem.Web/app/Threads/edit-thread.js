(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .component('editThreadComponent',
            {
                controller: controller,
                templateUrl: '/edit-thread.html',
                bindings: {
                    thread: '<'
                }
            });


    function controller(threadsService, $state) {
        const ctrl = this;
        // If there is no thread passed, we're creating a new thread
        ctrl.isCreateThread = !ctrl.thread;

        ctrl.pageTitle = ctrl.isCreateThread ? 'Create new thread' : 'Edit thread';
        ctrl.submitButtonText = ctrl.isCreateThread ? 'Create' : 'Save';

        ctrl.submit = function (form) {
            if (form.$valid) {
                if (ctrl.isCreateThread) {

                    threadsService.createThread(ctrl.thread).then(function (thread) {
                        console.log('post creted ctrl', thread);
                        $state.go('thread', { threadId: thread.id });
                    });

                }
            }
        }

        console.log(ctrl.thread);
    }

})();