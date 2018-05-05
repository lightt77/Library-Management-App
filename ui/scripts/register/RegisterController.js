registerModule.controller("RegisterController", ['$scope', 'RegisterService', function ($scope, RegisterService) {
    console.log("RegisterController");

    $scope.register = function () {
        console.log("register");
        RegisterService.register($scope.registerDetails);
    }

    
}]);