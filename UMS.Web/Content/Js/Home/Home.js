$(function () {
    $.post("/Home/GetTree", { id: "0" },
        function (data) {
          
            if (data == "0") {
                window.location = "/Account";
            } 
            $(data).each(function (i,el) {
                var id = el.id;
                $('#menuPanel').accordion('add', {
                    id:id,
                    title: el.text,
                    content: '<div  style="background-color:#fff;padding:5px"><ul id="tree'+i+'" name="' + el.text + '" class="easyui-tree"></ul></div>',
                    selected: false,
                    iconCls: el.iconCls
                });
               
                //加载子节点，即二级菜单 
                $.post("/Home/GetTree?id=" + id, function (data) {//循环创建树的项
                    $("ul[name = '" + el.text + "']").tree({
                        data: data,
                        onBeforeExpand: function (node, param) {
                            $("#tree" + id).tree('options').url = "/@info/Home/GetTree?id=" + node.id;
                        },
                        onClick: function (node) {
                            if (node.state == 'closed') {
                                $(this).tree('expand', node.target);
                            }  else {
                                var tabTitle = node.text;
                                var url = "../../" + node.value;
                                var icon = node.iconCls;
                                addTab(tabTitle, url, icon);
                            }
                        }
                    });
                }, 'json');


            });
        }, "json");

    ////异步 
    //$('#menuPanel').accordion({
    //    onSelect: function (title, index) {
            
    //       var p = $('#menuPanel').accordion('getSelected');
    //        //查找id
    //       var id= p[0].attributes.id.value;
    //        var o = {
    //            showcheck: false,
    //            url: "/Home/GetTree",
    //            queryParams: { "id": id },
    //            lines: true,
    //            onnodeclick: function (item) {
    //                var tabTitle = item.text;
    //                var url = "../../" + item.value;
    //                var icon = item.iconCls;
    //                if (!item.hasChildren) {
    //                    addTab(tabTitle, url, icon);
    //                } else {
    //                    $(this).parent().find("img").trigger("click");
    //                }
    //            }
    //        }
    //        $("ul[name = '" + title + "']").tree(o);
    //    }
    //});
});