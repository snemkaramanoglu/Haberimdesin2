var HaberimdesinApp = angular.module('Haberimdesin', []);

//routing

//Controllers
var habers = null;
var lastNew = null;
var last2News = null;
var updatedLikes = false;
var haberLikes = null;
var commentLikes = null;
var haberDislikes = null;
var commentDislikes = null;
var activeUserID = null; 
HaberimdesinApp.controller('News', ['$scope', '$http', function ($scope, $http) {
    $scope.activeHaber = null;
    $scope.getUserID = function () {
        return activeUserID;
    }
    $scope.last2News = function () {
        return last2News;
    }
    $scope.lastNew = function () {
        return lastNew;
    }
    $scope.habers = function () {
        return habers;
    };

    $scope.getHaberLikesOf = function (id) {
        var usrName = "@HttpContext.Current.User.Identity.Name";
        if (haberLikes.has(id))
            return haberLikes.get(id).length;
        else return 0;
    }
    $scope.getHaberDislikesOf = function (id) {
        if (haberDislikes.has(id))
            return haberDislikes.get(id).length;
        else return 0;
    }
    $scope.getCommentDislikesOf = function (id) {
        if (commentDislikes.has(id))
            return commentDislikes.get(id).length;
        else return 0;
    }
    $scope.getCommentLikesOf = function (id) {
        if (commentLikes.has(id))
            return commentLikes.get(id).length;
        else return 0;
    }
    $http.get('/haberimdesin/getCategories').success(function (res) {
        $scope.categories = res.categories;
    }).error(function (err) {
        console.log(err);
    });
  
    $scope.updateUserID = function(){
        var url = "/haberimdesin/getUserID";
        $http.get(url).success(function (re) {
            activeUserID = re.usID;
        }).error(function (err) { console.log(err); });
    }
    $scope.getAllNews = function () {
        var url = "/haberimdesin/getAllNews";
        $http.get(url).success(function (re) {

            habers = re.newsList.reverse();
            lastNew = habers[0];
            last2News = habers.slice(1, 3);

            $scope.activeHaber = null;
        }).error(function (err) { console.log(err); });
    }
    
    $scope.updateHaberLikes = function () {
        if (updatedLikes) return;
        updatedLikes = true;

        haberLikes = new Map;
        haberDislikes = new Map;
        commentLikes = new Map;
        commentDislikes = new Map;

        var url = "/haberimdesin/getAllHaberLikes";
        $http.get(url).success(function (re) {
            
            for (i = 0; i < re.haberLikeList.length; i++) {
                var id = re.haberLikeList[i].haberID;
                if (haberLikes.has(id) === false)
                    haberLikes.set(id, []);
                var value = haberLikes.get(id);
                value.push(re.haberLikeList[i].userID);
                haberLikes.set(id, value);
            }
        }).error(function (err) { console.log(err); });

        
        url = "/haberimdesin/getAllHaberDislikes";
        $http.get(url).success(function (re) {

            for (i = 0; i < re.haberDislikeList.length; i++) {
                var id = re.haberDislikeList[i].haberID;
                if (haberDislikes.has(id) === false)
                    haberDislikes.set(id, []);
                var value = haberDislikes.get(id);
                value.push(re.haberDislikeList[i].userID);
                haberDislikes.set(id, value);
            }
        }).error(function (err) { console.log(err); });


        url = "/haberimdesin/getAllCommentLikes";
        $http.get(url).success(function (re) {

            for (i = 0; i < re.commentLikeList.length; i++) {
                var id = re.commentLikeList[i].commentID;
                if (commentLikes.has(id) === false)
                    commentLikes.set(id, []);
                var value = commentLikes.get(id);
                value.push(re.commentLikeList[i].userID);
                commentLikes.set(id, value);
            }
        }).error(function (err) { console.log(err); });

        url = "/haberimdesin/getAllCommentDislikes";
        $http.get(url).success(function (re) {

            for (i = 0; i < re.commentDislikeList.length; i++) {
                var id = re.commentDislikeList[i].commentID;
                if (commentDislikes.has(id) === false)
                    commentDislikes.set(id, []);
                var value = commentDislikes.get(id);
                value.push(re.commentDislikeList[i].userID);
                commentDislikes.set(id, value);
            }
        }).error(function (err) { console.log(err); });
    }
    $scope.getAllNews();
    $scope.updateHaberLikes();
    $scope.updateUserID();
    $scope.getNewsByCategory = function (id) {
        
        var url = "/haberimdesin/getNewsByID/" + id;
        $http.get(url).success(function (re) {
           
            last2News = null;
            lastNew = null;
            habers = re.newsList.reverse();
     
            $scope.activeHaber = null;
        }).error(function (err) { console.log(err); });
        
    }
    $scope.likeComment = function (id) {

        for (i = 0 ; commentLikes.has(id) && i < commentLikes.get(id).length; i++) {
            if (commentLikes.get(id)[i] === activeUserID) return;
        }
        for (i = 0 ; commentDislikes.has(id) && i < commentDislikes.get(id).length; i++) {
            if (commentDislikes.get(id)[i] === activeUserID) return;
        }

        var fd = new FormData();
        fd.append('id', id);
        $http.post('/Haberimdesin/LikeComment', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (commentLikes.has(id) === false)
                commentLikes.set(id, []);
            var val = commentLikes.get(id);
            val.push(activeUserID);
            commentLikes.set(id, val);
        }).error(function (err) {
            console.log(err);
        });

    }
    $scope.dislikeComment = function (id) {


        for (i = 0 ; commentLikes.has(id) && i < commentLikes.get(id).length; i++) {
            if (commentLikes.get(id)[i] === activeUserID) return;
        }
        for (i = 0 ; commentDislikes.has(id) && i < commentDislikes.get(id).length; i++) {
            if (commentDislikes.get(id)[i] === activeUserID) return;
        }

        var fd = new FormData();
        fd.append('id', id);
        $http.post('/Haberimdesin/DislikeComment', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (commentDislikes.has(id) === false)
                commentDislikes.set(id, []);
            var val = commentDislikes.get(id);
            val.push(activeUserID);
            commentDislikes.set(id, val);
        }).error(function (err) {
            console.log(err);
        });

    }
    $scope.likeNews = function () {
        var id = $scope.activeHaber[0].haberID;
        
        for (i = 0 ; haberLikes.has(id) && i < haberLikes.get(id).length; i++) {
            if(haberLikes.get(id)[i] === activeUserID) return;
        }
        for (i = 0 ; haberDislikes.has(id) && i < haberDislikes.get(id).length; i++) {
            if (haberDislikes.get(id)[i] === activeUserID) return;
        }
        var fd = new FormData();
        fd.append('id',  $scope.activeHaber[0].haberID);
        $http.post('/Haberimdesin/LikeNews', fd,  {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (haberLikes.has(id) === false)
                haberLikes.set(id, []);
            var val = haberLikes.get(id);
            val.push(activeUserID);
            haberLikes.set(id, val);
        }).error(function (err) {
            console.log(err);
        });

    }
    $scope.dislikeNews = function () {

        var id = $scope.activeHaber[0].haberID;

        for (i = 0 ; haberLikes.has(id) && i < haberLikes.get(id).length; i++) {
            if (haberLikes.get(id)[i] === activeUserID) return;
        }
        for (i = 0 ; haberDislikes.has(id) && i < haberDislikes.get(id).length; i++) {
            if (haberDislikes.get(id)[i] === activeUserID) return;
        }
        var fd = new FormData();
        fd.append('id', $scope.activeHaber[0].haberID);
        $http.post('/Haberimdesin/DislikeNews', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (haberDislikes.has(id) === false)
                haberDislikes.set(id, []);
            var val = haberDislikes.get(id);
            val.push(activeUserID);
            haberDislikes.set(id, val);
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