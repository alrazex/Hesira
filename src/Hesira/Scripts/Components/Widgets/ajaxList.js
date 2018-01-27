var factory = function ($) {

    var AjaxList = function (options) {

        this.options = $.extend(true, this.options, options);

        this._init();
    };

    AjaxList.prototype.typeEnum = {

        listBox: 'listbox',
        dropdown: 'dropdown'

    };

    AjaxList.prototype.options = {

        // receive objects with id, text | Example :  { id: '-1', text: 'John McNuget'}
        // please provide negative values if you want to keep them in the options list
        persistentData: [],
        url: '',
        uniqueName: '',
        type: '',
        placeholder: '',
        listBoxCreateSearchChoice: false,
        textTag: true,
        select2TagsOpts: {
            tokenSeparators: [",", " "],
            tags: [],
            selectedValues: []
        },
        ajaxData: {}

    };

    AjaxList.prototype._init = function () {

        this.$element = this.element;

        if (!this.options.uniqueName) {
            this.options.uniqueName = this.$element.attr('id');
        }

        if (!this.options.uniqueName) {
            throw ': Ajax list needs a unique name or the element on which it is aplied has to have an id attr';
        }

        if (!this.options.url) {
            this.options.url = this.$element.attr('data-url');
        }

        if (!this.options.url) {
            throw ': Ajax list needs a url or the element on which it is aplied has to have a data-url attr';
        }

        if (!this.options.type) {
            throw ': Please specify a type for ajax list (string) : listbox or dropdown';
        } else {

            if (!(this.options.type.toLowerCase() === this.typeEnum.listBox || this.options.type.toLowerCase() === this.typeEnum.dropdown)) {
                throw ': Please specify a correct type for ajax list (string) : listbox or dropdown';
            }

        }

        if (this.options.type.toLowerCase() === this.typeEnum.listBox) {
            this._initListBox();
        } else if (this.options.type.toLowerCase() === this.typeEnum.dropdown) {
            this._initDropdown();
        }

    };

    AjaxList.prototype._initListBox = function () {

        var persistentData = this.options.persistentData;
        var createSearchChoice = this.options.listBoxCreateSearchChoice;
        var ajaxData = this.options.ajaxData;

        this.$element.bsSelectInput({
            select2TagsOpts: {
                placeholder: this.options.placeholder,
                formatResult: function (result, container, query, escapeMarkup) {
                    var markup = [];
                    window.Select2.util.markMatch(result.text || result.Text, query.term, markup, escapeMarkup);
                    return markup.join("");
                },
                initSelection: function ($element, callback) {
                    callback($element[0].defaultValue.split(',').map(function (text) {
                        return {
                            text: text,
                            value: text
                        };
                    }));
                },
                formatSelection: function (data, container) {
                    return data ? typeof data.Text !== "undefined" ? data.Text : data.text : undefined;
                },
                id: function (elem) {
                    return elem.Text != null ? elem.Text.trim() : elem.text.trim();
                },
                minimumInputLength: 0,
                createSearchChoice: createSearchChoice != null && createSearchChoice ?
                function (term, data) {
                    if ($(data).filter(function () {
                        return this.Text != null && typeof this.Text !== "undefined" ? this.Text.localeCompare(term) === 0 : this.text.localeCompare(term) === 0;
                    }).length === 0) {
                        return {
                            id: term,
                            text: term
                        };
                    }
                } : null,
                multiple: true,
                ajax: {
                    url: this.options.url,
                    dataType: "json",
                    data: function (term, page) {
                        var jsonData = {
                            queryString: term,
                            pageLimit: 10,
                            currentPage: page,
                        };
                        return $.extend(true, jsonData, ajaxData);
                    },
                    results: function (response, page) {
                        var leadingResults = page === 1 ? persistentData : [];
                        var more = (page * 10) < response.Data.total;
                        var finalResults = leadingResults.concat(response.Data.results);
                        return { results: finalResults, more: more };
                    }
                }
            }
        });

    };

    AjaxList.prototype._initDropdown = function () {

        var persistentData = this.options.persistentData;
        var ajaxData = this.options.ajaxData;

        this._selectSettings = this._getDropdownSettings();
        this._createInputForDropdown();



        this.$input.select2({
            placeholder: this.options.placeholder,
            formatResult: function (result, container, query, escapeMarkup) {
                var markup = [];
                window.Select2.util.markMatch(result.text || result.Text, query.term, markup, escapeMarkup);
                return markup.join("");
            },
            formatSelection: function (data, container) {
                return data ? typeof data.Text !== "undefined" ? data.Text : data.text : undefined;
            },
            id: function (elem) {
                return elem.Value != null ? elem.Value.trim() : elem.value.trim();
            },
            minimumInputLength: 0,
            ajax: {
                url: this.options.url,
                dataType: "json",
                data: function (term, page) {
                    var jsonData = {
                        queryString: term,
                        pageLimit: 10,
                        currentPage: page,
                    };
                    return $.extend(true, jsonData, ajaxData);
                },
                results: function (response, page) {
                    var leadingResults = page === 1 ? persistentData : [];
                    var more = (page * 10) < response.Data.total;
                    var finalResults = leadingResults.concat(response.Data.results);
                    return { results: finalResults, more: more };
                }
            }

        });

    };

    AjaxList.prototype._createInputForDropdown = function () {
        var $input = $('<input></input>');

        $input.prop('type', 'hidden');
        $input.prop('id', this.$element.prop('id'));
        $input.prop('name', this.$element.prop('name'));
        $input.prop('class', this.$element.prop('class'));
        $input.prop('value', this._selectSettings.selectedValues);

        $input.data(this.$element.data());

        var attrs = this.$element[0].attributes,
		    i = 0,
		    l = attrs.length,
		    toRemove = [];

        for (; i < l; i++) {
            var attr = attrs[i];
            if (typeof attr.nodeName !== 'undefined' && attr.nodeName.indexOf('data-') === 0) {
                $input.attr(attr.nodeName, attr.nodeValue);
                toRemove.push(attr.nodeName);
            }
        }

        for (var index = 0; index < toRemove.length; index++) {
            this.$element.removeAttr(toRemove[index]);
        }

        this.$element.prop('id', 'tag_' + $input.prop('id'));
        this.$element.prop('name', 'tag_' + $input.prop('name'));

        this.$input = $input;

        this.$input.on('change', function () {
            if (typeof $(this).valid === "function") {
                $(this).valid();
            }
        });

        this.$element.before(this.$input);
        this.$element.hide();
    };

    AjaxList.prototype._getDropdownSettings = function () {
        var settings = $.extend(true, {}, this.options.select2TagsOpts);

        if (this.$element.is('select')) {
            this.$element.find('option').each($.proxy(function (idx, opt) {
                var $opt = $(opt),
                    val = $opt.val();

                if (typeof val !== "undefined" && val !== '') {

                    var tagText = this.options.textTag === true ? $opt.text() : val;
                    settings.tags.push(tagText);

                    if ($opt.prop('selected') == true) {
                        settings.selectedValues.push(tagText);
                    }

                } else {
                    settings.placeholder = $opt.text();
                }

            }, this));
        }

        return settings;
    };

    $.widget('hesira.ajaxList', AjaxList.prototype);

    return AjaxList;

};

if (typeof window.define == 'function' && define.amd) {

    define('ajaxList', ['jquery', 'jquery-ui-core', 'bforms-ajax', 'bforms-select2'], factory);

} else {

    factory(window.jQuery);
}