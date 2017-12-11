<template>
  <el-dialog width="650px" :visible.sync="dialogVisible" :close-on-click-modal="false" :close-on-press-escape="false" :before-close="handleClose">
    <span slot="title">{{$route.name}}</span>
    <span>
      <el-form @submit.native.prevent :rules="rules" size="small" :inline="true" ref="ruleForm" :model="formData" label-width='80px' class="demo-form-inline">
        <el-form-item label="账号" prop="LoginName">
          <el-input v-model="formData.LoginName" placeholder="账号"></el-input>
        </el-form-item>
        <el-form-item label="角色" prop="RoleID">
          <el-select v-model="formData.RoleID" placeholder="角色">
            <el-option v-for="item in formData.RoleIDList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="用户类型" prop="UserTypeID">
          <el-select v-model="formData.UserTypeID" placeholder="用户类型">
            <el-option v-for="item in formData.UserTypeIDList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="部门" prop="DeptID">
          <el-select v-model="formData.DeptID" placeholder="部门">
            <el-option v-for="item in formData.DeptIDList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="真实姓名" prop="RealName">
          <el-input v-model="formData.RealName" placeholder="真实姓名"></el-input>
        </el-form-item>
        <el-form-item label="电话号码" prop="Phone">
          <el-input v-model="formData.Phone" placeholder="电话号码"></el-input>
        </el-form-item>
        <el-form-item label="邮箱" prop="Email">
          <el-input v-model="formData.Email" placeholder="邮箱"></el-input>
        </el-form-item>
        <el-form-item label="手机串号" prop="IMEICode">
          <el-input v-model="formData.IMEICode" placeholder="手机串号"></el-input>
        </el-form-item>
        <el-form-item label="有效否" prop="IsValid">
          <el-select v-model="formData.IsValid" placeholder="有效否">
            <el-option v-for="item in formData.IsValidList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>             
        <!-- <el-form-item label="图像" prop="Avatar">
          <el-input v-model="formData.Avatar" placeholder="图像"></el-input>
        </el-form-item> -->
        <el-form-item label="工作地点" prop="WorkingPlace">
          <el-input v-model="formData.WorkingPlace" placeholder="工作地点"></el-input>
        </el-form-item>   
      </el-form>
    </span>
    <span slot="footer">
      <custBotton>
        <el-button slot="cancleButton" @click="handleCanle">取 消</el-button>
        <el-button slot="saveButton" type="primary" @click="handleSave">保存</el-button>
      </custBotton>
    </span>
  </el-dialog>
</template>

<script>
import { FindSysUserForm, SaveSysUserForm } from "../../../api/api";
import custBotton from "./../../layout/layout_button";
export default {
  data() {
    return {
      dialogVisible: false,
      formData: {},
      rules: {
        LoginName: [{ required: true, message: "账号不能为空" }],
        RealName: [{ required: true, message: "姓名不能为空" }],
        RoleID: [{ required: true, message: "角色不能为空" }],
        UserTypeID: [{ required: true, message: "用户类型不能为空" }],
        Email: [{ type: "email", message: "请输入正确的邮箱地址" }]
        // DeptID: [{ required: true, message: "部门不能为空" }]
      }
    };
  },
  components: {
    custBotton
  },
  methods: {
    GetData(row) {
      FindSysUserForm(row).then(result => {
        this.formData = result.data;
        this.dialogVisible = true;
      });
    },
    handleSave() {
      this.$refs.ruleForm.validate(valid => {
        if (valid) {
          SaveSysUserForm(this.formData).then(result => {
            this.dialogVisible = false;
            this.$parent.$parent.$refs.table.GetData();
            this.$refs.ruleForm.resetFields();
          });
        } else {
          return false;
        }
      });
    },
    handleCanle() {
      this.dialogVisible = false;
      this.$refs.ruleForm.resetFields();
    },
    handleClose(done) {
      this.dialogVisible = false;
      this.$refs.ruleForm.resetFields();
      done();
    }
  }
};
</script>

<style scoped>
.el-input,
.el-input__inner {
  width: 200px;
}
</style>