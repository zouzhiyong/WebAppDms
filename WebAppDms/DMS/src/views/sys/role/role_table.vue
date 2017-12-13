<template>
  <custtable ref="table" :columns="columns" :api="api" :isOperate="true"></custtable>
</template>

<script>
import { FindSysRoleTable, DeleteSysRoleRow } from "../../../api/api";
import custtable from "./../../layout/layout_table";
export default {
  components: {
    custtable
  },
  data() {
    return {
      columns: [
        { prop: "RoleName", label: "角色名称", width: "", align: "" },
        {
          prop: "IsValid",
          label: "有效否",
          width: "",
          align: "center",
          formatter: function(row, column) {
            return row.IsValid == 0 ? "无效" : "有效";
          }
        },
        { prop: "ModifyDate", label: "修改时间", width: "", align: "" }
      ],
      api: {
        FindTable: FindSysRoleTable,
        DeleteRow: DeleteSysRoleRow
      }
    };
  },
  mounted() {
    this.$refs.table.GetData();
  }
};
</script>
