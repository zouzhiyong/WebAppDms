var JsMain = function () {
    var $this = this;

    $this.index = function () {
        var data = [{ DATE: '2017-10-12', TIME: '8：45', VALUE: 1.991 },
{ DATE: '2017-10-14', TIME: '19:15', VALUE: 1.98 },
{ DATE: '2017-10-15', TIME: '19:55', VALUE: 1.969 },
{ DATE: '2017-10-17', TIME: '1:20', VALUE: 1.98 },
{ DATE: '2017-10-18', TIME: '2:13', VALUE: 1.978 },
{ DATE: '2017-10-19', TIME: '2:51', VALUE: 1.973 },
{ DATE: '2017-10-19', TIME: '6:32', VALUE: 1.98 },
{ DATE: '2017-10-19', TIME: '21:59', VALUE: 1.972 },
{ DATE: '2017-10-20', TIME: '20:34', VALUE: 1.996 },
{ DATE: '2017-10-21', TIME: '14:49', VALUE: 1.979 },
{ DATE: '2017-10-24', TIME: '17:49', VALUE: 1.98 },
{ DATE: '2017-10-24', TIME: '20:13', VALUE: 1.982 },
{ DATE: '2017-10-25', TIME: '2:55', VALUE: 1.969 },
{ DATE: '2017-10-25', TIME: '11:22', VALUE: 1.973 },
{ DATE: '2017-10-25', TIME: '21:06', VALUE: 1.98 },
{ DATE: '2017-10-26', TIME: '5:11', VALUE: 1.984 },
{ DATE: '2017-10-26', TIME: '13:11', VALUE: 1.976 },
{ DATE: '2017-10-26', TIME: '22:49', VALUE: 1.983 },
{ DATE: '2017-10-27', TIME: '3:27', VALUE: 1.978 },
{ DATE: '2017-10-27', TIME: '16:25', VALUE: 1.976 },
{ DATE: '2017-10-28', TIME: '2:46', VALUE: 1.976 },
{ DATE: '2017-10-28', TIME: '6:46', VALUE: 1.984 },
{ DATE: '2017-10-29', TIME: '7:53', VALUE: 1.982 },
{ DATE: '2017-10-29', TIME: '21:33', VALUE: 1.985 },
{ DATE: '2017-10-30', TIME: '10:36', VALUE: 1.985 },
{ DATE: '2017-10-31', TIME: '10:12', VALUE: 1.969 },
{ DATE: '2017-11-1', TIME: '17:53', VALUE: 1.974 },
{ DATE: '2017-11-2', TIME: '0:06', VALUE: 1.983 },
{ DATE: '2017-11-2', TIME: '8:53', VALUE: 1.976 },
{ DATE: '2017-11-2', TIME: '11:31', VALUE: 1.984 },
{ DATE: '2017-11-2', TIME: '13:49', VALUE: 1.967 },
{ DATE: '2017-11-2', TIME: '13:54', VALUE: 1.966 },
{ DATE: '2017-11-2', TIME: '13:59', VALUE: 1.965 },
{ DATE: '2017-11-2', TIME: '14:03', VALUE: 1.976 },
{ DATE: '2017-11-2', TIME: '16:34', VALUE: 1.987 },
{ DATE: '2017-11-3', TIME: '3:19', VALUE: 1.983 },
{ DATE: '2017-11-4', TIME: '5:02', VALUE: 1.986 },
{ DATE: '2017-11-4', TIME: '5:57', VALUE: 1.978 },
{ DATE: '2017-11-5', TIME: '6:24', VALUE: 1.986 },
{ DATE: '2017-11-5', TIME: '10:45', VALUE: 1.984 },
{ DATE: '2017-11-5', TIME: '14:57', VALUE: 1.981 },
{ DATE: '2017-11-5', TIME: '19:42', VALUE: 1.995 },
{ DATE: '11-6', TIME: '11:33', VALUE: 1.981 },
{ DATE: '2017-11-6', TIME: '19:30', VALUE: 1.987 },
{ DATE: '2017-11-7', TIME: '9:25', VALUE: 1.984 },
{ DATE: '2017-11-7', TIME: '10:23', VALUE: 1.973 },
{ DATE: '2017-11-8', TIME: '5:09', VALUE: 1.983 },
{ DATE: '2017-11-8', TIME: '10:57', VALUE: 1.983 },
{ DATE: '2017-11-8', TIME: '18:20', VALUE: 1.987 },
{ DATE: '2017-11-9', TIME: '5:04', VALUE: 1.984 },
{ DATE: '11-9', TIME: '10:39', VALUE: 1.982 },
{ DATE: '2017-11-9', TIME: '17:34', VALUE: 1.988 },
{ DATE: '2017-11-9', TIME: '19:25', VALUE: 1.984 },
{ DATE: '2017-11-10', TIME: '7:06', VALUE: 1.983 },
{ DATE: '2017-11-10', TIME: '21:13', VALUE: 1.987 },
{ DATE: '2017-11-11', TIME: '6:10', VALUE: 1.99 },
{ DATE: '11-11', TIME: '9:38', VALUE: 1.98 },
{ DATE: '11-11', TIME: '18:57', VALUE: 1.985 },
{ DATE: '2017-11-12', TIME: '2:39', VALUE: 1.986 },
{ DATE: '2017-11-12', TIME: '10:20', VALUE: 1.984 },
{ DATE: '2017-11-12', TIME: '21:16', VALUE: 1.982 },
{ DATE: '2017-11-13', TIME: '2:46', VALUE: 1.981 },
{ DATE: '2017-11-13', TIME: '10:23', VALUE: 1.984 },
{ DATE: '2017-11-13', TIME: '13:32', VALUE: 1.984 },
{ DATE: '2017-11-13', TIME: '23:39', VALUE: 1.986 },
{ DATE: '2017-11-13', TIME: '19:28', VALUE: 1.987 },
{ DATE: '2017-11-14', TIME: '16:23', VALUE: 1.977 },
{ DATE: '2017-11-14', TIME: '21:42', VALUE: 1.992 },
{ DATE: '2017-11-15', TIME: '9:56', VALUE: 1.985 },
{ DATE: '2017-11-15', TIME: '11:01', VALUE: 1.993 },
{ DATE: '2017-11-15', TIME: '18:02', VALUE: 1.989 },
{ DATE: '2017-11-15', TIME: '21:47', VALUE: 1.997 },
{ DATE: '2017-11-16', TIME: '3:12', VALUE: 1.99 },
{ DATE: '2017-11-16', TIME: '6:41', VALUE: 1.986 },
{ DATE: '2017-11-16', TIME: '10:21', VALUE: 1.983 },
{ DATE: '2017-11-16', TIME: '11:06', VALUE: 1.977 },
{ DATE: '2017-11-16', TIME: '14:45', VALUE: 2 },
{ DATE: '2017-11-16', TIME: '20:32', VALUE: 1.985 },
{ DATE: '2017-11-16', TIME: '22:18', VALUE: 2.007 },
{ DATE: '2017-11-17', TIME: '5:18', VALUE: 2.005 },
{ DATE: '2017-11-17', TIME: '6:04', VALUE: 1.995 },
{ DATE: '2017-11-17', TIME: '11:16', VALUE: 1.986 },
{ DATE: '2017-11-17', TIME: '22:40', VALUE: 1.981 },
{ DATE: '2017-11-17', TIME: '18:02', VALUE: 1.984 },
{ DATE: '2017-11-17', TIME: '18:47', VALUE: 1.974 },
{ DATE: '2017-11-19', TIME: '2:20', VALUE: 1.968 },
{ DATE: '2017-11-19', TIME: '5:19', VALUE: 1.975 },
{ DATE: '2017-11-19', TIME: '9:55', VALUE: 1.987 },
{ DATE: '2017-11-20', TIME: '5:21', VALUE: 1.987 },
{ DATE: '2017-11-20', TIME: '9:30', VALUE: 1.998 },
{ DATE: '2017-10-20', TIME: '14:43', VALUE: 1.992 },
{ DATE: '2017-10-21', TIME: '10:38', VALUE: 1.99 },
{ DATE: '2017-10-21', TIME: '15:25', VALUE: 1.991 },
{ DATE: '2017-11-21', TIME: '21:14', VALUE: 1.978 },
{ DATE: '2017-11-22', TIME: '6:41', VALUE: 1.988 },
{ DATE: '2017-11-22', TIME: '10:54', VALUE: 1.994 },
{ DATE: '2017-11-22', TIME: '13:48', VALUE: 1.993 },
{ DATE: '2017-11-23', TIME: '6:51', VALUE: 1.988 },
{ DATE: '2017-11-23', TIME: '11:38', VALUE: 1.987 },
{ DATE: '2017-11-23', TIME: '14：43', VALUE: 1.984 },
{ DATE: '2017-11-24', TIME: '11:23', VALUE: 1.986 },
{ DATE: '2017-11-24', TIME: '17:50', VALUE: 1.979 },
{ DATE: '2017-11-25', TIME: '5:02', VALUE: 1.992 },
{ DATE: '2017-11-25', TIME: '19:56', VALUE: 1.993 },
{ DATE: '2017-11-25', TIME: '23:01', VALUE: 1.994 },
{ DATE: '2017-11-26', TIME: '21:01', VALUE: 1.995 },
{ DATE: '2017-11-26', TIME: '22:59', VALUE: 1.983 },
{ DATE: '2017-11-27', TIME: '4:03', VALUE: 1.993 },
{ DATE: '2017-11-27', TIME: '5:28', VALUE: 1.986 },
{ DATE: '2017-11-27', TIME: '10:43', VALUE: 1.994 },
{ DATE: '2017-11-28', TIME: '11:01', VALUE: 1.993 },
{ DATE: '2017-11-29', TIME: '1:21', VALUE: 1.984 },
{ DATE: '2017-11-29', TIME: '15:10', VALUE: 1.996 },
{ DATE: '2017-11-29', TIME: '21:32', VALUE: 1.978 },
{ DATE: '2017-11-29', TIME: '21:37', VALUE: 1.996 },
{ DATE: '2017-12-1', TIME: '11:11', VALUE: 1.987 },
{ DATE: '2017-12-1', TIME: '12:05', VALUE: 1.978 },
{ DATE: '2017-12-1', TIME: '18:45', VALUE: 1.989 },
{ DATE: '2017-12-2', TIME: '21:21', VALUE: 1.982 },
{ DATE: '2017-12-4', TIME: '0:45', VALUE: 1.983 },
{ DATE: '2017-12-5', TIME: '8:49', VALUE: 1.992 },
{ DATE: '2017-12-5', TIME: '16:37', VALUE: 1.978 },
{ DATE: '2017-12-7', TIME: '1:42', VALUE: 1.978 },
{ DATE: '2017-12-7', TIME: '4:44', VALUE: 1.972 },
{ DATE: '2017-12-7', TIME: '7:10', VALUE: 1.986 },
{ DATE: '2017-12-7', TIME: '10:38', VALUE: 1.981 },
{ DATE: '2017-12-7', TIME: '14:26', VALUE: 1.98 },
{ DATE: '2017-12-7', TIME: '15:35', VALUE: 1.977 },
{ DATE: '2017-12-7', TIME: '23:28', VALUE: 1.984 },
{ DATE: '2017-12-8', TIME: '10:28', VALUE: 1.987 },
{ DATE: '2017-12-9', TIME: '20:11', VALUE: 1.984 },
{ DATE: '2017-12-10', TIME: '1:16', VALUE: 1.996 },
{ DATE: '12-10', TIME: '21:07', VALUE: 1.98 },
{ DATE: '12-11', TIME: '4:14', VALUE: 1.977 },
{ DATE: '12-11', TIME: '6:37', VALUE: 1.992 },
{ DATE: '12-11', TIME: '13:45', VALUE: 1.976 },
{ DATE: '2017-12-11', TIME: '18:10', VALUE: 1.982 },
{ DATE: '2017-12-11', TIME: '22:33', VALUE: 1.996 },
{ DATE: '2017-12-12', TIME: '17:59', VALUE: 1.979 },
{ DATE: '2017-12-12', TIME: '19:23', VALUE: 1.995 },
{ DATE: '2017-12-13', TIME: '5:14', VALUE: 1.984 },
{ DATE: '2017-12-13', TIME: '10:00', VALUE: 1.985 },
{ DATE: '2017-12-13', TIME: '10:56', VALUE: 1.974 },
{ DATE: '2017-12-14', TIME: '11:00', VALUE: 1.983 },
{ DATE: '2017-12-14', TIME: '11:56', VALUE: 1.973 },
{ DATE: '2017-12-15', TIME: '19:03', VALUE: 1.989 },
{ DATE: '2017-12-15', TIME: '22:44', VALUE: 1.982 },
{ DATE: '2017-12-18', TIME: '16:09', VALUE: 1.975 }];

        var data1 = [];
        var data2 = [];
        var index = 0;
        var dataWarn = [];
        var dataRepeat = [];
        data.map(function (item, index) {
            item.ID = index + 1;
            item.STATUS = '';
            data1.push(item.DATE);
            data2.push(item.VALUE);
        })

        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('main'));


        // 指定图表的配置项和数据
        var max = Math.max.apply(null, data2);
        var min = Math.min.apply(null, data2);
        var mid = (max - min) / 2 + min;
        $("#UCL").val(max.toFixed(4));
        $("#PCL").val(mid.toFixed(4));
        $("#LCL").val(min.toFixed(4));
        $("#uclSpan").text(max.toFixed(4));
        $("#pclSpan").text(mid.toFixed(4));
        $("#lclSpan").text(min.toFixed(4));
        var option = {
            grid: {
                top: 40,    //距离容器上边界40像素
                bottom: 30   //距离容器下边界30像素
            },
            textStyle: {
                color: '#3333cc',
                fontSize: 12,
                fontFamily: 'microsoft yahei light',
                fontWeight: "bold"
            },
            toolbox: {
                show: true,
                feature: {
                    dataZoom: {
                        yAxisIndex: 'none'
                    },
                    dataView: { readOnly: false },
                    magicType: { type: ['line'] },
                    restore: {},
                    saveAsImage: {}
                }
            },
            tooltip: {
                trigger: 'axis'
            },
            xAxis: {
                type: 'category',
                data: data1,
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: '#eee'
                    }
                },
                axisLabel: {
                    interval: 0,//横轴信息全部显示
                    formatter: function (value, index) {
                        // 格式化成月/日，只在第一个刻度显示年份
                        if (index % 5 == 1) {
                            return index;
                        } else {
                            return ""
                        }

                    },
                    color: "#3333cc",
                    rotate: -90,
                },
                axisTick: {
                    show: false
                },
                position: 'top'
            },
            yAxis: {
                max: function (value) {
                    var temp = (function () { return max })();
                    if (temp > value.max) {
                        return temp;
                    } else {
                        return value.max;
                    }
                },
                min: function (value) {
                    if (min < value.min) {
                        return min;
                    } else {
                        return value.min;
                    }
                },
                axisLabel: {
                    formatter: function (value, index) {
                        return value.toFixed(4);
                    },
                    color: "#3333cc"
                },
                splitNumber: 10,
                splitArea: {
                    show: true,
                    areaStyle: {
                        color: ["#fff"]
                    }
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: '#eee'
                    }
                },
            },

            dataZoom: [
                {
                    type: 'inside'
                }
            ],
            series: [{
                symbol: 'circle',
                symbolSize: 8,
                itemStyle: {
                    normal: {
                        color: function (par) {
                            if (par.data > max || par.data < min) {
                                return "red"
                            } else {
                                return "#000"
                            }
                        },

                        lineStyle: {
                            color: "#3FA7DC",
                            width: 1,
                        }
                    }
                },
                data: data2,
                type: 'line',
                markLine: {
                    lineStyle: {
                        width: 1,
                        type: 'solid'
                    },
                    data: [
                        {
                            yAxis: max.toFixed(4),
                            lineStyle: {
                                color: 'red'
                            }
                        },
                        {
                            yAxis: mid.toFixed(4),
                            lineStyle: {
                                color: 'green'
                            }
                        },
                        {
                            yAxis: min.toFixed(4),
                            lineStyle: {
                                color: 'red'
                            }
                        }
                    ]
                }
            }]
        };


        // 使用刚指定的配置项和数据显示图表。
        myChart.clear();
        myChart.setOption(option);
        var tableAll = $('#tableAll');
        var tableWarn = $('#tableWarn');
        var tableRepeat = $('#tableRepeat');
        tableAll.bootstrapTable({ data: data });
        tableWarn.bootstrapTable({ data: dataWarn });
        tableRepeat.bootstrapTable({ data: dataRepeat });

        //点击事件
        myChart.on('click', function (params) {
            var arr = data.filter(function (item, index) {
                return item.VALUE == params.data;
            })
            tableRepeat.bootstrapTable('load', arr);
        });

        $("body").on('input propertychange', function () {
            max = parseFloat($("#UCL").val()).toFixed(4);
            mid = parseFloat($("#PCL").val()).toFixed(4);
            min = parseFloat($("#LCL").val()).toFixed(4);

            var series = [{
                symbol: 'circle',
                symbolSize: 8,
                itemStyle: {
                    normal: {
                        color: function (par) {
                            if (par.data > max || par.data < min) {
                                return "red"
                            } else {
                                return "#000"
                            }
                        },

                        lineStyle: {
                            color: "#3FA7DC",
                            width: 1,
                        }
                    }
                },
                data: data2,
                type: 'line',
                markLine: {
                    lineStyle: {
                        width: 1,
                        type: 'solid'
                    },
                    data: [
                        {
                            yAxis: max,
                            lineStyle: {
                                color: 'red'
                            }
                        },
                        {
                            yAxis: mid,
                            lineStyle: {
                                color: 'green'
                            }
                        },
                        {
                            yAxis: min,
                            lineStyle: {
                                color: 'red'
                            }
                        }
                    ]
                }
            }];
            option.series = series;

            myChart.clear();
            myChart.setOption(option);

            //
            dataWarn = [];
            var index = 0;
            var upCount = 0;;
            var lCount = 0;
            data.map(function (item) {
                if (item.VALUE.toFixed(4) > max || item.VALUE.toFixed(4) < min) {
                    var obj = JSON.parse(JSON.stringify(item));
                    index = index + 1;
                    obj.ID = index;

                    if (item.VALUE.toFixed(4) > max) {
                        obj.STATUS = 'U';
                        upCount = upCount + 1;
                    }

                    if (item.VALUE.toFixed(4) < min) {
                        obj.STATUS = 'L';
                        lCount = lCount + 1;
                    }
                    dataWarn.push(obj);
                }
            })

            $(".uclDanger").text(upCount);
            $(".lclDanger").text(lCount);

            tableWarn.bootstrapTable('load', dataWarn);

        });
    }
}