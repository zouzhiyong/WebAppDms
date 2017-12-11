<template>
<div style="height:100%">
  <div class="title">角色</div>
  <el-tree ref="tree" :data="treeData" :props="defaultProps" node-key="RoleID" :current-node-key="0" :highlight-current="true" :default-expanded-keys="[0]" @node-click="handleNodeClick"></el-tree>
</div>
</template>

<script>
import { FindSysRoleTree } from "../../../api/api";
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
      FindSysRoleTree().then(result => {
        this.treeData = result.data;
        if (this.treeData.length > 0) {
          this.$nextTick(() => {
            this.$refs.tree.setCurrentKey(this.treeData[0].RoleID);
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
.title {
  height: 36px;
  line-height: 36px;
  padding-left: 5px;
  background: #eef1f6;
  border-bottom: 1px solid #fff;
  text-align: center;
  font-weight: bold;
}
.el-tree {
  height: calc(100% - 36px);
  overflow-y: auto;
}
</style>
