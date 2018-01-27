/*
 * Translated default messages for the jQuery validation plugin.
 * Language: FR
 * Author: Marius Cosareanu
 */

(function (factory) {
    if (typeof define === "function" && define.amd) {
        define('validate-fr', ['jquery', 'bforms-validate'], factory);
    } else {
        factory(window.jQuery);
    }
}(function (jQuery) {
    jQuery.extend(jQuery.validator.messages, {
        required: "Ce champ est obligatoire.",
        mandatory: "Ce champ est obligatoire.",
        remote: "Se il vous plaît remplir ce champ.",
        email: "Se il vous plaît entrer une adresse email valide.",
        url: "Se il vous plaît entrer une URL valide.",
        date: "Se il vous plaît entrer une date correcte .",
        dateISO: "Se il vous plaît entrer une date ( ISO ) correcte .",
        number: "Se il vous plaît entrer un numéro valide.",
        digits: "Se il vous plaît entrer que des chiffres.",
        creditcard: "Se il vous plaît entrez un numéro de carte de crédit valide .",
        equalTo: "Se il vous plaît entrer de nouveau la valeur .",
        maxlength: jQuery.format("Se il vous plaît entrez pas plus de {0} caractères."),
        minlength: jQuery.format("Se il vous plaît entrer au moins { 0 } caractères."),
        rangelength: jQuery.format("Se il vous plaît entrer une valeur comprise entre {0} et {1} caractères."),
        range: jQuery.format("Se il vous plaît entrer une valeur comprise entre {0} et {1} ."),
        max: jQuery.format("Se il vous plaît entrer une valeur inférieure à {0} égale ou ."),
        min: jQuery.format("Se il vous plaît entrer une valeur égale ou supérieure à {0}.")
    });
}));