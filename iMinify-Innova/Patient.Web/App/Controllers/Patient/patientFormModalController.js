(function () {
    'use strict';

    angular
        .module('PatientApp')
        .controller('PatientFormModalController', PatientFormModalController);

    PatientFormModalController.$inject = ['$scope', '$modalInstance'];

    function PatientFormModalController($scope, $modalInstance) {

        $scope.ok = function () {
            $modalInstance.close($scope.Patient);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
})();