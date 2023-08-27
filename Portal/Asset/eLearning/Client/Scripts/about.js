
jQuery(document).ready(function() {
    // CKEDITOR.replace("Content");

    getAboutData();
});


function getAboutData() {
    var url = "/ElcHome/GetAboutData";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: {},
        contentType: "application/json",

        success: function (data) {
            console.log(data);

            jQuery("#Content").val(data.ResultObj.Introduce);


        },
        error: function (data) {
            console.log(data);
        }
    });
}