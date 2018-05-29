angular.module('BookShelfModule').service('BookShelfService', ['$http', '$cookies', function ($http, $cookies) {

    // send session id with the request
    $http.defaults.headers.common.SessionId = $cookies.get('session-id');

    var DOMAIN_NAME = 'http://localhost:59684/';
    var BOOK_SHELF = 'books/shelf';
    var RETURN_BOOK = 'books/return';

    this.serviceWorks = function () {
        console.log("book shelf service works");
    };

    this.getAllBooksForCurrentUser = function () {
        // send email-id with the request-header
        $http.defaults.headers.common.EmailId = $cookies.get('logged-in-email-id');
        return $http.get(DOMAIN_NAME + BOOK_SHELF);
    };

    this.returnBook = function (bookDetails) {
        $http.defaults.headers.common.EmailId = $cookies.get('logged-in-email-id');

        return $http.post(DOMAIN_NAME + RETURN_BOOK, JSON.stringify(bookDetails));
    };
}]);