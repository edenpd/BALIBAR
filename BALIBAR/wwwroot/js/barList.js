$(function () {

    // Init bar list
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: "/Bars/Search",
            success: function (data) {
                $("#barList").html(data);
            }
        })

        $.ajax({
            type: 'GET',
            url: "/Bars/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#barList").html(data);
            }
        })

    });

    // Bar list
    $("#searchButton").click(function () {
        $.ajax({
            type: 'GET',
            url: "/Bars/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#barList").html(data);
            }
        })
    })

    // Bar types
    var select = document.getElementById("Type_Name")
    var url = "/Bars/GetTypesList"
    $.ajax({
        type: 'GET',
        url: url,
        contentType: 'json',
        success: function (genres) {

            for (var i = 0; i < genres.length; i++) {
                var option = document.createElement("option");
                option.text = genres[i].name;
                select.add(option);
            }
        }
    })

    $.ajax({
        url: "/Bars/UserRecommendedBars",
        context: document.body
    }).done(function (data) {
        if (data.length > 0) {
            let bars = document.getElementsByClassName("room-card");
            for (i = 1; i < bars.length; i++) {
                var bar_id = Number(bars[i].getAttribute("data-value"));
                if (data.includes(bar_id)) {
                    bars[i].children[0].children[1].classList.add('recommended');
                }
            }
        } else {
            setTimeout(function () {
                var x = document.getElementById("snackbar");
                x.className = "show";
                setTimeout(function () { x.className = x.className.replace("show", ""); }, 4000);
            }, 1000);
        }
    });
});