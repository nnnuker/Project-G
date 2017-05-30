angular.module("GApp")
  .controller("LoginPanelController", ["$scope", "AccountService", function ($scope, accountService) {
    $scope.loginPanelActive = false;
    $scope.email = "";
    $scope.password = "";
    $scope.rememberMe = false;

    $scope.registerSubmit = function () {
      $scope.toggleLoginPanel();
      $scope.$parent.toggleLoader();
    }

    $scope.loginSubmit = function () {
      var model = {
        Email: $scope.email,
        Password: $scope.password,
        RememberMe: $scope.rememberMe
      };

      accountService.sendLogin(model);
    }

    $scope.toggleLoginPanel = function () {
      $scope.loginPanelActive = !$scope.loginPanelActive;
    }
  }
  ]);