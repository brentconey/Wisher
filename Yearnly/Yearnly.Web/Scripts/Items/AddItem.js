$(function () {
    $("#AddItem").click(function (e) {
        e.preventDefault();
        var title = $("#itemTitle").val();
        var link = $("#itemLink").val();
        var desc = $("#itemDesc").val();

        $.ajax({
            type: "POST",
            url: "items/ajaxcreate",
            data: {
                Title: title,
                Link: link,
                Description: desc
            },
            success: function (result) {
                if (result == "True") {
                    alert("Item created");
                    $(document).trigger("click");
                } else {
                    alert("ERROR");
                    $(document).trigger("click");
                }
            },
            error: function (result) {
                alert("Error ocurred");
            }
        });
    });
});