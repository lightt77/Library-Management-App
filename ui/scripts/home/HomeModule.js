var homeModule=angular.module("HomeModule",['ngRoute']);

homeModule.config(['$routeProvider',function($routeProvider){
    // $routeProvider
    //     .when('/home/catalogue',{
    //         templateUrl:'views/catalogue/catalogue.html',
    //     })
    //     .when('/wishlist',{
    //         templateUrl:'views/wishlist/wishlist.html'
    //     })
    //     .when('/notifications',{
    //         templateUrl:'views/notifications/notifications.html'
    //     })
    //     .when('/admin',{
    //         templateUrl:'views/admin/admin.html'
    //     })
    //     .otherwise({
    //         redirectTo:'/home/catalogue'
    //    });
}]);
