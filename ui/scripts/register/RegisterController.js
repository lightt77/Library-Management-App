registerModule.controller("RegisterController", ['$scope', 'RegisterService', function ($scope, RegisterService) {

    $scope.register = function () {
        console.log("register");
        RegisterService.register($scope.registerDetails);
    }


}]);