angular.module('CatalogueModule').controller('CatalogueController', ['$scope', 'catalogueService', '$interval', function ($scope, catalogueService, $interval) {

    var BOOK_LIST_INTERVAL_IN_SECONDS = 5;

    $scope.booksList = [];
    $scope.searchBookBy = 'Title';
    $scope.search = {
        Title: "",
        Author: "",
        Genre: ""
    };

    // clears previous search inputs
    $scope.clearSearchFields = function () {
        switch ($scope.searchBookBy) {
            case 'Title': $scope.search.Author = ''; $scope.search.Genre = ''; break;
            case 'Author': $scope.search.Title = ''; $scope.search.Genre = ''; break;
            case 'Genre': $scope.search.Author = ''; $scope.search.Title = ''; break;
            default: $scope.search.Title = $scope.searchBookInput; $scope.search.Author = ''; $scope.search.Genre = '';
        }
    };

    $scope.getGenresAsACommaSeperatedString = function (genres) {
        let output = "";

        for (var i in genres) {
            output += "" + genres[i];

            if (i < genres.length - 1)
                output += ",";
        }
        return output;
    };

    getAllBooks = function () {
        catalogueService.getAllBooks().then(
            (response) => {
                $scope.booksList = response.data;
                //console.log(response.data);
            },
            (error) => {
                console.log(error);
            }
        );
    };

    // fetch all books once at page reload
    getAllBooks();

    // periodically refresh books
     $interval(getAllBooks, BOOK_LIST_INTERVAL_IN_SECONDS * 1000);

    $scope.issueBook = function (bookName) {
        console.log("issue book "+bookName);
        catalogueService.makeBookIssueRequest({ "Title": bookName });

        // decrement quantity
        for (var i in $scope.booksList) {
            if (i.Title == bookName && i.Quantity != 0) {
                i.Quantity--;
            }
        }
    }

    $scope.addToWishList = function (bookName) {
        console.log("Adding " + bookName + " to wishlist");
        catalogueService.addToWishList({'Title':bookName});
    };

    // catalogueService.addBook(bookDetails).then(
    //     (response) => {
    //         console.log("book added");
    //         // redirect to catalogue page
    //         $window.location.href = "#!/home/catalogue";
    //     },
    //     (error) => {
    //         console.log(console.error);
    //     }
    // );


}]);