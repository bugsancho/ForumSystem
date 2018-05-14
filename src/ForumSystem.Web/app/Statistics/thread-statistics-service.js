(function () {
    'use strict';

    angular
        .module('forumSystem.statistics')
        .factory('threadStatisticsService', threadStatisticsService);


    function threadStatisticsService($http, $q, urls) {
        const service = {
            getStatistics: getStatistics
        };

        return service;

        function getStatistics(statisticsRequest) {
            const deferred = $q.defer();
            $http.get(urls.threadStatistics, { params: statisticsRequest }).then(function (result) {
                    const statistics = result.data;
                    console.log('received stats', statistics);
                    deferred.resolve(statistics);
                },
                function (error) {
                    deferred.reject(error);
                });

            return $q.when(deferred.promise);
        }
    }
})();