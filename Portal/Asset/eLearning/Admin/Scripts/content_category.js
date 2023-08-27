;
var dataTable = {};
var isClosed = false;

jQuery(document).ready(function () {
    jQuery("#btnAddCategory").click(function () {
        jQuery("#edit-flag").val("INSERT");
        jQuery("#btnSubmit").show();
        jQuery("#btnCancel").show();

        $("#categoryForm").dialog({
            title: 'Add Category',
            modal: true,
            draggable: false,
            resizable: true,
            show: 'blind',
            hide: 'blind',
            width: 600,
            height: 500,
            buttons: {
                "Save": function () {
                    isClosed = false;
                    $('#categoryForm').submit();
                },

                "Cancel": function () {
                    isClosed = true;
                    jQuery(this).dialog("close");
                }
            }
        });
    });

    $('#categoryForm').on('dialogclose', function (event) {
        reset_form_value();
    });
});

function save_course_category() {
    showProgressWaiting();

    var is_edit = jQuery("#edit-flag").val();
    if (is_edit == "EDIT") {
        var url = DOMAIN + "ElaContentCategory/Update";
        var user = get_form_value();

        jQuery.ajax({
            url: url,
            method: "POST",
            dataType: "json",
            data: user,
            success: function (data) {
                console.log(data);
                hiddenProgressWaiting();

                // Update for check permission:
                if (data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "ElaError/Index";
                    window.location.replace(url);
                }
                else {
                    reset_form_value();
                    window.location.reload();
                }
            },
            error: function (data) {
                console.log(data);
                hiddenProgressWaiting();
            }
        });
    } else {
        var url = DOMAIN + "ElaContentCategory/Create";
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
                hiddenProgressWaiting();
                // Update for check permission:
                if (data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "ElaError/Index";
                    window.location.replace(url);
                }
                else {
                    reset_form_value();
                    window.location.reload();
                }
            },
            error: function (data) {
                hiddenProgressWaiting();
                console.log(data);
            }
        });
    }
}

function get_form_value() {
    var appcategory = {};

    appcategory.ID = jQuery("#id").val();
    appcategory.Name = jQuery("#name").val();

    appcategory.Status = jQuery("#status")[0].checked;

    return appcategory;
}

function reset_form_value() {
    jQuery("input").prop('disabled', false);
    jQuery("#id").val('');
    jQuery("#name").val('');

    jQuery("#status").prop("checked", false);

    jQuery("#edit-flag").val("--");

    jQuery("span.error").remove();
}

function set_form_value(object_value) {
    jQuery("#id").val(object_value.ID);
    jQuery("#name").val(object_value.Name);

    jQuery("#status").prop("checked", object_value.Status);

    jQuery("#edit-flag").val("EDIT");
}

jQuery(document).ready(function () {
    jQuery(".categoryForm").validate({
        rules: {
            name: {
                required: true
            }
        },

        messages: {
            name: {
                required: "Please enter Name.",
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
                jQuery(element).addClass("is-invalid");
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            jQuery(element).removeClass("is-invalid");
        },
        //Submit Handler Function
        submitHandler: function (form) {
            save_course_category();
        }
    });
});

$(document).ready(function () {
    var urlGetPaging = DOMAIN + 'ElaContentCategory/GetAllPaging';

    dataTable = $('#tbl_categories').DataTable({
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

                if (json.data != null && json.data != undefined) {
                    for (var i = 0, ien = json.data.length; i < ien; i++) {
                        let edit = "<button class='btn-edit-emp' onclick=\"showEditForm('" + json.data[i].ID + "')\">" + "Edit" + "</button>";
                        let deletecategory = "<button class='btn-edit-ecategory' onclick=\"deletecategory('" + json.data[i].ID + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + deletecategory;

                        let cssIcon = "";

                        // link to display detail employee:
                        if (json.data[i].Name == null) {
                            json.data[i].Name = "";
                        }
                        let employeeDetail = `<a href='#' onclick="showDetailsForm('` + json.data[i].ID + `')">` + json.data[i].Name + `</a>`
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
                    "title": "Status", "data": "Status", "orderable": false
                },

                {
                    "title": "Action", "data": "edit", "orderable": false
                }

            ]
    });
});

function showEditForm(id) {
    showProgressWaiting();

    jQuery("#edit-flag").val("EDIT");

    getCategoryById(id);

    jQuery("#btnSubmit").show();
    jQuery("#btnCancel").show();
}

function showDetailsForm(id) {
    jQuery("#edit-flag").val("EDIT");
    jQuery("input").prop('disabled', true);

    getCategoryById(id);

    jQuery("#btnSubmit").hide();
    jQuery("#btnCancel").hide();
}

function deletecategory(id) {
    console.log("deleting user");
    var r = confirm("Your are delete user selected ?");
    if (r == true) {
        var url = DOMAIN + "ElaContentCategory/Delete";

        jQuery.ajax({
            url: url,
            type: "GET",
            dataType: "json",
            data: { ID: id },
            contentType: "application/json",
            success: function (data) {
                console.log(data);
                // Update for check permission:
                if (data.Data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "ElaError/Index";
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

function getCategoryById(id) {
    var url = DOMAIN + "ElaContentCategory/GetById";
    showProgressWaiting();

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: { ID: id },
        contentType: "application/json",

        success: function (data) {
            console.log(data);
            set_form_value(data.ResultObj);
            hiddenProgressWaiting();

            $("#categoryForm").dialog({
                title: 'Edit Category',
                modal: true,
                draggable: false,
                resizable: true,
                show: 'blind',
                hide: 'blind',
                width: 600,
                height: 500,
                buttons: {
                    "Save": function () {
                        isClosed = false;
                        $('#categoryForm').submit();
                    },

                    "Cancel": function () {
                        isClosed = true;
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