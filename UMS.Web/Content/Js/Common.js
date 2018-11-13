/** 
* 在iframe中调用，在父窗口中出提示框(herf方式不用调父窗口)
*/
$.extend({
    messageBox5s: function (title, msg) {
        $.messager.show({
            title: title, msg: msg, timeout: 5000, showType: 'slide', style: {
                left: '',
                right: 5,
                top: '',
                bottom: -document.body.scrollTop - document.documentElement.scrollTop + 5
            }
        });
    }
});
$.extend({
    messageBox10s: function (title, msg) {
        $.messager.show({
            title: title, msg: msg, height: 'auto', width: 'auto', timeout: 10000, showType: 'slide', style: {
                left: '',
                right: 5,
                top: '',
                bottom: -document.body.scrollTop - document.documentElement.scrollTop + 5
            }
        });
    }
});
$.extend({
    show_alert: function (strTitle, strMsg) {
        $.messager.alert(strTitle, strMsg);
    }
});

//ifram 返回
function frameReturnByClose() {
    $("#modalwindow").window('close');
}
function frameReturnByReload(flag) {
    if (flag)
        $("#List").treegrid('reload');
    else
        $("#List").treegrid('load');
}
function frameReturnByReloadOpt(flag) {
    if (flag)
        $("#OptList").datagrid('load');
    else
        $("#OptList").datagrid('reload');
}
function frameReturnByMes(mes) {
    $.messageBox5s('提示', mes);
}

function SetGridWidthSub(w) {
    return $(window).width() - w;
}
function SetGridHeightSub(h) {
    return $(window).height() - h
}

/** 
* panel关闭时回收内存，主要用于layout使用iframe嵌入网页时的内存泄漏问题
*/
$.fn.panel.defaults.onBeforeDestroy = function () {

    var frame = $('iframe', this);
    try {
        // alert('销毁，清理内存');
        if (frame.length > 0) {
            for (var i = 0; i < frame.length; i++) {
                frame[i].contentWindow.document.write('');
                frame[i].contentWindow.close();
            }
            frame.remove();
            if ($.browser.msie) {
                CollectGarbage();
            }
        }
    } catch (e) {
    }
};


// [IntRangeExpression(18, 30)] 数字在18与30之间,可以不填写，但填写就进入验证
jQuery.validator.addMethod('isinteger', function (value, element, params) {
    if (value >= parseInt(params['param1']) && value <= parseInt(params['param2']))
        return true;
    return false;
});
jQuery.validator.unobtrusive.adapters.add('isinteger', ['param1', 'param2'],
            function (options) {
                options.rules['isinteger'] = { param1: options.params.param1, param2: options.params.param2 };
                options.messages['isinteger'] = options.message;
            }
        );
// [MaxWordsExpression(50)]字符数在不能大50，可以不填写，但填写就进入验证
jQuery.validator.addMethod('checkMaxWords', function (value, element, param) {
    if (strLen(value) > param)
        return false;
    return true;
});
jQuery.validator.unobtrusive.adapters.add('maxwords', ['param'],
            function (options) {
                options.rules['checkMaxWords'] = { param: options.params.param };
                options.messages['checkMaxWords'] = options.message;
            }
         );
// [MinWordsExpression(50)]字符数在不能shaoyu 50，可以不填写，但填写就进入验证
jQuery.validator.addMethod('checkMinWords', function (value, element, param) {
    if (strLen(value) < param)
        return false;
    return true;
});
jQuery.validator.unobtrusive.adapters.add('minwords', ['param'],
            function (options) {
                options.rules['checkMinWords'] = { param: options.params.param };
                options.messages['checkMinWords'] = options.message;
            }
         );
// [NotEqualExpression("abcd")]，不能等于指定的值，可以不填写：如不能等于abcd
jQuery.validator.addMethod('checkNotEqual', function (value, element, param) {
    if (value == param)
        return false;
    return true;
});
jQuery.validator.unobtrusive.adapters.add('notequal', ['param'],
            function (options) {
                options.rules['checkNotEqual'] = { param: options.params.param };
                options.messages['checkNotEqual'] = options.message;
            }
         );
// [ContainExpression("abc")]  验证是否包含指定字符串，可以不填写：如必须包含abc
jQuery.validator.addMethod('checkContain', function (value, element, param) {
    if (value.indexOf(param) == -1)
        return false;
    return true;
});
jQuery.validator.unobtrusive.adapters.add('contain', ['param'],
            function (options) {
                options.rules['checkContain'] = { param: options.params.param };
                options.messages['checkContain'] = options.message;
            }
         );

// [NotContainExpression("abc")]，验证不能指定字符串,可以不填写,如不能含有abc
var v_checknotcontainReturn = "";
jQuery.validator.addMethod('checkNotContain', function (value, element, param) {
    if (value.indexOf(param) != -1)
        return false;
    return true;
});
jQuery.validator.unobtrusive.adapters.add('notcontain', ['param'],
            function (options) {
                options.rules['checkNotContain'] = { param: options.params.param };
                options.messages['checkNotContain'] = options.message;
            }
         );