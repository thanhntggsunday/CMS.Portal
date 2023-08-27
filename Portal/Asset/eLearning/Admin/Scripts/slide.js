
;
var dataTable = {};
var isClosed = false;

jQuery(document).ready(function () {

    jQuery("#btnAddSlide").click(function () {

        jQuery("#edit-flag").val("INSERT");
        jQuery("#btnSubmit").show();
        jQuery("#btnCancel").show();

        $("#slideForm").dialog({
            title: 'Add Slide',
            modal: true,
            draggable: false,
            resizable: true,
            show: 'blind',
            hide: 'blind',
            width: 600,
            height: 450,
            buttons: {
                "Save": function () {
                    isClosed = false;
                    $('#slideForm').submit();
                },

                "Cancel": function () {
                    isClosed = true;
                    jQuery(this).dialog("close");
                }
            }
        });

    });

    $('#slideForm').on('dialogclose', function (event) {
        reset_form_value();
    });

});

function save_slide() {
    showProgressWaiting();
    var is_edit = jQuery("#edit-flag").val();
    var frmData = get_form_value();

    if (is_edit == "EDIT") {
        var url = DOMAIN + "ElaSlide/Update";

        jQuery.ajax({
            url: url,
            method: "POST",
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify(frmData),
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
    } else {
        var url = DOMAIN + "ElaSlide/Create";

        if (frmData.ID == null || frmData.ID == undefined || frmData.ID == '') {
            frmData.ID = 0;
        }

        jQuery.ajax({
            url: url,
            method: "POST",
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify(frmData),
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
    var data = {};

    data.ID = jQuery("#id").val();
    data.Description = jQuery("#Description").val();
    data.Image = jQuery('#Picture').attr('src');
    data.Status = jQuery("#status")[0].checked;
    data.DisplayOrder = jQuery("#DisplayOrder").val();

    return data;
}

function reset_form_value() {

    jQuery("input").prop('disabled', false);
    jQuery("#id").val('');
    jQuery("#Description").val('');
    jQuery('#Picture').attr('src', '');
    jQuery("#status").prop("checked", false);
    jQuery("#DisplayOrder").val('');

    jQuery("#edit-flag").val("--");
    jQuery("span.error").remove();

}

function set_form_value(object_value) {

    jQuery("#id").val(object_value.ID);
    jQuery("#Description").val(object_value.Description);
    jQuery("#DisplayOrder").val(object_value.DisplayOrder);
    jQuery('#Picture').attr('src', object_value.Image);
    jQuery("#status").prop("checked", object_value.Status);
   
}

jQuery(document).ready(function () {
    jQuery("#slideForm").validate({
        rules: {
            
        },

        messages: {
           
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
            save_slide();
        }
    });
});



$(document).ready(function () {
    //---$('#reservation').val("");
    var urlGetPaging = DOMAIN + 'ElaSlide/GetAllPaging';

    dataTable = $('#tbl_Slides').DataTable({
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

                if (json.data != null && json.data != undefined) {
                    for (var i = 0, ien = json.data.length; i < ien; i++) {
                        let edit = "<button class='btn-edit-emp' onclick=\"showEditForm('" + json.data[i].ID + "')\">" + "Edit" + "</button>";
                        let deleteContent = "<button class='btn-edit-Content' onclick=\"deleteSlide('" + json.data[i].ID + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + deleteContent;

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

                        if (json.data[i].Image == null || json.data[i].Image == undefined || json.data[i].Image == '') {
                            json.data[i].Image = '/Asset/img/no-img.jpg';
                        }

                        let ImageUrl = `<img alt = '/Asset/img/no-img.jpg'  src="` + json.data[i].Image + `" height = '50px' width = '50px'  />`;
                        json.data[i].Image = ImageUrl;

                    }
                }

                return json.data;
            }
        },
        "columns":
            [

                {
                    "title": "DisplayOrder", "data": "DisplayOrder", "orderable": false
                },
                {

                    "title": "Image", "data": "Image", "orderable": false
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
    showProgressWaiting();

    jQuery("input").prop('disabled', false);
    jQuery("#edit-flag").val("EDIT");

    getSlideById(id);

    jQuery("#btnSubmit").show();
    jQuery("#btnCancel").show();
}

function showDetailsForm(id) {
    jQuery("#edit-flag").val("--");
    jQuery("input").prop('disabled', true);

    getSlideById(id);


    jQuery("#btnSubmit").hide();
    jQuery("#btnCancel").hide();
}

function deleteSlide(id) {
    console.log("deleting user");
    var r = confirm("Your are delete user selected ?");
    if (r == true) {
        var url = DOMAIN + "ElaSlide/Delete";

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

function getSlideById(id) {
    var url = DOMAIN + "ElaSlide/GetById";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: { Id: id },
        contentType: "application/json",

        success: function (data) {
            console.log(data);

            set_form_value(data.ResultObj);

            hiddenProgressWaiting();

            $("#slideForm").dialog({
                title: 'Edit Content',
                modal: true,
                draggable: false,
                resizable: true,
                show: 'blind',
                hide: 'blind',
                width: 900,
                height: 450,
                open: function () {
                 
                },
                buttons: {
                    "Save": function () {
                        isClosed = false;
                        $('#slideForm').submit();
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


