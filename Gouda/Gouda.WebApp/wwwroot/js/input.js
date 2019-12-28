$(document).ready(function () {
    wireInputs();
});

function getFunctionPointer(functionName) {
    return window[functionName];
}

function wireInputs() {
    wireIPAddressValidity();
    wireFormSubmissionSuppression();
}
function wireIPAddressValidity() {
    $("gouda-input .IPAddress").on("input", function (event) {
        if (this.validity.patternMismatch) {
            this.setCustomValidity("Please supply an IP Address.");
        }
        else {
            this.setCustomValidity("");
        }
    });
}
function wireFormSubmissionSuppression() {
    $("form.gouda-form").submit(function (args) { args.preventDefault(); });
}

function extractDestination(button) {
    var destination = $(button).data("destination");
    var method = $(button).data("method");
    return {
        url: destination,
        method: method
    };
}

function extractForm(form) {
    var array = form.serializeArray();
    var json = {};
    for (i = 0; i < array.length; i++) {
        json[array[i]["name"]] = array[i]["value"];
    }
    return json;
}