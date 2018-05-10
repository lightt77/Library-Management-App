angular.module('NotificationModule', []).controller('NotificationController', ['$scope', '$cookies', '$interval', 'NotificationService', function ($scope, $cookies, $interval, NotificationService) {

    let NOTIFICATION_REFRESH_INTERVAL_IN_SECONDS = 3;

    console.log("Notification controller called");

    $scope.notificationsList = [];

    // make all notifications inactive at the start
    $scope.currentlySelectedNotification = -1;


    // fetches all notifications for the logged-in user
    $scope.getAllNotificationsForCurrentUser = function () {

        console.log("sessionid is " + $cookies.get('session-id'));

        NotificationService.getAllNotificationsForUser().then(
            (response) => {
                console.log("Notification response is ");
                console.log(response);

                $scope.notificationsList = response.data;
            },
            (error) => {
                console.log("Notification error is ");
                console.log(error);
            }
        );
    }

    // get notificactions once at page reload
    $scope.getAllNotificationsForCurrentUser();

    // refresh notifications
    $interval($scope.getAllNotificationsForCurrentUser, NOTIFICATION_REFRESH_INTERVAL_IN_SECONDS*1000);

}]);