angular.module('LoginModule').service('LoginService', ['$http', function ($http) {

    var DOMAIN_NAME = 'http://localhost:59684/';
    var LOGIN = 'users/login';

    this.serviceWorks = function () {
        //console.log("regisetr service works");
    };

    this.login = function (loginDetails) {
        console.log(JSON.stringify(loginDetails));

        return $http.post(DOMAIN_NAME + LOGIN, JSON.stringify(loginDetails));
    };
}]);