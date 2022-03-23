let mymessages;

$(document).ready(function () {
    mymessages = $("#mymessages").DataTable({
        "ajax": {
            "url": "/api/mymessages",
            dataSrc: ""
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "email", "width": "25%" },
            { "data": "message", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return "<button class='btn btn-danger js-delete' data-mymessage-id=" + data + ">Delete</button>";
                }
            }
        ]
    });
    $("#mymessages").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Are you sure you want to delete this message?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/mymessages/" + button.attr("data-mymessage-id"),
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                    }
                });
            }
        });
    });
});

