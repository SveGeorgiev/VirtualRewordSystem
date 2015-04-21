'use strict';

app.factory('authService', ['$http', '$log',
      function ($http, $log) {
          return {
              isAuth: function isAuth(successcb) {
                  var isAuthUrl = apiUrl + 'users/isAuthenticated';
                  $http.get(isAuthUrl)
                       .success(function (data) {
                           successcb(data);
                       })
                      .error(function (data) {
                          $log.error(data);
                      });
              }
          }
      }]);