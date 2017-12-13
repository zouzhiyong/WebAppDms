<template>
  <custtable ref="table" :options="options" :conditionData="conditionData" :isOperate="true"></custtable:>
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
      options: [
        { prop: "Name", label: "部门名称", width: "200", align: "" },
        {
          prop: "IsValid",
          label: "有效否",
          width: "200",
          align: "center",
          formatter: function(row, column) {
            return row.IsValid == 0 ? "无效" : "有效";
          }
        },
        { prop: "ModifyDate", label: "修改时间", width: "200", align: "" },
        { prop: "Comment", label: "说明", width: "", align: "" }
      ],
      conditionData: {
        total: 0,
        FindTable: FindSysDeptTable,
        DeleteRow: DeleteSysDeptRow
      }
    };
  }
};
</script>
