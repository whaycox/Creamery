$(document).ready(function () {
    $(".nav-group-header").click(toggleNavGroup);
});

function toggleNavGroup(args) {
    var group = $(args.currentTarget).parent();
    if (group.attr("aria-expanded") === "true") {
        group.attr("aria-expanded", "false");
    }
    else {
        group.attr("aria-expanded", "true");
    }
}