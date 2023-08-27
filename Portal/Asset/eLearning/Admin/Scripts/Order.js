;
let dataTable = {};
let courseSelectedID = -1;

function loadAllStudentByCouresID() {
    var urlGetPaging = DOMAIN + 'eLearningAdmin/Order/GetAllPaging';

    dataTable = $('#tbl_orders').DataTable({
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

             
                //if (json.data != null && json.data != undefined) {
                //    for (var i = 0, ien = json.data.length; i < ien; i++) {
                //        let edit = "<button class='btn-edit-emp' onclick=\"showEditForm('" + json.data[i].Id + "')\">" + "Edit" + "</button>";
                //        let deletecourse = "<button class='btn-edit-course' onclick=\"deleteCourseLesson('" + json.data[i].Id + "')\">" + "Delete" + "</button>";
                //        json.data[i].edit = edit + " " + deletecourse;

                //        let cssIcon = "";

                //        // link to display detail employee:
                //        if (json.data[i].Name == null) {
                //            json.data[i].Name = "";
                //        }
                //        let employeeDetail = `<a href='#' onclick="showDetailsForm('` + json.data[i].Id + `')">` + json.data[i].Name + `</a>`
                //        json.data[i].Name = employeeDetail;

                //        if (json.data[i].Description == null) {
                //            json.data[i].Description = "";
                //        }

                //        if (json.data[i].Image == null || json.data[i].Image == undefined || json.data[i].Image == '') {
                //            json.data[i].Image = '/Asset/img/no-img.jpg';
                //        }

                //        let videoPath = `<a href='` + json.data[i].VideoPath + `' onclick="javaScript('` + json.data[i].VideoPath + `')">` + json.data[i].VideoPath + `</a>`
                //        json.data[i].VideoPath = videoPath;
                //    }
                //}

                return json.data;
            }
        },
        "columns":
            [

                {
                    "title": "Email", "data": "Email", "orderable": false
                },
                {
                    "title": "Full Name", "data": "FullName", "orderable": false
                }

            ]
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


$(document).ready(function () {
    GetAllCourses(-1);
    loadAllStudentByCouresID();

    $("#ddlCourses").change(function () {
        var obj = this.value;
        console.log(obj);
        courseSelectedID = this.value;
        dataTable.draw();
    });

});