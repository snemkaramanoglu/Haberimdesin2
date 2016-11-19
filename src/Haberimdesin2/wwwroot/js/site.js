var HaberimdesinApp = angular.module('Haberimdesin', ['ngRoute']);

//routing
HaberimdesinApp.config(function ($routeProvider) {

    $routeProvider
        .when('/', {
            templateUrl: "/Haberimdesin/Home",
            controller: "HomeController"
        })
        .otherwise({
            redirectTo: "/"
        })
});

//Controllers

HaberimsedinApp.controller('HomeController', function () {

});
