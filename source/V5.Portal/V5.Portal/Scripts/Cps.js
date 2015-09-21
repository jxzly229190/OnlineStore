$(function () {
    if ($.cookie("CPS_IN")) {
        var html;
        html = "<script type=\"text/javascript\"> \n";
        html += "  var _adwq = _adwq || []; \n";
        html += "  _adwq.push(['_setAccount', 'lbi2m']); \n";
        html += "  _adwq.push(['_setDomainName', 'gjw.com']); \n";
        html += "  _adwq.push(['_trackPageview']); \n";
        html += "</script> \n";
        html += "<script type=\"text/javascript\" src=\"http://d.emarbox.com/js/adw.js?adwa=lbi2m\"></script>";

        $("body").append(html);
    }
})