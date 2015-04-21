'use strict';

app.factory('viewService', ['$http', '$log', 'ngProgress',
    function ($http, $log, ngProgress) {
        ngProgress.color(ngProgressColor);
        ngProgress.start();

        return {
            getGifts: function getGifts(successcb) {
                var getGiftsUrl = apiUrl + 'View/GetGifts';

                $http.get(getGiftsUrl)
                     .success(function (data) {
                         successcb(data);
                         ngProgress.complete();
                     })
                     .error(function (data) {
                         $log.error(data);
                         ngProgress.complete();
                     });
            },

            getBadges: function getBadges(successcb) {
                var getBadgesUrl = apiUrl + 'View/GetBadges';

                $http.get(getBadgesUrl)
                     .success(function (data) {
                         successcb(data);
                         ngProgress.complete();
                     })
                     .error(function (data) {
                         $log.error(data);
                         ngProgress.complete();
                     });
            }
        }
    }]);