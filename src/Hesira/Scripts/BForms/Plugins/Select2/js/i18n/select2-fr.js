/**
 * Select2 Francais translation.
 */
(function(factory) {
    if (typeof define === "function" && define.amd) {
        define('select2-fr', ['jquery', 'select2'], factory);
    } else {
        factory(window.jQuery);
    }
}(function($) {
    $.extend($.fn.select2.defaults, {
        formatNoMatches: function () { return "Rien trouvé"; },
        formatInputTooShort: function (input, min) { var n = min - input.length; return "Se il vous plaît entrer encore " + n + " caractèr" + (n == 1 ? "" : "e"); },
        formatInputTooLong: function (input, max) { var n = input.length - max; return "Se il vous plaît entrer moins de " + n + " caractèr" + (n == 1 ? "" : "e"); },
        formatSelectionTooBig: function (limit) { return "Vous devez sélectionner au plus " + limit + " élément" + (limit >= 1 ? "s" : ""); },
        formatLoadMore: function (pageNumber) { return "Chargement ..."; },
        formatSearching: function () { return "Recherche ..."; },
        formatPlaceholder: function () {
            return "Choisir";
        },
        formatSaveItem: function (val) {
            return "Enregistrer " + val;
        },
    });
}));
