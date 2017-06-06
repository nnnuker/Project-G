angular.module("GApp")
  .service("PagesService", ["$http", function ($http)
  {
    return {
      getAll: function ()
      {
        return $http.get("/Content/AllPages/");
      },

      getByCategoryId: function (id)
      {
        return $http.get("/Content/GetPageByCategoryId/" + id);
      },

      removeItem: function (id)
      {
        return $http.delete("/Content/RemovePage/" + id);
      }
    };
  }]);