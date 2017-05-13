(function () {
    'use strict';

    angular
        .module('PatientApp')
        .controller('PatientController', PatientController);

    PatientController.$inject = ['$scope', '$q', 'PatientService', 'errorHandler', '$modal'];

    function PatientController($scope, $q, PatientService, errorHandler, $modal) {
        (function startup() {
            var Patients = PatientService.getPatients();

            $q.all([
                Patients
            ]).then(function (data) {
                if (data != null) {
                    $scope.Patients = data[0];
                }
            }, function (reason) {
                errorHandler.logServiceError('PatientController', reason);
            }, function (update) {
                errorHandler.logServiceNotify('PatientController', update);
            });
        })();

        function removePatient(PatientId) {
            for (var i = 0; i < $scope.Patients.length; i++) {
                if ($scope.Patients[i].id == PatientId) {
                    $scope.Patients.splice(i, 1);
                    break;
                }
            }
        };

        $scope.Patients = [];

        $scope.Commands = {
            savePatient: function (Patient) {
                debugger;
                PatientService.addPatient(Patient).then(
                    function (result) {
                        $scope.Patients.push(result.data);
                    },
                    function (response) {
                        console.log(response);
                    });
            },
            updatePatient: function (Patient) {
                PatientService.updatePatient(Patient).then(
                    function (result) {

                    },
                    function (response) {
                        console.log(response);
                    });
            }
        };

        $scope.Queries = {
            getPatients: function () {
                PatientService.getPatients();
            },
            getPatientById: function (PatientId) {
                PatientService.getPatientById(PatientId);
            }
        };
       
        $scope.calculateAge = function (birthday) { // pass in player.dateOfBirth            
            if (birthday!=null && birthday.length == 10) {
                var ageDifMs = Date.now() - new Date(birthday);
                var ageDate = new Date(ageDifMs); // miliseconds from epoch
                return Math.abs(ageDate.getUTCFullYear() - 1970);

            }
            return 0;
           
        }
        $scope.Modals = {
            open: function (Patient) {
                $scope.Patient = Patient;

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/App/Templates/Patient/PatientFormModal.html',
                    controller: 'PatientFormModalController',
                    size: 'md',
                    scope: $scope,
                    backdrop: 'static'
                });

                modalInstance.result.then(
                    function (Patient) {
                        if (Patient.id != null) {
                            $scope.Commands.updatePatient(Patient);
                        }
                        else {
                            $scope.Commands.savePatient(Patient);
                        }
                    },
                    function (event) {

                    });
            },
        }
    };
})
();