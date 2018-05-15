(function () {
    'use strict';

    angular
        .module('forumSystem.shared')
        .factory('messageService', messageService);


    function messageService($uibModal, $state) {
        const service = {
            showError: showError
        };

        return service;

        function showError(error) {
            // Currently we don't have a mechanism to extract a user-friendly message from exceptions.
            // For the time being we show just a generic error message with the option to reload the page
            var modalInstance = $uibModal.open({
                templateUrl: "/error-message.html",
                size: "sm",
                controllerAs: '$ctrl',
                controller: function () {
                    var ctrl = this;
                    ctrl.reloadPage = $state.reload;
                }

            });

            modalInstance.result.then(function () {
            },
                function () {
                    // When closing, the modal always throws an error.
                    // If you don't provide an error handler function, closing or dismissing the modal, results in an unhandled exception
                    console.log('Closing modal');
                });
        }
    }
})();