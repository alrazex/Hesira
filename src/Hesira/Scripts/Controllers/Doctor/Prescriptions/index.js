require([
    'jquery',
    'jquery-ui-core',
    'bootstrap',
    'bforms-ajax',
    'initLTE',
    'bforms-panel',
    'bforms-groupEditor'

], function () {

    //#region Constructor and Properties
    var DoctorPrescriptionsIndex = function (options) {

        this.options = $.extend(true, {}, options);
    };

    //#endregion 

    //#region Selectors 

    DoctorPrescriptionsIndex.prototype.selectors = {
        resetFormBtn: '.js-resetForm',
        addFormBtn: '.js-submitAddForm',
        newFormSelector: '.js-newPrescriptionForm',
        drugsBuilderSelector: '#js-prescriptionEditor',
        pageContent: '.content',
        saveFormBtn: '.js-submitFirstForm',
        inlineSearch: '.bs-tabInlineSearch'
    };

    //#endregion

    //#region Init

    DoctorPrescriptionsIndex.prototype._init = function () {


        this.$newForm = $(this.selectors.newFormSelector);

        this.$newForm.bsForm({
            uniqueName: 'newPrescriptionForm',
        });

        this._initComponents();

        //#endregion
    };

    DoctorPrescriptionsIndex.prototype._initComponents = function () {
        $(this.selectors.pageContent).on('click', this.selectors.resetFormBtn, $.proxy(this._resetForm, this));
        $(this.selectors.pageContent).on('click', this.selectors.addFormBtn, $.proxy(this._addForm, this));
        this._initGroupEditor();
    };

    DoctorPrescriptionsIndex.prototype._resetForm = function () {
        $(this.selectors.drugsBuilderSelector).bsGroupEditor('reset');
        this.$newForm.bsForm('reset');
    };

    DoctorPrescriptionsIndex.prototype._addForm = function (e) {
        e.preventDefault();
        var data = this.$newForm.bsForm('parse');
        var parseData = this.$groupBuilder.bsGroupEditor('parse');
        if (parseData.valid) {
            data['NewPrescriptionModel.PrescriptionBuilderModel'] = parseData.data;
        }

        $.bforms.ajax({
            data: data,
            url: this.options.newPrescriptionUrl,
            success: $.proxy(this._addPrescriptionSuccess, this),
            error: $.proxy(this._addPrescriptionError, this),
            context: this,
        });
    };

    DoctorPrescriptionsIndex.prototype._addPrescriptionError = function() {
        console.trace();
    };
    
    DoctorPrescriptionsIndex.prototype._addPrescriptionSuccess = function (response) {
        window.location.reload();
    };


    //#endregion

    //#region Handlers
    //#region Team Builder

    DoctorPrescriptionsIndex.prototype._initGroupEditor = function () {

        this.$groupBuilder = $(this.selectors.drugsBuilderSelector);

        if (this.$groupBuilder != null) {

            this.$groupBuilder.bsGroupEditor({
                getTabUrl: this.options.drugsBuilderPager,
                buildGroupItem: function (model) {
                    return $('<span>' + model.Name + '</span>');
                },
                enableQuickSearch: true,
                additionalData: {
                    settings: {
                       

                    }

                },
                buildDragHelper: function (model, tabId, connectsWith) {
                    return $('<div class="col-lg-6 col-md-6 bs-itemContent" style="z-index:999"><span>' + model.Name + '</span></div>');
                },
                onSaveSuccess: $.proxy(function () {
                }, this),
                getExtraItemData: $.proxy(function (e, data, $item) {
                    if ($item.data('model').Name != null && typeof $item.data('model').Name != "undefined") {
                        data.Name = $item.data('model').Name;

                    } else {
                        if ($item.data('model').Name != null && typeof $item.data('model').Name != "undefined") {
                            data.Name = $item.data('model').Name;
                        }
                    }
                }, this)
            });

        }

    };


    //#endregion
    //#endregion


    $(document).ready(function () {
        var ctrl = new DoctorPrescriptionsIndex(requireConfig.pageOptions.Index);
        ctrl._init();
    });

});