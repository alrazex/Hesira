require([
         'jquery',
         'jquery-ui-core',
         'bootstrap',
         'bforms-ajax',
         'initLTE',
         'bforms-namespace',
         'bforms-toolbar',
         'bforms-grid',
         'bforms-ajax',
         'ajaxList',

], function () {

    var DoctorUsersIndex = function (options) {
        this.options = $.extend(true, {}, options);
    };

    DoctorUsersIndex.prototype.selectors = {

        gridContainer: '#grid',
        toolbarContainer: '#toolbar',
        searchForm: '.js-searchForm',
        newForm: '.js-newForm',
        citizenshipList: '.js-citizenshipList',
        countriesDropdown: '.js-countriesDropdown',
        userRolesBtnList: '.js-userRoles',
        resetSearchBtn: '.js-btn-reset',
        quickSearchBox: '.bs-text',
        resetButton: '.bs-resetGrid',

    };

    DoctorUsersIndex.prototype.autocompleteComponents = $.extend(true, {}, requireConfig.pageOptions.Index.autocompleteComponents);
    DoctorUsersIndex.prototype.userRoles = $.extend(true, {}, requireConfig.pageOptions.Index.userRoles);


    DoctorUsersIndex.prototype._init = function () {
        this._initComponents();
        this._initGrid();
        this._initToolbar();
    };

    DoctorUsersIndex.prototype._initComponents = function () {
        this.$grid = $(this.selectors.gridContainer);
        this.$toolbar = $(this.selectors.toolbarContainer);
        this.$searchForm = this.$toolbar.find(this.selectors.searchForm).parent();
        this.$newForm = this.$toolbar.find(this.selectors.newForm).parent();
    };

    DoctorUsersIndex.prototype._initGrid = function () {

        this.$grid.bsGrid(this._bsGridOptions());
        this.$grid.on('click', this.selectors.resetButton, $.proxy(this._resetGrid, this));
    };

    DoctorUsersIndex.prototype._resetGrid = function (ev) {
        ev.preventDefault();
        $(this.selectors.resetSearchBtn).click();
        $(this.selectors.quickSearchBox).val('');
        this.$grid.bsGrid('reset');
    };

    DoctorUsersIndex.prototype._initToolbar = function () {

        this.$toolbar.bsToolbar({
            uniqueName: 'usersToolbar',
            subscribers: [this.$grid],
        });

        this.$searchForm.on('bsformafterinitui', $.proxy(this._initSearchForm, this));

        this.$newForm.on('bsformafterinitui', $.proxy(this._initNewForm, this));
        this.$newForm.on('bsformbeforeformvalidation', $.proxy(this._validationRules, this));

    };

    DoctorUsersIndex.prototype._validationRules = function (e, data) {

        data.validator.settings.ignore = function (idx, elem) {
            if (!$(elem).parents('.form-group').is(':visible')) {
                return true;
            } else {
                return false;
            }

        };

    };

    DoctorUsersIndex.prototype._initNewForm = function () {


        this.$newForm.find(this.selectors.citizenshipList).ajaxList({
            persistentData: [{ value: '', text: this.options.currentCulture == 'en' ? 'Choose' : 'Alegeți' }],
            url: this.options.updateComponentListUrl,
            type: 'dropdown',
            placeholder: this.options.currentCulture == 'en' ? 'Choose' : 'Alegeți',
            ajaxData: {
                componentId: this.autocompleteComponents.Citizenship
            },
        });


        this.$newForm.find(this.selectors.countriesDropdown).ajaxList({
            persistentData: [{ value: '', text: this.options.currentCulture == 'en' ? 'Choose' : 'Alegeți' }],
            url: this.options.updateComponentListUrl,
            type: 'dropdown',
            placeholder: this.options.currentCulture == 'en' ? 'Choose' : 'Alegeți',
            ajaxData: {
                componentId: this.autocompleteComponents.Countries
            },
        });

        this.$newForm.on('change', this.selectors.userRolesBtnList, $.proxy(this._userRoleChange, this));

    };

    DoctorUsersIndex.prototype._userRoleChange = function(ev) {

        var $currentTarget = $(ev.currentTarget);
        var selectedValue = parseInt($currentTarget.val(), 10);
        
        switch(selectedValue) {
        
            case this.userRoles.Patient:
                {
                    console.log('patient');
                    break;
                }
            case this.userRoles.Admin:
            case this.userRoles.Doctor:
                {
                    console.log('admin & doctor');
                    break;
                }
                
        }


    };

    DoctorUsersIndex.prototype._initSearchForm = function () {

        this.$searchForm.find(this.selectors.citizenshipList).ajaxList({
            persistentData: [{ value: '', text: this.options.currentCulture == 'en' ? 'Choose' : 'Alegeți' }],
            url: this.options.updateComponentListUrl,
            type: 'dropdown',
            placeholder: this.options.currentCulture == 'en' ? 'Choose' : 'Alegeți',
            ajaxData: {
                componentId: this.autocompleteComponents.Citizenship
            },
        });
    };

    DoctorUsersIndex.prototype._bsGridOptions = function () {

        var filterButtons = [{
            btnSelector: '.js-actives',
            filter: function ($el) {
                return $el.data('active') == 'True';
            }
        }, {
            btnSelector: '.js-inactives',
            filter: function ($el) {
                return $el.data('active') != 'True';
            },
        }];

        var ctx = this;

        var gridActions = [{
            btnSelector: '.js-btn-enable_selected',
            handler: $.proxy(function ($rows, context) {
                var data = {};

                var items = context.getSelectedRows();

                data.items = items;
                data.enable = true;

                this._ajaxEnableDisable($rows, data, this.options.enableDisableUrl, function (response) {

                    context.updateRows(response.RowsHtml);

                }, function (response) {
                    context._pagerAjaxError(response);
                });
            }, this),
            popover: true,
            popoverOptions: {
                show: function () {
                    debugger;
                    var question = ctx.options.currentCulture == 'en' ? "Confirm status change ?" : "Confirmați schimbarea stării ?";
                    $(this).bsInlineQuestion('content', {
                        question: question
                    });
                }
            },
            question: ctx.options.currentCulture == 'en' ? "Confirm status change ?" : "Confirmați schimbarea stării ?",
            confirmButtonText: ctx.options.currentCulture == 'en' ? 'Yes' : 'Da',
            cancelButtonText: ctx.options.currentCulture == 'en' ? 'No' : 'Nu',
            confirmCssClass: 'btn-default',
            cancelCssClass: 'btn-warning'
        }, {
            btnSelector: '.js-btn-disable_selected',
            handler: $.proxy(function ($rows, context) {
                var data = {};

                var items = context.getSelectedRows();
                data.items = items;
                data.enable = false;
                this._ajaxEnableDisable($rows, data, this.options.enableDisableUrl, function (response) {

                    context.updateRows(response.RowsHtml);

                }, function (response) {
                    context._pagerAjaxError(response);
                });
            }, this),
            popover: true,
            popoverOptions: {
                show: function () {
                    $(this).bsInlineQuestion('content', {
                        question: ctx.options.currentCulture == 'en' ? "Confirm status change ?" : "Confirmați schimbarea stării ?",
                    });
                }
            },
            question: ctx.options.currentCulture == 'en' ? "Confirm status change ?" : "Confirmați schimbarea stării ?",
            confirmButtonText: ctx.options.currentCulture == 'en' ? 'Yes' : 'Da',
            cancelButtonText: ctx.options.currentCulture == 'en' ? 'No' : 'Nu',
            confirmCssClass: 'btn-default',
            cancelCssClass: 'btn-warning'

        }, {
            btnSelector: '.js-btn-delete_selected',
            handler: $.proxy(function ($rows, context) {

                var items = context.getSelectedRows();

                this._ajaxDelete($rows, items, this.options.deleteUrl, $.proxy(function () {
                    context._getPage(true);
                    context._evOnRowCheckChange($rows);
                }, this), function (response) {
                    context._pagerAjaxError(response);
                });
            }, this),
            popover: true,
            popoverOptions: {
                show: function () {

                    var question = ctx.options.currentCulture == 'en' ? "Confirm deletion ?" : "Confirmați ștergerea ?";
                    $(this).bsInlineQuestion('content', {
                        question: question
                    });
                }
            },

            question: ctx.options.currentCulture == 'en' ? "Confirm deletion ?" : "Confirmați ștergerea ?",
            confirmButtonText: ctx.options.currentCulture == 'en' ? "Yes" : "Da",
            cancelButtonText: ctx.options.currentCulture == 'en' ? "No" : "Nu",
            confirmCssClass: 'btn-default',
            cancelCssClass: 'btn-warning'
        }];

        var rowActions = [];

        var gridOptions = {
            uniqueName: 'usersGrid',
            toolbar: this.$toolbar,
            pagerUrl: this.options.pagerUrl,
            beforePager: $.proxy(this._beforePager, this),
            filterButtons: filterButtons,
            rowActions: rowActions,
            gridActions: gridActions,
            sortable: false,
        };

        return gridOptions;



    };

    DoctorUsersIndex.prototype._beforePager = function (ev, data) {


    };

    //#region EnableDisableHandler

    DoctorUsersIndex.prototype._ajaxEnableDisable = function ($html, data, url, success, error) {
        var ajaxOptions = {
            name: '|enableDisable|' + $html.data('objid'),
            url: url,
            data: data,
            context: this,
            success: success,
            error: error,
            loadingElement: $html,
            loadingClass: 'loading'
        };
        $.bforms.ajax(ajaxOptions);
    };
    //#endregion


    //#region DeleteHandler
    DoctorUsersIndex.prototype._ajaxDelete = function ($html, data, url, success, error) {
        var ajaxOptions = {
            name: '|delete|' + data,
            url: url,
            data: data,
            context: this,
            success: success,
            error: error,
            loadingElement: $html,
            loadingClass: 'loading'
        };
        $.bforms.ajax(ajaxOptions);
    };
    //#endregion

    $(document).ready(function () {
        var ctrl = new DoctorUsersIndex(requireConfig.pageOptions.Index);
        ctrl._init();
    });

});