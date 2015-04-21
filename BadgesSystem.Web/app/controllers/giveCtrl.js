'use strict';

app.controller('giveCtrl', ['$scope', '$http', 'giveService', 'authService',
    function giveCtrl($scope, $http, giveService, authService) {
        $scope.model = {};
        $scope.data = {};
        $scope.give = {
            giftBadge: {},
            giftsBadges: [],
            users: []
        };
        $scope.isSend = false;

        giveService.getUsersName(function (data) {
            $scope.give.users = data;
        });

        giveService.getBadgesGifts(function (data) {
            $scope.give.giftsBadges = data;
        });

        $scope.giveBadgeGift = function (giveBadge, giveBadgeForm) {
            giveService.giveBadgeGift(giveBadge, giveBadgeForm, function (data) {
                if (angular.isObject(data)) {
                    $scope.data = data;
                    $('#giveModal').modal('show');
                    $scope.isSend = true;
                } else {
                    $scope.isSend = false;
                }
                $scope.give.giftBadge = {};
                $scope.model = data;
            });
        };

        $scope.flagTypeFilter = function (giftBadge) {
            return giveService.flagTypeFilter(giftBadge);
        };

        (function isAuthenticated() {
            authService.isAuth(function (callback) {
                if (!callback) {
                    window.location.href = redirectToOAuthController;
                }
            });
        })();

    }]);
