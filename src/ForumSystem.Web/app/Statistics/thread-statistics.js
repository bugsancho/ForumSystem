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
        ctrl.selectedInterval = _.indexOf(ctrl.intervalsArray, defaultAggregationInterval);
        ctrl.noDataAvailable = false;

        ctrl.$postLink = function () {

            let throttled;
            // Bind the zoom functionality to the scroll event
            angular.element(document.querySelector('#threadStatisticsContainer')).on('wheel',
                function (event) {
                    console.log('wheeled!!', event.wheelDelta);
                    // Prevent the default action of the scroll event, so the user doesn't scroll down the page while zooming
                    event.preventDefault();
                    event.stopPropagation();
                    event.stopImmediatePropagation();

                    // Depending on the mouse, the wheel event is sometimes fired 50+ times for a single scroll, so we need to throttle the event.
                    if (!throttled) {
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
            ctrl.selectedInterval = selectedInterval;
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

        ctrl.$onInit = updateStatistics;
    }
})();