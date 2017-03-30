angular.module("GApp")
  .controller("BaseController", ["$scope", function ($scope) {
    $scope.advancedSearchActive = false;
    $scope.navigatorActive = false;

    $scope.toggleNavigatorActive = function() {
      $scope.navigatorActive = !$scope.navigatorActive;
    }
  }
  ]);