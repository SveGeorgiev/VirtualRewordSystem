'use strict';

app.factory('saveService', ['$http', '$log', 'ngProgress',
    function ($http, $log, ngProgress) {
        return {
            getTags: function getTags(successcb) {
                var getUrl = apiUrl + 'tags';

                $http.get(getUrl)
                     .success(function (data) {
                         successcb(data);
                     })
                     .error(function (data) {
                         $log.error(data);
                     });
            },

            getUserRole: function getUserRole(successcb) {
                var getUrl = apiUrl + 'users/GetUserRole';

                $http.get(getUrl)
                     .success(function (data) {
                         successcb(data);
                     })
                     .error(function (data) {
                         $log.error(data);
                     });
            },

            uploadFileToUrl: function (file, badge, successcb) {
                var fd = new FormData();
                var uploadUrl = apiUrl + 'save';

                ngProgress.color(ngProgressColor);
                ngProgress.start();

                fd.append('file', file);
                fd.append('name', badge.name);
                fd.append('flagType', badge.flagType);
                fd.append('selectedTags', badge.selectedTags);

                $http.post(uploadUrl, fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                .success(function (data) {
                    successcb(data);
                    ngProgress.complete();
                })
                .error(function () {
                    ngProgress.complete();
                });
            }
        }
    }]);