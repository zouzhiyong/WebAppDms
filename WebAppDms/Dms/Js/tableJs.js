$.fn.extend({
    bootStrapTableInit: function (options) {
        $el = $(this);
        $el.options = options;
        $el.defaults = {
            url: null,
            initDataShow: true,//初次加载是否连接服务器查询数据
            data: { "rows": [], "pagenumber": 1, "pagesize": 20, "total": null },//首次加载不连服务器
            ExportObj: "#ExportObj",//导出按钮id
            method: 'post',
            dataType: 'json',
            height: $(window).height() - $el.offset().top - 5,
            pageSize: Math.ceil(($(window).height() - $el.offset().top - 110) / 32),
            pageNumber: 1,
            LoadingMessage:false,//是否显示默认加载内容
            striped: false,
            pagination: true,
            halign: 'center',
            valign: 'middle',
            align: 'center',
            showToggle: false,
            ajaxOptions: {},
            pageList: [],
            showExport: false,
            paginationPreText: "上一页",
            paginationNextText: "下一页",
            exportDataType: "all",
            paginationDetailHAlign: 'left',
            idField: 'Id', //标识哪个字段为id主键
            uniqueId: 'Id',
            cardView: false,
            showColumns: false, //显示隐藏列
            showRefresh: false,  //显示刷新按钮
            singleSelect: false,//复选框只能选择一条记录
            search: false,//是否显示右上角的搜索框
            clickToSelect: false,//点击行即可选中单选/复选框
            sidePagination: 'server',//表格分页的方式
            queryParamsType: '',//参数格式,发送标准的RESTFul类型的参数请求
            //toolbar: "#tools", //设置工具栏的Id或者class
            silent: true,  //刷新事件必须设置
            detailView: false,//父子表
            iconSize: 'outline',
            contentType: "application/x-www-form-urlencoded",
            queryParams: function (param) {
                var model = {
                    "pageSize": param.pageSize,
                    "pageNumber": param.pageNumber,
                    "sortName": param.sortName,
                    "sortOrder": param.sortOrder,
                };

                if ($el.options.QueryParams != null) {
                    return $.extend({}, model, $el.options.QueryParams);
                }
                else {
                    return model;
                }
            },
            columns: [],
            minimumCountColumns: 1,
            formatLoadingMessage: function () {
                if ($el.options.LoadingMessage) {
                    return "请稍等，正在加载中...";
                }                
            },
            formatNoMatches: function () {
                //$(".fixed-table-body .table").css("border-bottom", "none");  

                return "<div style='padding:50px;'><img src='" + getUrl("/Content/_Layout/img/NoMatches.png") + "'/></div>";
            },
            onLoadSuccess: function (data) {

                //layer.msg("加载成功");
                if ($el.options.height == undefined) {
                    if ($el.bootstrapTable('getOptions').data.length > 0) {
                        $el.bootstrapTable('resetView', { height: $(window).height() - $el.offset().top - 25 });//随窗口变化改变高度
                    }
                    else {
                        $el.bootstrapTable('resetView', { height: $(window).height() - $el.offset().top - 25 });//随窗口变化改变高度
                    }
                }

            },
            onLoadError: function (data) {
                //layer.msg("加载数据失败", { time: 1500, icon: 2 });
            },
            onClickRow: function (row) {

            },
            onDblClickRow: function (row, $element) {
                $element.find('.btn.edit').triggerHandler('click');
            },
            onRefresh: function () {

            },
        }

        $el.opts = $.extend({}, $el.defaults, $el.options);
        var urlTemp = $el.opts.url;//先将URL赋值给临时变量
        if (!$el.opts.initDataShow) {
            $el.opts.url = $el.defaults.url;
        }

        $el.init = function () {
            $el.bootstrapTable('destroy');
            $el.bootstrapTable($el.opts);
            $el.bootstrapTable('getOptions').url = urlTemp;
            $el.tableShowHideColumn();
            $el.Export($el);

            //自适应高度
            if ($el.options.height == undefined) {
                if ($el.bootstrapTable('getOptions').data.length > 0) {
                    $el.bootstrapTable('resetView', { height: $(window).height() - $el.offset().top - 25 });//随窗口变化改变高度
                }
                else {
                    $el.bootstrapTable('resetView', { height: $(window).height() - $el.offset().top - 25 });//随窗口变化改变高度
                }
                $(window).resize(function () {
                    if ($el.bootstrapTable('getOptions').data.length > 0) {
                        $el.bootstrapTable('resetView', { height: $(window).height() - $el.offset().top - 25 });//随窗口变化改变高度
                    }
                    else {
                        $el.bootstrapTable('resetView', { height: $(window).height() - $el.offset().top - 25 });//随窗口变化改变高度
                    }
                })
            }

            //键盘选择上下行，实际是选择第一列的checkbox
            if ($el.opts.clickToSelect == true && $el.opts.singleSelect==true) {
                $el.delegate(":input:checkbox:not(:disabled)", "keydown", function (event) {
                    //向下方向键
                    if (event.keyCode == 40) {
                        var $input = $('.table input:checkbox:not(:disabled)');
                        var n = $input.length;
                        var nextIndex = $input.index($(event.target)) + 1;
                        if (nextIndex < n) {
                            $input[nextIndex].focus();
                            $input[nextIndex].click();
                        }
                        else {
                            $el.bootstrapTable('nextPage');
                            setTimeout(function () {
                                $input = $('.table input:checkbox:not(:disabled)');
                                $input[0].focus();
                                $input[0].click();
                            }, 500);
                        }
                        return false;
                    }

                    //向上方向键
                    if (event.keyCode == 38) {
                        var $input = $('.table input:checkbox:not(:disabled)');
                        var n = $input.length;
                        var prevIndex = $input.index($(event.target)) - 1;
                        if (prevIndex == -1) {
                            $el.bootstrapTable('prevPage');                            
                            setTimeout(function () {
                                $input = $('.table input:checkbox:not(:disabled)');
                                n = $input.length-1;
                                $input[n].focus();
                                $input[n].click();
                            }, 500);                            
                        }
                        else {
                            $input[prevIndex].focus();
                            $input[prevIndex].click();
                        }
                        return false;
                    }

                    ////向下方向键
                    //if (event.keyCode == "40") {
                    //    var nxtIdx = $(":input:checkbox").index($(event.target)) + 1;
                    //    var obj = $(":input:checkbox:eq(" + nxtIdx + ")");

                    //    if (obj.attr("data-index")) {
                    //        obj.click();
                    //        obj.focus();                            
                    //    } else {
                    //        $el.bootstrapTable('nextPage');
                    //        setTimeout(function () {
                    //            obj = $(":input:checkbox:eq(0)");
                    //            obj.click();
                    //            obj.focus();                                
                    //        }, 500);
                    //    }
                    //    return false;
                    //};
                    ////向上方向键
                    //if (event.keyCode == "38") {
                    //    var nxtIdx = $(":input:checkbox").index($(event.target)) - 1;
                    //    var obj = $(":input:checkbox:eq(" + nxtIdx + ")");
                    //    if (nxtIdx == -1) {
                    //        $el.bootstrapTable('prevPage');
                    //        setTimeout(function () {
                    //            obj = $(":input:checkbox:eq(0)");
                    //            obj.click();
                    //            obj.focus();
                    //        }, 500);
                    //    } else {                            
                    //        obj.click();
                    //        obj.focus();
                    //    }
                        
                    //    return false;
                    //};

                    
                });
            }
        }

        //查找后重新加载,不带参数
        $el.refresh = function () {

            var option = $el.bootstrapTable('getOptions');
            option.pageNumber = 1;

            $el.bootstrapTable('refresh');
        }

        //查找后重新加载，带参数
        $el.refresh = function (options) {
            var _$el = this;            
            var option = _$el.bootstrapTable('getOptions');
            option.pageNumber = 1;
            _$el.options.QueryParams = $.extend({}, _$el.options.QueryParams, options);
            $el.options = _$el.options;
            _$el.bootstrapTable('refresh');
        }
     
        $el.Export = function (_$el_) {
            //console.log(_$el_);
            if (_$el_.opts.ExportObj == null) return;
            var $obj = $(_$el_.opts.ExportObj);
            var objHtml = '<div class="btn-group">' +
'<a href="javascript:;" class="btn widget-btn-icon dropdown-toggle" data-toggle="dropdown">' +
'<i class="iconfont icon-export"></i> 导出' +
'</a>' +
'<ul class="dropdown-menu" role="menu"  id="tableExport' + _$el_.attr("id") + '">' +
//'<li data-type="csv"><a href="javascript:void(0)" ondragstart="return false">CSV</a></li>' +
//'<li data-type="txt"><a href="javascript:void(0)" ondragstart="return false">TXT</a></li>' +
'<li data-type="xlsx"><a href="javascript:void(0)" ondragstart="return false">MS-Excel 2007 xlsx</a></li>' +
'<li data-type="excel"><a href="javascript:void(0)" ondragstart="return false">MS-Excel 2003 xls</a></li>' +
'</ul>' +
'</div>';
            $obj.html(objHtml);


            //表格导出事件
            $('#tableExport' + _$el_.attr("id")+' li').find('a').click(function () {
          
                _$this = this;
                if (_$el_.bootstrapTable('getOptions').exportDataType === 'all') {
                    //if($table.bootstrapTable('getOptions').pagination){                 
                    //    $table.one('post-body.bs.table', function () {
                    //        $table.tableExport({ type: $(this).parent("li[data-type]").attr("data-type"), escape: 'false' });
                    //        $table.bootstrapTable('togglePagination');
                    //    });
                    //    $table.bootstrapTable('togglePagination');
                    //}

                    _$el_.one('post-body.bs.table', function () {
                        var options = {
                            //ignoreRow: [1,11,12,-2],
                            //ignoreColumn: [0,-1],
                            //pdfmake: {enabled: true},
                            type: $(_$this).parent("li[data-type]").attr("data-type"),
                            escape: 'false', 
                            ignoreColumn: ["operate", "isCkeck"],
                            jspdf: {
                                orientation: 'l',
                                margins: { right: 10, left: 10, top: 40, bottom: 40 },
                                autotable: { tableWidth: 'auto' }
                            }
                            //tableName: 'Countries',
                            //worksheetName: 'Countries by population'
                        };

                        _$el_.tableExport(options);

                        //$el.tableExport({ type: $(_$this).parent("li[data-type]").attr("data-type"), escape: 'false', ignoreColumn: ["operate","isCkeck"] });
                        _$el_.bootstrapTable('togglePagination');
                    });
                    _$el_.bootstrapTable('togglePagination');
                }


                //if ($table.bootstrapTable('getOptions').exportDataType === 'all' && !$table.bootstrapTable('getOptions').pagination) {
                //    $table.one('post-body.bs.table', function () {
                //        $table.tableExport({ type: $(this).parent("li[data-type]").attr("data-type"), escape: 'false' });                        
                //    });
                //    $table.bootstrapTable('refresh');
                //}
            });
        }

        $el.tableShowHideColumn = function () {
            $toolbar = $("#tools");
            var switchableCount = 0;

            $keepOpen = $toolbar.find('.keep-open');

            var columnsButton = "<a id='colCheck' href='javascript:;' class='btn widget-btn-icon dropdown-toggle' data-toggle='dropdown'>" +
            "                <i class='iconfont icon-column-set'></i> <span>列配置</span>" +
            "            </a>" +
            "            <ul class='dropdown-menu' role='menu'>";

            $.each($el.opts.columns, function (i, column) {
                columnsButton = columnsButton + "<li><label><input " + (column.switchable ? "disabled" : "") + " data-field='" + column.field + "' " + (column.visible == false ? "" : "checked='checked'") + " type='checkbox'> " + column.title + "</label></li>";

                //var checked = column.visible==false?'': ' checked="checked"';


            });
            columnsButton = columnsButton + "</ul>";
            $keepOpen.html(columnsButton);


            //if (switchableCount <= opts.minimumCountColumns) {
            //    $keepOpen.find('input').prop('disabled', true);
            //}

            $keepOpen.find('li').off('click').on('click', function (event) {
                event.stopImmediatePropagation();
            });
            $keepOpen.find('input').off('click').on('click', function () {
                field = $(this).attr("data-field");
                isCheck = $(this).is(':checked');

                $el.bootstrapTable(isCheck ? 'showColumn' : "hideColumn", field);

            });
        }


        return $el;
    }

})
