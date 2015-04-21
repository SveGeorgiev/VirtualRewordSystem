'use strict';

var ngProgressColor = 'blue';
var apiUrl = '/api/';
var viewUrl = '/app/views/';
var redirectToOAuthController = $("#redirectToOAuthController").val(); 
var app = angular.module('app', ['ngRoute', 'ngProgress']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    $routeProvider
        .when('/', {
            templateUrl: viewUrl + 'give.html',
        })
        .when('/tags', {
            templateUrl: viewUrl + 'tags.html',
        })
        .when('/upload', {
            templateUrl: viewUrl + 'save.html',
        })
        .when('/give', {
            templateUrl: viewUrl + 'give.html',
        })
        .when('/view', {
            templateUrl: viewUrl + 'view.html',
        })
       .otherwise({
           redirectTo: '/'
       });
}]);

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

$(document).ready(function () {
    $.scrollUp({
        scrollName: 'scrollUp', // Element ID
        scrollImg: true,
        topDistance: '1000', // Distance from top before showing element (px)
        topSpeed: 500, // Speed back to top (ms)
        animation: 'fade', // Fade, slide, none
        animationInSpeed: 200, // Animation in speed (ms)
        animationOutSpeed: 200, // Animation out speed (ms)
        scrollText: 'Scroll to top', // Text for element
        activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
    });
});