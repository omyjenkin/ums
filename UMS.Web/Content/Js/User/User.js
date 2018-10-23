$(function () {

    $('#List').datagrid({
        url: '/User/GetList',
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
            { field: 'UserName', title: '姓名', width: 120, align: 'center' },
            { field: 'TrueName', title: '说明', width: 120, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 60, align: 'center' }
        ]],
        onLoadSuccess: function (data) {
            //if (data.total == 0)
            //    alert("没有数据。")
        }
    });


    $("#btnCreate").click(function () {
        $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='auto' frameborder='0'' src='/User/Create?timestamp=" + new Date().getTime() + "'></iframe>");
        $("#modalwindow").window({
            title: '新增', width: 700, height: 400, iconCls: 'icon-add'
        }).window('open');
    });
    $("#btnEdit").click(function () {
        var row = $('#List').datagrid('getSelected');
        if (row != null) {
            $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='auto' frameborder='0' src='/User/Edit?id=" + row.Id + "&timestamp=" + new Date().getTime() + "'></iframe>");
            $("#modalwindow").window({
                title: '编辑', width: 700, height: 430, iconCls: 'icon-edit'
            }).window('open');
        } else { $.messageBox5s('提示', '@Suggestion.PleaseChooseToOperatingRecords'); }
    });
    $("#btnDetails").click(function () {
        var row = $('#List').datagrid('getSelected');
        if (row != null) {
            $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='no' frameborder='0' src='/User/Details?id=" + row.Id + "&timestamp=" + new Date().getTime() + "'></iframe>");
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
                    $.post("/User/Delete?id=" + row.Id, function (data) {
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
            $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='no' frameborder='0' src='/User/GetRoleByUser?userId=" + row.Id + "&timestamp=" + new Date().getTime() + "'></iframe>");
            $("#modalwindow").window({ title: '分配角色', width: 500, height: 300, iconCls: 'icon-details' }).window('open');
        } else { $.messageBox5s('提示', '@Suggestion.PleaseChooseToOperatingRecords'); }
    });

    $("#btnSave").click(function () {
        console.log('aaaaa');
        $.ajax({
            url: "/User/Create",
            type: "Post",
            data: $("#CreateForm").serialize(),
            dataType: "json",
            success: function (data) {
                if (data.ResultType == 0) {
                    window.parent.frameReturnByMes(data.Message);
                    window.parent.frameReturnByReload(true);
                    window.parent.frameReturnByClose()
                }
                else {
                    window.parent.frameReturnByMes(data.Message);
                }
            }
        });
    });
    $("#btnReturn").click(function () {
        $("#modalwindow").window('close');
        window.parent.frameReturnByClose();
    });

    $.getScript("/Content/Js/ext.js");
})
