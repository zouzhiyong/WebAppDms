<template>
  <el-button slot="saveButton" type="text" icon="fa fa-save" style="padding-top:0;padding-bottom:0" @click="handleClickSave">保存</el-button>
</template>
<script>
import { SaveSysRoleMenuForm } from "../../../api/api";
export default {
  methods: {
    handleClickSave() {
      let data = this.$parent.$parent.$parent.$parent.$refs.table.checkedData;
      SaveSysRoleMenuForm(data).then(result => {
        console.log(result.data);
      });
    }
  }
};
</script>

