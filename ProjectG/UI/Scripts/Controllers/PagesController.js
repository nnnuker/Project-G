angular.module("GApp")
    .controller("PagesController", ["$scope", "PagesService", function($scope, pagesService) {
        $scope.pages = [];

        init();

        $scope.toggleFavourite = function(index, item) {

        }

        $scope.removePage = function(index, item) {
            pagesService.removeItem(item.Id).then(function(response) {
                if (response && response.status === 200) {
                    $.notify("Deleted " + item.Id, { className: "success", globalPosition: "bottom right" });
                    $scope.pages.splice(index, 1);
                }
                else {
                    $.notify("Deletion error", { className: "error", globalPosition: "bottom right" });
                }
            });
        };

        function init() {
            pagesService.getAll().then(function(response) {
                if (response.data.length !== 0) {
                    for (var i = 0; i < response.data.length; i++) {
                        var obj = cast(response.data[i]);
                        $scope.pages.push(obj);
                    }
                }
            });
        };

        function castDate(date) {
            var re = /-?\d+/;
            var m = re.exec(date);
            return new Date(parseInt(m[0]));
        }

        function cast(item) {
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