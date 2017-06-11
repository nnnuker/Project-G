angular.module("GApp")
  .controller("NavigatorController", ["$scope", "CategoriesService", "PagesService", function ($scope, categoriesService, pagesService)
  {
    $scope.categories = [];
    $scope.breadCrumbs = [];
    $scope.letterLimit = 17;
    $scope.pages = [];

    $scope.changedCategory = function (item)
    {
      let index = $scope.breadCrumbs.indexOf(item);

      if (index !== -1)
      {
        $scope.breadCrumbs.splice(index, 100);
        $scope.getByParentId(item);
      }
    };

    $scope.goToCategory = function (item)
    {
      addBreadCrumb(item);

      $scope.toggleRubricsActive(false);

      $scope.raiseChangedCategory(item.Id);
    };

    $scope.getByParentId = function (item)
    {
      addBreadCrumb(item);

      categoriesService.getByParentId(item.Id).then(function (response)
      {
        if (response.status !== 200)
        {
          $.notify("Error occured", { className: "error", globalPosition: "bottom right" });
          return;
        }

        if (response.data.length !== 0)
        {
          $scope.categories = [];

          for (let i = 0; i < response.data.length; i++)
          {
            let obj = cast(response.data[i]);
            $scope.categories.push(obj);
          }
        }

        pagesService.getByCategoryId(item.Id).then(setPages);        
      });
    };

    init();

    function init()
    {
      $scope.getByParentId({
        Id: 2,
        Name: "Категории",
        HasChilds: true,
        ParentId: 1,
        ChildCount: "All",
        IsPage: false
      });
    }

    function setPages(response)
    {
      $scope.pages = [];

      if (response.data.length !== 0)
      {
        for (var i = 0; i < response.data.length; i++)
        {
          var obj = castPage(response.data[i]);
          $scope.pages.push(obj);

          $scope.categories.push({
            Id: obj.Id,
            ParentId: 0,
            Name: obj.Title,
            HasChilds: false,
            ChildCount: 0,
            IsPage: true,
            Page: obj
          });
        }
      }
    }

    function addBreadCrumb(item)
    {
      let last = $scope.breadCrumbs.length - 1;

      if (last < 0)
      {
        $scope.breadCrumbs.push(item);
        return;
      }

      if ($scope.breadCrumbs[last].HasChilds)
      {
        $scope.breadCrumbs.push(item);
      }
      else
      {
        $scope.breadCrumbs.splice(last, 1);
        $scope.breadCrumbs.push(item);
      }
    }

    function cast(item)
    {
      return {
        Id: item.Id,
        ParentId: item.ParentId,
        Name: item.Name,
        HasChilds: item.HasChilds,
        ChildCount: item.ChildCount,
        IsPage: false,
        Page: null
      };
    }

    function castDate(date)
    {
      var re = /-?\d+/;
      var m = re.exec(date);
      return new Date(parseInt(m[0]));
    }

    function castPage(item)
    {
      return {
        Id: item.Id,
        SeoUrl: item.SeoUrl,
        Date: castDate(item.Date).toDateString(),
        Title: item.Title
      };
    }
  }
  ]);