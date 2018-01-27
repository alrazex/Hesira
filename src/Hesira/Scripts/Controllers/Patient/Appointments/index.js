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

    var PatientAppointmentsIndex = function (options) {
        this.options = $.extend(true, {}, options);
    };

    PatientAppointmentsIndex.prototype.selectors = {

        newFormSelector: '.js-newAppointmentForm',
        resetFormBtn: '.js-resetForm',
        calendarComp: '#calendar',

    };

    PatientAppointmentsIndex.prototype._init = function () {

        this.$newForm = $(this.selectors.newFormSelector);
        this._toggleLoading();
        this._initComponents();
        this._initCalendar();
    };

    PatientAppointmentsIndex.prototype._initCalendar = function () {

        var eventsUrl = this.options.getEvents;

        $.bforms.ajax({
            url: eventsUrl,
            success: $.proxy(this._initCalendarSuccess, this),
            error: $.proxy(this._initCalendarError, this)
        });

    };

    PatientAppointmentsIndex.prototype._initCalendarSuccess = function (response) {
        

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

    PatientAppointmentsIndex.prototype._initCalendarError = function () {
        console.trace();
    };

    PatientAppointmentsIndex.prototype._initComponents = function () {

        this.$newForm.on('click', this.selectors.resetFormBtn, $.proxy(this._resetForm, this));

    };

    PatientAppointmentsIndex.prototype._resetForm = function () {
        this.$newForm.bsForm('reset');
    };

    PatientAppointmentsIndex.prototype._toggleLoading = function () {

        this.$newForm.bsForm({
            uniqueName: 'newAppointmentForm'
        });
        if (this.$newForm.find('.form_container').hasClass('loading')) {
            this.$newForm.find('.form_container').removeClass('loading');
        } else {
            this.$newForm.find('.form_container').addClass('loading');
        }
    };

    $(document).ready(function () {
        var ctrl = new PatientAppointmentsIndex(requireConfig.pageOptions.Index);
        ctrl._init();
    });
});