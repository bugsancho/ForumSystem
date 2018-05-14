angular.module('forumSystem.statistics')
    .constant('aggregationIntervals',
        {
            'day': { name: 'Day', id: 1 },
            'week': { name: 'Week', id: 2 },
            'month': { name: 'Month', id: 3 },
            'year': { name: 'Year', id: 4 }
        });