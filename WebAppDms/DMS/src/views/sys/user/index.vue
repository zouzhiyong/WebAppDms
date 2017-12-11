<template>
  <layout>
    <userTools slot="toolbar"></userTools>
    <userTree slot="tree"></userTree>
    <userTable slot="table" ref="table"></userTable>
    <userForm slot="form" ref="form"></userForm>
  </layout>
</template>

<script>
import layout from "./../../layout/layout_left_right";
import userTools from "./user_tools";
import userTree from "./user_tree";
import userTable from "./user_table";
import userForm from "./user_form";

export default {
  components: {
    layout,
    userTools,
    userForm,
    userTree,
    userTable
  }
};
</script>