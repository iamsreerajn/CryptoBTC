
    var btnText;
$(document).ready(function () {
    ClearFields();
        
        $("#btnLastUpdated").mouseenter(function () {
            $("#btnLastUpdated").text("Click here to update");
        });

    $("#btnLastUpdated").mouseleave(function () {
        $("#btnLastUpdated").text(btnText);
        });
    $("#txtAvg").css("color", "green").css("border-radius", "20px") ;

    var date = new Date();
        $("#fromDate").datetimepicker({ minDate: moment().add(-365, 'days'),maxDate:moment(), format: 'L'}); //maxDate: "+1M +15D" });
        $("#toDate").datetimepicker({ minDate: moment().add(-365, 'days'), maxDate:moment(), format: 'L' }); 
        GetLast31DaysOfCoins();
            
        });
    var table;
    function ClearFields(){
        $("#dpFromDate").val('');
    $("#dpToDate").val('');
    $("#txtAvg").val('$ Average Close in USD').css("color","gray");
    $('#dpFromDate').css('border-color', '');
    $('#dpToDate').css('border-color', '');
    }
    function UpdateCoinDetails(){
        if ($("#btnLastUpdated").text() == "Click here to update") {
        $.ajax({
            type: "GET",
            url: "/Crypto/UpdateTimer",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    populateCoins(response._candleBTC);

                    var datetime = new Date(response.lu);
                    btnText = datetime;
                    $("#btnLastUpdated").text(btnText);
                   
                } else {
                    console.log("Something went wrong");
                }
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
        }
    else
        {

    }
    }
    function GetLast31DaysOfCoins() {
        showLoader(true);
    $.ajax({
        type: "GET",
    url: "/Crypto/GetCoins",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {
                    if (response != null) {
        populateCoins(response._candleBTC);

    var datetime = new Date(response.lu);
    btnText = datetime;
    $("#btnLastUpdated").text(btnText);
                        
                    } else {
        console.log("Something went wrong");
                    }
                },
    complete:function(response)
    {
        showLoader(false);
                },
    failure: function (response) {
        console.log(response.responseText);
                },
    error: function (response) {
        console.log(response.responseText);
                }
            });
}
    function populateCoins(response) {
        table = $('#tblCryptoCoins').DataTable(
            {
                response: true,
                bLengthChange: true,
                bFilter: true,
                bSort: true,
                bPaginate: false,//
                data: response,
                //scrollY: true,
                select: true,
                scrollX: false,
                sScrollXInner: "10%",
                destroy: true,
                scrollCollapse: true,
                scrollY: '250px',
                order: [[0, "desc"]],
                columns: [
                    {
                        data: 'timestampISO', title: 'Date',
                        render: function (data) {

                            if (data != null) {
                                return data.split('T')[0];
                            }

                            else {
                                return "No date fetched";
                            }
                        }
                    },
                    {
                        data: 'high', title: 'High in USD',
                        render: function (data) {
                            return '$ ' + data
                        }
                    },

                    {
                        data: 'low', title: 'Low in USD',
                        render: function (data) {
                            return '$ ' + data
                        }
                    },

                    {
                        data: 'open', title: 'Open in USD',
                        render: function (data) {
                            return '$ ' + data
                        }
                    },

                    {
                        data: 'close', title: 'Close in USD',
                        render: function (data) {
                            return '$ ' + data
                        }

                    },
                    {
                        data: 'volume', title: 'Volume in USD',

                        render: function (data) {
                            return '$ ' + data
                        }
                    },

                ]
            });

        }

    function SubmitRequest() {
        //var fromDate = $("#dpFromDate").val() != '' ? $("#dpFromDate").datepicker('getDate') : '';
        //var toDate = $("#dpToDate").val() != '' ? $("#dpToDate").datepicker('getDate') : '';
        var fromDate = $("#dpFromDate").val();
    var toDate = $("#dpToDate").val();

    var d = {
        FromDate: fromDate,
    ToDate: toDate            
        };
    if (isValidEntry())
    {
        $.ajax({
            type: "GET",
            url: "/Crypto/Calculate",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: d,
            success: function (response) {
                if (response != null) {
                    $("#txtAvg").val('$ ' + response.average).css("color", "green");;
                    MessageBox('Average!', 'fa fa-tick', response.average, 'green', 'btn btn-success', 'Okay');
                } else {
                    console.log("Something went wrong");
                }
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
        }
        
    }
    function isValidEntry() {
        var valid = true;
    var message = '';
    var count = 0;

    if ($("#dpFromDate").val() == '') {
        $('#dpFromDate').css('border-color', 'red');
    valid = false;
    count = count + 1;
    message += count + '. From Date </br>';
        }
        else {
    $('#dpFromDate').css('border-color', '');
}
if ($("#dpToDate").val() == '') {
    $('#dpToDate').css('border-color', 'red');
    valid = false;
    count = count + 1;
    message += count + '. To Date </br>';
}
else {
    $('#dpToDate').css('border-color', '');
}

if (message != '') {
    MessageBox('Required!', 'fa fa-warning', message, 'red', 'btn btn-danger', 'Okay');
    valid = false;
    message = '';
    count = 0;
}
else {
    valid = true;
}
return valid;
    }
function MessageBox(title, icon, content, type, btnClass, btnText) {
    $.confirm({
        title: title,
        icon: icon,
        content: content,
        type: type,
        //  theme:'dark',
        buttons: {
            omg: {
                text: btnText,
                btnClass: btnClass,
            },
        }
    });
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
