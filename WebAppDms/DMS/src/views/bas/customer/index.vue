<template>
  <layout>
    <customerCondition slot="condition" ref="condition"></customerCondition>
    <customerTools slot="toolbar"></customerTools>
    <customerTable slot="table" ref="table"></customerTable>
    <customerForm slot="form" ref="form"></customerForm>
  </layout>
</template>

<script>
import layout from "./../../layout/layout_left_right";
import customerTools from "./customer_tools";
import customerCondition from "./customer_condition";
import customerTable from "./customer_table";
import customerForm from "./customer_form";

export default {
  components: {
    layout,
    customerTools,
    customerCondition,
    customerForm,
    customerTable
  }
};
</script>