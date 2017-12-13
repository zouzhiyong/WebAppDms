<template>
  <custtable ref="table" :columns="columns" :api="api" :isOperate="true"></custtable>
</template>

<script>
import { FindBasCustomerTable, DeleteBasCustomerRow } from "../../../api/api";
import custtable from "./../../layout/layout_table";
export default {
  components: {
    custtable
  },
  data() {
    return {
      columns: [
        { prop: "Code", label: "客户编码", width: "100", align: "" },
        { prop: "CustomerName", label: "客户名称", width: "", align: "" },
        { prop: "LinkMan", label: "联系人", width: "100", align: "" },
        { prop: "LinkManPhone", label: "联系电话", width: "150", align: "" },
        { prop: "RegionName", label: "销售区域", width: "300", align: "" },
        {
          prop: "IsValid",
          label: "有效否",
          width: "80",
          align: "center",
          formatter: function(row, column) {
            return row.IsValid == 0 ? "关门" : "正常";
          }
        },
        { prop: "ModifyDate", label: "修改时间", width: "200", align: "" }
      ],
      api: {
        FindTable: FindBasCustomerTable,
        DeleteRow: DeleteBasCustomerRow
      }
    };
  }
};
</script>