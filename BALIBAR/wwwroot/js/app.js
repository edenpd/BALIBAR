function create_pie_chart(data, wanted_element) {
    var width = 960,
        height = 400,
        radius = Math.min(width, height) / 2;

    var color = d3.scaleOrdinal(d3.schemeCategory10);

    var arc = d3.arc()
        .outerRadius(radius - 10)
        .innerRadius(0);

    var labelArc = d3.arc()
        .outerRadius(radius - 40)
        .innerRadius(radius - 40);

    var pie = d3.pie()
        .sort(null)
        .value(function (d) { return d.reservations; });

    var svg = d3.select(wanted_element).append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    var g = svg.selectAll(".arc")
        .data(pie(data))
        .enter().append("g")
        .attr("class", "arc");

    g.append("path")
        .attr("d", arc)
        .style("fill", function (d) { return color(d.data.barName); });

    g.append("text")
        .attr("transform", function (d) {                    //set the label's origin to the center of the arc
            //we have to make sure to set these before calling arc.centroid
            d.innerRadius = 0;
            d.outerRadius = radius;
            return "translate(" + arc.centroid(d) + ")";        //this gives us a pair of coordinates like [50, 50]
        })
        .attr("text-anchor", "middle")                          //center the text on it's origin 
        .text(function (d, i) { return data[i].barName + " : " + data[i].reservations; });        //get the label from our original data array
};


function create_bar_chart(data, wanted_element) {
    let months = ["", "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"];
    var margin = { top: 80, right: 80, bottom: 80, left: 80 },
        width = 1000 - margin.left - margin.right,
        height = 400 - margin.top - margin.bottom;
    var x = d3.scaleBand().range([0, width]).padding(0.2);
    var y = d3.scaleLinear().range([height, 0]);

    var xAxis = d3.axisBottom(x);
    // create left yAxis
    var yAxisLeft = d3.axisLeft(y).ticks(4);
    // create right yAxis
    var svg = d3.select(wanted_element).append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("class", "graph")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
    x.domain(data.map(function (d) { return months[d.month]; }));
    y.domain([0, d3.max(data, function (d) { return d.reservations; })]);

    svg.append("g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis);
    svg.append("g")
        .attr("class", "y axis axisLeft")
        .attr("transform", "translate(0,0)")
        .call(yAxisLeft)
        .append("text")
        .attr("y", 0)
        .attr("dy", "-2em")
        .style("text-anchor", "end")
        .style("text-anchor", "end")
        .text("Orders");

    bars = svg.selectAll(".bar").data(data).enter();
    bars.append("rect")
        .attr("class", "bar1")
        .attr("x", function (d) { return x(months[d.month]); })
        .attr("width", x.bandwidth())
        .attr("y", function (d) { return y(d.reservations); })
        .attr("height", function (d, i, j) { return height - y(d.reservations); });
    function type(d) {
        d.reservations = +d.reservations;
        return d;
    }
}
