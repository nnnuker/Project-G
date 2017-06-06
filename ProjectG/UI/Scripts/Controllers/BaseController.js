angular.module("GApp")
  .controller("BaseController", ["$scope", function ($scope)
  {
    $scope.advancedSearchActive = false;
    $scope.loaderActive = false;

    $scope.raiseChangedCategory = function (id)
    {
      $scope.$broadcast("changedCategory", id);
    }

    $scope.toggleLoader = function (value)
    {
      if (!!value)
      {
        $scope.loaderActive = value;
        return;
      }

      $scope.loaderActive = !$scope.loaderActive;
    }
  }
  ]);