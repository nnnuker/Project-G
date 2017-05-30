angular.module("GApp")
  .controller("CategoriesController", ["$scope", "CategoriesService", function ($scope, categoriesService) {
    $scope.categories = [];
    $scope.newCategory = "";
    $scope.newParentId = "";

    init();

    $scope.addCategory = function () {
      if ($scope.newCategory) {
        let obj = {
          Id : -1,
          ParentId : $scope.newParentId,
          Name : $scope.newCategory
        };

        categoriesService.addItem(obj).then(function (response) {
          if (response.data)
          {
            $scope.categories.push(cast(response.data));
          }
          else if (response.status === 200)
          {
            $.notify("Already exists", { className: "warn", globalPosition: "bottom right" });
          }
          else
          {
            $.notify("Error", { className: "error", globalPosition: "bottom right" });
          }
        });
      }
    };

    $scope.removeCategory = function (index, item) {
      categoriesService.removeItem(item.Id).then(function (response) {
        if (response && response.status === 200) {
          $.notify("Deleted " + item.Id, { className: "success", globalPosition: "bottom right" });
          $scope.categories.splice(index, 1);
        }
        else {
          $.notify("Deletion error", { className: "error", globalPosition: "bottom right" });
        }
      });
    };

    $scope.updateCategory = function (index, item) {
      categoriesService.updateItem(item).then(function (data) {
        if (data && data.status === 200) {
          $("#updateButton-" + item.Id).notify("Updated", "success");
        }
        else {
          $("#updateButton-" + item.Id).notify("Updated", "warn");
        }
      });
    };

    function init() {
      categoriesService.getAll().then(function (response) {
        if (response.data.length !== 0) {
          for (var i = 0; i < response.data.length; i++) {
            var obj = cast(response.data[i]);
            $scope.categories.push(obj);
          }
        }
      });
    };

    function cast(item) {
      return {
        Id: item.Id,
          ParentId: item.ParentId,
            Name: item.Name
      };
    }
  }
  ]);