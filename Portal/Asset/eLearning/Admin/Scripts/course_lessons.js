;
let dataTable = {};
let courseSelectedID = -1;
let isClosed = false;

jQuery(document).ready(function () {
    jQuery("#btn_add_course_lesson").click(function () {
        // jQuery("#course_modal").modal();
        jQuery("#edit-flag").val("INSERT");
        jQuery("#btnSubmit").show();
        jQuery("#btnCancel").show();

        $("#courseLessonForm").dialog({
            title: 'Add Course-Lesson',
            modal: true,
            draggable: false,
            resizable: true,
            show: 'blind',
            hide: 'blind',
            width: 600,
            height: 500,
            open: function () {},
            buttons: {
                "Save": function () {
                    isClosed = false;
                    $('#courseLessonForm').submit();
                },

                "Cancel": function () {
                    isClosed = true;
                    jQuery(this).dialog("close");
                }
            }
        });

    });

    $('#courseLessonForm').on('dialogclose', function (event) {
        reset_form_value();
    });
   
});

function save_course() {
    var is_edit = jQuery("#edit-flag").val();
    var frmData = get_form_value();

    if (is_edit == "EDIT") {
        var url = DOMAIN + "eLearningAdmin/CourseLesson/Update";

        jQuery.ajax({
            url: url,
            method: "POST",
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify(frmData),
            success: function (data) {
                console.log(data);
                // Update for check permission:
                if (data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "eLearningAdmin/Error/Index";
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
        var url = DOMAIN + "eLearningAdmin/CourseLesson/Create";

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
                // Update for check permission:
                if (data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "eLearningAdmin/Error/Index";
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
    var appcourse = {};

    appcourse.Id = jQuery("#id").val();
    appcourse.LessonName = jQuery("#Name").val();
    appcourse.CourseName = jQuery("#CourseName").val();
    appcourse.VideoPath = jQuery("#VideoPath").val();
    appcourse.SlidePath = jQuery("#SlidePath").val();

    appcourse.CourseId = $('#courseName').data("id");

    appcourse.Status = jQuery("#status")[0].checked;

    return appcourse;
}

function reset_form_value() {
    jQuery("#id").val('');
    jQuery("#Name").val('');
    jQuery("#CourseName").val('');
    jQuery("#VideoPath").val('');
    jQuery("#SlidePath").val('');
    // jQuery("#courseName").attr('data-id', '');
    jQuery("#edit-flag").val("--");

    jQuery("span.error").remove();
}

function set_form_value(object_value) {
    jQuery("#id").val(object_value.Id);
    jQuery("#CourseName").val(object_value.CourseName);
    jQuery("#Name").val(object_value.LessonName);
    jQuery("#VideoPath").val(object_value.VideoPath);
    jQuery("#SlidePath").val(object_value.SlidePath);
    jQuery("#status")[0].checked = object_value.Status;
    jQuery("#courseName").attr('data-id', object_value.CourseId);
}

jQuery(document).ready(function () {
    jQuery(".courseLessonForm").validate({
        rules: {
            Name: {
                required: true
            },
            courseName: {
                required: true
            }
           
        },

        messages: {
            Name: {
                required: "Please enter Name.",
            },
            courseName: {
                required: "Please enter Course-Name.",
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
            save_course();
        },
    });
});

function loadAllCoursLessonByCouresID() {
    var urlGetPaging = DOMAIN + 'eLearningAdmin/CourseLesson/GetAllPaging';

    dataTable = $('#tbl_course_lesson').DataTable({
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
                data.courseID = courseSelectedID;
            },
            "dataSrc": function (json) {
                json.draw = json.draw;
                json.recordsTotal = json.recordsTotal;
                json.recordsFiltered = json.recordsFiltered;
                
                if (json.data != null && json.data != undefined) {
                    for (var i = 0, ien = json.data.length; i < ien; i++) {
                        let edit = "<button class='btn-edit-emp' onclick=\"showEditForm('" + json.data[i].Id + "')\">" + "Edit" + "</button>";
                        let deletecourse = "<button class='btn-edit-course' onclick=\"deleteCourseLesson('" + json.data[i].Id + "')\">" + "Delete" + "</button>";
                        json.data[i].edit = edit + " " + deletecourse;

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

                        let videoPath = `<a href='` + json.data[i].VideoPath + `' onclick="javaScript('` + json.data[i].VideoPath + `')">` + json.data[i].VideoPath + `</a>`
                        json.data[i].VideoPath = videoPath;
                    }
                }

                return json.data;
            }
        },
        "columns":
            [

                {
                    "title": "Name", "data": "LessonName", "orderable": false
                },
                {
                    "title": "Video Url", "data": "VideoPath", "orderable": false
                },

                {
                    "title": "Action", "data": "edit", "orderable": false
                }

            ]
    });
}



function showEditForm(id) {
    // jQuery("#course_modal").modal();
    jQuery("input").prop('disabled', false);
    jQuery("#edit-flag").val("EDIT");

    getCourseLessonById(id);

    jQuery("#btnSubmit").show();
    jQuery("#btnCancel").show();
}

function showDetailsForm(id) {
    jQuery("#course_modal").modal();
    jQuery("#edit-flag").val("--");
    jQuery("input").prop('disabled', true);

    getCourseLessonById(id);

    jQuery("#btnSubmit").hide();
    jQuery("#btnCancel").hide();
}

function deleteCourse(id) {
    console.log("deleting user");
    var r = confirm("Your are delete user selected ?");
    if (r == true) {
        var url = DOMAIN + "eLearningAdmin/CourseLesson/Delete";

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
                    var url = DOMAIN + "eLearningAdmin/Error/Index";
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

function getCourseLessonById(id) {
    var url = DOMAIN + "eLearningAdmin/CourseLesson/GetById";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: { Id: id },
        contentType: "application/json",

        success: function (data) {
            console.log(data);
            // jQuery("#course_modal").modal();
            set_form_value(data.ResultObj);

            //GetAllCourses(data.ResultObj.CourseId);
            GetCourseForCreateOrEdit(data.ResultObj.CourseId);
        },
        error: function (data) {
            console.log(data);
        }
    });
}

function GetAllCourses(courseId) {
    var ddlCustomers = $("#ddlCourses");
    
    ddlCustomers.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "/eLearningAdmin/Course/GetAll",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response.data, function () {
                ddlCustomers.append($("<option></option>").val(this['Id']).html(this['CourseName']));
                
            });

            $.each(response.data, function (i) {
                var item = response.data[i];
                console.log(item);

                if (item.Id == courseId) {
                    $('#ddlCourses option[value=' + item.Id + ']').attr("selected", "selected");
                }
            });
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function GetCourseForCreateOrEdit(courseId) {
    var ddlCustomers = $("#puDdlCourses");
    ddlCustomers.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "/eLearningAdmin/Course/GetAll",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response.data, function () {
                ddlCustomers.append($("<option></option>").val(this['Id']).html(this['CourseName']));
            });

            $.each(response.data, function (i) {
                var item = response.data[i];
                console.log(item);

                if (item.Id == courseId) {
                    $('#puDdlCourses option[value=' + item.Id + ']').attr("selected", "selected");
                }
            });


            $("#courseLessonForm").dialog({
                title: 'Edit Course-Lesson',
                modal: true,
                draggable: false,
                resizable: true,
                show: 'blind',
                hide: 'blind',
                width: 600,
                height: 500,
                open: function () { },
                buttons: {
                    "Save": function () {
                        isClosed = false;
                        $('#courseLessonForm').submit();
                    },

                    "Cancel": function () {
                        isClosed = true;
                        jQuery(this).dialog("close");
                    }
                }
            });
        },
       
        error: function (response) {
            console.log(response.responseText);
        }
    });
}

function GetCourseById(courseId) {
    var ddlCustomers = $("#puDdlCourses");
    ddlCustomers.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "/eLearningAdmin/Course/GetAll",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(response.data, function () {
                ddlCustomers.append($("<option></option>").val(this['Id']).html(this['CourseName']));
            });

            $.each(response.data, function (i) {
                var item = response.data[i];
                console.log(item);

                if (item.Id == courseId) {
                    $('#puDdlCourses option[value=' + item.Id + ']').attr("selected", "selected");
                }
            });


        },

        error: function (response) {
            console.log(response.responseText);
        }
    });
}

$(document).ready(function () {
    GetAllCourses(-1);
    loadAllCoursLessonByCouresID();

    if (jQuery('#ddlCourses').find(":selected").val() === "0") {
        jQuery("#btn_add_course_lesson").hide();
    } else {
        jQuery("#btn_add_course_lesson").show();
    }

    $("#ddlCourses").change(function () {
        var obj = this.value;
        console.log(obj);
        courseSelectedID = this.value;
        dataTable.draw();

        var couseName = $('#ddlCourses').find(":selected").text();

        jQuery("#courseName").val(couseName);
        jQuery("#courseName").attr('data-id', obj);
        // jQuery("#txtCourseID").attr('data-id', obj);

        if (jQuery('#ddlCourses').find(":selected").val() === "0") {
            jQuery("#btn_add_course_lesson").hide();
        } else {
            jQuery("#btn_add_course_lesson").show();
        }
    });   

});