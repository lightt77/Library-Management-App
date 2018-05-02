angular.module("BookModule").controller('BookController',['$scope', 'BookService', function($scope, BookService){
    
    BookService.foo();

    $scope.getAllBooks = function(){
      BookService.getAllBooks().then((response) => {
        $scope.booksList = response.data;
      }, (error) => {
        console.log(error);
      });
    };
  
    // $scope.getBooksByGenre = function(){
    //   BookService.getBooksByGenre($scope.GenreName).then((response) => {
    //     $scope.booksListByGenre = response.data;
    //   }, (error) => {
    //     console.log(error);
    //   });
    // };
  
    // $scope.getBooksByAuthor = function(){
    //   BookService.getBooksByAuthor($scope.AuthorName).then((response) => {
    //     $scope.booksListByAuthor = response.data;
    //   }, (error) => {
    //     console.log(error);
    //   });
    // };
  
    // $scope.getUsersForBook = function(){
    //   BookService.getUsersForBook($scope.bookDetails).then((response) => {
    //     $scope.UsersForBook = response.data;
    //   }, (error) => {
    //     console.log(error);
    //   });
    // };
  
    // $scope.AddBook = function(){
    //   BookService.AddBook($scope.bookDetails).then((response) => {
    //     $scope.bookAdded = response.data;
    //   }, (error) => {
    //     console.log(error);
    //   });
    // };
  
    // $scope.DeleteBook = function(){
    //   BookService.DeleteBook($scope.bookDetails).then((response) => {
    //     $scope.bookDeleted = response.data;
    //   }, (error) => {
    //     console.log(error);
    //   });
    // };
  
  }]);

// BookController.$inject = ['$scope' , 'BookService'];
