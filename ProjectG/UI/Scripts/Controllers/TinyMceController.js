angular.module("GApp")
  .controller("TinyMceController", ["$scope", "CategoriesService", function ($scope, categoriesService) {
    $scope.tinymceModel = null;
    $scope.tinymceOptions = {
      height: 500,
      menubar: false,
      plugins: [
        'advlist autolink lists link image charmap print preview anchor',
        'searchreplace visualblocks code fullscreen',
        'insertdatetime media table contextmenu paste code'
      ],
      toolbar: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
      content_css: [
        '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
        '//www.tinymce.com/css/codepen.min.css']
    };

    $scope.categories = [];

    init();

    var dialog,
      seoUrl = $("#SeoUrl"),
      category = $("#Category"),
      textArea = $("#Page"),
      allFields = $([]).add(seoUrl).add(category),
      tips = $(".validateTips");

    function updateTips(t) {
      tips
        .text(t)
        .addClass("ui-state-highlight");
      setTimeout(function () {
        tips.removeClass("ui-state-highlight", 1500);
      }, 500);
    }

    function checkLength(o, n, min, max) {
      if (!!o.val() && (o.val().length > max || o.val().length < min)) {
        o.addClass("ui-state-error");
        updateTips("Length of " + n + " must be between " +
          min + " and " + max + ".");
        return false;
      } else {
        return true;
      }
    }

    function addPage() {
      let valid = true;
      allFields.removeClass("ui-state-error");

      valid = valid && checkLength(seoUrl, "seoUrl", 3, 16);

      if (valid) {
        sendData();
        dialog.dialog("close");
      }
      return valid;
    }

    function sendData() {
      let seo = $("<input>")
        .attr("type", "hidden")
        .attr("name", "SeoUrl").val(seoUrl.val());
      let cat = $("<input>")
        .attr("type", "hidden")
        .attr("name", "CategoryId").val(category.val());

      $("#addPageForm").append($(seo));
      $("#addPageForm").append($(cat));

      $("#addPageForm").submit();
    }

    function init() {
      categoriesService.getAll().then(function (response) {
        if (response.data.length !== 0) {
          for (var i = 0; i < response.data.length; i++) {
            var obj = cast(response.data[i]);
            $scope.categories.push(obj);
          }

          dialog = $("#dialog-form").dialog({
            autoOpen: false,
            minHeight: 200,
            minWidth: 200,
            modal: true,
            buttons: {
              "Create": addPage,
              Cancel: function () {
                dialog.dialog("close");
              }
            },
            close: function () {
              allFields.removeClass("ui-state-error");
            },
            title: "Add page",
            closeText: "Close"
          });

          category.selectmenu();
        }
      });
    }

    function cast(item) {
      return {
        Id: item.Id,
        Name: item.Name
      };
    }

    $("#addPageButton").button().on("click", function () {
      dialog.dialog("open");
    });
  }
  ]);