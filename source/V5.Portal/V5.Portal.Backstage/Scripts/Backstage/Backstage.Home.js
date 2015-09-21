var SystemVisitor = {
    Init: function (option) {
        SystemVisitor.SetOption(option);
        SystemVisitor.LoadEvent();
        //SystemVisitor.GetVisitorPV();
    },

    SetOption: function () {
        $("#time").kendoDropDownList();
        $("#year").kendoDropDownList();
        $("#season").kendoDropDownList();
        $("#month").kendoDropDownList();

        var day = $("#day")[0];
        var count = SystemVisitor.GetDaysInMonth(new Date().getFullYear(), new Date().getMonth());
        for (var i = 1; i <= count; i++) {
            var item = new Option(i + "号", i);
            day.options.add(item);
        }
        $("#day").kendoDropDownList();
    },

    LoadEvent: function () {
        var company = "购酒网";

        $("#time").change(function () {
            var time = $(this).val();
            var day = (time == "day3" ? 4 : 6);
            var begdt = new Date();
            var enddt = new Date();
            begdt.setDate(begdt.getDate() - day);

            begdt = begdt.getFullYear() + "/" + (begdt.getMonth() + 1) + "/" + begdt.getDate();
            enddt = enddt.getFullYear() + "/" + (enddt.getMonth() + 1) + "/" + enddt.getDate();

            var option = { startTime: begdt, endTime: enddt, condition: "day" };
            SystemVisitor.GetVisitorCount(option, function (data) {
                var mylabels = [];
                for (var i = day - 1; i >= 0; i--) {
                    var dt = new Date();
                    dt.setDate(dt.getDate() - i);
                    mylabels.push(dt.getFullYear() + "/" + (dt.getMonth() + 1) + "/" + dt.getDate());
                }
                SystemVisitor.BindVisitorData(time, "近" + (day - 1) + "日", data, mylabels);
            });
        }).change();

        //年
        $("#year").change(function () {
            var year = $(this).val();
            var begdt = year + "/1/1";
            var enddt = year + "/12/31";

            var option = { startTime: begdt, endTime: enddt, condition: "year" };
            SystemVisitor.GetVisitorCount(option, function (data) {
                SystemVisitor.BindVisitorData("year", year + "年", data);
            });
        });

        //季
        $("#season").change(function () {
            var year = $("#year").val();
            var season = $(this).val();

            var begdt = year + "/" + ((season - 1) * 3 + 1) + "/1";
            var enddt = year + "/" + season * 3 + "/" + SystemVisitor.GetDaysInMonth(year, season * 3);

            var option = { startTime: begdt, endTime: enddt, condition: "season" };
            SystemVisitor.GetVisitorCount(option, function (data) {
                var mylabels = SystemVisitor.CreateArray(3);
                for (var i = 0; i < 3; i++) {
                    mylabels[i] = year + "/" + ((season - 1) * 3 + 1 + i);
                }

                var flow = SystemVisitor.CreateArray(3, 0);
                var min = data.length > 0 ? data[0].count : 0;
                var max = data.length > 0 ? data[0].count : 0;

                for (var i = 0; i < data.length || i < 3; i++) {
                    if (data[i]) {
                        flow[i] = data[i].count;

                        min = data[i].count > min ? min : data[i].count;
                        max = data[i].count < max ? max : data[i].count;
                    }
                }

                var mydata = [
		         	{
		         	    name: 'PV',
		         	    value: flow,
		         	    color: '#0d8ecf',
		         	    line_width: 2
		         	}
		         ];

                var option = {
                    render: "canvasDiv",
                    data: mydata,
                    labels: mylabels,
                    title: SystemVisitor.GetTitle(year + "年第" + season + "季度"),
                    subtitle: SystemVisitor.GetSubTitle(year + "年第" + season + "季度", min, max),
                    footnote: ""
                }

                if (time == "day3") {
                    option.scale_size = 3;
                }
                ichartHelper.LineBasic2D(option);
            });
        });

        //月
        $("#month").change(function () {
            var year = $("#year").val();
            var month = Number($(this).val());

            var begdt = year + "/" + month + "/1";
            var enddt = year + "/" + month + "/" + SystemVisitor.GetDaysInMonth(year, month);

            var option = { startTime: begdt, endTime: enddt, condition: "month" };
            SystemVisitor.GetVisitorCount(option, function (data) {
                SystemVisitor.BindVisitorData("month", year + "年" + month + "月份", data);
            });
        });

        //天
        $("#day").change(function () {
            var year = $("#year").val();
            var month = Number($("#month").val());
            var day = $(this).val();

            var begdt = year + "/" + month + "/" + day;
            var enddt = year + "/" + month + "/" + (Number(day) + 1);

            var option = { startTime: begdt, endTime: enddt, condition: "day" };
            SystemVisitor.GetVisitorCount(option, function (data) {
                SystemVisitor.BindVisitorData("day", year + "年" + month + "月" + day + "日", data);
            });
        });
    },

    GetVisitorPV: function () {
        setInterval(function () {
            $("#record_dt").text(new Date().toLocaleString());
        }, 1000);

        setInterval(function () {
            $.ajax({
                type: "POST",
                url: "/Home/SystemVisitorPV",
                success: function (data) {
                    $("#record_pv").text(data);
                }
            });
        }, 3600);
    },

    GetVisitorCount: function (data, fn) {
        $.ajax({
            type: "POST",
            url: "/Home/SystemVisitorCount",
            data: data,
            success: function (data) {
                if ($.isFunction(fn)) {
                    fn(data);
                }
            }
        });
    },

    BindVisitorData: function (time, fvalue, data, mylabels) {
        var len = 0;
        switch (time) {
            case "year":
                len = 12;
                break;
            case "month":
                len = 31
                break;
            case "day":
                len = 24;
                break;
            case "day3":
                len = 3;
                break;
            case "day5":
                len = 5;
                break;
        }

        mylabels = mylabels || SystemVisitor.CreateArray(len);

        switch (time) {
            case "day":
                var labels = mylabels.join(',');
                labels = "0," + labels.replace(",24", "");
                mylabels = labels.split(',');
                break;
            case "year":
                var year = fvalue.replace("年", "");
                for (var i = 0; i < mylabels.length; i++) {
                    mylabels[i] = year + "/" + mylabels[i];
                }
                break;
        }

        var flow = SystemVisitor.CreateArray(len, 0);
        var min = data.length > 0 ? data[0].count : 0;
        var max = data.length > 0 ? data[0].count : 0;
        for (var i = 0; i < data.length || i < len; i++) {
            if (data[i]) {
                flow[i] = data[i].count;
                min = data[i].count > min ? min : data[i].count;
                max = data[i].count < max ? max : data[i].count;
            }
        }

        var mydata = [
		         	{
		         	    name: 'PV',
		         	    value: flow,
		         	    color: '#0d8ecf',
		         	    line_width: 2
		         	}
		         ];

        var option = {
            render: "canvasDiv",
            data: mydata,
            labels: mylabels,
            title: SystemVisitor.GetTitle(fvalue),
            subtitle: SystemVisitor.GetSubTitle(fvalue, min, max),
            footnote: ""
        }
        ichartHelper.LineBasic2D(option);
    },

    GetTitle: function (time) {
        return "购酒网" + (time || "") + "PV流量趋势";
    },

    GetSubTitle: function (time, min, max) {
        return (time || "") + "访问量,峰值为" + (max || "0") + ",谷值为" + (min || "0") + ".";
    },

    CreateArray: function (len, def) {
        var data = new Array(len);
        for (var i = 0; i < len; i++) {
            data[i] = isNaN(def) ? i + 1 : 0;
        }
        return data;
    },

    GetDaysInMonth: function (year, month) {
        month = parseInt(month, 10);
        var d = new Date(year, month, 0);
        return d.getDate();
    }
} 
