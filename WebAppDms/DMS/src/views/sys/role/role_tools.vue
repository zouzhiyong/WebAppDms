<template>
  <el-button slot="newButton" type="text" icon="el-icon-news" style="padding-top:0;padding-bottom:0" @click="handleClickNew">新建</el-button>
</template>
<script>
export default {
  methods: {
    handleClickNew() {
      let row = { RoleID: 0 };
      this.$parent.$parent.$parent.$parent.$refs.form.GetData(row);
    }
  }
};
</script>

