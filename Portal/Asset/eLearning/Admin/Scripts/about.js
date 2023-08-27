var aboutID = 0;

jQuery(document).ready(function () {
    CKEDITOR.replace("Introduce");
    CKEDITOR.replace("Contact");

    getAboutData();

    jQuery("#ok").click(function (event) {
        event.preventDefault(); // cancel default behavior

        var formData = getFormData();
        var url = DOMAIN + "ElaHome/Update";

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


function getAboutData() {
    var url = "/Home/GetAboutData";

    jQuery.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: {},
        contentType: "application/json",

        success: function (data) {
            console.log(data);

            aboutID = data.ResultObj.ID;
            CKEDITOR.instances['Introduce'].setData(data.ResultObj.Introduce);
            CKEDITOR.instances['Contact'].setData(data.ResultObj.Contact);
            jQuery("#Email").val(data.ResultObj.Email);
            jQuery("#OpenTime").val(data.ResultObj.OpenTime);
            jQuery("#Address").val(data.ResultObj.Address);
            jQuery("#PhoneNumber").val(data.ResultObj.PhoneNumber);
        },
        error: function (data) {
            console.log(data);
        }
    });
}

function getFormData() {
    var data = {};

    data.ID = aboutID;
    data.Introduce = CKEDITOR.instances['Introduce'].getData();
    data.PhoneNumber = jQuery("#PhoneNumber").val();
    data.Email = jQuery("#Email").val();
    data.OpenTime = jQuery("#OpenTime").val();
    data.Address = jQuery("#Address").val();
    data.Contact = CKEDITOR.instances['Contact'].getData();

    return data;
}