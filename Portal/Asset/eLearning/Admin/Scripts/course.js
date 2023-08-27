
;
var dataTable = {};
var isClosed = false;

jQuery(document).ready(function () {

    jQuery("#btnAddCourse").click(function () {

        jQuery("#edit-flag").val("INSERT");
        jQuery("#btnSubmit").show();
        jQuery("#btnCancel").show();

        GetAllCategories(-1, undefined);
    });

    $('#CourseForm').on('dialogclose', function (event) {
        reset_form_value();
    });

});

function save_Content() {
    showProgressWaiting();
    var is_edit = jQuery("#edit-flag").val();
    var frmData = get_form_value();

    if (is_edit == "EDIT") {
        var url = DOMAIN + "ElaCourse/Update";

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
        var url = DOMAIN + "ElaCourse/Create";

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
    var appContent = {};

    appContent.Id = jQuery("#id").val();
    appContent.CourseName = jQuery("#CourseName").val();
    appContent.Description = jQuery("#Description").val();
    appContent.Detail = CKEDITOR.instances['Detail'].getData();

    appContent.Image = jQuery('#Picture').attr('src');
    appContent.CategoryID = $('#ddlCategories').val();

    appContent.Status = jQuery("#status")[0].checked;

    return appContent;
}

function reset_form_value() {

    jQuery("input").prop('disabled', false);
    jQuery("#id").val('');
    jQuery("#CourseName").val('');
    jQuery("#Description").val('');
    jQuery("#Detail").val('');

    jQuery('#Picture').attr('src', '');
    jQuery("#status").prop("checked", false);

    // CKEDITOR.instances['Content'].setData('');

    jQuery("#edit-flag").val("--");
    jQuery("span.error").remove();

}

function set_form_value(object_value) {

    jQuery("#id").val(object_value.Id);
    jQuery("#CourseName").val(object_value.CourseName);
    jQuery("#Description").val(object_value.Description);
    jQuery("#Detail").val(object_value.Detail);

    jQuery('#Picture').attr('src', object_value.Image);
    jQuery("#status").prop("checked", object_value.Status);

    CKEDITOR.instances['Detail'].setData(object_value.Content);

}

jQuery(document).ready(function () {
    jQuery("#CourseForm").validate({
        rules: {
            CourseName: {
                required: true,
            }
        },

        messages: {
            CourseName: {
                required: "Please enter Course Name.",
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
            save_Content();
        }
    });
});



$(document).ready(function () {
    //---$('#reservation').val("");
    var urlGetPaging = DOMAIN + 'ElaCourse/GetAllPaging';

    dataTable = $('#tbl_Courses').DataTable({
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
                if (json == 'ACCESS_DENIED') {
                    var url = DOMAIN + "ElaError/Index";
                    window.location.replace(url);
                }

                json.draw = json.draw;
                json.recordsTotal = json.recordsTotal;
                json.recordsFiltered = json.recordsFiltered;

                // Reset allEmpIdArrayOnPage:

                if (json.data != null && json.data != undefined) {
                    for (var i = 0, ien = json.data.length; i < ien; i++) {
                        let edit = "<button class='btn-edit-emp' onclick=\"showEditForm('" + json.data[i].Id + "')\">" + "Edit" + "</button>";
                        let deleteContent = "<button class='btn-edit-Content' onclick=\"deleteCourse('" + json.data[i].Id + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + deleteContent;

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
                    "title": "Course Name", "data": "CourseName", "orderable": false
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

    getCourseById(id);

    jQuery("#btnSubmit").show();
    jQuery("#btnCancel").show();
}

function showDetailsForm(id) {
    jQuery("#edit-flag").val("--");
    jQuery("input").prop('disabled', true);

    getCourseById(id);


    jQuery("#btnSubmit").hide();
    jQuery("#btnCancel").hide();
}

function deleteCourse(id) {
    console.log("deleting user");
    var r = confirm("Your are delete user selected ?");
    if (r == true) {
        var url = DOMAIN + "ElaContent/Delete";

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

function getCourseById(id) {
    var url = DOMAIN + "ElaCourse/GetById";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: { Id: id },
        contentType: "application/json",

        success: function (data) {
            console.log(data);

            // set_form_value(data.ResultObj);

            GetAllCategories(data.ResultObj.CategoryID, data.ResultObj);
        },
        error: function (data) {
            console.log(data);
        }
    });
}


function GetAllCategories(categoryIdSelected, CourseData) {
    var ddlCustomers = $("#ddlCategories");
    ddlCustomers.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "/ElaCourseCategory/GetAll",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response.data, function () {
                ddlCustomers.append($("<option></option>").val(this['ID']).html(this['Name']));
            });

            $.each(response.data, function (i) {
                var item = response.data[i];
                console.log(item);

                if (item.ID == categoryIdSelected) {
                    $('#ddlCategories option[value=' + item.ID + ']').attr("selected", "selected");

                } else {
                    if (categoryIdSelected == -1 && i == 0) {
                        $('#ddlCategories option[value=' + 1 + ']').attr("selected", "selected");
                    }
                }
            });

            hiddenProgressWaiting();

            // dialog-extend options
            var dialogExtendOptions = {
                "closable": true,
                "maximizable": true,
                "minimizable": true,
                "collapsable": true
            };


            if (categoryIdSelected === -1) {
                $("#CourseForm").dialog({
                    title: 'Add Course',
                    modal: true,
                    draggable: false,
                    resizable: true,
                    show: 'blind',
                    hide: 'blind',
                    width: 600,
                    height: 400,
                    zIndex: 11000000,
                    open: function () {
                        // CKEDITOR.replace("Content");
                        var editor = CKEDITOR.instances["Detail"];
                        if (editor) { editor.destroy(true); }
                        CKEDITOR.replace("Detail");
                    },
                    buttons: {
                        "Save": function () {
                            isClosed = false;
                            $('#CourseForm').submit();
                        },

                        "Cancel": function () {
                            isClosed = true;
                            jQuery(this).dialog("close");
                        }
                    }
                }).dialogExtend(dialogExtendOptions);
            } else {
                $("#CourseForm").dialog({
                    title: 'Edit Course',
                    modal: true,
                    draggable: false,
                    resizable: true,
                    show: 'blind',
                    hide: 'blind',
                    width: 600,
                    height: 400,
                    open: function () {
                        var editor = CKEDITOR.instances["Detail"];
                        if (editor) { editor.destroy(true); }
                        CKEDITOR.replace("Detail");

                        if (CourseData !== undefined && CourseData !== null) {
                            set_form_value(CourseData);
                        }

                    },
                    buttons: {
                        "Save": function () {
                            isClosed = false;
                            $('#CourseForm').submit();
                        },

                        "Cancel": function () {
                            isClosed = true;
                            jQuery(this).dialog("close");
                        }
                    }
                }).dialogExtend(dialogExtendOptions);
            }
        },

        error: function (response) {
            hiddenProgressWaiting();
            console.log(response.responseText);
        }
    });
}
