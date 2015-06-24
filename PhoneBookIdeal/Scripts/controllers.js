var phonebookControllers = angular.module("phonebookControllers", []);

phonebookControllers.controller("ListController", ["$scope", "$http", function ($scope, $http) {
    $http.get("/api/Phone").success(function (data) {
        $scope.phoneRecords = data;
        $scope.recordOrder = 'FirstName';
    });
    $scope.deletePhone = function (phoneID) {
        $http.delete("/api/Phone/" + phoneID).success(function (data) {
            alert("Phone deleted.");
            window.location.reload();
        });
    };
}]);

phonebookControllers.controller("DetailsController", ["$scope", "$http", "$routeParams", function ($scope, $http, $routeParams) {
    var phoneID = $routeParams.itemId;
    $http.get("/api/Phone/" + phoneID).success(function (data) {
        $scope.phoneRecord = data;
    });
}]);

phonebookControllers.controller("AddController", ["$scope", "$http", function ($scope, $http) {
    $scope.savePhone = function () {
        var phoneData = { FirstName: $scope.firstName, LastName: $scope.lastName, PhoneNumber: $scope.phoneNumber };
        $http.post("/api/Phone", phoneData).success(function (data) {
            alert("Phone added.");
        });
    };
}]);

phonebookControllers.controller("EditController", ["$scope", "$http", "$routeParams", function ($scope, $http, $routeParams) {
    var phoneID = $routeParams.itemId;
    $http.get("/api/Phone/" + phoneID).success(function (data) {
        $scope.phoneID = phoneID;
        $scope.firstName = data.FirstName;
        $scope.lastName = data.LastName;
        $scope.phoneNumber = data.PhoneNumber;
    });
    $scope.savePhone = function () {
        var phoneData = { ID: phoneID, FirstName: $scope.firstName, LastName: $scope.lastName, PhoneNumber: $scope.phoneNumber };
        $http.put("/api/Phone/" + phoneID, phoneData).success(function (data) {
            alert("Phone updated.");
        });
    };
}]);
