var dataTable = {};
var isClosed = false;

jQuery(document).ready(function () {
    console.log("This is role.js");

    jQuery("#btnAddRole").click(function () {
        jQuery("#edit-flag").val("INSERT");
        jQuery("#btnSubmit").show();
        jQuery("#btnCancel").show();

        var dial = $("#roleForm").clone();
        
        $("#roleForm").dialog({
            title: 'Add Role',
            modal: true,
            buttons: {
                "Save": function () {
                    isClosed = false;
                    $('#roleForm').submit();
                },

                "Cancel": function () {
                    isClosed = true;
                    reset_form_value();
                    jQuery(this).dialog("close");
                }
            }
        });


    });

   
});

function save_user() {
    var is_edit = jQuery("#edit-flag").val();
    if (is_edit == "EDIT") {
        var url = DOMAIN + "CmaRole/Update";
        var user = get_form_value();

        jQuery.ajax({
            url: url,
            method: "POST",
            dataType: "json",
            data: user,
            success: function (data) {
                console.log(data);
                // Update for check permission:
                if (data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "CmaError/Index";
                    window.location.replace(url);
                }
                else {
                    reset_form_value();
                    window.location.reload();
                }

              
            },
            error: function (data) {
                console.log(data);
            }
        });
    } else {
        var url = DOMAIN + "CmaRole/Create";
        var user = get_form_value();

        if (user.Id == null || user.Id == undefined || user.Id == '') {
            user.Id = 0;
        }

        jQuery.ajax({
            url: url,
            method: "POST",
            dataType: "json",
            data: user,
            success: function (data) {
                console.log(data);
                // Update for check permission:
                if (data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "CmaError/Index";
                    window.location.replace(url);
                }
                else {
                    reset_form_value();
                    window.location.reload();
                }

              
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
}

function get_form_value() {
    var appRole = {};

    appRole.Id = jQuery("#id").val();
    appRole.Name = jQuery("#name").val();
    appRole.Description = jQuery("#description").val(); 

    return appRole;
}

function reset_form_value() {

    //jQuery("input").prop('disabled', false);
    jQuery("#id").val('');
    jQuery("#name").val('');
    jQuery("#description").val('');
  
    jQuery("#edit-flag").val("--");

    jQuery("span.error").remove();
}

function set_form_value(object_value) {
    jQuery("#id").val(object_value.Id);
    jQuery("#name").val(object_value.Name);
    jQuery("#description").val(object_value.Description);
  
    jQuery("#edit-flag").val("EDIT");
}

jQuery(document).ready(function () {
    jQuery("#roleForm").validate({
        rules: {
            name: {
                required: true
            },

            description: {
                required: true
            }
        },

        messages: {
            name: {
                required: "Please enter Name."
            },

            description: {
                required: "Please enter Description."
            }

        },
        errorElement: "span",
        errorClass: "error",
        errorPlacement: function (error, element) {
            if (isClosed === false) {
                error.addClass("invalid-feedback");
                element.closest(".form-group").append(error);
            }
           
        },
        highlight: function (element, errorClass, validClass) {
            if (isClosed === false) {
                jQuery(element).addClass("invalid-feedback");
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            jQuery(element).removeClass("invalid-feedback");
        },
        //Submit Handler Function
        submitHandler: function (form) {
            save_user();
        }
    });


    $('#roleForm').on('dialogclose', function (event) {
        // alert('closed');
        reset_form_value();
    });
});



$(document).ready(function () {
    //---$('#reservation').val("");
    var urlGetPaging = DOMAIN + 'CmaRole/GetAllPaging';

    dataTable = $('#tbl_roles').DataTable({
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
                        let deleteRole = "<button class='btn-edit-erole' onclick=\"deleteRole('" + json.data[i].Id + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + deleteRole;

                        let cssIcon = "";

                        // link to display detail employee:
                        if (json.data[i].Name == null) {
                            json.data[i].Name = "";
                        }
                        let employeeDetail = `<a href='#' onclick="showDetailsForm('` + json.data[i].Id + `')">` + json.data[i].Name + `</a>`
                        json.data[i].Name = employeeDetail;


                        if (json.data[i].Description == null) {
                            json.data[i].Description = "";
                        }

                    }
                }                

                return json.data;
            }
        },
        "columns":
            [

                {
                    "title": "Name", "data": "Name", "orderable": false
                },
                {

                    "title": "Description", "data": "Description", "orderable": false
                },
                {

                    "title": "Action", "data": "edit", "orderable": false
                }

            ]

    });

});


function showEditForm(id) {
    // jQuery("#role_modal").modal();
    jQuery("#edit-flag").val("EDIT");
    getRoleById(id);

    jQuery("#btnSubmit").show();
    jQuery("#btnCancel").show();
}

function deleteRole(id) {
    console.log("deleting user");
    var r = confirm("Your are delete user selected ?");
    if (r == true) {
        var url = DOMAIN + "Admin/Role/Delete";

        jQuery.ajax({
            url: url,
            type: "GET",
            dataType: "json",
            data: { Id: id },
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

function getRoleById(id) {
    showProgressWaiting();

    var url = DOMAIN + "CmaRole/GetById";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: { Id: id },
        contentType: "application/json",

        success: function (data) {
            console.log(data);
            // jQuery("#role_modal").modal();
            set_form_value(data);
            hiddenProgressWaiting();

            $("#roleForm").dialog({
                title: 'Edit Role',
                modal: true,
                buttons: {
                    "Save": function () {
                        isClosed = false;
                        $('#roleForm').submit();
                    },

                    "Cancel": function () {
                        isClosed = true;
                        reset_form_value();
                        jQuery(this).dialog("close");
                    }
                }
            });
        },
        error: function (data) {
            console.log(data);
            hiddenProgressWaiting();
        }
    });
}

