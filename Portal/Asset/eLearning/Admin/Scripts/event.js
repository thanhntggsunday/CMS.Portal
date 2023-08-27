
;
var dataTable = {};
var isClosed = false;

jQuery(document).ready(function () {

    jQuery("#btnAddEvent").click(function () {

        jQuery("#edit-flag").val("INSERT");
        jQuery("#btnSubmit").show();
        jQuery("#btnCancel").show();

        GetAllCategories(-1, undefined);
    });

    $('#EventForm').on('dialogclose', function (event) {
        reset_form_value();
    });

});

function save_Event() {
    showProgressWaiting();
    var is_edit = jQuery("#edit-flag").val();
    var frmData = get_form_value();

    if (is_edit == "EDIT") {
        var url = DOMAIN + "ElaEvent/Update";

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
        var url = DOMAIN + "ElaEvent/Create";

        if (frmData.Id == null || frmData.Id == undefined || frmData.Id == '') {
            frmData.Id = 0;
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
    var appEvent = {};

    appEvent.Id = jQuery("#id").val();
    appEvent.EventName = jQuery("#EventName").val();
    appEvent.Description = jQuery("#Description").val();
    appEvent.Content = CKEDITOR.instances['Content'].getData();
    appEvent.Price = jQuery("#Price").val();
    appEvent.PromotionPrice = jQuery("#PromotionPrice").val();
    appEvent.Image = jQuery('#Picture').attr('src');
    appEvent.CategoryId = $('#ddlCategories').val();

    appEvent.Status = jQuery("#status")[0].checked;

    return appEvent;
}

function reset_form_value() {

    jQuery("input").prop('disabled', false);
    jQuery("#id").val('');
    jQuery("#EventName").val('');
    jQuery("#Description").val('');
    jQuery("#Content").val('');
    jQuery("#Price").val('');
    jQuery("#PromotionPrice").val('');
    jQuery('#Picture').attr('src', '');
    jQuery("#status").prop("checked", false);

    // CKEDITOR.instances['Content'].setData('');

    jQuery("#edit-flag").val("--");
    jQuery("span.error").remove();

}

function set_form_value(object_value) {

    jQuery("#id").val(object_value.Id);
    jQuery("#EventName").val(object_value.EventName);
    jQuery("#Description").val(object_value.Description);
    jQuery("#Content").val(object_value.Content);
    jQuery("#Price").val(object_value.Price);
    jQuery("#PromotionPrice").val(object_value.PromotionPrice);
    jQuery('#Picture').attr('src', object_value.Image);
    jQuery("#status").prop("checked", object_value.Status);

    CKEDITOR.instances['Content'].setData(object_value.Content);

}

jQuery(document).ready(function () {
    jQuery("#EventForm").validate({
        rules: {
            EventName: {
                required: true,
            }
        },

        messages: {
            EventName: {
                required: "Please enter EventName.",
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
            save_Event();
        }
    });
});



$(document).ready(function () {
    //---$('#reservation').val("");
    var urlGetPaging = DOMAIN + 'ElaEvent/GetAllPaging';

    dataTable = $('#tbl_Events').DataTable({
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
                        let edit = "<button class='btn-edit-emp' onclick=\"showEditForm('" + json.data[i].Id + "')\">" + "Edit" + "</button>";
                        let deleteEvent = "<button class='btn-edit-Event' onclick=\"deleteEvent('" + json.data[i].Id + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + deleteEvent;

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
                    "title": "Event Name", "data": "EventName", "orderable": false
                },
                {

                    "title": "Image", "data": "Image", "orderable": false
                },
                {

                    "title": "Description", "data": "Description", "orderable": false
                },

                {

                    "title": "Trainer Name", "data": "TrainerName", "orderable": false
                },
                {

                    "title": "Price", "data": "Price", "orderable": false
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

    getEventById(id);

    jQuery("#btnSubmit").show();
    jQuery("#btnCancel").show();
}

function showDetailsForm(id) {
    jQuery("#edit-flag").val("--");
    jQuery("input").prop('disabled', true);

    getEventById(id);


    jQuery("#btnSubmit").hide();
    jQuery("#btnCancel").hide();
}

function deleteEvent(id) {
    console.log("deleting user");
    var r = confirm("Your are delete user selected ?");
    if (r == true) {
        var url = DOMAIN + "ElaEvent/Delete";

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

function getEventById(id) {
    var url = DOMAIN + "ElaEvent/GetById";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: { Id: id },
        contentType: "application/json",

        success: function (data) {
            console.log(data);

            // set_form_value(data.ResultObj);

            GetAllCategories(data.ResultObj.CategoryId, data.ResultObj);
        },
        error: function (data) {
            console.log(data);
        }
    });
}


function GetAllCategories(categoryIdSelected, EventData) {
    var ddlCustomers = $("#ddlCategories");
    ddlCustomers.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "/ElaEventCategory/GetAll",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response.data, function () {
                ddlCustomers.append($("<option></option>").val(this['Id']).html(this['Name']));
            });

            $.each(response.data, function (i) {
                var item = response.data[i];
                console.log(item);

                if (item.Id == categoryIdSelected) {
                    $('#ddlCategories option[value=' + item.Id + ']').attr("selected", "selected");

                }
            });

            hiddenProgressWaiting();

            if (categoryIdSelected === -1) {
                $("#EventForm").dialog({
                    title: 'Add Event',
                    modal: true,
                    draggable: false,
                    resizable: true,
                    show: 'blind',
                    hide: 'blind',
                    width: 900,
                    height: 600,
                    open: function () {
                        // CKEDITOR.replace("Content");
                        var editor = CKEDITOR.instances["Content"];
                        if (editor) { editor.destroy(true); }
                        CKEDITOR.replace("Content");
                    },
                    buttons: {
                        "Save": function () {
                            isClosed = false;
                            $('#EventForm').submit();
                        },

                        "Cancel": function () {
                            isClosed = true;
                            jQuery(this).dialog("close");
                        }
                    }
                });
            } else {
                $("#EventForm").dialog({
                    title: 'Edit Event',
                    modal: true,
                    draggable: false,
                    resizable: true,
                    show: 'blind',
                    hide: 'blind',
                    width: 900,
                    height: 600,
                    open: function () {
                        var editor = CKEDITOR.instances["Content"];
                        if (editor) { editor.destroy(true); }
                        CKEDITOR.replace("Content");

                        if (EventData !== undefined && EventData !== null) {
                            set_form_value(EventData);
                        }

                    },
                    buttons: {
                        "Save": function () {
                            isClosed = false;
                            $('#EventForm').submit();
                        },

                        "Cancel": function () {
                            isClosed = true;
                            jQuery(this).dialog("close");
                        }
                    }
                });
            }
        },

        error: function (response) {
            hiddenProgressWaiting();
            console.log(response.responseText);
        }
    });
}
