adminModule.controller('AdminController', ['$scope', function ($scope) {
    console.log("Admin Controller");

    $scope.approve = function (notification) {
        console.log("Approve");
        console.log(notification);
    }

    $scope.reject = function (notification) {
        console.log("Reject");
        console.log(notification);
    }

    $scope.return = function (notification) {
        console.log("return");
        console.log(notification);
    }

    $scope.issue = function (notification) {
        console.log("issue");
        console.log(notification);
    }
}]);