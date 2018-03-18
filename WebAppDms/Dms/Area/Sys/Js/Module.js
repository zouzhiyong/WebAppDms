var Module = function () {
    var $this = this;

    $this.index = function () {
        $this.VM = new Vue({
            el: "#SysModuleIndex180318",
            data: {
                treeData: [],
                defaultProps: {
                    children: "children",
                    label: "label"
                },
                columns: [
        { prop: "Code", label: "模块编码", width: "100", align: "left" },
        { prop: "ParentCode", label: "上级编码", width: "100", align: "left" },
        { prop: "Name", label: "模块名称", width: "150", align: "" },
        { prop: "URL", label: "模块路径", width: "200", align: "" },
        { prop: "ICON", label: "图标", width: "150", align: "" },
        {
            prop: "IsMenu",
            label: "是否菜单",
            width: "",
            align: "center",
            formatter: function (row, column) {
                return row.IsMenu == 0 ? "否" : "是";
            }
        },
        {
            prop: "IsValid",
            label: "有效否",
            width: "",
            align: "center",
            formatter: function (row, column) {
                return row.IsValid == 0 ? "否" : "是";
            }
        },
        { prop: "Sequence", label: "显示顺序", width: "", align: "right" }
                ],
                currentPage: 1,
                pageSize: 0,
                total: 0,
                conditionData: {},
                tableData: []
            },
            created: function () {
                this.iniData();
            },
            methods: {
                iniData: function () {
                    var _self = this;
                    var obj = [
                    {
                        FID: 0,
                        Code: "&",
                        label: "所有模块",
                        children: []
                    }];
                    ajaxData('api/Menu/FindSysModuleTree').then(function (result) {
                        obj[0].children = result.data;
                        _self.treeData = obj;
                        if (_self.treeData.length > 0) {
                            _self.$nextTick(function () {
                                _self.$refs.tree.setCurrentKey(_self.treeData[0].FID);
                                _self.handleNodeClick(_self.treeData[0]);
                            });
                        }
                    });
                },
                GetData: function () {
                    var _self = this;
                    _self.pageSize = Math.floor(_self.$refs.table.$el.clientHeight / 40);
                    _self.conditionData.currentPage = _self.currentPage;
                    _self.conditionData.pageSize = _self.pageSize;
                    ajaxData('api/Menu/FindSysMoudleTable', { data: JSON.stringify(_self.conditionData), contentType: "application/json;charset=UTF-8" }).then(function (result) {
                        _self.tableData = result.rows;
                        _self.total = result.total;
                    });
                },
                handleNodeClick(data) {
                    this.conditionData = data;
                    this.GetData();
                },
                handleDeleteClick(row) {
                    var _self = this;
                    this.$confirm("是否确认删除?", "提示", {
                        type: "warning"
                    })
                      .then(function () {
                          ajaxData('api/Menu/DeleteSysMoudleRow').then(function (result) {
                              _self.GetData();
                          });
                      })
                },
                handleEditClick(row) {
                    layer.open({
                        type: 2,
                        title: '菜单编辑',
                        shadeClose: false,
                        shade: 0.8,
                        maxmin: true, //开启最大化最小化按钮
                        area: ['893px', '600px'],
                        content: ''
                    });
                },
                handleCurrentChange(currentPage) {
                    this.currentPage = currentPage;
                    this.GetData();
                }
            }
        })
    }
}