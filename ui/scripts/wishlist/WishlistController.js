angular.module("WishlistModule").controller('WishlistController', ['WishlistService', '$scope', function (WishlistService, $scope) {
    console.log("wishlist");

    WishlistService.getAllBooksForCurrentUser().then((response) => {
        console.log(response.data);
        $scope.wishlistRecords=response.data;
    }, (error) => {
        console.log(error);
    })
}]);