angular.module("GApp")
  .service("PagesService", ["$http", function ($http) {
    return {
      getAll: function () {
        return $http.get("/Content/AllPages/");
      },

      removeItem: function (id) {
        return $http.delete("/Content/RemovePage/" + id);
      }
    };
  }]);