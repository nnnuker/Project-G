angular.module("GApp")
  .service("CategoriesService", ["$http", function ($http) {
    return {
      getAll: function () {
        return $http.get("/Content/AllCategories");
      },

      addItem: function (item) {
        return $http.post("/Content/AddCategory/", item);
      },

      removeItem: function (id) {
        return $http.delete("/Content/RemoveCategory/" + id);
      },

      updateItem: function (item) {
        return $http.put("/Content/UpdateCategory/", item);
      }
    };
  }]);