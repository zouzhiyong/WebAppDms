<template>
  <el-form :inline="true" size="small" :model="formInline" class="demo-form-inline">
  <el-form-item label="客户名称">
    <el-input v-model="formInline.CustomerName" placeholder="客户名称"></el-input>
  </el-form-item>
  <el-form-item label="销售区域">
    <el-cascader :clearable="true" :options="RegionList" v-model="formInline.RegionName" @change="handleChange"></el-cascader>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="onSubmit">查询</el-button>
  </el-form-item>
</el-form>
</template>

<script>
import { FindBasRegionList } from "../../../api/api";
export default {
  data() {
    return {
      RegionList: [],
      formInline: {
        CustomerName: "",
        RegionName: [],
        Region: []
      }
    };
  },
  created() {
    FindBasRegionList().then(result => {
      this.RegionList = result.data;
      this.formInline.Region = this.formInline.RegionName.toString();
      this.$parent.$parent.$parent.$parent.$refs.table.conditionData = this.formInline;
      this.$parent.$parent.$parent.$parent.$refs.table.GetData();
    });
  },
  methods: {
    onSubmit() {
      this.$parent.$parent.$parent.$parent.$refs.table.conditionData = this.formInline;
      this.$parent.$parent.$parent.$parent.$refs.table.GetData();
    },
    handleChange(value) {
      this.formInline.Region = value.toString();
    }
  }
};
</script>
<style scoped lang="scss">
.toolbar .el-form-item {
  margin-bottom: 0;
}
</style>