﻿    $(function () {
        $('#List').treegrid({
            url: 'Module/GetList',
            width: $(window).width() - 270,
            method: 'get',
            height: $(window).height() - 35,
            fitColumns: true,
            sortName: 'Sort',
            sortOrder: 'asc',
            treeField: 'Name',
            idField: 'Id',
            pagination: false,
            striped: true, //奇偶行是否区分
            singleSelect: true,//单选模式
            //rownumbers: true,//行号
            columns: [[
                { field: 'Id', title: '唯一标识', width: 120},
                { field: 'Name', title: '名称', width: 220, sortable: true },
                { field: 'EnglishName', title: '英文名称', width: 80, sortable: true,hidden:true },
                { field: 'ParentId', title: '上级Id', width: 80, sortable: true },
                { field: 'Url', title: '链接地址', width: 80, sortable: true },
                { field: 'Iconic', title: '图标', width: 80, sortable: true },
                { field: 'Sort', title: '排序号', width: 80, sortable: true },
                { field: 'Remark', title: '说明', width: 80, sortable: true },
                 {
                     field: 'Enable', title: '是否启用', width: 60,align:'center', formatter: function (value) {
                         if (value) {
                             return "<img src='/Content/Images/icon/pass.png'/>";
                         } else {
                             return "<img src='/Content/Images/icon/no.png'/>";
                         }
                     }
                 },
                { field: 'CreateUser', title: '创建人', width: 80, sortable: true },
                { field: 'CreateTime', title: '创建时间', width: 120, sortable: true },
                {
                    field: 'IsLast', title: '是否最后一项', align: 'center', width: 100, formatter: function (value) {
                        if (value) {
                            return "是";
                        } else {
                            return "否";
                        }
                    }
                },
            ]],
            onClickRow: function (index, data) {
                var row = $('#List').treegrid('getSelected');
                if (row != null) {
                    $('#OptList').datagrid({
                        url: 'Module/GetOptListByModule?mid=' + row.Id,
                        method: 'post'
                    });
                }
            }
        });

        //$("#List").treegrid({
        //    url: '/Content/mdata.json',
        //    idField: 'id',
        //    treeField: 'name',
        //    method: 'get',
        //    columns: [[
        //        { title: 'Task Name', field: 'name', width: 180 },
        //        { field: 'progress', title: 'progress', width: 60, align: 'right' },
        //        { field: 'begin', title: 'Begin Date', width: 80 },
        //        { field: 'end', title: 'End Date', width: 80 }
        //    ]]
        //});


        $('#OptList').datagrid({
            url: 'Module/GetOptListByModule',
            width: 255,
            method: 'post',
            height: $(window).height() - 35,
            fitColumns: true,
            sortName: 'Sort',
            sortOrder: 'asc',
            idField: 'Id',
            pageSize: 1000,
            pagination: false,
            striped: true, //奇偶行是否区分
            singleSelect: true,//单选模式
            //rownumbers: true,//行号
            columns: [[
                { field: 'Id', title: '', width: 80, hidden: true },
                { field: 'Name', title: '名称', width: 80, sortable: true },
                { field: 'KeyCode', title: '操作码', width: 80, sortable: true },
                { field: 'ModuleId', title: '所属模块', width: 80, sortable: true, hidden: true },
                 {
                     field: 'IsValid', title: '是否验证', width: 80, align: 'center', formatter: function (value) {
                         if (value) {
                             return "<img src='/Content/Images/icon/pass.png'/>";
                         } else {
                             return "<img src='/Content/Images/icon/no.png'/>";
                         }
                     }
                 },
                { field: 'Sort', title: '排序', width: 80, sortable: true }
            ]]
        });

        //自动宽高
        $(window).resize(function () {
            $('#List').datagrid('resize', {
                width: $(window).width() - 270,
                height: $(window).height() - 35
            }).datagrid('resize', {
                width: $(window).width() - 270,
                height: $(window).height() - 35
            });
            $('#OptList').datagrid('resize', {
                height: $(window).height() - 35
            }).datagrid('resize', {
                height: $(window).height() - 35
            });
        });
    });

$(function () {
    $("#btnCreate").click(function () {
        var row = $('#List').treegrid('getSelected');
        $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='no' frameborder='0'' src='/Module/Create?id=" + (row != null ? row.Id : "0") + "&timestamp=" + new Date().getTime() + "'></iframe>");
        $("#modalwindow").window({ title: '新增', width: 700, height: 400, iconCls: 'icon-add' }).window('open');
    });
    $("#btnEdit").click(function () {
        var row = $('#List').treegrid('getSelected');
        if (row != null) {
            $("#modalwindow").html("<iframe width='100%' height='99%'  frameborder='0' src='/Module/Edit?id=" + row.Id + "&timestamp=" + new Date().getTime() + "'></iframe>");
            $("#modalwindow").window({ title: '编辑', width: 700, height: 430, iconCls: 'icon-edit' }).window('open');
        } else { $.messageBox5s('提示', '@Suggestion.PleaseChooseToOperatingRecords'); }
    });
    $("#btnDelete").click(function () {
        var row = $('#List').treegrid('getSelected');
        if (row != null) {
            $.messager.confirm('提示', '@Suggestion.YouWantToDeleteTheSelectedRecords', function (r) {
                if (r) {
                    $.post("/Module/Delete?id=" + row.Id, function (data) {
                        if (data.type == 1)
                            $("#List").treegrid('reload');
                        $.messageBox5s('提示', data.message);
                    }, "json");

                }
            });
        } else { $.messageBox5s('提示', '@Suggestion.PleaseChooseToOperatingRecords'); }
    });

    $("#btnCreateOpt").click(function () {
        var row = $('#List').treegrid('getSelected');
        if (row != null) {
            if (row.IsLast) {
                $("#modalwindow").html("<iframe width='100%' height='99%'  frameborder='0' src='/Module/CreateOpt?moduleId=" + row.Id + "&timestamp=" + new Date().getTime() + "'></iframe>");
                $("#modalwindow").window({ title: '新增操作码', width: 500, height: 330, iconCls: 'icon-edit' }).window('open');

            } else {
                $.messageBox5s('提示', '只有最后一项的菜单才能设置操作码!');
            }

        } else { $.messageBox5s('提示', '请选择一个要赋予操作码的模块!'); }
    });
    $("#btnDeleteOpt").click(function () {
        var row = $('#OptList').datagrid('getSelected');
        if (row != null) {
            $.messager.confirm('提示', '您确定要删除“' + row.Name+ '”这个操作码？', function (r) {
                if (r) {
                    $.post("/Module/DeleteOpt?id=" + row.Id, function (data) {
                        if (data.ReturnType == 0)
                        {
                            $("#OptList").datagrid('load');
                        }
                    }, "json");

                }
            });
        } else { $.messageBox5s('提示', '请选择一个要赋予操作码的模块!'); }
    });
});
 