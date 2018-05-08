angular.module('NotificationModule').service('NotificationService', ['$http', '$cookies', function ($http, $cookies) {
    $http.defaults.headers.common.SessionId = $cookies.get('session-id');
    var DOMAIN_NAME = 'http://localhost:59684/';
    var NOTIFICATIONS = 'users/notifications';

    this.serviceWorks = function () {
        console.log("notification service works");
    };

    this.getAllNotificationsForUser = function () {
        return $http.get(DOMAIN_NAME + NOTIFICATIONS);
    };

}]);