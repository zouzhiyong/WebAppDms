//ajax全局事件调用
var ajaxGloble = function() {
    $.ajaxSetup({ //设置全局性的Ajax选项
        type: "POST",
        timeout: 30000, //超时时间设置，单位毫秒
        dataType: "json"
    })
    var index;
    var loadTimeOut = true;

    var loading = "<div class='loadingBox ajax'>" +
            "<div class='loading1'></div>" +
            "<div class='loading2'></div>" +
            "<div class='loading'></div>" +
            "<div class='loadingTxt'>请稍候</div>" +
        "</div>";
    

    $(document).ajaxStart(function() {
        //超过500毫秒才会显示加载层
        setTimeout(function() {
            if (loadTimeOut) {
                window.top.$(".loadingBox.ajax", window.top.document).remove();
                $("body").append(loading);
            }
        }, 500);
    }).ajaxSuccess(function (e, xhr, o) {
        try{
            //判断返回状态是否为真
            if (xhr.responseJSON.result == true) {
                if (xhr.responseJSON.message != "" && xhr.responseJSON.message != null) {
                    new Vue().$message({
                        message: xhr.responseJSON.message,
                        type: 'success'
                    });              
                }
            }
            //判断返回状态是否为否
            if (xhr.responseJSON.result == false) {
                new Vue().$message({
                    message: xhr.responseJSON.message,
                    type: 'success'
                });
                if (xhr.responseJSON.direct) {
                    window.location.href = xhr.responseJSON.direct;
                }
            }
        }catch(e){

        }
    }).ajaxError(function(e, xhr, o) {
        if (xhr.statusText == "abort") {
            return;
        }

        if (xhr.statusText == "timeout") {
            new Vue().$message({
                message: "访问超时！",
                type: 'error'
            });
        } else {
            new Vue().$message({
                message: xhr.statusText,
                type: 'error'
            });
            //parent.layer.msg(xhr.statusText, {
            //    icon: 5,
            //    offset: 't',
            //    anim: 6
            //});
        }
        //layer.close(index);
        loadTimeOut = false;
        window.top.$(".loadingBox.ajax", window.top.document).remove();
    }).ajaxComplete(function(e, xhr, o) {

    }).ajaxStop(function() {
        //layer.close(index);
        loadTimeOut = false;
        window.top.$(".loadingBox.ajax", window.top.document).remove();
    });
}


var ajaxData = function(url, option) {
    ajaxGloble();
    var _defaults = {
        url: url,
        type: 'POST',
        async: true,
        global: true,
        beforeSend: function(xhr) {
            var _ticket = sessionStorage.getItem("Ticket");
            if (_ticket) {
                xhr.setRequestHeader('Authorization', 'BasicAuth ' + _ticket);
            }
        },
        success: function(result) {
            return result;
        },
        error: function(e) {
            return e;
        }
    }

    var opts = $.extend({}, _defaults, option);
    opts.url = getUrl(opts.url);
    return $.ajax(opts);
}


// var hostUrl = 'http://localhost:64573/';
var hostUrl = 'http://localhost/WebAppDms/';
var getUrl = function (url) {
    return hostUrl + url;
}

