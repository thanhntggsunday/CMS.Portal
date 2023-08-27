var aboutID = 0;

jQuery(document).ready(function () {
    CKEDITOR.replace("Content");
    
    getFooterData();

    jQuery("#ok").click(function (event) {
        event.preventDefault(); // cancel default behavior

        var formData = getFormData();
        var url = DOMAIN + "ElaLayout/UpdateFooter";

        jQuery.ajax({
            url: url,
            method: "POST",
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (data) {
                console.log(data);
                hiddenProgressWaiting();
                // Update for check permission:
                if (data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "ElaError/Index";
                    window.location.replace(url);
                }
                else {
                    // reset_form_value();
                    window.location.reload();
                }


            },
            error: function (data) {
                hiddenProgressWaiting();
                console.log(data);
            }
        });

    });

    jQuery("#cancel").click(function () {
        window.location.reload();
    });
});


function getFooterData() {
    var url = "/ElaLayout/GetFooterFirstOrDefault";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: {},
        contentType: "application/json",

        success: function (data) {
            console.log(data);

            aboutID = data.ResultObj.ID;
            CKEDITOR.instances['Content'].setData(data.ResultObj.Content);
           
        },
        error: function (data) {
            console.log(data);
        }
    });
}

function getFormData() {
    var data = {};

    data.ID = aboutID;
    data.Content = CKEDITOR.instances['Content'].getData();
   
    return data;
}