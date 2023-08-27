var dataTable = {};
var isClosed = false;

jQuery(document).ready(function () {
    console.log("This is user.js");

    jQuery("#btnAddEmp").click(function () {
        var url = '/CmaUser/Create';

        // window.location.replace(url);
        // window.open(url, "_top")
        var width = screen.width;
        var height = screen.height;

        const pos = {
            x: (screen.width / 2) - (width / 2),
            y: (screen.height / 2) - (height / 2)
        };     

        const features = `width=${width} height=${height} left=${pos.x} top=${pos.y}`;

        // dialog-extend options
        var dialogExtendOptions = {
            "closable": true,
            "maximizable": true,
            "minimizable": true,
           
            "collapsable": true
        };
       
      
        $("#userForm").dialog({
            title: 'Add User',
            modal: true,
            draggable: false,
            resizable: true,
            show: 'blind',
            hide: 'blind',
            width: 'auto',
            height: 500,
            buttons: {
                "Save": function () {
                    isClosed = false;
                    $('#userForm').submit();
                },

                "Cancel": function () {
                    isClosed = true;
                    reset_form_value();
                    jQuery(this).dialog("close");
                }
            }
        }).dialogExtend(dialogExtendOptions);
    });

   
    $('#userForm').on('dialogclose', function (event) {
        // alert('closed');
        reset_form_value();
    });

});

function showEditForm(userId) {
    jQuery("#edit-flag").val("EDIT");
    getUserById(userId);
}


$(document).ready(function () {
    //---$('#reservation').val("");
    var urlGetPaging = DOMAIN + 'CmaUser/GetAllPaging';

    dataTable = $('#tbl_users').DataTable({
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
                var allEmpIdArrayOnPage = [];
                var empIdArraySelected = [];

                if (json.data != null && json.data != undefined) {

                    for (var i = 0, ien = json.data.length; i < ien; i++) {
                        let edit = "<button class='btn-edit-emp' onclick=\"showEditForm('" + json.data[i].Id + "')\">" + "Edit" + "</button>";
                        let editEmployeeRole = "<button class='btn-edit-erole' onclick=\"deleteUser('" + json.data[i].Id + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + editEmployeeRole;

                        let cssIcon = "";
                        //image
                        if (json.data[i].Avatar == "" || json.data[i].Avatar == undefined || json.data[i].Avatar == null) {
                            json.data[i].Avatar = "/Asset/img/no-img.jpg";
                        }
                        let ImageUrl = `<img alt = '/Asset/img/no-img.jpg'  src="` + json.data[i].Avatar + `" height = '50px' width = '50px' />`;
                        json.data[i].Avatar = ImageUrl;

                        // link to display detail employee:
                        if (json.data[i].FullName == null) {
                            json.data[i].FullName = "";
                        }
                        let employeeDetail = `<a href='#' onclick="showDetailsForm('` + json.data[i].Id + `')">` + json.data[i].FullName + `</a>`
                        json.data[i].FullName = employeeDetail;

                    }
                }

                return json.data;
            }
        },
        "columns":
            [
                {
                    "title": "Avatar", "data": "Avatar", "orderable": false
                },
               
                {
                    "title": "Full-name", "data": "FullName", "orderable": false
                },
                {

                    "title": "Email", "data": "Email", "orderable": false
                },
                {

                    "title": "Action", "data": "edit", "orderable": false
                }

            ]

    });

});




function deleteUser(userId) {
    console.log("deleting user");
    var r = confirm("Your are delete user selected ?");
    if (r == true) {
        var url = DOMAIN + "CmaUser/Delete";

        jQuery.ajax({
            url: url,
            type: "GET",
            dataType: "json",
            data: { Id: userId },
            contentType: "application/json",
            success: function (data) {
                console.log(data);
                // Update for check permission:
                if (data.Data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "CmaError/Index";
                    window.location.replace(url);
                }
                else {
                    window.location.reload();
                }
               
            },
            error: function (data) {
                console.log(data);
            }
        });
    } else {
        
    } 
}

function showDetailsForm(userId) {

    jQuery("#user_modal").modal();
    jQuery("#edit-flag").val("EDIT");
    jQuery("input").prop('disabled', true);

    getUserById(userId);

}


