angular.module("GApp")
  .controller("LeftPanelController", ["$scope", function ($scope)
  {
    $scope.rubricsActive = false;
    $scope.favouriteActive = false;

    $scope.toggleRubricsActive = function (value)
    {
      $scope.favouriteActive = false;

      if (!!value)
      {
        $scope.rubricsActive = value;
        return;
      }

      $scope.rubricsActive = !$scope.rubricsActive;
    }

    $scope.toggleFavouriteActive = function (value)
    {
      $scope.rubricsActive = false;

      if (!!value)
      {
        $scope.favouriteActive = value;
        return;
      }

      $scope.favouriteActive = !$scope.favouriteActive;
    }
  }
  ]);