'use strict';

app.factory('tagsService', ['$http', '$log',
    function ($http, $log) {
        var tagsUrl = apiUrl + 'tags';

        return {
            getTags: function getTags(successcb) {
                $http.get(tagsUrl)
                    .success(function (data) {
                        successcb(data);
                    })
                    .error(function (data) {
                        $log.error(data);
                    });
            },
            addTag: function addTag(tag, successcb) {
                $http.post(tagsUrl, tag)
                     .success(function (data) {
                         if (data === null) {
                             return false;
                         } else if (data.name === null) {
                             alert('Tag already exists!')
                         } else {
                             successcb(data);
                         }
                     })
                     .error(function (data) {
                         $log.error(data);
                     });
            }
        }
    }]);