(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .component('threadsComponent',
            {
                bindings: {
                    threads: '<',
                    page: '@'
                },
                controller: controller,
                templateUrl: '/threads.html'
        });


    function controller($state) {
        const ctrl = this;

        ctrl.pageChanged = function () {
            console.log('page changesss', ctrl.currentPage);
            $state.go('threads', { page: ctrl.currentPage });
        }

        ctrl.$onInit = function() {
            ctrl.currentPage = ctrl.threads.page;
        }

    }
})();