'use strict';

app.controller('tagsCtrl', ['$scope', '$http', 'tagsService', 'ngProgress', 'authService',
    function tagsCtrl($scope, $http, tagsService, ngProgress, authService) {
        ngProgress.start();
        ngProgress.color(ngProgressColor);

        $scope.model = {};
        $scope.new = {
            Tag: {}
        };

        tagsService.getTags(function (data) {
            $scope.model = data;
            ngProgress.complete();
        });

        $scope.addTag = function () {
            tagsService.addTag($scope.new.Tag, function (data) {
                if (data !== '') {
                    $scope.model.push(data);
                }
                $scope.new.Tag = {};
            });
        };

        (function isAuthenticated() {
            authService.isAuth(function (callback) {
                if (!callback) {
                    window.location.href = redirectToOAuthController;
                }
            });
        })();

    }]);