<template>
  <layout>
    <moduleTools slot="toolbar"></moduleTools>
    <moduleTree slot="tree"></moduleTree>
    <moduleTable slot="table" ref="table"></moduleTable>
    <moduleForm slot="form" ref="form"></moduleForm>
  </layout>
</template>

<script>
import layout from "./../../layout/layout_left_right";
import moduleTools from "./module_tools";
import moduleTree from "./module_tree";
import moduleTable from "./module_table";
import moduleForm from "./module_form";

export default {
  components: {
    layout,
    moduleTools,
    moduleForm,
    moduleTree,
    moduleTable
  }
};
</script>