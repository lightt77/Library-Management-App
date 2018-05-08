loginModule.controller('LoginController', ['$scope', 'LoginService', '$window', function ($scope, LoginService,$window) {

    $scope.login = function () {
        //console.log($scope.loginDetails);
        LoginService.login($scope.loginDetails).then(
            (response) => {
                console.log("success");
                console.log(response);
                // redirect to catalogue page on successful login
                $window.location.href = '#!/home/catalogue';
            },
            (error) => {
                console.log("error");
                console.log(error);
            }
        );
    };
}]);