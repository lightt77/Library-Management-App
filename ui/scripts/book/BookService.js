bookModule.service('BookService', ['$http', function ($http) {

    var DOMAIN_NAME = 'http://localhost:59684/';
    var GET_ALL_BOOKS = 'Books/all';
    var GET_BOOKS_BY_GENRE = 'Books/genre';
    var GET_BOOKS_BY_AUTHOR = 'Books/author';
    var ADD_BOOK = 'Books/add';
    var DELETE_BOOK = 'Books/delete';
    var GET_USERS_FOR_BOOK = 'Books/users';

    this.foo = function () {
        //console.log("Service works...");
    };

    this.getAllBooks = function () {
        return $http.get(DOMAIN_NAME + GET_ALL_BOOKS);
    };
}]);