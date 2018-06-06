var app = angular.module("App", ['ngRoute', 'ngCookies',
    'BookModule',
    'HomeModule',
    'CatalogueModule',
    'LoginModule',
    'RegisterModule',
    'NotificationModule',
    'AdminModule',
    'BookShelfModule',
    'RequestModule',
    'WishlistModule'
]);

app.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
    $routeProvider
        .when('/home', {
            templateUrl: 'views/home.html',
        })
        .when('/login', {
            templateUrl: 'views/login/login.html',
            //resolve trial
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (!isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .when('/register', {
            templateUrl: 'views/register/register.html',
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (!isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .when('/home/catalogue', {
            templateUrl: 'views/catalogue/catalogue.html',
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .when('/home/bookshelf', {
            templateUrl: 'views/bookshelf/bookshelf.html',
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .when('/home/wishlist', {
            templateUrl: 'views/wishlist/wishlist.html',
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .when('/home/requests', {
            templateUrl: 'views/requests/requests.html',
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .when('/home/notifications', {
            templateUrl: 'views/notifications/notifications.html',
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .when('/home/admin', {
            templateUrl: 'views/admin/admin.html',
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .when('/home/addBookForm', {
            templateUrl: 'views/catalogue/addBookForm.html',
            resolve: {
                app: function ($q) {
                    var defer = $q.defer();
                    if (isLoggedIn())
                        defer.resolve();
                    return defer.promise;
                }
            }
        })
        .otherwise({
            redirectTo: '/login'
        });

    var isLoggedIn = function () {
        console.log("our cookies are " + document.cookie);

        if (document.cookie.indexOf("logged-in-email-id") >= 0) {
            return true;
        }

        return false;
    }
    //$httpProvider.defaults.headers.common = { 'SessionId' : $cookies.get('session-id') };
}]);

app.controller("MainController", ['$scope', '$cookies', function ($scope, $cookies) {
    //console.log("main controller running");

    $scope.isUserLoggedIn = function () {
        return ($cookies.get('logged-in-email-id') != undefined);
    };

    $scope.isUserAdmin = function () {
        return ($cookies.get('admin-status') == "true");
    };

    $scope.getCurrentUserEmailId = function () {
        return ($cookies.get('logged-in-email-id')).split('@')[0];
    };
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
