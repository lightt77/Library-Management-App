angular.module('AdminModule').service('AdminService', ['$http', '$cookies', function ($http, $cookies) {
    
    var DOMAIN_NAME = 'http://localhost:59684/';
    var NOTIFICATIONS = 'users/notifications';

    this.serviceWorks = function () {
        console.log("admin service works");
    };

    // this.getAllNotificationsForUser = function () {
    //     return $http.get(DOMAIN_NAME + NOTIFICATIONS);
    // };

}]);