(function () {
    'use strict';

    angular
        .module('PatientApp')
        .factory('PatientService', PatientService);

    PatientService.$inject = ['$resource', '$q', '$http'];

    function PatientService($resource, $q, $http) {
        var resource = $resource('/api/Patients/:action/:param', { action: '@action', param: '@param'}, {
            'update': { method: 'PUT' }
        });

        var _getPatients = function () {
            var deferred = $q.defer();
            resource.query({ action: "get", param: ""},
				function (result) {
				    if (result == null) {
				        result = [];
				    };
				    deferred.resolve(result);
				},
				function (response) {
				    deferred.reject(response);
				});
            return deferred.promise;

        };

        var _getPatientById = function (PatientId) {
            var deferred = $q.defer();
            resource.query({ action: 'ById', param: PatientId},
				function (result) {
				    if (result == null) {
				        result = [];
				    };

				    deferred.resolve(result);
				},
				function (response) {
				    deferred.reject(response);
				});
            return deferred.promise;
        };

        var _addPatient = function (PatientDto) {
            var deferred = $q.defer();

            $http.post('/api/Patients', PatientDto)
                .then(function (result) {
                        deferred.resolve(result);
                    },
                        function (response) {
                            deferred.reject(response);
                        });

            return deferred.promise;
        };

        var _updatePatient = function (PatientDto) {
            var deferred = $q.defer();

            $http.put('/api/Patients/' + PatientDto.id, PatientDto)
                .then(function (result) {
                    deferred.resolve(result);
                },
                        function (response) {
                            deferred.reject(response);
                        });

            return deferred.promise;
        };

        var _deletePatient = function (PatientId) {
            var deferred = $q.defer();

            resource.delete({ action: "", param: PatientId},
                    function (result) {
                        if (result == null) {
                            result = [];

                        };
                        deferred.resolve(result);
                    },
                    function (response) {
                        deferred.reject(response);
                    });
            return deferred.promise;
        };

        return {
            getPatients: _getPatients,
            getPatientById: _getPatientById,
            addPatient: _addPatient,
            updatePatient: _updatePatient,
            deletePatient: _deletePatient
        };

    }

})();