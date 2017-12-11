<template>
  <el-dialog width="650px" :visible.sync="dialogVisible" :close-on-click-modal="false" :close-on-press-escape="false" :before-close="handleClose">
    <span slot="title">{{$route.name}}</span>
    <span>
      <el-form @submit.native.prevent :inline="true" :rules="rules" size="small" ref="ruleForm" :model="formData" label-width='80px' class="demo-form-inline">
        <el-form-item label="客户编码" prop="Code">
          <el-input v-model="formData.Code" placeholder="客户编码"></el-input>
        </el-form-item> 
        <el-form-item label="客户名称" prop="CustomerName">
          <el-input v-model="formData.CustomerName" placeholder="客户名称"></el-input>
        </el-form-item> 
        <el-form-item label="联系人" prop="LinkMan">
          <el-input v-model="formData.LinkMan" placeholder="联系人"></el-input>
        </el-form-item>  
        <el-form-item label="联系电话" prop="LinkManPhone">
          <el-input v-model="formData.LinkManPhone" placeholder="联系电话"></el-input>
        </el-form-item>  
        <el-form-item label="销售区域" prop="RegionName">
          <el-cascader :clearable="true" :options="RegionList" v-model="formData.RegionName" @change="handleChange"></el-cascader>
        </el-form-item>     
        <el-form-item label="有效否" prop="IsValid">
          <el-select v-model="formData.IsValid" placeholder="有效否">
            <el-option v-for="item in formData.IsValidList" :key="item.value" :label="item.label" :value="item.value">
            </el-option>
          </el-select>
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
import { FindBasCustomerForm, SaveBasCustomerForm } from "../../../api/api";
import custBotton from "./../../layout/layout_button";
export default {
  data() {
    return {
      dialogVisible: false,
      RegionList: [],
      formData: {},
      rules: {
        CustomerName: [{ required: true, message: "客户名称不能为空" }]
      }
    };
  },
  components: {
    custBotton
  },
  methods: {
    GetData(row) {
      FindBasCustomerForm(row).then(result => {
        this.RegionList = this.$parent.$parent.$refs.condition.RegionList;
        result.data.RegionName = result.data.Region.split(",");
        this.formData = result.data;
        this.dialogVisible = true;
      });
    },
    handleSave() {
      this.$refs.ruleForm.validate(valid => {
        if (valid) {
          SaveBasCustomerForm(this.formData).then(result => {
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
      this.$refs.ruleForm.resetFields();
      this.dialogVisible = false;
      done();
    },
    handleChange(value) {
      this.formData.Region = value.toString();
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