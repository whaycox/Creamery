$(document).ready(function () {
    $(".satellite-add .add-expander").click(toggleAddSatellite);
});

function toggleAddSatellite() {
    var addSatellite = $(".satellite-add");
    if (addSatellite.attr("aria-expanded") === "true") {
        addSatellite.attr("aria-expanded", "false");
    }
    else {
        addSatellite.attr("aria-expanded", "true");
    }
}