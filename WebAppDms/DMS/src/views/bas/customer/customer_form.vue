<template>
  <el-dialog width="550px" :visible.sync="dialogVisible" :close-on-click-modal="false" :close-on-press-escape="false" :before-close="handleClose">
    <span slot="title">{{$route.name}}</span>
    <span>
      <el-col :span="15">
<el-form @submit.native.prevent :inline="false" :rules="rules" size="small" ref="ruleForm" :model="formData" label-width='80px' class="demo-form-inline">
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
      </el-col>
      <el-col :span="9">
          <el-upload ref="upload" class="avatar-uploader" accept="image/png,image/jpeg" :headers="headers" action="/WebAppDms/api/customer/uploadPost" :show-file-list="false" :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload">
            <img v-if="imageUrl" :src="imageUrl" class="avatar">
            <i v-else class="el-icon-plus avatar-uploader-icon"></i>
          </el-upload>
      </el-col>
     
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
let ticket = "BasicAuth " + sessionStorage.getItem("Ticket") || "";
export default {
  data() {
    return {
      headers: {
        Authorization: ticket
      },
      imageUrl: "",
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
            this.$parent.$parent.$refs.table.$refs.table.GetData();
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
      this.$refs.upload.clearFiles();
      done();
    },
    handleChange(value) {
      this.formData.Region = value.toString();
    },
    handleAvatarSuccess(res, file) {
      this.imageUrl = URL.createObjectURL(file.raw);
    },
    beforeAvatarUpload(file) {
      const isJPG = file.type === "image/png" || file.type === "image/jpeg";
      const isLt2M = file.size / 1024 / 1024 < 2;

      if (!isJPG) {
        this.$message.error("上传头像图片只能是 JPG、PNG 格式!");
      }
      if (!isLt2M) {
        this.$message.error("上传头像图片大小不能超过 2MB!");
      }
      return isJPG && isLt2M;
    }
  }
};
</script>

<style scoped  lang="scss">
.el-cascader,
.el-input,
.el-input__inner {
  width: 200px;
}
.avatar-uploader /deep/ .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader /deep/ .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>