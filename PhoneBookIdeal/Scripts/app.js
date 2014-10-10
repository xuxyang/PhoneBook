var phonebookApp = angular.module('phonebookApp', [
    'ngRoute',
    'phonebookControllers'
]);

phonebookApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
    when('/List', {
        templateUrl: '/Partials/List.html',
        controller: 'ListController'
    }).
    when('/Details/:itemId', {
        templateUrl: '/Partials/Details.html',
        controller: 'DetailsController'
    }).
    when('/Add', {
        templateUrl: '/Partials/Edit.html',
        controller: 'AddController'
    }).
    when('/Edit/:itemId', {
        templateUrl: '/Partials/Edit.html',
        controller: 'EditController'
    }).
    otherwise({
        redirectTo: '/List'
    });
}]);