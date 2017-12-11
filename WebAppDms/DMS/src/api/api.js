import axios from 'axios';
import Vue from 'vue'
import { Loading } from "element-ui";

let options = {
    text: '正在加载',
    lock: true,
    spinner: 'el-icon-loading',
    background: 'rgba(0, 0, 0, 0.8)'
        //target: document.querySelector('.content-container')
}
let loadingInstance;
// import VueProgressBar from 'vue-progressbar'
// Vue.use(VueProgressBar, {
//     color: '#20a0ff',
//     failedColor: 'red',
//     thickness: '2px',
//     location: "bottom"
// })

let base = 'http://localhost/webDmsApi';

let _time = null;
// 添加请求拦截器
axios.interceptors.request.use(function(config) {
    // 在发送请求之前做些什么
    // new Vue().$Progress.start()
    _time = setTimeout(() => {
        loadingInstance = Loading.service(options)
    }, 500);


    return config;
}, function(error) {
    // 对请求错误做些什么
    // this.$Progress.fail()

    return Promise.reject(error);
});

// 添加响应拦截器
axios.interceptors.response.use(function(response) {
    // 对响应数据做点什么
    // new Vue().$Progress.finish();
    clearTimeout(_time);
    try {
        loadingInstance.close();
    } catch (e) {}
    if (response.data.result === true) {
        if (response.data.message !== "" && response.data.message !== null) {
            new Vue().$message({
                message: response.data.message,
                type: 'success'
            });
        }
    }
    //判断返回状态是否为否
    if (response.data.result === false) {
        new Vue().$message({
            message: response.data.message,
            type: 'error'
        });
    }
    return response;
}, function(error) {
    // 对响应错误做点什么
    // this.$Progress.fail()
    clearTimeout(_time);
    try {
        loadingInstance.close();
    } catch (e) {}
    new Vue().$message({
        message: error.response.data.ExceptionMessage,
        type: 'error'
    });

    return Promise.reject(error);
});


//登录
export const requestLogin = params => { return axios.post(`${base}/api/Login/Login`, params).then(res => res.data); };
//模块设置页面
export const getMenu = params => { return axios.post(`${base}/api/Menu/FindMenu`).then(res => res.data); };
export const FindSysModuleTree = params => { return axios.post(`${base}/api/Menu/FindSysModuleTree`).then(res => res.data); };
export const FindSysMoudleTable = params => { return axios.post(`${base}/api/Menu/FindSysMoudleTable`, params).then(res => res.data); };
export const FindSysMoudleForm = params => { return axios.post(`${base}/api/Menu/FindSysMoudleForm`, params).then(res => res.data); };
export const SaveSysMoudleForm = params => { return axios.post(`${base}/api/Menu/SaveSysMoudleForm`, params).then(res => res.data); };
export const DeleteSysMoudleRow = params => { return axios.post(`${base}/api/Menu/DeleteSysMoudleRow`, params).then(res => res.data); };
//部门设置页面
export const FindSysDeptTable = params => { return axios.post(`${base}/api/Dept/FindSysDeptTable`, params).then(res => res.data); };
export const DeleteSysDeptRow = params => { return axios.post(`${base}/api/Dept/DeleteSysDeptRow`, params).then(res => res.data); };
export const FindSysDeptForm = params => { return axios.post(`${base}/api/Dept/FindSysDeptForm`, params).then(res => res.data); };
export const SaveSysDeptForm = params => { return axios.post(`${base}/api/Dept/SaveSysDeptForm`, params).then(res => res.data); };
//用户设置页面
export const FindSysDeptTree = params => { return axios.post(`${base}/api/User/FindSysDeptTree`).then(res => res.data); };
export const FindSysUserTable = params => { return axios.post(`${base}/api/User/FindSysUserTable`, params).then(res => res.data); };
export const FindSysUserForm = params => { return axios.post(`${base}/api/User/FindSysUserForm`, params).then(res => res.data); };
export const SaveSysUserForm = params => { return axios.post(`${base}/api/User/SaveSysUserForm`, params).then(res => res.data); };
export const DeleteSysUserRow = params => { return axios.post(`${base}/api/User/DeleteSysUserRow`, params).then(res => res.data); };
//权限设置页面
export const FindSysRoleTree = params => { return axios.post(`${base}/api/Role/FindSysRoleTree`).then(res => res.data); };
export const FindSysRoleMenuTable = params => { return axios.post(`${base}/api/Role/FindSysRoleMenuTable`, params).then(res => res.data); };
export const SaveSysRoleMenuForm = params => { return axios.post(`${base}/api/Role/SaveSysRoleMenuForm`, params).then(res => res.data); };
export const FindSysRoleTable = params => { return axios.post(`${base}/api/Role/FindSysRoleTable`, params).then(res => res.data); };
export const DeleteSysRoleRow = params => { return axios.post(`${base}/api/Role/DeleteSysRoleRow`, params).then(res => res.data); };
export const FindSysRoleForm = params => { return axios.post(`${base}/api/Role/FindSysRoleForm`, params).then(res => res.data); };
export const SaveSysRoleForm = params => { return axios.post(`${base}/api/Role/SaveSysRoleForm`, params).then(res => res.data); };
//客户设置页面
export const FindBasRegionList = params => { return axios.post(`${base}/api/customer/FindBasRegionList`).then(res => res.data); };
export const FindBasCustomerTable = params => { return axios.post(`${base}/api/customer/FindBasCustomerTable`, params).then(res => res.data); };
export const DeleteBasCustomerRow = params => { return axios.post(`${base}/api/customer/DeleteBasCustomerRow`, params).then(res => res.data); };
export const FindBasCustomerForm = params => { return axios.post(`${base}/api/customer/FindBasCustomerForm`, params).then(res => res.data); };
export const SaveBasCustomerForm = params => { return axios.post(`${base}/api/customer/SaveBasCustomerForm`, params).then(res => res.data); };