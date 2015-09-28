$(document).ready(function () {
    $('#listofproducts').click(function() {
        alert('asdf');
    });

    $('.status').animate({
        color: "#000"
    }, 5000);

    $(function () {
        $(".modal-dialog").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 1000
            },
            hide: {
                effect: "explode",
                duration: 1000
            }
        });

        $("#opener").click(function () {
            $("#dialog").dialog("open");
        });
    });

    $(".tablesorter").tablesorter({ sortList: [[0, 0], [2, 1]], widgets: ['zebra'] });
});