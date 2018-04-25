var app = angular.module("dummy", []);

app.controller("DummyController", ['$scope', function ($scope) {

    console.log("hello World");
    $scope.clickCallBack = function () {
        alert("button clicked");
    };
}]);
