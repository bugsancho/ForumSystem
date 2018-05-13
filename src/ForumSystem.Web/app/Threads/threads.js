(function () {
    'use strict';

    angular
        .module('forumSystem.threads')
        .component('threadsComponent',
            {
                bindings: {
                    threads: '<'
                },
                templateUrl: '/threads.html'
            });
})();