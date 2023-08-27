;
debugger;

var DOMAIN = '/';
// var sessionStart = false;


function convertDateTimeToString(dt) {
    // var dt = new Date();
    var current_date = dt.getDate();
    var current_month = dt.getMonth() + 1;
    var current_year = dt.getFullYear();
    var current_hrs = dt.getHours();
    var current_mins = dt.getMinutes();
    var current_secs = dt.getSeconds();
    var current_datetime = "";
    // Add 0 before date, month, hrs, mins or secs if they are less than 0
    current_date = current_date < 10 ? '0' + current_date : current_date;
    current_month = current_month < 10 ? '0' + current_month : current_month;
    current_hrs = current_hrs < 10 ? '0' + current_hrs : current_hrs;
    current_mins = current_mins < 10 ? '0' + current_mins : current_mins;
    current_secs = current_secs < 10 ? '0' + current_secs : current_secs;
    // currentWeekDay = currentWeekDay < 10 ? '0' + currentWeekDay : currentWeekDay;
    // Current datetime
    // String such as 2016-07-16T19:20:30
    // current_datetime = current_year + '-' + current_month + '-' + current_date;
    current_datetime = current_year + "-" + current_month + '-' + current_date;
    return current_datetime;
}

function showProgressWaiting() {
    $(".ui-modal").show();
}

function hiddenProgressWaiting() {
    $(".ui-modal").hide();
}
