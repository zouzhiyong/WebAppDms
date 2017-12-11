<template>
  <layout>
    <roleTools slot="toolbar"></roleTools>
    <roleTable slot="table" ref="table"></roleTable>
    <roleForm slot="form" ref="form"></roleForm>
  </layout>
</template>

<script>
import layout from "./../../layout/layout_left_right";
import roleTools from "./role_tools";
import roleTable from "./role_table";
import roleForm from "./role_form";

export default {
  components: {
    layout,
    roleTools,
    roleForm,
    roleTable
  }
};
</script>