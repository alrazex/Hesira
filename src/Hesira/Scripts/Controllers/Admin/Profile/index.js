require([
         'jquery',
         'jquery-ui-core',
         'bootstrap',
         'bforms-ajax',
         'initLTE',
         'bforms-panel',
         'bforms-namespace',
], function () {

    var AdminProfileIndex = function (options) {
        this.options = $.extend(true, {}, options);
    };

    AdminProfileIndex.prototype.selectors = {

        userInfoPanel: '.js-userInfo',
        contactPanel: '.js-contact'

    };

    AdminProfileIndex.prototype._init = function () {

        this._initComponents();
    };

    AdminProfileIndex.prototype._initComponents = function () {

        $(this.selectors.userInfoPanel).bsPanel({
            name: 'userInfo',
            cacheReadonlyContent: false,
            editSuccessHandler: function (e, data) {

            }
        });

        $(this.selectors.contactPanel).bsPanel({
            name: 'contact',
            cacheReadonlyContent: false,
        });

    };

    $(document).ready(function () {
        var ctrl = new AdminProfileIndex(requireConfig.pageOptions);
        ctrl._init();
    });
});