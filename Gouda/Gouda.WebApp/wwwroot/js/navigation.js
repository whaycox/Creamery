$(document).ready(function () {
    $("gouda-nav-group .header").click(toggleNavGroup);
});

function toggleNavGroup(args) {
    var group = $(args.currentTarget).parent();
    toggleExpanded(group);
}