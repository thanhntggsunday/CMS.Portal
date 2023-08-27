var arrayLevelPermissions = [];
var comboTree1 = undefined;
var arrFunctionAction = [];
var strArrayFunctionActionId = "";
var strArrayFunctionActionIdSelected = "";
var isClosed = false;


function getRolesAll() {
    showProgressWaiting();
    var url = DOMAIN + "CmaPermission/GetRoleAll"
    $.ajax(
        {
            url: url,
            type: "GET",

            data: JSON.stringify({}),
            contentType: "application/json",
            //contentType: "application/x-www-form-urlencoded",
            dataType: 'json',

            success: function (result) {
                displayMembers(result.data);
                getFunctionsAll();
            },
            error: function (err) {
                console.log(err);
            }
        }
    );
}

function getFunctionsAll() {
    var url = DOMAIN + "CmaPermission/GetFunctionAll"
    $.ajax(
        {
            url: url,
            type: "GET",

            data: JSON.stringify({}),
            contentType: "application/json",
            //contentType: "application/x-www-form-urlencoded",
            dataType: 'json',

            success: function (result) {
                displayFunctionsDropdownlist(result.data);
                hiddenProgressWaiting();

                $("#formPermission").dialog({
                    title: 'Set permission',
                    modal: true,
                    draggable: false,
                    resizable: true,
                    show: 'blind',
                    hide: 'blind',
                    width: 600,
                    height: 400,
                    buttons: {
                        "Save": function () {
                            isClosed = false;
                            saveAssignPermission();
                        },

                        "Cancel": function () {
                            isClosed = true;
                            resetAssignRoleForm();
                            jQuery(this).dialog("close");
                        }
                    }
                });
            },
            error: function (err) {
                console.log(err);
                hiddenProgressWaiting();
            }
        }
    );
}

jQuery(document).ready(function ($) {
    $('#btnAssignPermission').click(function () {
        // Reset table:
        $('#level-permission-assign').empty();
        jQuery("#modal-title-id").html("Assign Role Permission");
        jQuery("#edit-flag").val("INSERT");

        getRolesAll();
        // getFunctionsAll();
      
    });
    // Assign role to employees:
    //$('#saveAssignPermission').click(function () {
      
    //});

    $('#formPermission').on('dialogclose', function (event) {
        // alert('closed');
        resetAssignRoleForm();
    });
});

function saveAssignPermission() {
    let arrRolesId = [];
    var functionId = jQuery('#ddlFunctions :selected').val();

    for (let index = 0; index < comboTreePluginSelectedItems.length; index++) {
        const element = comboTreePluginSelectedItems[index];

        arrRolesId.push(element.id);
    }
    let formIsValid = validateAssignRoleForm();
    if (formIsValid) {
        assignAssignPermissionToRole(arrRolesId, functionId, arrayLevelPermissions);
    }
}

function displayMembers(result) {
    let arrMembers = BuilArrayMemberForAssignRole(result);
    comboTree1 = $('#roles-id').comboTree({
        source: arrMembers,
        isMultiple: true,
        cascadeSelect: false
    });
}

function BuilArrayMemberForAssignRole(memberRows) {
    let arrayMembers = [];

    if (memberRows !== null) {
        for (let index = 0; index < memberRows.length; index++) {
            const element = memberRows[index];

            let mObject = {};
            mObject.id = element.Id;

            if (element.Name != null && element.Name != undefined) {
                mObject.title = element.Name;
            }
            else {
                mObject.title = element.Name;
            }

            arrayMembers.push(mObject);
        }
    }

    return arrayMembers;
}

function displayFunctionsDropdownlist(response) {
    var ddlFunctions = $("#ddlFunctions");
    ddlFunctions.empty().append('<option selected="selected" value="0">Please select</option>');
    $.each(response, function () {
        ddlFunctions.append($("<option></option>").val(this['ID']).html(this['ID']));
    });
}

function getAllLevelpemissionsForAssign() {
    var url = DOMAIN + "CmaPermission/GetLevePermissionAll";
    $.ajax(
        {
            url: url,
            type: "GET",
            data: JSON.stringify({}),
            contentType: "application/json",
            dataType: 'json',
            success: function (result) {
                let trOfTable = "";
                for (let index = 0; index < result.data.length; index++) {
                    const element = result.data[index];

                    let stringCheckbox = builHtmlOfCheckboxForAssign(element, arrayLevelPermissions);
                    stringCheckbox = "<td>" + stringCheckbox + "</td>"
                    trOfTable = trOfTable + stringCheckbox;

                    if ((index + 1) % 4 == 0) {
                        trOfTable = "<tr>" + trOfTable + "<tr/>";
                        $('#level-permission-assign').append(trOfTable);
                        trOfTable = "";
                    }
                    else {
                        if (index == result.data.length - 1) {
                            let div = (index + 1) % 4;
                            let del = 4 - div;

                            for (let j = 1; j <= del; j++) {
                                trOfTable = trOfTable + "<td></td>"
                            }

                            trOfTable = "<tr>" + trOfTable + "<tr/>";

                            $('#level-permission-assign').append(trOfTable);
                            trOfTable = "";
                        }
                        else {
                        }
                    }
                    // $('#level-permission-assign').append(stringCheckbox);
                }
            }, error: function (err) {
                console.log(err);
            }
        }
    );
}

function builHtmlOfCheckboxForAssign(item, arrayLevelPermissions) {
    var htmlCheckbox = "";

    if (arrayLevelPermissions.indexOf(item.ActionId) > -1) {
        htmlCheckbox = `<label style="font-weight:normal;" class="checkbox-inline new-select">
          <input  type="checkbox" class="checkbox-assign" id = "{0}" value="{1}" checked = "checked"> <span> {2}</span></label> &nbsp; &nbsp;`;
    }
    else {
        htmlCheckbox = `<label style="font-weight:normal;" class="checkbox-inline new-select">
          <input  type="checkbox" class="checkbox-assign" id = "{0}" value="{1}"> <span> {2}</span></label> &nbsp; &nbsp;`;
    }

    htmlCheckbox = htmlCheckbox.replace("{0}", item.ActionId);
    htmlCheckbox = htmlCheckbox.replace("{1}", item.ActionId);
    htmlCheckbox = htmlCheckbox.replace("{2}", item.ActionId);

    return htmlCheckbox;
}

function validateAssignRoleForm() {
    let spanTagError = `<span class="error">{0}</span> <br/>`;
    let messageRequiredSelectedRole = "Please selected role!";
    let messageForRequiredSelectedFunction = "Please selected function!";
    let color = "red";

    let isValid = true;
    let flag = jQuery("#edit-flag").val();

    if (comboTreePluginSelectedItems.length == 0 && flag == "INSERT") {
        document.getElementById("msg-role").innerHTML = messageRequiredSelectedRole;
        document.getElementById("msg-role").style.color = color;

        isValid = false;
    }
    else {
        document.getElementById("msg-role").innerHTML = "";
    }

    var functionId = jQuery('#ddlFunctions :selected').val();

    if (functionId == '0') {
        document.getElementById("msg-function").innerHTML = messageForRequiredSelectedFunction;
        document.getElementById("msg-function").style.color = color;

        isValid = false;
    }
    else {
        document.getElementById("msg-function").innerHTML = "";
    }

    return isValid;

    // return true;
}

function assignAssignPermissionToRole(arrRolesIds, functionId, arrLevelPermission) {
    var url = DOMAIN + "CmaPermission/AssignPermissionToRole";
    var strArrayRoleIds = arrRolesIds.join(';');
    var strArrLevelPermission = arrLevelPermission.join(';');

    console.log(arrFunctionAction);
    strArrayFunctionActionId = "";

    for (var i = 0; i < arrFunctionAction.length; i++) {
        var item = arrFunctionAction[i];
        strArrayFunctionActionId += ";" + item.FunctionActionId;
    }

    if (strArrayFunctionActionId != "") {
        strArrayFunctionActionId = strArrayFunctionActionId.substring(1, strArrayFunctionActionId.length);
    }

    for (var i = 0; i < arrayLevelPermissions.length; i++) {
        var item = arrayLevelPermissions[i];
        for (var j = 0; j < arrFunctionAction.length; j++) {
            var objItem = arrFunctionAction[j];

            if (objItem.ActionId == item) {
                strArrayFunctionActionIdSelected += ";" + objItem.FunctionActionId;
                break;
            }
        }

       
    }

    if (strArrayFunctionActionId != "") {
        strArrayFunctionActionIdSelected = strArrayFunctionActionIdSelected.substring(1, strArrayFunctionActionIdSelected.length);
    }

    jQuery.ajax(
        {
            url: url,
            type: "POST",

            data: JSON.stringify({
                FunctionId: functionId,
                StrArryRolesId: strArrayRoleIds,
                StrArrayLevePermissionsId: strArrLevelPermission,
                StrArrayFunctionActionId: strArrayFunctionActionId,
                StrArrayFunctionActionIdSelected: strArrayFunctionActionIdSelected
            }),
            contentType: "application/json",
            dataType: 'json',
            success: function (result) {
                console.log(result);

                if (result == 'ACCESS_DENIED') {
                    var url = DOMAIN + "CmaError/Index";
                    window.location.replace(url);
                }
                else {
                    if (result.ReturnStatus == true) {
                        // $('#modal-lg-assign-role').modal().hide();
                        resetAssignRoleForm();
                        window.location.reload(true);
                    }
                }

               
            },
            error: function (err) {
                console.log(err);
            }
        }
    );
}

function resetAssignRoleForm() {
    arrayLevelPermissions = [];
    comboTreePluginSelectedItems = [];
    document.getElementById("msg-function").innerHTML = "";
    document.getElementById("msg-role").innerHTML = "";
    document.getElementById("msg-levelper").innerHTML = "";

    let tagMembersId = `<input type="text" id="roles-id" class="col-md-12" placeholder="Select" />`;

    $("#div-members").empty();
    $("#div-members").append(tagMembersId);
    // Reset table:
    // $('#roles-assign').empty();
    jQuery("#edit-flag").val("--");
    jQuery("#closeAssignModal").show();
    jQuery("#saveAssignPermission").show();
}

$(document).ready(function () {
    getRolesWhenSelectCheckboxForAssign();

    $("#ddlFunctions").change(function () {
        var functionId = jQuery('#ddlFunctions :selected').val();

        if (functionId == '0') {
            document.getElementById("msg-function").innerHTML = messageForRequiredSelectedFunction;
            document.getElementById("msg-function").style.color = color;

            isValid = false;
        }
        else {
            document.getElementById("msg-function").innerHTML = "";
        }
    });
});

function getRolesWhenSelectCheckboxForAssign() {
    $(document).on("change", "input[class='checkbox-assign']", function () {
        // alert("FECK");

        if (this.checked) {
            console.log("checked level permission: " + this.value);
            arrayLevelPermissions = addOrRemoveElementInArrayRoles(arrayLevelPermissions, this.value, true);
        }
        else {
            console.log("unchecked level permission: " + this.value);
            arrayLevelPermissions = addOrRemoveElementInArrayRoles(arrayLevelPermissions, this.value, false);
        }

        let strRoles = arrayLevelPermissions.join(",");

        //$("#roles").val(strRoles);
        //console.log($("#roles").val());

        if (arrayLevelPermissions.length > 0) {
            document.getElementById("msg-levelper").innerHTML = "";
        }
    });
}

// levelPermissionId
function addOrRemoveElementInArrayRoles(arrFunctionActionId, levelPermissionId, isAdd) {
    var isInclude = false;

    for (let index = 0; index < arrFunctionActionId.length; index++) {
        const element = arrFunctionActionId[index];

        if (element == levelPermissionId && isAdd == false) {
            arrFunctionActionId.splice(index, 1);
            index--;
        }
        if (element == levelPermissionId && isAdd == true) {
            isInclude = true;
        }
    }

    if (isInclude == false && isAdd == true) {
        arrFunctionActionId.push(levelPermissionId);
    }

    return arrFunctionActionId;
}

function getActionsByFunctionId(functionId) {
    var url = DOMAIN + "CmaPermission/GetActionsByFunctionId";
    jQuery.ajax(
        {
            url: url,
            type: "GET",
            data: { functionId: functionId },
            contentType: "application/json",
            dataType: 'json',
            success: function (result) {
                let trOfTable = "";
                arrFunctionAction = [];

                for (let index = 0; index < result.data.length; index++) {
                    const element = result.data[index];
                    arrFunctionAction.push(element);

                    let stringCheckbox = builHtmlOfCheckboxForAssign(element, arrayLevelPermissions);
                    stringCheckbox = "<td>" + stringCheckbox + "</td>"
                    trOfTable = trOfTable + stringCheckbox;

                    if ((index + 1) % 4 == 0) {
                        trOfTable = "<tr>" + trOfTable + "<tr/>";
                        $('#level-permission-assign').append(trOfTable);
                        trOfTable = "";
                    }
                    else {
                        if (index == result.data.length - 1) {
                            let div = (index + 1) % 4;
                            let del = 4 - div;

                            for (let j = 1; j <= del; j++) {
                                trOfTable = trOfTable + "<td></td>"
                            }

                            trOfTable = "<tr>" + trOfTable + "<tr/>";

                            $('#level-permission-assign').append(trOfTable);
                            trOfTable = "";
                        }
                        else {
                        }
                    }
                    // $('#level-permission-assign').append(stringCheckbox);
                }

                hiddenProgressWaiting();
            }, error: function (err) {
                console.log(err);
                hiddenProgressWaiting();
            }
        }
    );
}

jQuery(document).ready(function () {
    //jQuery('#modal-lg-assign-role').on('hidden.bs.modal', function () {
    //    // do something…
    //    console.log('cloase assign role modal');
    //    resetAssignRoleForm();
    //});

    jQuery("#ddlFunctions").change(function () {
        var id = this.value;
        console.log(id);
        $('#level-permission-assign').empty();
        showProgressWaiting();
        getActionsByFunctionId(id);
    });
});