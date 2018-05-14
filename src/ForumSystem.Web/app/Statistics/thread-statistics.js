(function () {
    'use strict';
    angular
        .module('forumSystem.threads')
        .component('threadStatistics',
            {
                bindings: {
                    threadId: '@'
                },
                controller: controller,
                templateUrl: '/thread-statistics.html'
            });

    function controller(aggregationIntervals, threadStatisticsService,$timeout) {
        const ctrl = this;
        const defaultAggregationInterval = aggregationIntervals.month;

        //ctrl.datasetOverride = [
        //    {
        //        label: "Line chart 1",
        //        borderWidth: 1,
        //        type: 'bar'
        //    },
        //    {
        //        label: "Line chart",
        //        borderWidth: 3,
        //        hoverBackgroundColor: "rgba(255,99,132,0.4)",
        //        hoverBorderColor: "rgba(255,99,132,1)",
        //        type: 'line'
        //    }
        //];

        ctrl.data= [
      [65, -59, 80, 81, -56, 55, -40],
      [28, 48, -40, 19, 86, 27, 90]
        ];


        ctrl.$postLink = function() {
            angular.element(document.querySelector('#threadStatistics')).on('click',
                function() {
                    console.log('clicked!!');
                });

            let throttled;
            angular.element(document.querySelector('#threadStatistics')).on('wheel',
                function () {
                    console.log('wheeled!!');

                    // Depending on the mouse, the wheel event is sometimes fired 50+ times for a single scroll, so we need to throttle the event.
                    if (!throttled) {
                        console.log('throttled!!');
                        throttled = true;
                        $timeout(function() {
                                throttled = false;
                            },
                            1500);
                    }
                });
        }
        ctrl.$onInit = function () {
            const threadId = ctrl.threadId;
            threadStatisticsService
                .getStatistics({ threadId: threadId, aggregationInterval: defaultAggregationInterval }).then(
                    function (statistics) {
                        let labels = [];
                        let hitsDataSet = [];
                        let absoluteHitsDataSet = [];
                        for (let index = 0; index < statistics.length; index++) {
                            let currentDataPoint = statistics[index];

                            labels.push(currentDataPoint.label);
                            hitsDataSet.push(currentDataPoint.hits);
                            absoluteHitsDataSet.push(currentDataPoint.absoluteHits);
                        }
                        ctrl.series = ['Hits', 'Absolute hits'];
                        ctrl.labels = labels;
                        ctrl.data = [hitsDataSet, absoluteHitsDataSet];

                        console.log('received ctrl statistics', ctrl.data);
                    });
        }
    }
})();