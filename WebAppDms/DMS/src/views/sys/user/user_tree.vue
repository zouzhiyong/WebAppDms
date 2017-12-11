<template>
  <el-tree ref="tree" :expand-on-click-node="false" :data="treeData" :props="defaultProps" node-key="DeptID" :current-node-key="0" :highlight-current="true" :default-expanded-keys="[0]" @node-click="handleNodeClick"></el-tree>
</template>

<script>
import { FindSysDeptTree } from "../../../api/api";
export default {
  data() {
    return {
      treeData: [],
      defaultProps: {
        children: "children",
        label: "label"
      }
    };
  },
  created() {
    this.iniData();
  },
  methods: {
    iniData() {
      var obj = [
        {
          DeptID: 0,
          label: "所有部门"
        }
      ];
      FindSysDeptTree().then(result => {
        obj[0].children = result.data;
        this.treeData = obj;
        if (this.treeData.length > 0) {
          this.$nextTick(() => {
            this.$refs.tree.setCurrentKey(this.treeData[0].DeptID);
            this.handleNodeClick(this.treeData[0]);
          });
        }
      });
    },
    handleNodeClick(data) {
      this.$parent.$parent.$parent.$parent.$refs.table.conditionData = data;
      this.$parent.$parent.$parent.$parent.$refs.table.GetData();
    }
  }
};
</script>
<style scoped lang="scss">
.el-tree {
  height: 100%;
  overflow-y: auto;
}
</style>
