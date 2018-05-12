(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .component('threadComponent',
            {
                controller: controller,
                templateUrl: '/thread.html',
                bindings: {
                    threadId: '@'
                }
            });


    function controller($stateParams) {
        const ctrl = this;
        console.log($stateParams.threadId);
        console.log(ctrl.threadId);
    }

})();