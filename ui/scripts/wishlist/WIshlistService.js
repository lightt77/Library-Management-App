angular.module('WishlistModule').service('WishlistService', ['$http', '$cookies', function ($http, $cookies) {

    // send session id with the request
    $http.defaults.headers.common.SessionId = $cookies.get('session-id');

    var DOMAIN_NAME = 'http://localhost:59684/';
    var WISHLIST = 'user/wishlist';

    this.serviceWorks = function () {
        console.log("wishlist service works");
    };

    this.getAllBooksForCurrentUser = function () {
        // send email-id with the request-header
        $http.defaults.headers.common.EmailId = $cookies.get('logged-in-email-id');
        return $http.get(DOMAIN_NAME + WISHLIST);
    };
}]);