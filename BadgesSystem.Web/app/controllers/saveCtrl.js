'use strict';

app.controller('saveCtrl', ['$scope', '$http', 'saveService', 'ngProgress', 'authService',
    function saveCtrl($scope, $http, saveService, ngProgress, authService) {
        var gift = 'Gift';
        ngProgress.color(ngProgressColor);
        ngProgress.start();

        $scope.model = {};
        $scope.new = { badge: {}, data: {} };
        $scope.new.badge.flagType = gift;
        $scope.tagsList = {};
        $scope.isSave = false;

        saveService.getTags(function (data) {
            $scope.tagsList = data;
            ngProgress.complete();
        });

        saveService.getUserRole(function (data) {
            $scope.model.userRole = data;
        });

        $scope.submitForm = function (badge) {
            var file = $scope.myFile;
            var inputElement = $('input[type="file"]');
            saveService.uploadFileToUrl(file, badge, function (data) {
                if (angular.isObject(data)) {
                    if (data.name === null) {
                        $scope.new.badge = {};
                        $scope.new.badge.flagType = gift;
                        angular.element(inputElement).val(null);
                        alert("Already exists.")
                    } else {
                        $scope.new.badge = {};
                        $scope.new.badge.flagType = gift;
                        angular.element(inputElement).val(null);
                        $scope.model = data;
                        $scope.model.userRole = data.user.userRole;
                        $scope.isSave = true;
                        $scope.new.data = data;
                        $('#myModal').modal('show');
                        $scope.myFile = null;
                    }
                }
            });
        };

        $scope.saveGift = function () {
            $('#saveBtn').val('Save Gift');
        };

        $scope.saveBadge = function () {
            $('#saveBtn').val('Save Badge');
        };

        (function isAuthenticated() {
            authService.isAuth(function (callback) {
                if (!callback) {
                    window.location.href = redirectToOAuthController;
                }
            });
        })();
    }]);