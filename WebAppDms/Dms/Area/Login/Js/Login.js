$(function () {
    sessionStorage.removeItem("user");
    sessionStorage.removeItem("Ticket");
    sessionStorage.removeItem("Menu");


    new Vue({
        el: '.login',
        data: {
            ruleForm: {
                userName: '',
                passWord: ''
            },
            loginRules: {
                userName: [{
                    required: true,
                    message: '帐号不能为空',
                    trigger: 'blur'
                }],
                passWord: [{
                    required: true,
                    message: '密码不能为空',
                    trigger: 'blur'
                }]
            }
        },
        methods: {
            handleLogin: function () {
                var _self = this;
                this.$refs.ruleForm.validate(function (valid) {
                    if (valid) {                        
                        ajaxData('api/Login/Login', {
                            data: {
                                strUser: _self.ruleForm.userName,
                                strPwd: _self.ruleForm.passWord
                            }
                        }).then(function (result) {
                            if (result.bRes) {
                                //登录成功之后将用户名和用户票据带到主界面
                                sessionStorage.setItem("user", JSON.stringify(result.user));
                                sessionStorage.setItem("Ticket", result.Ticket);
                                sessionStorage.setItem("Menu", JSON.stringify(result.menu));
                                console.log(result.menu)
                                window.location.href = "index.html";
                            } else {
                                _self.$message.error(result.message);
                                return;
                            }
                        }, function () {

                        });
                    } else {
                        return false;
                    }
                });
            }
        }
    })
});