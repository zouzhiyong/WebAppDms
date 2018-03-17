//检测路由
(function () {
    var localRoutes=sessionStorage.user;
    if (localRoutes) {

    } else {
        window.location.href = "login.html";
    }
})();