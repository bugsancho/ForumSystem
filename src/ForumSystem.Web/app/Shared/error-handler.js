(function () {
    'use strict';

    angular
        .module('forumSystem.shared')
        .config(exceptionConfig);


    function exceptionConfig($provide) {
        $provide.decorator('$exceptionHandler', extendExceptionHandler);


        function extendExceptionHandler($delegate, $injector) {
            return function(exception, cause) {
                $delegate(exception, cause);
                const messageService = $injector.get('messageService');

                // For the time being, just open a generic error dialog
                messageService.showError();
            };
        }
    }


})();