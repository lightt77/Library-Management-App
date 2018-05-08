angular.module('NotificationModule', []).controller('NotificationController', ['$scope', '$cookies', 'NotificationService', function ($scope, $cookies, NotificationService) {
    console.log("Notification controller called");

    $scope.notificationsList = [];

    $scope.getAllNotificationsForCurrentUser = function () {
        $cookies.put('dummy', 'Abhishek');
        // console.log($cookies.get('logged-in'));
        // console.log($cookies.get('email'));

        console.log("Email = " + $cookies.get('dummy'));
        console.log("sessionid is " + $cookies.get('session-id'));

        NotificationService.getAllNotificationsForUser().then(
            (response) => {
                console.log("Notification response is ");
                console.log(response);
            },
            (error) => {
                console.log("Notification error is ");
                console.log(error);
            }
        );

        // NotificationService.getAllNotificationsForUser().then(
        //     (response) => {
        //         console.log("works")
        //     },
        //     (error) => {
        //         console.log(error);
        //     }
        // );



    }

    // fetch all notifications on page reload
    $scope.getAllNotificationsForCurrentUser();




}]);