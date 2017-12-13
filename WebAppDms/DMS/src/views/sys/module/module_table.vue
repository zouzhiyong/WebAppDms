<template>
  <custtable ref="table" :columns="columns" :api="api" :isOperate="true"></custtable>
</template>

<script>
import { FindSysMoudleTable, DeleteSysMoudleRow } from "../../../api/api";
import custtable from "./../../layout/layout_table";
export default {
  components: {
    custtable
  },
  data() {
    return {
      columns: [
        { prop: "MenuName", label: "模块名称", width: "180", align: "" },
        { prop: "MenuPath", label: "模块路径", width: "180", align: "" },
        { prop: "MenuIcon", label: "图标", width: "", align: "" },
        {
          prop: "IsValid",
          label: "有效否",
          width: "",
          align: "center",
          formatter: function(row, column) {
            return row.IsValid == 0 ? "无效" : "有效";
          }
        }
      ],
      api: {
        FindTable: FindSysMoudleTable,
        DeleteRow: DeleteSysMoudleRow
      }
    };
  }
};
</script>
