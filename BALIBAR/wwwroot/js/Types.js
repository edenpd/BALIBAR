$(function () {

    $(document).ready(function () {

        var url = "/Types/GetTypeDetails"
        $.ajax({
            type: 'GET',
            url: url,
            contentType: 'json',
            success: function (data) {
                document.getElementById('num_of_bars').innerHTML = data.numofbars;
                document.getElementById('num_of_reservations').innerHTML = data.numofreservations;
            }
        })

    });

    $("#barsButton").click(function () {
        $.ajax({
            type: 'POST',
            url: "/Types/NavToBars",
            dataType: 'json',
            crossDomain: true,
            success: function (data) {
                window.location.href = data;
            }
        })
    })
});