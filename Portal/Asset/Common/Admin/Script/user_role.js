var dataTable = {};


$(document).ready(function () {
    //---$('#reservation').val("");
    var urlGetPaging = DOMAIN + 'CmaUserRole/GetAllUserRolesPaging';

    dataTable = $('#tbl_user_role').DataTable({
        "processing": true,
        "serverSide": true,
        "paging": true,
        "pageLength": 5,
        "lengthMenu": [[5, 10, 20, 50, 500], [5, 10, 20, 50, 500]],
        'searching': true,
        "ordering": false,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "ajax": {
            "url": urlGetPaging,
            "type": "GET",
            'data': function (data) {

            },
            "dataSrc": function (json) {
                json.draw = json.draw;
                json.recordsTotal = json.recordsTotal;
                json.recordsFiltered = json.recordsFiltered;

                // Reset allEmpIdArrayOnPage:
                allEmpIdArrayOnPage = [];
                empIdArraySelected = [];

                if (json.data != null && json.data != undefined) {
                    for (var i = 0, ien = json.data.length; i < ien; i++) {
                        let edit = "<button class='btn-edit' onclick=\"showEditForm('" + json.data[i].UserId + "')\">" + "Edit" + "</button>";
                        let editEmployeeRole = "<button class='btn-edit' onclick=\"deleteUserRole('" + json.data[i].UserId + "', '" + json.data[i].RoleName + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + editEmployeeRole;

                        let cssIcon = "";

                        // link to display detail employee:
                        if (json.data[i].UserName == null) {
                            json.data[i].UserName = "";
                        }
                        let email = `<a href='#' onclick="showDetailsForm('` + json.data[i].UserId + `')">` + json.data[i].Email + `</a>`
                        json.data[i].Email = email;

                    }
                }
                

                return json.data;
            }
        },
        "columns":
            [

                {
                    "title": "Email", "data": "Email", "orderable": false
                },
                {

                    "title": "UserName", "data": "UserName", "orderable": false
                },
                {

                    "title": "RoleName", "data": "RoleName", "orderable": false
                },
                {

                    "title": "Action", "data": "edit", "orderable": false
                }

            ]

    });

});


