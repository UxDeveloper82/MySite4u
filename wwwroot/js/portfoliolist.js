var table;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    table = $('#myTable').DataTable({
        "ajax": {
            "url": "/api/Portfolio",
            dataSrc: ""
        },
        columns: [
            { "data": "name" },
            { "data": "type" },
            { "data": "details" },
            { "data": "language" },
            { "data": "codeLink"},
            { "data": "link" },
            {
                "data": "portfolioPhoto",
                "render": function (data) {
                    var img = '/content/blog/' + data;
                    return '<img src="' + img + '" height="50px" width="50px" >';
                }
            },
            {
                "data": "edit",
                "render": function (data, type, port) {
                    return "<a class='btn btn-warning' href='/Admin/MyPortfolio/edit/" + port.id + "'> Edit</a>";
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return "<button class='btn btn-danger js-delete' data-port-id=" + data + ">Delete</button>";
                }
            }
        ], "pageLength": 10
    });
    $("#myTable").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Are you sure you want to delete this port?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/portfolio/" + button.attr("data-port-id"),
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                })
            }
        });
    });
}