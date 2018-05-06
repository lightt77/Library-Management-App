angular.module('RegisterModule').service('RegisterService', ['$http', function ($http) {

    var DOMAIN_NAME = 'http://localhost:59684/';
    var REGISTER = 'users/register';
    
    this.serviceWorks = function () {
        console.log("regisetr service works");
    };

    this.register = function (registrationDetails) { 
        console.log(JSON.stringify(registrationDetails));

        $http.post(DOMAIN_NAME+REGISTER,JSON.stringify(registrationDetails));
    };
}]);