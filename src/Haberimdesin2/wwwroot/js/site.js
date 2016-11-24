var HaberimdesinApp = angular.module('Haberimdesin', []);

//routing

//Controllers

HaberimdesinApp.controller('News', ['$scope', '$http', function ($scope, $http) {
    $http.get('/haberimdesin/getCategories').success(function (res) {
        $scope.categories = res.categories;
    }).error(function (err) {
        console.log(err);
    });
    $scope.getNewsByCategory = function (id) {
        var url = "/haberimdesin/getNewsByID/" + id;
        $http.get(url).success(function (re) {
            $scope.habers = re.newsList;
            console.log(re);
        }).error(function (err) { console.log(err); });
    }
}]);

HaberimdesinApp.controller('addNewsController', ['$scope', '$http', '$q', function ($scope, $http, $q) {
    $scope.haberHeader = "";
    $scope.haberHeadline = "";
    $scope.haberDetail = "";
    $scope.imageFiles = [];
    $scope.date = new Date();
    

    $http.get('/haberimdesin/getCategories').success(function (res) {
        console.log(res);
        $scope.data = {
            model: null,
            availableOptions: res.categories
        };
   
    }).error(function (err) { console.log(err)});

    function getMyLocation() {
        return $q(function (resolve, reject) {
            if (navigator.geolocation) {
                navigator.geolocation.watchPosition(function (position) {
                    lat = position.coords.latitude;
                    longi = position.coords.longitude;
                    return resolve({ lat: lat, longi: longi });
                }, function (error) {
                    switch (error.code) {
                        case error.PERMISSION_DENIED:
                            return reject({ error: "User denied the request for Geolocation." });
                            break;
                        case error.POSITION_UNAVAILABLE:
                            return reject({ error: "Location information is unavailable." });
                            break;
                        case error.TIMEOUT:
                            return reject({ error: "The request to get user location timed out." });
                            break;
                        case error.UNKNOWN_ERROR:
                            return reject({ error: "An unknown error occurred." });
                            break;
                    }
                });
            } else {
                return reject({ error: "Browser Not Support" });
            }
        });
    }


    $scope.uploadNews = function () {

        getMyLocation().then(function (res) {

            var fileArray = $scope.imageFiles;
            var fd = new FormData();
            for (var i in fileArray) {
                fd.append('file[]', fileArray[i]);
            }
            fd.append('haberHeader', $scope.haberHeader);
            fd.append('haberHeadline', $scope.haberHeadline);
            fd.append('haberDetail', $scope.haberDetail);
            fd.append('latitude', res.lat);
            fd.append('longitude', res.longi);
            fd.append('CategoryID',$scope.data.model);

            $http.post('/Haberimdesin/CreateNews', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                console.log(response);
            }).error(function (err) {
                console.log(err);
            });
        }, function (rej) {
            console.log(res);
        });
    };

}]);
//  DIRECTIVES

HaberimdesinApp.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;
            var isMultiple = attrs.multiple;
            element.bind('change', function () {
                scope.$apply(function () {
                    if (isMultiple) {
                        var files = element[0].files;
                        modelSetter(scope, files);
                    } else {
                        modelSetter(scope, element[0].files[0]);
                    }
                });
            });
        }
    };
}]);