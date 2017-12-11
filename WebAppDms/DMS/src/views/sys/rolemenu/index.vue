<template>
  <layout>
    <rolemenuTools slot="toolbar"></rolemenuTools>
    <rolemenuTree slot="tree"></rolemenuTree>
    <rolemenuTable slot="table" ref="table"></rolemenuTable>
  </layout>
</template>

<script>
import layout from "./../../layout/layout_left_right";
import rolemenuTools from "./rolemenu_tools";
import rolemenuTree from "./rolemenu_tree";
import rolemenuTable from "./rolemenu_table";

export default {
  components: {
    layout,
    rolemenuTools,
    rolemenuTree,
    rolemenuTable
  }
};
</script>