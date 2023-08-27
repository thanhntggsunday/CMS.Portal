jQuery(document).ready(function () {
    // CKEDITOR.replace("Content");

    getFooterData();
});


function getFooterData() {
    var url = "/Home/GetAboutData";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: {},
        contentType: "application/json",

        success: function (data) {
            // console.log(data);

            jQuery("li#phone").text( 'Phone: ' + data.ResultObj.PhoneNumber);
            jQuery("li#address").text('Address: ' + data.ResultObj.Address);
            jQuery("li#email").text('Email: ' + data.ResultObj.Email);
        },
        error: function (data) {
            console.log(data);
        }
    });
}