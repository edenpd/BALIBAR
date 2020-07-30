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
                option.text = genres[i];
                select.add(option);
            }
        }
    })

});