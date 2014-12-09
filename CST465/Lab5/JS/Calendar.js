$(document).ready(function () {
    DocumentLoad();
})

function DocumentLoad() {
    $(".day").bind("click", dayClicked);
    $("Input[type=checkbox]").bind("click", toggleEventView);
    
    //$(".calendarEvent").bind("click", eventClicked);
    $(".CalendarCurrentDay").removeClass(".day");
}

function eventClicked(event) {

}

function toggleEventView(event) {
    if ($(event.target).is(":checked")) {
        $(".day").find(".EventVisibility").show();
    } else {
        $(".day").find(".EventVisibility").hide();
    }
    $(".CalendarCurrentDay").find(".EventVisibility").show();
}

function dayClicked(event) {
    var v = $(event.target).attr('data-day');
    if (v != undefined) {
        var current = $(".CalendarCurrentDay");
        $(current).addClass("day");
        $(current).toggleClass("CalendarCurrentDay");
        if (!$('span.showAllEvents').find($("input[type=checkbox]")).prop('checked')) {
            $(current).find(".EventVisibility").hide();
        }
        $(event.target).toggleClass("CalendarCurrentDay");
        $(event.target).removeClass("day");
        $(event.target).find(".EventVisibility").show();
    }
}