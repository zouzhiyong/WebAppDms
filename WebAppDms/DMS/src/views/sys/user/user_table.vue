<template>
  <custtable ref="table" :columns="columns" :api="api" :isOperate="true"></custtable>
</template>

<script>
import { FindSysUserTable, DeleteSysUserRow } from "../../../api/api";
import custtable from "./../../layout/layout_table";
export default {
  components: {
    custtable
  },
  data() {
    return {
      columns: [
        { prop: "LoginName", label: "登录账号", width: "180", align: "" },
        { prop: "RealName", label: "用户姓名", width: "180", align: "" },
        { prop: "DeptName", label: "部门", width: "", align: "" },
        { prop: "RoleName", label: "角色", width: "", align: "" },
        { prop: "Phone", label: "手机号码", width: "", align: "" },
        {
          prop: "IsValid",
          label: "有效否",
          width: "80",
          align: "center",
          formatter: function(row, column) {
            return row.IsValid == 0 ? "无效" : "有效";
          }
        }
      ],
      api: {
        FindTable: FindSysUserTable,
        DeleteRow: DeleteSysUserRow
      }
    };
  }
};
</script>
