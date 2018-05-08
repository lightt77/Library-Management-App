loginModule.controller('LoginController', ['$scope', 'LoginService', '$window', '$cookies', function ($scope, LoginService, $window, $cookies) {

    $scope.login = function () {
        //console.log($scope.loginDetails);
        LoginService.login($scope.loginDetails).then(
            (response) => {
                //console.log("success");
                console.log(response);
                // TODO: change this once session is added
                $scope.currentUserEmail = $scope.loginDetails.EmailAddress;
                $cookies.put('session-id',response.data);
                // console.log(response.headers);
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