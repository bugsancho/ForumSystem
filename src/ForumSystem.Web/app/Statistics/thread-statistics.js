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

    function controller(aggregationIntervals, threadStatisticsService, $timeout, _) {
        const ctrl = this;
        const defaultAggregationInterval = aggregationIntervals.month;

        ctrl.intervalsCount = _.size(aggregationIntervals);
        ctrl.intervalsArray = _.chain(aggregationIntervals).values().sortBy(function (interval) {
            return interval.id;
        }).value();

        ctrl.selectedIntervalIndex = _.indexOf(ctrl.intervalsArray, defaultAggregationInterval);
        ctrl.noDataAvailable = false;

        ctrl.$postLink = function () {

            let throttled;
            angular.element(document.querySelector('#threadStatisticsContainer')).on('wheel',
                function (event) {
                    console.log('wheeled!!', event.wheelDelta);
                    // Prevent the default of the event
                    event.preventDefault();
                    event.stopPropagation();
                    event.stopImmediatePropagation();

                    // Depending on the mouse, the wheel event is sometimes fired 50+ times for a single scroll, so we need to throttle the event.
                    if (!throttled) {
                        console.log('throttled!!');
                        // event.wheelData indicates the direction of the scroll with positive number meaning one direction and negative, the opposite
                        if (event.wheelDelta < 0 && ctrl.selectedIntervalIndex < ctrl.intervalsCount - 1) {
                            ctrl.selectedIntervalIndex++;
                            updateStatistics();

                        }
                        else if (event.wheelDelta > 0 && ctrl.selectedIntervalIndex > 0) {
                            ctrl.selectedIntervalIndex--;
                            updateStatistics();
                        }

                        throttled = true;
                        $timeout(function () {
                            throttled = false;
                        }, 500);
                    }
                });

            ctrl.sliderChanged = updateStatistics;


        }

        ctrl.aggregationIntervals = aggregationIntervals;

        function updateStatistics() {
            const selectedInterval = ctrl.intervalsArray[ctrl.selectedIntervalIndex];
            getStatistics(selectedInterval);
        }

        function getStatistics(aggregationInterval) {
            threadStatisticsService
                .getStatistics({ threadId: ctrl.threadId, aggregationInterval: aggregationInterval.id }).then(
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

                        if (hitsDataSet.length === 0) {
                            ctrl.noDataAvailable = true;
                        } else {
                            ctrl.noDataAvailable = false;
                        }

                        console.log('received ctrl statistics', ctrl.data);
                    });
        }

        ctrl.$onInit = function () {
            getStatistics(defaultAggregationInterval);
        }
    }
})();