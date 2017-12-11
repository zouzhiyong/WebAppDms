<template>
  <el-dialog width="340px" :visible.sync="dialogVisible" :close-on-click-modal="false" :close-on-press-escape="false" :before-close="handleClose">
    <span slot="title">{{$route.name}}</span>
    <span>
      <el-form @submit.native.prevent :rules="rules" size="small" ref="ruleForm" :model="formData" label-width='80px' class="demo-form-inline">
        <el-form-item label="部门名称" prop="Name">
          <el-input v-model="formData.Name" placeholder="部门名称"></el-input>
        </el-form-item>        
        <el-form-item label="有效否" prop="IsValid">
          <el-select v-model="formData.IsValid" placeholder="有效否">
            <el-option v-for="item in formData.IsValidList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>   
        <el-form-item label="说明" prop="Comment">
          <el-input v-model="formData.Comment" placeholder="角色名称"></el-input>
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
import { FindSysDeptForm, SaveSysDeptForm } from "../../../api/api";
import custBotton from "./../../layout/layout_button";
export default {
  data() {
    return {
      dialogVisible: false,
      formData: {},
      rules: {
        Name: [{ required: true, message: "部门名称不能为空" }]
      }
    };
  },
  components: {
    custBotton
  },
  methods: {
    GetData(row) {
      FindSysDeptForm(row).then(result => {
        this.formData = result.data;
        this.dialogVisible = true;
      });
    },
    handleSave() {
      this.$refs.ruleForm.validate(valid => {
        if (valid) {
          SaveSysDeptForm(this.formData).then(result => {
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