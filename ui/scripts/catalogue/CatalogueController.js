catalogueModule.controller('CatalogueController', ['$scope', 'CatalogueService', function ($scope, CatalogueService) {

    CatalogueService.foo();

    (function () {
        CatalogueService.getAllBooks().then((response) => {
            $scope.booksList = response.data;

            console.log($scope.booksList);
        }, (error) => {
            console.log(error);
        });
    })();

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