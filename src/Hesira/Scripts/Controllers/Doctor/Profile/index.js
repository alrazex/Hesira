require([
         'jquery',
         'jquery-ui-core',
         'bootstrap',
         'bforms-ajax',
         'initLTE',
         'bforms-panel',
         'bforms-namespace',
], function () {

    var DoctorProfileIndex = function (options) {
        this.options = $.extend(true, {}, options);
    };

    DoctorProfileIndex.prototype.selectors = {

        userInfoPanel: '.js-userInfo',
        contactPanel: '.js-contact'

    };

    DoctorProfileIndex.prototype._init = function () {

        this._initComponents();
    };

    DoctorProfileIndex.prototype._initComponents = function () {

        $(this.selectors.userInfoPanel).bsPanel({
            name: 'userInfo',
            editSuccessHandler: function (e, data) {

            }
        });

        $(this.selectors.contactPanel).bsPanel({
            name: 'contact',
        });

    };

    $(document).ready(function () {
        var ctrl = new DoctorProfileIndex(requireConfig.pageOptions);
        ctrl._init();
    });
});