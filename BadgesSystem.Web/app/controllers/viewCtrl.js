'use strict';

app.controller('viewCtrl', ['$scope', '$http', 'viewService', 'authService',
    function viewCtrl($scope, $http, viewService, authService) {
        $scope.model = {
            gifts: [],
            badges: []
        };
        $scope.model.gifts = [];
        $scope.tags = [];

        viewService.getGifts(function (data) {
            angular.forEach(data, function (gift) {
                $scope.model.gifts.push(gift);
            });
        });

        viewService.getBadges(function (data) {
            angular.forEach(data, function (badge) {
                $scope.model.badges.push(badge);
            });
        });

        (function isAuthenticated() {
            authService.isAuth(function (callback) {
                if (!callback) {
                    window.location.href = redirectToOAuthController;
                }
            });
        })();
    }]);