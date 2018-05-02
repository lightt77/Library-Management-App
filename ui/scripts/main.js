var app=angular.module("App",['ngRoute','BookModule'
        ]);

app.config(['$routeProvider',function($routeProvider){
    $routeProvider
        .when('/home',{
            templateUrl:'views/home.html',
        })
        .when('/login',{
            templateUrl:'views/login.html'
        })
        .when('/register',{
            templateUrl:'views/register.html'
        })
        .otherwise({
            redirectTo:'/home'
        });
}]);

app.controller("MainController",function(){
    console.log("main controller running");
});

app.controller("LoginController",function($scope){
    $scope.clickFunction=function(){
        console.log("button clicked");
    };
});

app.controller("HomeController",['$scope','$http',function($scope,$http){
    $http.get("http://localhost:59684/books/all")
        .then(successCallBack,failureCallBack);

    function successCallBack(response){
        console.log("success");
        console.log(response);

        console.log(response.data);
    };

    function failureCallBack(error){
        console.log("failure");
        console.log(error);
    };
        // .success(function(data){
        //     console.log("request succeeded...");
        // })
        // .failure(function(data){
        //     console.log("failed");
        // });
}]);

// angular.module("App").controller("BookController",function(){
//     console.log("kdaksdkajdk");

// });
