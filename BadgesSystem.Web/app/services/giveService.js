'use strict';

app.factory('giveService', ['$http', '$log', 'ngProgress',
    function ($http, $log, ngProgress) {
        ngProgress.color(ngProgressColor);

        return {
            getUsersName: function getUsersName(successcb) {
                var getUrl = apiUrl + 'users/getUsersName';

                ngProgress.start();

                $http.get(getUrl)
                     .success(function (data) {
                         successcb(data);
                         ngProgress.complete();
                     })
                    .error(function (data) {
                        $log.error(data);
                    });
            },

            getBadgesGifts: function getBadgesGifts(successcb) {
                var getUrl = apiUrl + 'give';

                ngProgress.start();

                $http.get(getUrl)
                     .success(function (data) {
                         successcb(data);
                         ngProgress.complete();
                     })
                     .error(function (data) {
                         $log.error(data);
                     });
            },

            giveBadgeGift: function giveBadgeGift(giveBadge, giveBadgeForm, successcb) {
                if (giveBadgeForm.$valid) {
                    var postUrl = apiUrl + 'give';

                    ngProgress.start();

                    $http.post(postUrl, giveBadge)
                         .success(function (data) {
                             successcb(data);
                             ngProgress.complete();
                             if (data === null) {
                                 alert("The user already have this one.");
                             }
                         })
                         .error(function (data) {
                             $log.error(data);
                         });
                }
            },

            flagTypeFilter: function flagTypeFilter(giftBadge) {
                var gift = $('#giftCB').is(':checked');
                var badge = $('#badgeCB').is(':checked');

                if (!gift && !badge) {
                    $('#gifts').val('');
                    return false;
                } else if (giftBadge.flagType === gift) {
                    return true;
                } else if (giftBadge.flagType === !badge) {
                    return true;
                }
            }
        }
    }]);