$(function () {
    
    $("#btnLogin").click(function(){
        LoginSys();
    });
    $("#userName").keydown(function (e) {
        var curkey = e.which;
        if (curkey == 13) {
            LoginSys();
            return false;
        }
    });
    $("#password").keydown(function (e) {
        var curkey = e.which;
        if (curkey == 13) {
            LoginSys();
            return false;
        }
    });
    $("#ValidateCode").keydown(function (e) {
        var curkey = e.which;
        if (curkey == 13) {
            LoginSys();
            return false;
        }
    }); 

});

function LoginSys() {
    $("#mes").html("");
    $("#userName").removeClass("input-validation-error");
    $("#password").removeClass("input-validation-error");
    $("#ValidateCode").removeClass("input-validation-error");
    if ($.trim($("#userName").val()) == "") {
        $("#userName").addClass("input-validation-error").focus();
        $("#mes").html("用户名不能为空！");
        return;
    }
    if ($.trim($("#password").val()) == "") {
        $("#password").addClass("input-validation-error").focus();
        $("#mes").html("密码不能为空！");
        return;
    }
    if ($.trim($("#ValidateCode").val()) == "") {
        $("#ValidateCode").addClass("input-validation-error").focus();
        $("#mes").html("验证码不能为空！");
        return;
    }
    $("#Loading").show();

    $.post('/Account/Login', { userName: $("#userName").val(), password: $("#password").val(), code: $("#validateCode").val() },
    function (data) {

        if (data.ResultType == "0") {
            window.location = "/Home/Index"
        } else {
            $("#mes").html(data.message);
        }
        $("#Loading").hide();
    }, "json");
    return false; 
}