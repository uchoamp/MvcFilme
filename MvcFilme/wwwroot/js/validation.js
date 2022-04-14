$.validator.methods.date = function (value, element, param) {
    console.log(value);
    return (new Date(value) !== "Invalid Date") && !isNaN(new Date(value));
}

$.validator.addMethod("isdateafter", function (value, element, params) {
    let parts = element.name.split(".");
    let prefix = "";
    for (var i = 0; i < parts.length - 1; i++)
        prefix = parts[i] + ".";
    let startdateelement = $('input[name="' + prefix + params.propertytested + '"]');
    let startdatevalue = startdateelement.val();

    if (!value || !startdatevalue) {
        $(`[data-valmsg-for='${params.propertytested}']`).text("");
        return true;
    }

    let allowequal = params.allowequaldates.toLowerCase() == "true";
    console.log(params.allowequaldates.toLowerCase);
    if (allowequal ? Date.parse(startdatevalue) <= Date.parse(value) :
        Date.parse(startdatevalue) < Date.parse(value)) {
        $(`[data-valmsg-for='${params.propertytested}']`).text("");
        return true;
    }

    $(`[data-valmsg-for='${params.propertytested}']`).text(startdateelement.data("val-isdatebefore"));

    return false;
});

$.validator.addMethod("isdatebefore", function (value, element, params) {
    var parts = element.name.split(".");
    var prefix = "";
    for (var i = 0; i < parts.length - 1; i++)
        prefix = parts[i] + ".";
    let enddateelement = $('input[name="' + prefix + params.propertytested + '"]');
    let enddatevalue = enddateelement.val();

    if (!value || !enddatevalue) {
        $(`[data-valmsg-for='${params.propertytested}']`).text("");
        return true;
    }

    let allowequal = params.allowequaldates.toLowerCase() === "true";
    if (allowequal ? Date.parse(enddatevalue) >= Date.parse(value) :
        Date.parse(enddatevalue) > Date.parse(value)) {
        $(`[data-valmsg-for='${params.propertytested}']`).text("");
        return true;
    }

    $(`[data-valmsg-for='${params.propertytested}']`).text(enddateelement.data("val-isdateafter"));
    return false;
});


$.validator.unobtrusive.adapters.add(
    "isdateafter",
    ["propertytested", "allowequaldates"],
    function (options) {
        options.rules['isdateafter'] = options.params;
        options.messages['isdateafter'] = options.message;
    }
);

$.validator.unobtrusive.adapters.add(
    "isdatebefore",
    ["propertytested", "allowequaldates"],
    function (options) {
        options.rules['isdatebefore'] = options.params;
        options.messages['isdatebefore'] = options.message;
    }
);

