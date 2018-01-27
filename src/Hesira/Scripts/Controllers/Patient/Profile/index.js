require([
         'jquery',
         'jquery-ui-core',
         'bootstrap',
         'bforms-ajax',
         'initLTE',
         'bforms-panel',
         'bforms-namespace',
], function () {

    var PatientProfileIndex = function (options) {
        this.options = $.extend(true, {}, options);
    };

    PatientProfileIndex.prototype.selectors = {
        
        userInfoPanel: '.js-userInfo',
        contactPanel: '.js-contact'
        
    };

    PatientProfileIndex.prototype._init = function () {

        this._initComponents();
    };

    PatientProfileIndex.prototype._initComponents = function () {
        
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
        var ctrl = new PatientProfileIndex(requireConfig.pageOptions);
        ctrl._init();
    });
});