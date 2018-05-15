angular.module('RequestModule').service('RequestService', ['$http', '$cookies', function ($http, $cookies) {

    // send session id with the request
    $http.defaults.headers.common.SessionId = $cookies.get('session-id');

    // send email-id with the request-header
    $http.defaults.headers.common.EmailId = $cookies.get('logged-in-email-id');

    var DOMAIN_NAME = 'http://localhost:59684/';
    var GET_PENDING_REQUESTS = 'pending-requests';
    var REJECT_REQUEST = 'reject-request';
    var APPROVE_REQUEST = 'approve-request';

    this.serviceWorks = function () {
        console.log("request service works");
    };

    this.getPendingRequests = function () {
        return $http.get(DOMAIN_NAME + GET_PENDING_REQUESTS);
    };

    this.reject = function (rentalDetails) {
        return $http.put(DOMAIN_NAME + REJECT_REQUEST, JSON.stringify(rentalDetails));
    };

    this.approve = function (rentalDetails) {
        return $http.put(DOMAIN_NAME + APPROVE_REQUEST, JSON.stringify(rentalDetails));
    };
}]);