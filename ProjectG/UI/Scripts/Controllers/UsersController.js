angular.module("GApp")
  .controller("UsersController", ["$scope", "UsersService", "RolesService", function ($scope, usersService, rolesService)
  {
    $scope.users = [];
    $scope.roles = [];
    $scope.newEmail = "";
    $scope.newFirstName = "";
    $scope.newLastName = "";
    $scope.newPassword = "";
    $scope.newRoleId = 1;

    init();

    $scope.addUser = function ()
    {
      let obj = {
        Id: 0,
        Email: $scope.newEmail,
        FirstName: $scope.newFirstName,
        LastName: $scope.newLastName,
        RoleId: $scope.newRoleId,
        Password: $scope.newPassword
      };

      usersService.addItem(obj).then(function (response)
      {
        if (response.data)
        {
          pushUser(response.data);
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
    };

    $scope.removeUser = function (index, item)
    {
      usersService.removeItem(item.Id).then(function (response)
      {
        if (response && response.status === 200)
        {
          $.notify("Deleted " + item.Id, { className: "success", globalPosition: "bottom right" });
          $scope.users.splice(index, 1);
        }
        else
        {
          $.notify("Deletion error", { className: "error", globalPosition: "bottom right" });
        }
      });
    };

    $scope.updateUser = function (index, item)
    {
      usersService.updateItem(item).then(function (data)
      {
        if (data && data.status === 200)
        {
          $("#updateButton-" + item.Id).notify("Updated", "success");
        }
        else
        {
          $("#updateButton-" + item.Id).notify("Updated", "warn");
        }
      });
    };

    function init()
    {
      rolesService.getAll().then(function (response)
      {
        if (response.data.length !== 0)
        {
          for (let i = 0; i < response.data.length; i++)
          {
            let obj = castRole(response.data[i]);
            $scope.roles.push(obj);
          }
        }
      });

      //$("#newRole").selectmenu();

      usersService.getAll().then(function (response)
      {
        if (response.data.length !== 0)
        {
          for (let i = 0; i < response.data.length; i++)
          {
            pushUser(response.data[i]);
          }
        }
      });
    }

    function pushUser(obj)
    {
      $scope.users.push(castUser(obj));
      $("#select-" + obj.Id).selectmenu();
    }

    function castRole(item)
    {
      return {
        Id: item.Id,
        Name: item.Name
      };
    }

    function castUser(item)
    {
      return {
        Id: item.Id,
        Email: item.Email,
        FirstName: item.FirstName,
        LastName: item.LastName,
        RoleId: item.RoleId,
        Password: ""
      };
    }
  }
  ]);