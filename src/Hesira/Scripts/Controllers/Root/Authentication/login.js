require([
         'jquery',
         'jquery-ui-core',
         'bootstrap',
         'bforms-ajax',
         'initLTE',
], function () {

    var AuthLogin = function (options) {
        this.options = $.extend(true, {}, options);
    };

    AuthLogin.prototype.selectors = {
      
        loginForm: '.js-loginForm'
  
    };

    AuthLogin.prototype._init = function () {

        this.$loginForm = $(this.selectors.loginForm);
        this._toggleLoading();
    };

    AuthLogin.prototype._toggleLoading = function() {

        if (this.$loginForm.find('.form_container').hasClass('loading')) {
            this.$loginForm.find('.form_container').removeClass('loading');
        } else {
            this.$loginForm.find('.form_container').addClass('loading');
        }
    };

    $(document).ready(function () {
        var ctrl = new AuthLogin(requireConfig.pageOptions);
        ctrl._init();
    });
});