$(document).ready(function () {
    $("#nav-toggle").click(toggleNav);
});

function toggleNav() {
    toggleExpanded($("nav.gouda-nav-tree"));
    $("gouda-nav-group[aria-expanded='true']").attr("aria-expanded", "false");
}

function toggleExpanded(ele) {
    var element = $(ele);
    if (element.attr("aria-expanded") === "true") {
        element.attr("aria-expanded", "false");
    }
    else {
        element.attr("aria-expanded", "true");
    }
}