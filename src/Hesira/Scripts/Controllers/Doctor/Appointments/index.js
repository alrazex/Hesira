require([
    'jquery',
    'jquery-ui-core',
    'bootstrap',
    'bforms-ajax',
    'initLTE',
    'bforms-panel',
    'bforms-namespace',
    'fullcalendar'
], function () {

    var DoctorAppointmentsIndex = function (options) {
        this.options = $.extend(true, {}, options);
    };

    DoctorAppointmentsIndex.prototype.selectors = {

        calendarComp: '#calendar',

    };

    DoctorAppointmentsIndex.prototype._init = function () {

        this._initCalendar();
    };

    DoctorAppointmentsIndex.prototype._initCalendar = function () {

        var eventsUrl = this.options.getEvents;

        $.bforms.ajax({
            url: eventsUrl,
            success: $.proxy(this._initCalendarSuccess, this),
            error: $.proxy(this._initCalendarError, this)
        });

    };

    DoctorAppointmentsIndex.prototype._initCalendarSuccess = function (response) {


        var events = response.Events.slice();

        $(this.selectors.calendarComp).fullCalendar({
            weekends: false,
            header: false,
            weekMode: 'variable',
            defaultView: 'agendaDay',
            allDaySlot: false,
            minTime: "08:00 AM",
            maxTime: "7:00 PM",
            events: events
        });

    };

    DoctorAppointmentsIndex.prototype._initCalendarError = function () {
        console.trace();
    };


    $(document).ready(function () {
        var ctrl = new DoctorAppointmentsIndex(requireConfig.pageOptions.Index);
        ctrl._init();
    });
});