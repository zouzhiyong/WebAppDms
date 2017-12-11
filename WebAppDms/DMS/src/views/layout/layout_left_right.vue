<template> 
  <div class='body'>
    <el-row class="toolbar">
      <el-col :span="12" class="condition"><slot name="condition"></slot></el-col>
      <el-col :span="$slots.condition?12:24" class="button"><slot name="toolbar"></slot></el-col>
    </el-row>
    <el-row class="content">
    <el-col class='left' v-if="$slots.tree">
      <div class="leftbox">
        <slot name="tree"></slot>  
      </div>    
    </el-col>
    <el-col class='right' v-if="$slots.tree">
        <slot name="table"></slot>      
    </el-col>
    <el-col class="table" v-if="!$slots.tree">
        <slot name="table"></slot>      
    </el-col>
  </el-row>
  <slot name="form"></slot>      
  </div>
</template>

<style  scoped lang="scss">
.body {
  padding: 10px;
  height: 100%;
  background: #fff;
  // /deep/ .cust-el-dialog {
  //   width: 650px;
  //   height: 400px;
  // }
  .toolbar {
    margin-bottom: 10px;
  }
  .button {
    text-align: right;
    height: 33px;
    line-height: 33px;
  }
  .content {
    height: calc(100% - 40px);
    .left {
      width: 200px;
      height: 100%;
      padding-right: 5px;
      .leftbox {
        border: 1px solid #d1dbe5;
        height: 100%;
      }
    }
    .right {
      height: 100%;
      width: calc(100% - 200px);
    }
    .table {
      height: 100%;
    }
  }
}
</style>