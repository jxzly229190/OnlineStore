var ichartHelper = {
    LineBasic2D: function (option) {
        if (!option || !option.render || !option.data || !option.labels) return;

        option.title = option.title || "";
        option.subtitle = option.subtitle || "";
        option.footnote = option.footnote || "";

        var chart = new iChart.LineBasic2D({
            render: option.render,
            data: option.data,
            align: 'center',
            title: {
                text: option.title, //'ichartjs官方网站最近5天PV流量趋势',
                fontsize: 24,
                color: '#f7f7f7'
            },
            subtitle: {
                text: option.subtitle, //'平均18:00-22:00访问量达到最大值(单位：万)',
                color: '#f1f1f1'
            },
            footnote: {
                text: option.footnote, //'数据来源：模拟数据',
                color: '#f1f1f1'
            },
            width: 1000,
            height: 450,
            shadow: true,
            shadow_color: '#20262f',
            shadow_blur: 4,
            shadow_offsetx: 0,
            shadow_offsety: 2,
            background_color: '#383e46',
            animation: true, //开启过渡动画
            animation_duration: 600, //600ms完成动画
            tip: {
                enable: true,
                shadow: true
            },
            crosshair: {
                enable: true,
                line_color: '#62bce9'
            },
            sub_option: {
                label: false,
                hollow_inside: false,
                point_size: 8
            },
            coordinate: {
                width: 860,
                height: 300,
                grid_color: '#cccccc',
                axis: {
                    color: '#cccccc',
                    width: [0, 0, 2, 2]
                },
                grids: {
                    vertical: {
                        way: 'share_alike',
                        value: option.labels.length - 1
                    }
                },
                scale: [{
                    position: 'left',
                    start_scale: option.start_scale,
                    end_scale: option.end_scale,
                    scale_space: option.scale_space,
                    scale_size: option.scale_size,
                    label: { color: '#ffffff', fontsize: 11 },
                    scale_color: '#9f9f9f'
                }, {
                    position: 'bottom',
                    label: { color: '#ffffff', fontsize: 11 },
                    labels: option.labels
                }]
            }
        });

        //开始画图
        chart.draw();
    }
}