angular.module("GApp")
  .controller("BaseController", ["$scope", "PagesService", "CategoriesService", "$http", "$window",
    function ($scope, pagesService, categoriesService, $http, $window)
  {
    $scope.advancedSearchActive = false;
    $scope.loaderActive = false;
    $scope.searchResults = [];
    $scope.searchString = "";
    $scope.searchActive = false;

    $scope.raiseChangedCategory = function (id)
    {
      $scope.$broadcast("changedCategory", id);
    }

    $scope.raiseSearchPages = function ()
    {
      $scope.searchActive = false;
      $scope.$broadcast("searchedPages", $scope.searchString);
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

    $scope.goToPage = function (item)
    {
      $scope.searchActive = false;
      if (item.IsPage)
      {
        $window.location.href = item.Link;
      }
      else
      {
        $scope.raiseChangedCategory(item.Link)
      }
    };

    $scope.onChange = function ()
    {
      if ($scope.searchString.length > 3)
      {
        $scope.searchActive = true;
        Search($scope.searchString);
      }
      else
      {
        $scope.searchActive = false;
      }
    };

    $scope.resetInput = function ()
    {
      $scope.searchString = "";
      $scope.searchActive = false;
    };

    function Search(value)
    {
      $http.get("/Content/SearchByString/" + value).then(function (response)
      {
        if (response.data.length !== 0)
        {
          $scope.searchResults = [];

          for (let i = 0; i < response.data.length; i++)
          {
            let obj = cast(response.data[i]);
            $scope.searchResults.push(obj);
          }
        }
      });
    }

    function cast(item)
    {
      return {
        Name: item.Name,
        Link: item.Link,
        IsPage: item.IsPage
      };
    }
  }
  ]);