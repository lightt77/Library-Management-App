var app = angular.module("App", ['ngRoute', 'ngCookies',
    'BookModule',
    'HomeModule',
    'CatalogueModule',
    'LoginModule',
    'RegisterModule',
    'NotificationModule',
    'AdminModule',
    'BookShelfModule',
    'RequestModule'
]);

app.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
    $routeProvider
        .when('/home', {
            templateUrl: 'views/home.html',
        })
        .when('/login', {
            templateUrl: 'views/login/login.html'
        })
        .when('/register', {
            templateUrl: 'views/register/register.html'
        })
        .when('/home/catalogue', {
            templateUrl: 'views/catalogue/catalogue.html'
        })
        .when('/home/bookshelf', {
            templateUrl: 'views/bookshelf/bookshelf.html'
        })
        .when('/home/wishlist', {
            templateUrl: 'views/wishlist/wishlist.html'
        })
        .when('/home/requests', {
            templateUrl: 'views/requests/requests.html'
        })
        .when('/home/notifications', {
            templateUrl: 'views/notifications/notifications.html'
        })
        .when('/home/admin', {
            templateUrl: 'views/admin/admin.html'
        })
        .otherwise({
            redirectTo: '/login'
        });

    //$httpProvider.defaults.headers.common = { 'SessionId' : $cookies.get('session-id') };
}]);

app.controller("MainController", ['$scope', '$cookies', function ($scope, $cookies) {
    //console.log("main controller running");

    $scope.isUserLoggedIn = function () {
        return ($cookies.get('logged-in-email-id') != undefined);
    };

    $scope.isUserAdmin=function(){
        return ($cookies.get('admin-status')=="true");
    }
}]);


// app.controller("HomeController",['$scope','$http',function($scope,$http){
//     $http.get("http://localhost:59684/books/all")
//         .then(successCallBack,failureCallBack);

//     function successCallBack(response){
//         console.log("success");
//         console.log(response);

//         console.log(response.data);
//     };

//     function failureCallBack(error){
//         console.log("failure");
//         console.log(error);
//     };
//         // .success(function(data){
//         //     console.log("request succeeded...");
//         // })
//         // .failure(function(data){
//         //     console.log("failed");
//         // });
// }]);

// angular.module("App").controller("BookController",function(){
//     console.log("kdaksdkajdk");

// });
