require([
         'jquery',
         'jquery-ui-core',
         'bootstrap',
         'bforms-ajax',
         'initLTE',

], function () {

    var HomeIndex = function (options) {
        this.options = $.extend(true, {}, options);
    };

    HomeIndex.prototype._init = function () {
        

    };

    $(document).ready(function () {
        var ctrl = new HomeIndex(requireConfig.pageOptions);
        ctrl._init();
    });
});