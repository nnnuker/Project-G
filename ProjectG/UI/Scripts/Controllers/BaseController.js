angular.module("GApp")
  .controller("BaseController", ["$scope", function ($scope) {
    $scope.advancedSearchActive = false;
    $scope.navigatorActive = false;
    $scope.loaderActive = false;

    $scope.toggleNavigatorActive = function () {
      $scope.navigatorActive = !$scope.navigatorActive;
    }

    $scope.toggleLoader = function () {
      //$scope.loaderActive = !$scope.loaderActive;
    }
  }
  ]);