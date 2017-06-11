angular.module("GApp")
  .service("UsersService", ["$http", function ($http)
  {
    return {
      getAll: function ()
      {
        return $http.get("/Admin/AllUsers");
      },

      addItem: function (item)
      {
        return $http.post("/Admin/AddUser/", item);
      },

      removeItem: function (id)
      {
        return $http.delete("/Admin/RemoveUser/" + id);
      },

      updateItem: function (item)
      {
        return $http.put("/Admin/UpdateUser/", item);
      }
    };
  }]);