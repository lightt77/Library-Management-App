angular.module('NotificationModule').service('NotificationService', ['$http', '$cookies', function ($http, $cookies) {
    
    // send session id with the request
    $http.defaults.headers.common.SessionId = $cookies.get('session-id');
    
    // send email-id with the request-header
    $http.defaults.headers.common.EmailId = $cookies.get('logged-in-email-id');

    var DOMAIN_NAME = 'http://localhost:59684/';
    var NOTIFICATIONS = 'users/notifications';

    this.serviceWorks = function () {
        console.log("notification service works");
    };

    this.getAllNotificationsForUser = function () {
        return $http.get(DOMAIN_NAME + NOTIFICATIONS);
    };
}]);