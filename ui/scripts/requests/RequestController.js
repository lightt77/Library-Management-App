angular.module('RequestModule').controller('RequestController',['$scope','RequestService',function($scope,RequestService){
    console.log("requests");

    RequestService.getPendingRequests()
        .then(
            (response)=>{
                console.log(response.data);
                $scope.pendingRentalsList=response.data;
            },
            (error)=>{
                console.log(error);
            }
        );
    
    $scope.approve=function(rentalDetails){
        console.log(rentalDetails);
        RequestService.approve(rentalDetails);
    };

    $scope.reject=function(rentalDetails){
        console.log(rentalDetails);
        RequestService.reject(rentalDetails);
    };

}]);