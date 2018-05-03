catalogueModule.controller('CatalogueController', ['$scope', 'CatalogueService', function ($scope, CatalogueService) {

    $scope.searchBookBy = 'Title';
    $scope.search = {
        Title: "",
        Author: "",
        Genre: ""
    };

    $scope.issueBook=function(){
        
    };

    $scope.clearSearchFields = function () {
        switch ($scope.searchBookBy) {
            case 'Title': $scope.search.Author = ''; $scope.search.Genre = ''; break;
            case 'Author': $scope.search.Title = ''; $scope.search.Genre = ''; break;
            case 'Genre': $scope.search.Author = ''; $scope.search.Title = ''; break;
            default: $scope.search.Title = $scope.searchBookInput; break;
        }
    };

    (function () {
        CatalogueService.getAllBooks().then((response) => {
            $scope.booksList = response.data;

            console.log($scope.booksList);
        }, (error) => {
            console.log(error);
        });
    })();

    $scope.setSearchBy = function () {
        console.log("setSearchBy called");

        switch ($scope.searchBookBy) {
            case 'Title': console.log("jkd"); $scope.search.Title = $scope.searchBookInput; break;
            case 'Author': $scope.search.Author = $scope.searchBookInput; break;
            case 'Genre': $scope.search.Genre = $scope.searchBookInput; break;
            default: $scope.search.Title = $scope.searchBookInput; break;
        }
    };

    $scope.foo = function () {
        console.log("jaldkjlakjdljaskdjalksjd");
    };

    // $scope.getAllBooks = function () {
    //     CatalogueService.getAllBooks().then((response) => {
    //         $scope.booksList = response.data;

    //         console.log($scope.booksList);
    //     }, (error) => {
    //         console.log(error);
    //     });
    // };

    // $scope.$watch("booksList", function(oldBooksList,newBooksList){

    // });


}]);