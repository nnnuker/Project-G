angular.module("GApp")
  .controller("PagesController", ["$scope", "PagesService", function ($scope, pagesService)
  {
    $scope.pages = [];

    $scope.toggleFavourite = function (index, item)
    {

    };

    $scope.$on("changedCategory", function (event, data)
    {
      changedCategory(data);
    });

    $scope.removePage = function (index, item)
    {
      pagesService.removeItem(item.Id).then(function (response)
      {
        if (response && response.status === 200) {
          $.notify("Deleted " + item.Id, { className: "success", globalPosition: "bottom right" });
          $scope.pages.splice(index, 1);
        }
        else {
          $.notify("Deletion error", { className: "error", globalPosition: "bottom right" });
        }
      });
    };

    $scope.pagesCountText = function ()
    {
      let count = $scope.pages.length;
      return count > 0 ? "Найдено " + count : "По вашему запросу ничего не найдено";
    }

    init();

    function init()
    {
      $scope.toggleLoader(true);
      pagesService.getAll().then(setPages);
    }

    function changedCategory(id)
    {
      $scope.toggleLoader(true);
      pagesService.getByCategoryId(id).then(setPages);
    }

    function setPages(response)
    {
      $scope.pages = [];

      if (response.data.length !== 0)
      {
        for (var i = 0; i < response.data.length; i++)
        {
          var obj = cast(response.data[i]);
          $scope.pages.push(obj);
        }
      }
      $scope.toggleLoader(false);
    }

    function castDate(date)
    {
      var re = /-?\d+/;
      var m = re.exec(date);
      return new Date(parseInt(m[0]));
    }

    function cast(item)
    {
      return {
        Id: item.Id,
        CategoryId: item.CategoryId,
        PageDescription: item.PageDescription,
        SeoUrl: item.SeoUrl,
        Date: castDate(item.Date).toDateString(),
        Title: item.Title
      };
    }
  }
  ]);