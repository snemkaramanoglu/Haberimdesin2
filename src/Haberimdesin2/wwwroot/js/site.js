<script type="text/javascript" src="http://l2.io/ip.js?var=myip"></script>

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


