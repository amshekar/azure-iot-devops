(function () {
    'use strict';

    angular
        .module('PatientApp')
        .controller('GlobalController', GlobalController);

    GlobalController.$inject = ['$scope', '$location'];

    function GlobalController($scope, $location) {
        $scope.$on('$viewContentLoaded', onLoaded);
        $scope.$on('viewContentLoadComplete', onLoadComplete);

        function onLoaded() {
            $scope.$broadcast('viewContentLoadComplete');
        }

        function onLoadComplete() {

        }
    };
})();
