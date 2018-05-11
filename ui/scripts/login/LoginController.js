loginModule.controller('LoginController', ['$scope', 'LoginService', '$window', '$cookies', function ($scope, LoginService, $window, $cookies) {

    $scope.logOut = function () {
        $cookies.remove('logged-in-email-id');

        // clear notificaction list
        $scope.notificationsList = [];

        // redirect to login page
        $window.location.href = '#!/login';
    }

    $scope.login = function () {
        //console.log($scope.loginDetails);
        LoginService.login($scope.loginDetails).then(
            (response) => {
                console.log(response);
                // TODO: change this once session is added
                $scope.currentUserEmail = $scope.loginDetails.EmailAddress;

                // persist session-id in a cookie
                $cookies.put('session-id', response.data);

                // persist email-id in a cookie
                // TODO: remove this when session functionality is complete
                $cookies.put('logged-in-email-id', $scope.loginDetails.EmailAddress);

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