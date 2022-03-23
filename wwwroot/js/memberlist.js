//declare Values
var members;
$(document).ready(function () {
    loadDataTable();
});
//LoadData
function loadDataTable() {
    dataTable = $('#members').DataTable({
        "ajax": {
            "url": "/api/members",
            dataSrc:""
        },
        columns: [
            { data: "userName", "width": "10%" },
            { data: "gender", "width": "10%" },
            { data: "dateOfBirth", "width": "10%" },
            { data: "created", "width": "10%" },
            { data: "knownAs", "width": "10%" },
            { data: "lastActive", "width": "10%" },
            { data: "introduction", "width": "40%" },
            {
                "data": "photo",
                "render": function (data) {
                    var img = '/content/blog/' + data;
                    return '<img src="' + img + '" height="50px" width="50px" >';
                }
            },
            { data: "city", "width": "10%" },
            { data: "country", "width": "10%" },
            {
                data: "member",
                render: function (data, type, member) {
                    return "<a class='btn btn-outline-success' href='/members/edit/" + member.id + "'>Edit</a>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn btn-outline-danger js-delete' data-member-id=" + data + ">Delete</button>";
                }, "width": "40%"
            }
        ]
    });
    //Delete Members
    $("#members").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Are you sure you want to delete this message?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/members/" + button.attr("data-member-id"),
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                    }
                });
            }
        });
    });
}

