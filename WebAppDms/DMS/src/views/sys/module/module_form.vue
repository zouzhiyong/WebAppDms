<template>
  <el-dialog width="650px" :visible.sync="dialogVisible" :close-on-click-modal="false" :close-on-press-escape="false" :before-close="handleClose">
    <span slot="title">{{$route.name}}</span>
    <span>
      <el-form @submit.native.prevent :rules="rules" size="small" :inline="true" ref="ruleForm" :model="formData" label-width='80px' class="demo-form-inline">
        <el-form-item label="模块名称" prop="MenuName">
          <el-input v-model="formData.MenuName" placeholder="模块名称"></el-input>
        </el-form-item>
        <el-form-item label="上级模块" prop="MenuParentID">
          <el-select v-model="formData.MenuParentID" placeholder="上级模块">
            <el-option v-for="item in formData.MenuParentIDList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="模块地址" prop="MenuPath">
          <el-input v-model="formData.MenuPath" placeholder="模块地址"></el-input>
        </el-form-item>
        <el-form-item label="图标" prop="MenuIcon">
          <el-input v-model="formData.MenuIcon" placeholder="图标"></el-input>
        </el-form-item>
        <el-form-item label="有效否" prop="IsValid">
          <el-select v-model="formData.IsValid" placeholder="有效否">
            <el-option v-for="item in formData.IsValidList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="平台类型" prop="ApplicationNo">
          <el-select v-model="formData.ApplicationNo" placeholder="平台类型">
            <el-option v-for="item in formData.ApplicationNoList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="顺序号" prop="Xh">
          <el-input v-model="formData.Xh" placeholder="顺序号"></el-input>
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
import { FindSysMoudleForm, SaveSysMoudleForm } from "../../../api/api";
import custBotton from "./../../layout/layout_button";
export default {
  data() {
    return {
      dialogVisible: false,
      formData: {},
      rules: {
        MenuName: [{ required: true, message: "模块名称不能为空" }]
      }
    };
  },
  components: {
    custBotton
  },
  methods: {
    GetData(row) {
      FindSysMoudleForm(row).then(result => {
        this.formData = result.data;
        this.dialogVisible = true;
      });
    },
    handleSave() {
      this.$refs.ruleForm.validate(valid => {
        if (valid) {
          SaveSysMoudleForm(this.formData).then(result => {
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
.el-cascader,
.el-input,
.el-input__inner {
  width: 200px;
}
</style>