<template>
  <div id="app">
    <transition name="fade" mode="out-in">
      <router-view></router-view>
    </transition>
  </div>
</template>

<script>
import { getMenu } from "./api/api";
export default {
  name: "app",
  components: {},
  created() {},
  mounted() {},
  methods: {}
};
</script>

<style lang="scss">
html {
  height: 100%;
}

* {
  font-size: 13px !important;
  font-family: Helvetica Neue, Helvetica, PingFang SC, Hiragino Sans GB,
    Microsoft YaHei, SimSun, sans-serif;
  box-sizing: border-box;
}

.el-submenu .el-menu-item,
.el-menu-item,
.el-submenu__title {
  height: 45px;
  line-height: 45px;
} // .el-icon-arrow-left:before {
//   content: "上一页";
// }
// .el-icon-arrow-right:before {
//   content: "下一页";
// }
.fa {
  width: 24px;
}

.el-select > .el-input {
  width: 200px;
}

.el-input__inner,
.el-button {
  border-radius: 2px;
}

.el-button--text {
  padding-left: 8px;
  padding-right: 8px;
}

.el-tree {
  cursor: default; // background: #fff;
  // border: 1px solid #d1dbe5;
}

.el-tree-node__content {
  height: 36px;
  line-height: 36px;
}

.el-table th {
  padding: 2px 0px;
  height: 40px;
  line-height: 40px;
  background-color: #eef1f6;
}

.el-table td {
  padding: 2px 0px;
  height: 40px;
  line-height: 40px;
}

.el-pagination {
  font-weight: normal;
}

.el-pagination button {
  width: 50px;
}

.el-table__footer-wrapper thead div,
.el-table__header-wrapper thead div {
  background-color: #eef1f6;
  color: #1f2d3d;
}

body {
  height: 100%;
  margin: 0px;
  padding: 0px;
  /*background: url(assets/bg1.jpg) center !important;
		background-size: cover;*/
  // background: #1F2D3D;
  font-family: Helvetica Neue, Helvetica, PingFang SC, Hiragino Sans GB,
    Microsoft YaHei, SimSun, sans-serif;
  font-size: 14px;
  -webkit-font-smoothing: antialiased;
}

#app {
  position: absolute;
  top: 0px;
  bottom: 0px;
  width: 100%;
} // .el-submenu [class^=fa] {
// 	vertical-align: baseline;
// 	margin-right: 10px;
// }
// .el-menu-item [class^=fa] {
// 	vertical-align: baseline;
// 	margin-right: 10px;
// }
//.toolbar {//
// background: #f2f2f2;
// padding: 10px; //border:1px solid #dfe6ec;
// margin: 0 0 5px;
// .el-form-item {
//   margin-bottom: 10px;
// }
//} // .fade-enter-active,
// .fade-leave-active {
//   transition: all 0.2s ease;
// }
// .fade-enter,
// .fade-leave-active {
//   opacity: 0;
// }
</style>