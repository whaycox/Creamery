$(document).ready(function () {
    wireInputs();
});

function wireInputs() {
    $("gouda-input .ipAddress").on("input", function (event) {
        if (this.validity.patternMismatch) {
            this.setCustomValidity("Please supply an IP Address.");
        }
        else {
            this.setCustomValidity("");
        }
    });
    $(".gouda-form").on("submit", function (event) {
        event.preventDefault();
        if (event.currentTarget.checkValidity()) {
            var destination = $(event.currentTarget).data("destination");
            var data = $(event.currentTarget).serialize();
            var success = window[$(event.currentTarget).data("success")];
            $.ajax({
                url: destination,
                method: "POST",
                data: data,
                success: success
            });
        }
    });
}