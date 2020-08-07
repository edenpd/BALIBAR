$(function () {

    // Init the reservations list
    $(document).ready(function () {

        $.ajax({
            type: 'GET',
            url: "/Reservations/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#reservationList").html(data);
            }
        });
    });

    // Reservation list
    //$("#searchButton").click(function () {
    //    $.ajax({
    //        type: 'GET',
    //        url: "/Reservations/Search",
    //        data: $("#searchForm").serialize(),
    //        success: function (data) {
    //            $("#reservationList").html(data);
    //        }
    //    });
    //});

    $("#searchForm").submit(function (e) {
        e.preventDefault();
        $.ajax({
            type: 'GET',
            url: "/Reservations/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#reservationList").html(data);
            }
        });
    });

    //$("#searchButton").click(function () {
    //    $.ajax({
    //        type: 'GET',
    //        url: "/Reservations/Search",
    //        data: $("#searchForm").serialize(),
    //        success: function (data) {
    //            $("#reservationList").html(data);
    //        }
    //    });
    //});
});