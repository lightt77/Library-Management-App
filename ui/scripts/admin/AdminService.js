angular.module('AdminModule').service('AdminService', ['$http', '$cookies', function ($http, $cookies) {

    var DOMAIN_NAME = 'http://localhost:59684/';
    var ADMIN_STATUS = 'users/admin';

    this.serviceWorks = function () {
        console.log("admin service works");
    };

    this.checkIfUserIsAdmin = function () {
        // send email-id with the request-header
        $http.defaults.headers.common.EmailId = $cookies.get('logged-in-email-id');
        return $http.get(DOMAIN_NAME + ADMIN_STATUS);
    }

    // this.checkAdminStatus = function () {
    //     if($cookies.get("admin-status"))
    //     {
    //         return true;
    //     }

    //     return false;
    // };

    // this.getAllNotificationsForUser = function () {
    //     return $http.get(DOMAIN_NAME + NOTIFICATIONS);
    // };

}]);