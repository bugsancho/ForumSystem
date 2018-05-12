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


    function controller($stateParams) {
        const ctrl = this;
        console.log('stateparams',$stateParams.threadId);
        console.log(ctrl.thread);
    }

})();