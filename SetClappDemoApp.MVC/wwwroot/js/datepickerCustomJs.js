$(function () {
    $("#datepickerStartFrom").datepicker({
        beforeShowDay: $.datepicker.noWeekends,
        beforeShow: function () {
            setTimeout(function () {
                $("#ui-datepicker-div").css("z-index", 99999999999999);
                $("#ui-datepicker-div").css("content", "");
                $("#ui-datepicker-div").css("display", "table");
                $("#ui-datepicker-div").css("border-collapse", "collapse");
            });
        },
    });

    $("#datepickerFinish").datepicker({
        beforeShowDay: $.datepicker.noWeekends,
        beforeShow: function () {
            setTimeout(function () {
                $("#ui-datepicker-div").css("z-index", 99999999999999);
                $("#ui-datepicker-div").css("content", "");
                $("#ui-datepicker-div").css("display", "table");
                $("#ui-datepicker-div").css("border-collapse", "collapse");
            });
        },
    });
});