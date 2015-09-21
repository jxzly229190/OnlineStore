if ($("#hidClock").length > 0) {
    var totalsec = $("#hidClock").val();
    if (totalsec > 0) {
        var tmr_clock = setInterval(function () {
            if (totalsec == 1) {
                clearInterval(tmr_clock);
                window.location.reload();
            }
            else if (totalsec < 1) {
                clearInterval(tmr_clock);
            }
            else {
                var sec = totalsec % 60;
                var totalmin = (totalsec - sec) / 60;
                var min = totalmin % 60;
                var hou = (totalmin - min) / 60;
                var str = hou;
                if (hou < 10) str = "0" + str;
                $("#crazy_time_h").html(str+"时");
                str = min;
                if (min < 10) str = "0" + str;
                $("#crazy_time_m").html(str+"分");
                str = sec;
                if (sec < 10) str = "0" + str;
                $("#crazy_time_s").html(str+"秒");
                totalsec--;
            }
        }, 1000)
    } 
}