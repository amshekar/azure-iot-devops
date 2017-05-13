(function () {
    'use strict';

    angular.module('PatientApp')
        .config(['$routeProvider', function ($routeProvider) {
            $routeProvider.when('/', {
                templateUrl: '/App/Templates/Patient/Index.html',
                controller: 'PatientController'
            })
            .otherwise({
                templateUrl: '/App/Templates/Shared/_404.html'
            })
        }]);
        
})();