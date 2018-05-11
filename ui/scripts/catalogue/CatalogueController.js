angular.module('CatalogueModule').controller('CatalogueController', ['$scope', 'catalogueService', function ($scope, catalogueService) {

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

    function getGenresAsACommaSeperatedString(genres){
        let output="";

        for(var genre in genres)
            output+=""+genre+",";
        
        return output;
    };

    catalogueService.getAllBooks().then(
        (response) => {
             $scope.booksList = response.data;
        },
        (error) => {
            console.log(error);
        }
    );


    $scope.issueBook=function(bookName){
        //console.log(bookName);
        catalogueService.makeBookIssueRequest(bookName);

    }


}]);