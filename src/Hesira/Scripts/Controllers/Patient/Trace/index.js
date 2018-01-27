require([
         'jquery',
         'jquery-ui-core',
         'bootstrap',
         'bforms-ajax',
         'initLTE',
         'bforms-panel',
         'bforms-namespace',
], function () {

    var PatientTraceIndex = function (options) {
        this.options = $.extend(true, {}, options);
    };

    PatientTraceIndex.prototype.selectors = {


    };

    PatientTraceIndex.prototype._init = function () {

        this._initComponents();
    };

    PatientTraceIndex.prototype._initComponents = function () {


    };

    $(document).ready(function () {
        var ctrl = new PatientTraceIndex(requireConfig.pageOptions.Index);
        ctrl._init();
    });
});