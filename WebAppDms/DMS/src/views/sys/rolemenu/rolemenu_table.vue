<template>
  <div style="height:100%">
    <el-table :data="tableData" ref="table" border height="100%">
      <el-table-column type="index" width="100" header-align="center" align="center">
      </el-table-column>
      <el-table-column label="一级模块" width="180" header-align="center">
        <template slot-scope="scope">
          <i :class="scope.row.MenuIcon"></i>
          <el-checkbox :indeterminate="scope.row.isIndeterminate" v-model="scope.row.isMenuRole" @change="handleCheckAllChange(scope.row)">{{scope.row.MenuName}}</el-checkbox>
        </template>
      </el-table-column>
      <el-table-column label="二级模块" width="180" header-align="center">
        <template slot-scope="scope">
          <el-checkbox-group v-model="scope.row.MenuRolesData" @change="handleCheckedDataChange(scope.row)">
            <el-checkbox v-for="item in scope.row.chilDren" :label="item.MenuID" :key="item.MenuID">{{item.MenuName}}</el-checkbox>
          </el-checkbox-group>
        </template>
      </el-table-column>
      <el-table-column label="说明" prop="Comment" header-align="center">
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { FindSysRoleMenuTable } from "../../../api/api";
export default {
  data() {
    return {
      tableData: [],
      conditionData: {}
    };
  },
  computed: {
    checkedData() {
      var arr = [];
      var obj = {};
      this.tableData.map(item => {
        arr = arr.concat(item.MenuRolesData);
        if (item.isMenuRole == true || item.isIndeterminate == true) {
          arr.push(item.MenuID);
        }
      });
      // arr.map(item => {
      //   obj.push({
      //     RoleID: this.conditionData.RoleID,
      //     MenuID: item
      //   });
      // });
      obj.RoleID = this.conditionData.RoleID;
      obj.MenuID = arr;
      return obj;
    }
  },
  methods: {
    GetData() {
      var data = this.conditionData;
      FindSysRoleMenuTable(data).then(result => {
        result.data.map(item => {
          item.isMenuRole = item.MenuRolesData.length > 0 ? true : false;
          item.isIndeterminate =
            item.MenuRolesData.length > 0 &&
            item.MenuRolesData.length < item.chilDren.length;
        });
        this.tableData = result.data;
      });
    },
    handleCheckAllChange(row) {
      var tempArr = [];
      row.chilDren.map(item => {
        tempArr.push(item.MenuID);
      });
      row.MenuRolesData = row.isMenuRole ? tempArr : [];
      row.isIndeterminate = false;
    },
    handleCheckedDataChange(row) {
      let checkedCount = row.MenuRolesData.length;
      row.isMenuRole = checkedCount === row.chilDren.length;
      row.isIndeterminate =
        checkedCount > 0 && checkedCount < row.chilDren.length;
    }
  }
};
</script>
<style scoped lang="scss">
.el-table {
  height: 100%;
}

.el-checkbox {
  margin-left: 30px;
  margin-top: 5px;
  margin-bottom: 5px;
}
</style>