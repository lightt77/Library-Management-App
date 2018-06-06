angular.module('CatalogueModule').controller('CatalogueController', ['$window', '$scope', 'catalogueService', '$interval', 'AdminService', function ($window, $scope, catalogueService, $interval, AdminService) {

    var BOOK_LIST_INTERVAL_IN_SECONDS = 3;

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

    $scope.routeToAddBooksForm = function () {
        // redirect to add book form
        $window.location.href = '#!/home/addBookForm';
    };

    $scope.addNewBook = function () {
        // parse genres from the comma-seperated list into genre array
        $scope.bookDetails.Genre=$scope.commaSeperatedGenresList.split(",");

        console.log($scope.bookDetails);
        catalogueService.addBook($scope.bookDetails);
        $window.location.href = "#!/home/catalogue";
    };

    getAllBooks = function () {
        console.log("getAllBooks() called");
        catalogueService.getAllBooks().then(
            (response) => {
                $scope.booksList = response.data;
                //$scope.adminStatus=AdminService.checkAdminStatus();
                //console.log($scope.adminStatus);
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
        console.log("issue book " + bookName);
        catalogueService.makeBookIssueRequest({ "Title": bookName });

        // decrement quantity
        // for (var i in $scope.booksList) {
        //     if (i.Title == bookName && i.Quantity != 0) {
        //         i.Quantity--;
        //     }
        // }
    }

    $scope.addToWishList = function (bookName) {
        console.log("Adding " + bookName + " to wishlist");
        catalogueService.addToWishList({ 'Title': bookName });
    };

    // $scope.isUserAdmin = function () {
    //     console.log(AdminService.checkAdminStatus());
    //     return AdminService.checkAdminStatus();
    // };

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