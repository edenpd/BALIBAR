$(function () {

    // Init bar list
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: "/Types/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#typesList").html(data);
            }
        })

    });


    // Bar list
    $("#searchButton").click(function () {
        $.ajax({
            type: 'GET',
            url: "/Types/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#typesList").html(data);
            }
        })
    });


    $("#nameForm").keyup(function () {
        $.ajax({
            type: 'GET',
            url: "/Types/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#typesList").html(data);
            }
        })
    })

    $("#musicForm").keyup(function () {
        $.ajax({
            type: 'GET',
            url: "/Types/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#typesList").html(data);
            }
        })
    })


    $("#searchForm").change(function () {
        $.ajax({
            type: 'GET',
            url: "/Types/Search",
            data: $("#searchForm").serialize(),
            success: function (data) {
                $("#typesList").html(data);
            }
        })
    })
});