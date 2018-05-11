angular.module('CatalogueModule').service('catalogueService', ['$http', '$cookies', function ($http, $cookies) {

    // send session id with the request
    $http.defaults.headers.common.SessionId = $cookies.get('session-id');

    // send email-id with the request-header
    $http.defaults.headers.common.EmailId = $cookies.get('logged-in-email-id');

    var DOMAIN_NAME = 'http://localhost:59684/';
    var GET_ALL_BOOKS = 'Books/all';
    var GET_BOOKS_BY_GENRE = 'Books/genre';
    var GET_BOOKS_BY_AUTHOR = 'Books/author';
    var ADD_BOOK = 'Books/add';
    var DELETE_BOOK = 'Books/delete';
    var GET_USERS_FOR_BOOK = 'Books/users';

    var MAKE_BOOK_ISSUE_REQUEST = 'users/book/issue';

    this.foo = function () {
        console.log("Cataloguehadkjhaskjds Service works...");
    };

    this.getAllBooks = function () {
        return $http.get(DOMAIN_NAME + GET_ALL_BOOKS);
    };

    this.makeBookIssueRequest = function (bookName) {
        console.log(bookName);
        $http.defaults.headers.common.BookName = bookName;

        return $http.get(DOMAIN_NAME + MAKE_BOOK_ISSUE_REQUEST);
    }
}]);