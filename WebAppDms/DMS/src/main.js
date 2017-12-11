import babelpolyfill from 'babel-polyfill'
import Vue from 'vue'
import 'element-ui/lib/theme-chalk/index.css'
import App from './App'
import ElementUI from 'element-ui'

//import './assets/theme/theme-green/index.css'
import VueRouter from 'vue-router'
import store from './vuex/store'
import Vuex from 'vuex'
// import VueProgressBar from 'vue-progressbar'
//import NProgress from 'nprogress'
//import 'nprogress/nprogress.css'
import routes from './routes'
// import Mock from './mock'
// Mock.bootstrap();
import 'font-awesome/css/font-awesome.min.css'

Vue.use(ElementUI)
Vue.use(VueRouter)
Vue.use(Vuex)

// Vue.prototype.ExportJsonExcel = ExportJsonExcel;
// Vue.use(VueProgressBar, {
//     color: 'rgb(143, 255, 199)',
//     failedColor: 'red',
//     height: '2px'
// })

Vue.config.productionTip = false;


//NProgress.configure({ showSpinner: false });

const router = new VueRouter({
    // mode: 'history',
    saveScrollPosition: true,
    routes
})


//检测本地路由
let localRoutes = sessionStorage.routes;
if (localRoutes) {
    JSON.parse(localRoutes).map(item => {
        item.component = resolve => require([`./views/Home.vue`], resolve);
        item.children.map(_item => {
            _item.component = resolve =>
                require(["./views/" + _item.MenuPath + `.vue`], resolve);
        });
        router.options.routes.push(item);
    });

    router.addRoutes(router.options.routes);
}

router.beforeEach((to, from, next) => {
    //NProgress.start();
    if (to.path == '/login') {
        sessionStorage.removeItem('user');
        sessionStorage.removeItem('routes');
        if (from.path == '/index') {
            location.reload();
        }
    }
    let user = JSON.parse(sessionStorage.getItem('user'));
    if (!user && to.path != '/login') {
        sessionStorage.removeItem('user');
        sessionStorage.removeItem('routes');
        next({ path: '/login' })
    } else {
        next()
    }
})



//router.afterEach(transition => {
//NProgress.done();
//});

new Vue({
    //el: '#app',
    //template: '<App/>',
    router,
    store,
    //components: { App }
    render: h => h(App)
}).$mount('#app')