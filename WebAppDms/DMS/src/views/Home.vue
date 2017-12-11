<template>
  <el-row class="container">
    <el-col :span="24" class="header">
      <el-col :span="10" class="logo" :class="collapsed?'logo-collapse-width':'logo-width'">
        {{collapsed?'':sysName}}
      </el-col>
      <el-col :span="10">
        <div class="tools" @click.prevent="collapse">
          <i class="fa fa-align-justify"></i>
        </div>
      </el-col>
      <el-col :span="4" class="userinfo">
        <el-dropdown trigger="hover">
          <span class="el-dropdown-link userinfo-inner">
            <img :src="this.sysUserAvatar" /> {{sysUserName}}</span>
          <el-dropdown-menu slot="dropdown">
            <el-dropdown-item>我的消息</el-dropdown-item>
            <el-dropdown-item>设置</el-dropdown-item>
            <el-dropdown-item divided @click.native="logout">退出登录</el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </el-col>
    </el-col>
    <el-col :span="24" class="main">
      <aside :class="collapsed?'menu-collapsed':'menu-expanded'">
        <!--导航菜单-->
        <el-menu :default-active="$route.path" class="el-menu-vertical-demo" @open="handleopen" @close="handleclose" @select="handleselect" unique-opened router :collapse="collapsed" background-color="#393d49" text-color="#fff" active-text-color="#20a0ff">
          <template v-for="(item,index) in menuData" v-if="!item.hidden">
            <el-submenu :index="index+''" v-if="!item.leaf" :key="index">
              <template slot="title">
                <i :class="item.iconCls"></i>
                <span slot="title">{{item.meta.name}}</span>
              </template>
              <el-menu-item v-for="child in item.children" :index="child.path" :key="child.path" v-if="!child.hidden">{{child.meta.name}}</el-menu-item>
            </el-submenu>
            <el-menu-item v-if="item.leaf&&item.children.length>0" :index="item.children[0].path" :key="index">
              <i :class="item.iconCls"></i>
              <span slot="title">{{item.children[0].meta.name}}</span>
            </el-menu-item>
          </template>
        </el-menu>
      </aside>
      <section class="content-container">
        <div class="grid-content bg-purple-light">
          <el-col :span="24" class="breadcrumb-container">
            <strong class="title">{{$route.meta.name}}</strong>
            <el-breadcrumb separator="/" class="breadcrumb-inner">
              <el-breadcrumb-item v-if="item.meta.name!=''" v-for="item in $route.matched" :key="item.path">
                {{ item.meta.name }}
              </el-breadcrumb-item>
            </el-breadcrumb>
          </el-col>
          <el-col :span="24" class="content-wrapper">
            <!-- <transition name="fade" mode="out-in"> -->
            <router-view :view-data="1"></router-view>
            <!-- </transition> -->
          </el-col>
        </div>
        <div class="footer">© 2016 XXX.com 版权所有 ICP证：湘ICP备XXXXXXX</div>
      </section>
    </el-col>
  </el-row>
</template>

<script>
import { getMenu } from "../api/api";
import custBotton from "./layout/layout_button";
export default {
  data() {
    return {
      sysName: "VUEADMIN",
      collapsed: false,
      sysUserName: "",
      sysUserAvatar: "",
      menuData: ""
    };
  },
  components: {
    custBotton
  },
  methods: {
    onSubmit() {
      console.log("submit!");
    },
    handleopen() {
      //console.log('handleopen');
    },
    handleclose() {
      //console.log('handleclose');
    },
    handleselect: function(a, b) {},
    //退出登录
    logout: function() {
      var _this = this;
      this.$confirm("确认退出吗?", "提示", {
        //type: 'warning'
      })
        .then(() => {
          sessionStorage.removeItem("user");
          sessionStorage.removeItem("routes");
          sessionStorage.removeItem("menuData");
          _this.$router.push("/login");
          location.reload();
        })
        .catch(() => {});
    },
    //折叠导航栏
    collapse: function() {
      this.collapsed = !this.collapsed;
      // var uiwidth = document.getElementById("lastclass");
      // if (uiwidth.offsetWidth === 0) {
      //   uiwidth.style.width = "230px";
      // }
    },
    showMenu(i, status) {
      this.$refs.menuCollapsed.getElementsByClassName(
        "submenu-hook-" + i
      )[0].style.display = status ? "block" : "none";
    }
  },
  mounted() {},
  created() {
    this.menuData = JSON.parse(sessionStorage.routes);
  },
  mounted() {
    var user = sessionStorage.getItem("user");
    if (user) {
      user = JSON.parse(user);
      this.sysUserName = user.name || "";
      this.sysUserAvatar = require("./../assets/" + user.avatar);
    }
  }
};
</script>

<style scoped lang="scss">
@import "~scss_vars";
.container {
  position: absolute;
  top: 0px;
  bottom: 0px;
  width: 100%;
  .header {
    height: 60px;
    line-height: 60px;
    background: $color-primary;
    color: #fff;
    .userinfo {
      text-align: right;
      padding-right: 35px;
      float: right;
      .userinfo-inner {
        cursor: pointer;
        color: #fff;
        img {
          width: 40px;
          height: 40px;
          border-radius: 20px;
          margin: 10px 0px 10px 10px;
          float: right;
        }
      }
    }
    .logo {
      //width:230px;
      height: 60px;
      font-size: 22px;
      padding-left: 20px;
      padding-right: 20px;
      border-color: rgba(238, 241, 146, 0.3);
      border-right-width: 1px;
      border-right-style: solid;
      img {
        width: 40px;
        float: left;
        margin: 10px 10px 10px 18px;
      }
      .txt {
        color: #fff;
      }
      &:not(.logo-collapse-width) {
        width: 200px;
        transition: width 0.3s;
      }
    } // .logo-width {
    //   width: 200px;
    // }
    .logo-collapse-width {
      width: 64px;
      transition: width 0.3s;
    }
    .tools {
      padding: 0px 23px;
      width: 14px;
      height: 60px;
      line-height: 60px;
      cursor: pointer;
    }
  }
  .main {
    display: flex; // background: #324057;
    position: absolute;
    top: 60px;
    bottom: 0px;
    overflow: hidden;
    aside {
      // flex: 0 0 200px;
      // width: 200px;
      .el-menu-vertical-demo {
        &:not(.el-menu--collapse) {
          width: 200px;
        }
      } // position: absolute;
      // top: 0px;
      // bottom: 0px;
      .el-menu {
        height: 100%;
        border-radius: 0;
        // .is-active {
        //   background-color: #20a0ff !important;
        // }
        .el-menu-item {
          [class^="fa"] {
            margin-right: 5px;
            width: 24px;
            text-align: center;
            color: #fff;
          }
        }

        .el-submenu {
          [class^="fa"] {
            vertical-align: middle;
            margin-right: 5px;
            width: 24px;
            text-align: center;
            color: #fff;
          }
          .el-menu-item {
            min-width: auto;
            padding-right: 20px;
            background-color: #282b33 !important;
          }
        }
      } // .collapsed {
      //   width: 60px;
      //   .item {
      //     position: relative;
      //   }
      //   .submenu {
      //     position: absolute;
      //     top: 0px;
      //     left: 60px;
      //     z-index: 99999;
      //     height: auto;
      //     display: none;
      //   }
      // }
    } // .menu-collapsed {
    //   flex: 0 0 60px;
    //   width: 60px;
    // }
    // .menu-expanded {
    //   flex: 0 0 230px;
    //   width: 230px;
    // }
    .content-container {
      // background: #f1f2f7;
      height: 100%;
      flex: 1; // position: absolute;
      // right: 0px;
      // top: 0px;
      // bottom: 0px;
      // left: 230px;
      overflow-y: auto;
      padding: 0;
      .grid-content {
        height: calc(100% - 40px);
        .breadcrumb-container {
          padding: 10px;
          border-bottom: solid 1px #eee; //margin-bottom: 15px;
          .title {
            width: 200px;
            float: left;
            color: #475669;
          }
          .breadcrumb-inner {
            float: right;
          }
        }
      }
      .content-wrapper {
        background: #f0f0f0;
        box-sizing: border-box;
        height: calc(100% - 38px);
        padding: 20px;
      }
      .footer {
        text-align: right;
        border-top: solid 1px #cbd6eb;
        padding: 10px;
      }
    }
  }
}
</style>
<style>
.el-dialog__header {
  padding: 0 80px 0 20px;
  height: 42px;
  line-height: 42px;
  border-bottom: 1px solid #eee;
  color: #333;
  overflow: hidden;
  background-color: #f8f8f8;
  border-radius: 2px 2px 0 0;
  cursor: move;
}

.el-dialog__headerbtn {
  position: absolute;
  right: 15px;
  top: 15px;
  font-size: 0;
  line-height: initial;
}

.el-dialog__body {
  height: calc(100% - 100px);
  overflow: auto;
}

.el-dialog__footer {
  padding: 10px 20px 15px;
  text-align: center;
  box-sizing: border-box;
  background: #fff;
}
</style>