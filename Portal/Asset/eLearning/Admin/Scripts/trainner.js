;
var dataTable = {};

jQuery(document).ready(function () {

    jQuery("#btnAddTrainner").click(function () {
        jQuery("#trainner_modal").modal();
        jQuery("#edit-flag").val("INSERT");
        jQuery("#btnSubmit").show();
        jQuery("#btnCancel").show();
    });

    jQuery("#trainner_modal").on("hidden.bs.modal", function () {
        // do something…
        reset_form_value();

        jQuery('.trainnerform span.error-inline').remove();
        jQuery('.trainnerform input').removeClass("is-invalid");
    });

});

function save_trainner() {
    var is_edit = jQuery("#edit-flag").val();
    if (is_edit == "EDIT") {
        var url = DOMAIN + "ElaTrainner/Update";
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
            }
        });
    } else {
        var url = DOMAIN + "ElaTrainner/Create";
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
            }
        });
    }
}

function get_form_value() {
    var data = {};

    data.Id = jQuery("#id").val();
    data.Name = jQuery("#name").val();
    data.Bio = jQuery("#bio").val();
    data.Avatar = jQuery("#avatar").val();

    //data.Status = jQuery("#status")[0].checked;

    return data;
}

function reset_form_value() {

    jQuery("input").prop('disabled', false);
    jQuery("#id").val('');
    jQuery("#name").val('');
    jQuery("#avatar").val('');
    jQuery("#bio").val('');
  
    //jQuery("#status").prop("checked", false);

    jQuery("#edit-flag").val("--");
}

function set_form_value(object_value) {

    jQuery("#id").val(object_value.Id);
    jQuery("#name").val(object_value.Name);
    jQuery("#bio").val(object_value.Bio);
    jQuery("#avatar").val(object_value.Avatar);
 
    //jQuery("#status").prop("checked", object_value.Status);

    jQuery("#edit-flag").val("EDIT");
}

jQuery(document).ready(function () {
    jQuery(".trainnerform").validate({
        rules: {
            full_name: {
                required: true,
            },

            email: {
                required: true,
            }
        },

        messages: {
            name: {
                required: "Please enter Name.",
            },

            description: {
                required: "Please enter Description.",
            }

        },
        errorElement: "span",
        errorClass: "error-inline",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            element.closest(".form-group").append(error);
        },
        highlight: function (element, errorClass, validClass) {
            jQuery(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            jQuery(element).removeClass("is-invalid");
        },
        //Submit Handler Function
        submitHandler: function (form) {
            save_trainner();
        },
    });
});



$(document).ready(function () {
    //---$('#reservation').val("");
    var urlGetPaging = DOMAIN + 'ElaTrainner/GetAllPaging';

    dataTable = $('#tbl_trainners').DataTable({
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
                        let edit = "<button class='btn-edit-emp' onclick=\"showEditForm('" + json.data[i].Id + "')\">" + "Edit" + "</button>";
                        let deletetrainner = "<button class='btn-edit-trainner' onclick=\"deleteTrainner('" + json.data[i].Id + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + deletetrainner;

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

                    "title": "Bio", "data": "Bio", "orderable": false
                },
                {

                    "title": "Avatar", "data": "Avatar", "orderable": false
                },
                {

                    "title": "Action", "data": "edit", "orderable": false
                }

            ]

    });

});


function showEditForm(id) {

    jQuery("#trainner_modal").modal();
    jQuery("#edit-flag").val("EDIT");

    getTrainnerById(id);

    jQuery("#btnSubmit").show();
    jQuery("#btnCancel").show();
}

function showDetailsForm(id) {

    jQuery("#trainner_modal").modal();
    jQuery("#edit-flag").val("EDIT");
    jQuery("input").prop('disabled', true);

    getTrainnerById(id);


    jQuery("#btnSubmit").hide();
    jQuery("#btnCancel").hide();
}

function deleteTrainner(id) {
    console.log("deleting user");
    var r = confirm("Your are delete user selected ?");
    if (r == true) {
        var url = DOMAIN + "ElaTrainner/Delete";

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

function getTrainnerById(id) {
    var url = DOMAIN + "ElaTrainner/GetById";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: { Id: id },
        contentType: "application/json",

        success: function (data) {
            console.log(data);
            jQuery("#trainner_modal").modal();
            set_form_value(data.ResultObj);
        },
        error: function (data) {
            console.log(data);
        }
    });
}

