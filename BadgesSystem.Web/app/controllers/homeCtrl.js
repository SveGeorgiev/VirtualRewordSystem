'use strict';

app.controller('homeCtrl', ['$scope', 'saveService', function ($scope, saveService) {
    saveService.getUserRole(function (data) {
        $scope.userRole = data;
    });
}]);