var layoutModule = angular.module("Layout", ["ngRoute"]);

layoutModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/login", {
            templateUrl:"Views/Login1.html"
        })
        .otherwise({
            redirectTo:"/login"
        })
}]);

layoutModule.controller("LayoutController", [function () {
    alert("works");
}]);