

function save_user() {
    var url = DOMAIN + "CmaAccount/Save";
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
                var url = DOMAIN + "CmaError/Index";
                window.location.replace(url);
            }
            else {
                if (data.ReturnStatus == true) {
                    reset_form_value();
                    // window.location.reload();
                    var url = '/CmaAccount';
                    window.location.replace(url);
                }
                else {
                    var message = "";
                    if (data.ReturnMessage != null) {
                        for (var i = 0; i < data.ReturnMessage.length; i++) {

                            message += " " + data.ReturnMessage[i];
                        }
                        displayError(message);
                    }
                    else {
                        displayError("Error");
                    }
                }
            }



        },
        error: function (data) {
            console.log(data);
            displayError("Error");
        }
    });
 
}

function get_form_value() {
    var appUser = {};

    appUser.Id = jQuery("#id").val();
    appUser.FullName = jQuery("#full_name").val();
    appUser.BirthDay = jQuery("#birth_day").val();
    appUser.Email = jQuery("#email").val();
    appUser.Address = jQuery("#address").val();
    appUser.UserName = jQuery("#user_name").val();
    appUser.PhoneNumber = jQuery("#phone_number").val();
    // appUser.Gender = jQuery("#gender").val();
    appUser.Gender = jQuery('input[name=gender]:checked').val()
    //appUser.Status = jQuery("#status").val();
    appUser.Status = jQuery('input[name=status]:checked').val()
    appUser.Address = jQuery("#address").val();
    appUser.Avatar = jQuery("#avatar").val();
    appUser.Password = jQuery("#password").val();
    appUser.Country = jQuery("#country").val();
    appUser.CountryRegionCode = jQuery("#country-region-code").val();

    return appUser;
}

function reset_form_value() {

    jQuery("input").prop('disabled', false);
    jQuery("#id").val('');
    jQuery("#full_name").val('');
    jQuery("#birth_day").val('');
    jQuery("#email").val('');
    jQuery("#address").val('');
    jQuery("#user_name").val('');
    jQuery("#phone_number").val('');
    jQuery("#gender").val('');
    jQuery("#status").val('');
    jQuery("#address").val('');
    jQuery("#avatar").val('');
    jQuery("#password").val('');
    jQuery("#edit-flag").val("--");
    jQuery("#country").val('');
    jQuery("#country-region-code").val('');
}

function set_form_value(object_value) {
    // jQuery("#cate_modal").modal();
    var dob = new Date(object_value.StrBirthDay);
    var strDob = convertDateTimeToString(dob);

    jQuery("#id").val(object_value.Id);
    jQuery("#full_name").val(object_value.FullName);
    jQuery("#birth_day").val(strDob);
    jQuery("#email").val(object_value.Email);
    jQuery("#address").val(object_value.Address);
    jQuery("#user_name").val(object_value.UserName);
    jQuery("#phone_number").val(object_value.PhoneNumber);
    //jQuery("#gender").val(object_value.Gender);
    jQuery("#status").val(object_value.Status);
    jQuery("#address").val(object_value.Address);
    jQuery("#avatar").val(object_value.Avatar);
    jQuery("#password").val(object_value.PasswordHash);
    jQuery("#edit-flag").val("EDIT");

    jQuery("#country").val(object_value.Country);
    jQuery("#country-region-code").val(object_value.CountryRegionCode);

    if (object_value.Gender == "MALE") {
        $("#male").prop("checked", true);
    }
    else {
        $("#female").prop("checked", true);
    }

    if (object_value.Status == true) {
        $("#active").prop("checked", true);
    }
    else {
        $("#in-active").prop("checked", true);
    }

    jQuery("#user-img").attr('src', object_value.Avatar);

}

jQuery(document).ready(function () {
    jQuery(".userform").validate({
        rules: {
            full_name: {
                required: true,
            },

            email: {
                required: true,
            },

            password: {
                required: true,
            },

            user_name: {
                required: true,
            }
        },

        messages: {
            email: {
                required: "Please enter email.",
            },

            full_name: {
                required: "Please enter full name.",
            },
            password: {
                required: "Please enter password.",
            },
            user_name: {
                required: "Please enter username.",
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
            save_user();
        },
    });
});






function displayError(message) {

    let color = "red";
    let isValid = true;
    let flag = jQuery("#edit-flag").val();

    document.getElementById("msg-error").innerHTML = message;
    document.getElementById("msg-error").style.color = color;

    isValid = false;

    return isValid;

}

jQuery(document).ready(function () {
    jQuery("#imageUploadForm").change(function () {
        var formData = new FormData();
        var totalFiles = document.getElementById("imageUploadForm").files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("imageUploadForm").files[i];
            formData.append("imageUploadForm", file);
        }
        var url = DOMAIN + 'AdminFile/Upload';
        jQuery.ajax({
            type: "POST",
            url: url,
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false
            //success: function(response) {
            //    alert('succes!!');
            //},
            //error: function(error) {
            //    alert("errror");
            //}
        }).done(function (data) {
            console.log('success');
            jQuery("#avatar").val(data.Data);
            jQuery("#user-img").attr('src', data.Data);
        }).fail(function (xhr, status, errorThrown) {
            console.log('fail');
        });
    });
});


jQuery(document).ready(function () {

    jQuery("#btnCancel").click(function () {
        var url = '/Admin/Account';

        window.location.replace(url);
    });


});

jQuery(document).ready(function () {
    var userId = jQuery('#user-id').data('value-id');

    getCurrentUser();

    function getCurrentUser() {
        var url = DOMAIN + "CmaAccount/GetCurrentUser";

        jQuery.ajax({
            url: url,
            type: "GET",
            dataType: "json",
            data: { Id: userId },
            contentType: "application/json",

            success: function (data) {
                console.log(data);
                // Update for check permission:
                if (data.Data == 'ACCESS_DENIED') {
                    var url = DOMAIN + "CmaError/Index";
                    window.location.replace(url);
                }
                else {
                    jQuery("#user_modal").modal();
                    set_form_value(data);
                }


            },
            error: function (data) {
                console.log(data);
            }
        });
    }


});