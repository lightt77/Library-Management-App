angular.module('LoginModule').service('LoginService', ['$http', '$cookies', function ($http, $cookies) {


    var DOMAIN_NAME = 'http://localhost:59684/';
    var LOGIN = 'users/login';

    this.serviceWorks = function () {
        //console.log("regisetr service works");
    };

    this.login = function (loginDetails) {
        // send email-id with the request-header
        $http.defaults.headers.common.EmailId = $cookies.get('logged-in-email-id');

        console.log(JSON.stringify(loginDetails));

        return $http.post(DOMAIN_NAME + LOGIN, JSON.stringify(loginDetails));
    };
}]);