angular.module("GApp")
  .service("AccountService", ["$http", function ($http) {
    return {
      sendLogin: function (model) {
        return $http.post("/Account/Login/", model);
      },

      getById: function (id) {
        return $http.get("/api/ToDos/" + id);
      },

      addItem: function (item) {
        return $http.post("/api/ToDos/", item);
      },

      removeItem: function (id) {
        return $http.delete("/api/ToDos/" + id);
      },

      updateItem: function (item) {
        return $http.put("/api/ToDos", item);
      }
    };
  }]);