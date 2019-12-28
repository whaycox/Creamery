$(document).ready(function () {
    $("#nav-toggle").click(toggleNav);
});

function toggleNav() {
    toggleExpanded($("nav.gouda-nav-tree"));
    $("gouda-nav-group[aria-expanded='true']").attr("aria-expanded", "false");
}

function toggleExpanded(ele) {
    if ($(ele).attr("aria-expanded") === "true") {
        collapseElement(ele);
    }
    else {
        expandElement(ele);
    }
}
function expandElement(ele) {
    $(ele).attr("aria-expanded", "true");
}
function collapseElement(ele) {
    $(ele).attr("aria-expanded", "false");
}