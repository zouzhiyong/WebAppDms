<template>
  <custtable ref="table" :columns="columns" :api="api" :isOperate="true"></custtable>
</template>

<script>
import { FindSysDeptTable, DeleteSysDeptRow } from "../../../api/api";
import custtable from "./../../layout/layout_table";
export default {
  components: {
    custtable
  },
  data() {
    return {
      columns: [
        { prop: "Name", label: "部门名称", width: "", align: "" },
        {
          prop: "IsValid",
          label: "有效否",
          width: "",
          align: "center",
          formatter: function(row, column) {
            return row.IsValid == 0 ? "无效" : "有效";
          }
        },
        { prop: "ModifyDate", label: "修改时间", width: "", align: "" },
        { prop: "Comment", label: "说明", width: "", align: "" }
      ],
      api: {
        FindTable: FindSysDeptTable,
        DeleteRow: DeleteSysDeptRow
      }
    };
  },
  mounted() {
    this.$refs.table.GetData();
  }
};
</script>
