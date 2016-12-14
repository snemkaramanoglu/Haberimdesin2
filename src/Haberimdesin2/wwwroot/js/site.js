var HaberimdesinApp = angular.module('Haberimdesin', []);

//routing

//Controllers
var habers = null
var lastNew = null;
var last2News = null;
HaberimdesinApp.controller('News', ['$scope', '$http', function ($scope, $http) {
    $scope.activeHaber = null;
    $scope.last2News = function () {
        return last2News;
    }
    $scope.lastNew = function () {
        return lastNew;
    }
    $scope.habers = function () {
        return habers;
    };
    $http.get('/haberimdesin/getCategories').success(function (res) {
        $scope.categories = res.categories;
    }).error(function (err) {
        console.log(err);
    });
  

    $scope.getAllNews = function () {
        var url = "/haberimdesin/getAllNews";
        $http.get(url).success(function (re) {

            habers = re.newsList.reverse();
            lastNew = habers[0];
            last2News = habers.slice(1, 3);

            $scope.activeHaber = null;
        }).error(function (err) { console.log(err); });
    }
    $scope.getAllNews();
 
  
    $scope.getNewsByCategory = function (id) {
        
        var url = "/haberimdesin/getNewsByID/" + id;
        $http.get(url).success(function (re) {
           
            last2News = null;
            lastNew = null;
            habers = re.newsList.reverse();
     
            $scope.activeHaber = null;
        }).error(function (err) { console.log(err); });
        
    }
    $scope.likeComment = function (id, index) {
        var fd = new FormData();
        fd.append('id', id);
        $http.post('/Haberimdesin/LikeComment', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            $scope.yorumlar[index].like = $scope.yorumlar[index].like + 1;
        }).error(function (err) {
            console.log(err);
        });

    }
    $scope.dislikeComment = function (id, index) {
        var fd = new FormData();
        fd.append('id', id);
        $http.post('/Haberimdesin/DislikeComment', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            $scope.yorumlar[index].dislike = $scope.yorumlar[index].dislike + 1;
        }).error(function (err) {
            console.log(err);
        });

    }
    $scope.likeNews = function () {
        var fd = new FormData();
        fd.append('id',  $scope.activeHaber[0].haberID);
        $http.post('/Haberimdesin/LikeNews', fd,  {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            $scope.activeHaber[0].like = $scope.activeHaber[0].like + 1;
        }).error(function (err) {
            console.log(err);
        });

    }
    $scope.dislikeNews = function () {
        var fd = new FormData();
        fd.append('id', $scope.activeHaber[0].haberID);
        $http.post('/Haberimdesin/DislikeNews', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            $scope.activeHaber[0].dislike = $scope.activeHaber[0].dislike + 1;
        }).error(function (err) {
            console.log(err);
        });
    }
    $scope.getNewsDetail = function (id) {
        window.scrollTo(0, 0);
        var url = "/haberimdesin/getNewsDetail/" + id;
        $http.get(url).success(function (re) {
            habers = null;
            $scope.yorumlar = re.yorumList.reverse();
            $scope.lastNew = null;
            $scope.last2News = null;
            $scope.kisiler = re.userNameList.reverse();
            $scope.activeHaber = re.newsList;
        }).error(function (err) { console.log(err); });
    }
    
    $scope.uploadComment = function () {
        var fd = new FormData();
        fd.append('yorumIcerik', $scope.$$childTail.yorumIcerik);
        fd.append('haberID', $scope.activeHaber[0].haberID)
        $http.post('/Haberimdesin/CreateComment', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            console.log(response);
        }).error(function (err) {
            console.log(err);
        });

        var newObject = jQuery.extend({}, $scope.yorumlar[0]);
        
        
        newObject.content = $scope.$$childTail.yorumIcerik;
        newObject.like = 0;
        newObject.dislike = 0;
        newObject.timeStamp = new Date();
        newObject.commentID = newObject.commentID + 1;
        $scope.yorumlar.splice(0, 0, newObject);


        var newUser = $scope.activeHaber[0].user.name + " " + $scope.activeHaber[0].user.surname;
        $scope.kisiler.splice(0, 0, newUser);
        
    }
}]);

HaberimdesinApp.controller('addNewsController', ['$scope', '$http', '$q', function ($scope, $http, $q) {
    $scope.haberHeader = "";
    $scope.haberHeadline = "";
    $scope.haberDetail = "";
    $scope.imageFiles = [];
    $scope.date = new Date();


    $http.get('/haberimdesin/getCategories').success(function (res) {
        //console.log(res);
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

                        case error.POSITION_UNAVAILABLE:
                            return reject({ error: "Location information is unavailable." });

                        case error.TIMEOUT:
                            return reject({ error: "The request to get user location timed out." });

                        case error.UNKNOWN_ERROR:
                            return reject({ error: "An unknown error occurred." });

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
                console.log(response+"oldu");
              window.location.assign('/Home/Index');
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