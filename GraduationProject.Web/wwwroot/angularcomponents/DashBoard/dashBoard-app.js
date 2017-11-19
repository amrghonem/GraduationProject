var dashBoard = angular.module('dashBoard-app', ['ngRoute'])
    .config(function ($routeProvider, $locationProvider) {

        $routeProvider.when('/skills',
            {
                templateUrl: "/angularcomponents/DashBoard/templates/skills.html",
            controller: 'skillsCtrl'  
           })




    });



    