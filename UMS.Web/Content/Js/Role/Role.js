$(function () {

    $('#List').datagrid({
        url: '/Role/GetList',
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
        columns: [[
            { field: 'Id', title: '编号', width: 80, align: 'center' },
            { field: 'Name', title: '名称', width: 120, align: 'center' },
            { field: 'Description', title: '说明', width: 120, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 60, align: 'center' }
        ]],
        onLoadSuccess: function (data) {
            if(data.total==0)
                alert("没有数据。")
        }
    });


    $("#btnCreate").click(function () {
        $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='no' frameborder='0'' src='/Role/Create?timestamp=" + new Date().getTime() + "'></iframe>");
        $("#modalwindow").window({
            title: '新增', width: 700, height: 400, iconCls: 'icon-add'
        }).window('open');
    });
    $("#btnEdit").click(function () {
        var row = $('#List').datagrid('getSelected');
        if (row != null) {
            $("#modalwindow").html("<iframe width='100%' height='99%'  frameborder='0' src='/Role/Edit?id=" + row.Id + "&timestamp="+new Date().getTime() + "'></iframe>");
            $("#modalwindow").window({
                title: '编辑', width: 700, height: 430, iconCls: 'icon-edit'}).window('open');
        } else { $.messageBox5s('提示', '@Suggestion.PleaseChooseToOperatingRecords'); }
    });
    $("#btnDetails").click(function () {
        var row = $('#List').datagrid('getSelected');
        if (row != null) {
            $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='no' frameborder='0' src='/Role/Details?id=" + row.Id + "&timestamp=" + new Date().getTime() + "'></iframe>");
            $("#modalwindow").window({ title: '详细', width: 500, height: 300, iconCls: 'icon-details' }).window('open');
        } else { $.messageBox5s('提示', '@Suggestion.PleaseChooseToOperatingRecords'); }
    });
    $("#btnQuery").click(function () {
        var queryStr = $("#txtQuery").val();
        if (queryStr == null) {
            queryStr = "%";
        }
        $('#List').datagrid({
            url: '@Url.Action("GetList")?queryStr=' + encodeURI(queryStr)
        });

    });
    $("#btnDelete").click(function () {
        var row = $('#List').datagrid('getSelected');
        if (row != null) {
            $.messager.confirm('提示', '@Suggestion.YouWantToDeleteTheSelectedRecords', function (r) {
                if (r) {
                    $.post("@Url.Action('Delete')?id=" + row.Id, function (data) {
                        if (data.type == 1)
                            $("#List").datagrid('load');
                        $.messageBox5s('提示', data.message);
                    }, "json");

                }
            });
        } else { $.messageBox5s('提示', '@Suggestion.PleaseChooseToOperatingRecords'); }
    });
    $("#btnAllot").click(function () {
        var row = $('#List').datagrid('getSelected');
        if (row != null) {
            $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='no' frameborder='0' src='/Role/GetUserByRole?roleId=" + row.Id + "&timestamp=" + new Date().getTime() + "'></iframe>");
            $("#modalwindow").window({ title: '设置角色包含的用户', width: 500, height: 300, iconCls: 'icon-details' }).window('open');
        } else { $.messageBox5s('提示', '@Suggestion.PleaseChooseToOperatingRecords'); }
    });
})
 