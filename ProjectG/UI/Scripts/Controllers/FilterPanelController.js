angular.module("GApp")
  .controller("FilterPanelController", ["$scope", function ($scope) {
    $scope.active = false;
    $scope.sortingDropdownActive = false;

    $scope.toggleSortingDropdown = function() {
      $scope.sortingDropdownActive = !$scope.sortingDropdownActive;
    }
  }]);