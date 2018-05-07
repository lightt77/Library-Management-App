loginModule.controller('LoginController', ['$scope', 'LoginService', function ($scope, LoginService) {

    $scope.login = function () {
        //console.log($scope.loginDetails);
        LoginService.login($scope.loginDetails).then(
            (response) => {
                console.log("success");
                console.log(response);
            },
            (error) => {
                console.log("error");
                console.log(error);
            }
        );
    };
}]);