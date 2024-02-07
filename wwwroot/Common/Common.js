﻿function callAjax(method, url, data, successCallbackFn, errorCallbackFn, bAsync = false, dataType = 'json') {

    showLoader(true);
    var forgeryToken = $("input[name='__RequestVerificationToken']").val();
    try {
        $.ajax({
            type: method,
            url: url,
            cache: false,
            headers: { "__RequestVerificationToken": forgeryToken },
            //contentType: 'application/json',
            data: data,
            dataType: dataType,
            async: bAsync,
            success: function (response) {

                if (response.success == true) {
                    MessageBox('Congratulations!', 'fa fa-check', response.responseText, 'green', 'btn btn-success','Okay');

                } else {
                    MessageBox('Error!', 'fa fa-times', response.responseText, 'red', 'btn btn-danger','Okay');
                }
                
            },
            complete: function (response) {
                showLoader(false);

            },
           
            error: function (exception) {
                MessageBox('Error!', 'fa fa-times', 'Something went wrong!', 'red', 'btn btn-danger', 'Okay');
            }
        });
    }
    catch (exception) {

    }
};
function getRootFolderPath() {
    try {
        var _location = document.location.toString();
        var applicationNameIndex = _location.indexOf('/', _location.indexOf('://') + 3);
        var applicationName = _location.substring(0, applicationNameIndex) + '/';
        var webFolderIndex = _location.indexOf('/', _location.indexOf(applicationName) + applicationName.length);
        var webFolderFullPath = _location.substring(0, webFolderIndex);
        var rootFolderName = webFolderFullPath.replace(applicationName, "");

        if (rootFolderName.toLocaleLowerCase() == "ARIS") {
            return "/" + rootFolderName;
        }
        else {
            return "";
        }

    }
    catch (err) {
        console.log("Error occured" + err);
    }
}
function showAlert(pData) {
    try {
        showLoader(false);
        //$('#modal-Loader').modal('hide');
        var isCancelButtonClicked = false;
        $('#alert-modal .modal-content').removeClass('alert-success');
        $('#alert-modal .modal-content').removeClass('alert-danger');
        $('#alert-modal .modal-content').removeClass('alert-warning');
        $('#alert-modal .modal-content').removeClass('alert-info');
        //$('#btnDialogYes').hide();
        //$('#btnDialogNo').hide();
        $('#btnDialogClose').show();
        //$('#btnDialogNo').html('<i class="fa fa-close"></i> Close');
        $('#alert-modal .modal-title').html(pData.title);
        $('#alert-modal .modal-body').html('<p style="font-size:16px">' + pData.message + '</p>');

        if (pData.type == 'SUCCESS') {
            $('#alert-modal .modal-content').addClass('alert-success');
        }
        else if (pData.type == 'ERROR') {
            $('#alert-modal .modal-content').addClass('alert-danger');
        }
        else if (pData.type == 'WARNING') {
            $('#alert-modal .modal-content').addClass('alert-warning');
        }
        else if (pData.type == 'INFO') {
            $('#alert-modal .modal-content').addClass('alert-info');
        }
        else if (pData.type == 'CONFIRM') {
            $('#alert-modal .modal-content').addClass('alert-warning');
            $('#btnDialogYes').show();
            $('#btnDialogNo').show();
            $('#btnDialogClose').hide();
        }
        
        $('#alert-modal').modal({
            backdrop: true,
            keyboard: true
           
        });
    }
    catch (err) {
        alert(err);
    }
}
function showAlert_OLD(pData) {
    try {
        var isCancelButtonClicked = false;
        $('#alert-modal').removeClass('modal-success');
        $('#alert-modal').removeClass('modal-danger');
        $('#alert-modal').removeClass('modal-warning');
        $('#alert-modal').removeClass('modal-info');
        $('#btnDialogYes').hide();
        $('#btnDialogNo').html('<i class="fa fa-close"></i> Close');
        $('#alert-modal .modal-title').html(pData.title);
        $('#alert-modal .modal-body').html('<p style="font-size:16px">' + pData.message + '</p>');
        if (pData.type == 'SUCCESS') {
            $('#alert-modal').addClass('modal-success');
        }
        else if (pData.type == 'ERROR') {
            $('#alert-modal').addClass('bg-danger');
        }
        else if (pData.type == 'WARNING') {
            $('#alert-modal').addClass('modal-warning');
        }
        else  if (pData.type == 'INFO') {
            $('#alert-modal').addClass('modal-info');
        }
        else if (pData.type == 'CONFIRM') {
            $('#alert-modal').addClass('modal-warning');
            $('#btnDialogYes').html('<i class="fa fa-close"></i> No');
            $('#btnDialogNo').show();
        }
        $('#btnDialogYes').unbind('click');
        $('#btnDialogNo').unbind('click');
        if (pData.action) {
            $("#btnDialogYes").bind('click', function () {
                $('.modal-backdrop').remove();
                pData.action();
            });
        }
        if (pData.cancel) {
            $("#btnDialogNo").bind('click', function () {
                $('.modal-backdrop').remove();
                isCancelButtonClicked = true;
                pData.cancel();
            });
        }
        $('#alert-modal').on('hidden.bs.modal', function () {
            $('.modal-backdrop').remove();
            if (pData.cancel && isCancelButtonClicked == false) {
                isCancelButtonClicked = false;
                pData.cancel();
            }
        });
        $('#alert-modal').modal({
            backdrop: true,
            keyboard: true
        });
    }
    catch (err) {
        alert(err);
    }
}

function bindDropDownList(ddl,type,url,dataType,value,name) {
    try {
        showLoader(true);
        var ddlName = $("#" + ddl);
        ddlName.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading...</option>');
        $.ajax({
            type: type,
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: dataType,
            success: function (response) {
                ddlName.empty().append('<option selected="selected" value="0">Select</option>');
                $.each(response, function () {
                   ddlName.append($("<option></option>").val(this['' + value + '']).html(this['' + name + '']));
                });
            },
            complete: function (response) {
                showLoader(false);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
    catch (err) {

    }
}
function showLoader(val) {
    try {
        if (val == true) {
            $("body").addClass("loading");
        }
        else {
            $("body").removeClass("loading");
        }
    }
    catch (err) {
        console.log(err);
    }
}


