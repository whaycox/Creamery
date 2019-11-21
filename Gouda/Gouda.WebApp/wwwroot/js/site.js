$(document).ready(function () {
    $("#nav-toggle").click(toggleNav);
});

function toggleNav() {
    var nav = $("nav.nav-tree");
    if (nav.attr("aria-expanded") === "true") {
        nav.find(".nav-group").attr("aria-expanded", "false");
        nav.attr("aria-expanded", "false");
    }
    else {
        nav.attr("aria-expanded", "true");
    }
}