angular.module('BookShelfModule').controller('BookShelfController', ['$scope', 'BookShelfService', function ($scope, BookShelfService) {
    console.log("BookShelf controller");

    $scope.getAllBooksForTheUser = function () {
        console.log(BookShelfService.getAllBooksForCurrentUser());
    };

    $scope.returnBook = function (bookDetails) {
        console.log(bookDetails);
    };

    BookShelfService.getAllBooksForCurrentUser()
        .then(
            (response) => {
                console.log(response.data);
                $scope.booksWithCurrentUser = response.data;
            },
            (error) => {
                console.log(error);
            }
        );
}]);