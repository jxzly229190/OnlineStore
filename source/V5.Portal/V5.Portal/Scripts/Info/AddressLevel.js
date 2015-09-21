(function ($) {
    var Areas = function (ele, opt) {
        this.$element = ele,
                this.defaults = {
                    province: "province",
                    city: "city",
                    country: "country",
                    countryId: "-1"
                },
                this.options = $.extend({}, this.defaults, opt);
    };
    Areas.prototype = {
        provinceUrl: "../Utility/GetProvince",
        cityUrl: "../Utility/GetCity",
        countryUrl: "../Utility/GetCountry",
        changeProvinceId: 0,
        changeCityId: 0,
        changeCountryId: 0,
        CounData: Object,
        CData: Object,
        PData: Object,
        initialize: function () {
            this.AreasData();
            this.Province();
            this.City();
            this.Country();
            this.AttachEvent();
            this.GetSimilarCountry();

        },
        AreasData: function () {
            var ajaxThis = this;
            $.ajax({
                type: "POST",
                async: false,
                url: ajaxThis.provinceUrl,
                dataType: "json",
                success: function (data) {
                    ajaxThis.PData = data.Data;
                }
            });
            $.ajax({
                type: "POST",
                async: false,
                url: ajaxThis.cityUrl,
                dataType: "json",
                success: function (data) {
                    ajaxThis.CData = data.Data;
                }
            });
            $.ajax({
                type: "POST",
                async: false,
                url: ajaxThis.countryUrl,
                dataType: "json",
                success: function (data) {
                    ajaxThis.CounData = data.Data;
                }
            });
        },
        //绑定事件
        AttachEvent: function () {
            var oThis = this;
            $("#" + this.options.province + "").change(function () {
                oThis.changeProvinceId = $(this).val();
                oThis.City();
            });
            $("#" + this.options.city + "").change(function () {
                oThis.changeCityId = $(this).val();
                oThis.Country();
            });
            $("#" + this.options.country + "").change(function () { });
        },
        //加载城市 
        City: function () {
            var html = "", length = this.CData.length, CData = this.CData;
            html += "<option value='0' selected='selected'>请选择市/地区</option>";
            for (var i = 0; i < length; i++) {
                if (CData[i].ProvinceID == this.changeProvinceId) {
                    html += "<option value='" + CData[i].ID + "'>" + CData[i].Name + "</option>";
                }
            }
            $("#" + this.options.city + "").html(html);
        },
        //加载省
        Province: function () {
            var html = "", PData = this.PData, length = PData.length;
            html += "<option value='0' selected='selected'>请选择省/直辖市</option>";
            for (var i = 0; i < length; i++) {
                html += "<option value='" + PData[i].ID + "'>" + PData[i].Name + "</option>";
            }
            $("#" + this.options.province + "").html(html);
        },
        //加载区
        Country: function () {
            var html = "", CounData = this.CounData, length = CounData.length;
            html += "<option value='0' selected='selected'>请选择市区/县</option>";
            for (var i = 0; i < length; i++) {
                if (CounData[i].CityID == this.changeCityId) {
                    html += "<option value='" + CounData[i].ID + "'>" + CounData[i].Name + "</option>";
                }
            }
            $("#" + this.options.country + "").html(html);
        },
        GetSimilarProvince: function () {
            var html = "<option value='0' selected='selected'>请选择省/直辖市</option>", PData = this.PData, length = PData.length;
            var provinceId = parseInt(this.changeProvinceId);
            for (var i = 0; i < length; i++) {
                if (PData[i].ID == provinceId) {
                    html += "<option selected='selected' value='" + PData[i].ID + "'>" + PData[i].Name + "</option>";
                }
                html += "<option value='" + PData[i].ID + "'>" + PData[i].Name + "</option>";
            }
            $("#" + this.options.province + "").html(html);
        },
        GetSimilarCity: function () {
            var html = "<option value='0' selected='selected'>请选择市/地区</option>";
            var CData = this.CData, length = CData.length;
            var cityId = parseInt(this.changeCityId);
            for (var i = 0; i < length; i++) {
                if (CData[i].ID == cityId) {
                    this.changeProvinceId = CData[i].ProvinceID;
                    for (var j = 0; j < length; j++) {
                        if (CData[j].ProvinceID == this.changeProvinceId) {
                            if (CData[j].ID == this.changeCityId) {
                                html += "<option value='" + CData[j].ID + "' selected='selected''>" + CData[j].Name + "</option>"
                            }
                            else {
                                html += "<option>" + CData[j].Name + "</option>";
                            }
                        }
                    }
                }
            }
            this.GetSimilarProvince();
            $("#" + this.options.city + "").html(html);
        },
        GetSimilarCountry: function () {
            var html = "<option value='0' selected='selected'>请选择市区/县</option>", cityId;
            var CounData = this.CounData, length = CounData.length;
            var countryId = parseInt(this.options.countryId);
            if (countryId == -1) return;
            for (var i = 0; i < length; i++) {
                if (CounData[i].ID == countryId) {
                    cityId = CounData[i].CityID;
                    for (var j = 0; j < length; j++) {
                        if (CounData[j].CityID == cityId) {
                            if (CounData[j].ID == countryId) {
                                html += "<option value='" + CounData[j].ID + "'selected='selected'>" + CounData[j].Name + "</option>";
                                this.changeCityId = CounData[i].CityID;
                            } else {
                                html += "<option value='" + CounData[j].ID + "'>" + CounData[j].Name + "</option>";
                            }
                        }
                    }
                }
            }
            this.GetSimilarCity();
            $("#" + this.options.country + "").html(html);
        }
    };
    $.fn.myAreas = function (options) {
        var area = new Areas(this, options);
        return area.initialize();
    };
})(jQuery);