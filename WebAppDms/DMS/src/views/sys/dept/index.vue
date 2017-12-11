<template>
  <layout>
    <deptTools slot="toolbar"></deptTools>
    <deptTable slot="table" ref="table"></deptTable>
    <deptForm slot="form" ref="form"></deptForm>
  </layout>
</template>

<script>
import layout from "./../../layout/layout_left_right";
import deptTools from "./dept_tools";
import deptTable from "./dept_table";
import deptForm from "./dept_form";

export default {
  components: {
    layout,
    deptTools,
    deptForm,
    deptTable
  }
};
</script>