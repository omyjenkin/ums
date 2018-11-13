$(function () {

    $('#List').datagrid({
        url: '/Log/GetList',
        width: $(window).width() - 10,
        method: 'post',
        height: $(window).height() - 35,
        fitColumns: true,
        sortName: 'CreateTime',
        sortOrder: 'desc',
        idField: 'Id',
        striped: true, //奇偶行是否区分
        singleSelect: true,//单选模式
        rownumbers: true,//行号
        pageSize: 10,
        pageList: [10, 20, 30, 40, 50],
        pagination: true,
        columns: [[
            { field: 'Id', title: '编号', width: 80, align: 'center' },
            { field: 'Category', title: '类别', width: 120, align: 'center' },
            { field: 'Message', title: '消息', width: 120, align: 'center' },
            { field: 'Exception', title: '异常', width: 120, align: 'center' },
            { field: 'Params', title: '参数', width: 120, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 60, align: 'center' }
        ]],
        onLoadSuccess: function (data) {
            //if (data.total == 0)
            //    alert("没有数据。")
        }
    });
}