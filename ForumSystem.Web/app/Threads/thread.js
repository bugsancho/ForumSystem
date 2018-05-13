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


    function controller() {
        const ctrl = this;

        ctrl.newPost = function (newPost) {
            ctrl.thread.posts.push(newPost);
        }
    }

})();