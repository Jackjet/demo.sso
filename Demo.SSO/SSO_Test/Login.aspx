<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SSO_Test.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.8.2.min.js"></script>
    <script src="Scripts/md5.js"></script>
    <script src="Handles/Login.ashx"></script>
    <title>login - Domain site2</title>
</head>


<body>
    <form id="form1" runat="server">
        <h1>首页登录</h1>
        <div id="divs">

            <label for="male">用户名:</label>
            <input id="txtUserName" type="text" style="margin-left: 17px" value="tang" />
            <label for="male" >密码</label>
            <input id="txtPassword" value="123456" type="password" />

            <input type="button" style="margin-left: 20px;" value=" 登 录 " onclick="Login()" />
        </div>

        <p>
            <label for="male">TokenID：</label>
            <input id="lblMessage" type="text" />
            <input type="button" value=" 获取用户信息 " onclick="GetUserInfoByToken()" />

        </p>
        <p>
            <textarea id="lblINFO" style="width: 100%; height: 130px" cols="20" rows="2"></textarea>
        </p>
    </form>
</body>
</html>


<script type="text/javascript">


    function Login() {

        document.getElementById("lblMessage").value = "";

        var userName = document.getElementById("txtUserName").value;
        var password = document.getElementById("txtPassword").value;
        password = hex_md5(password);
        if (userName == null || userName == "") {
            alert("用户名不能为空");
        }
        if (password == null || password == "") {
            alert("密码不能为空");
        }

        var postData = { Func: "Login", userName: userName, password: password };
        $.ajax({
            type: "Post",
            url: "http://192.168.1.26:3001/SSO_Server/Handles/SSO_ServerCenter.ashx?returnUrl=http://192.168.1.26:3001/SSO_Test/Login.aspx",
            data: postData,
            dataType: "jsonp",
            jsonp: "jsoncallback",
            success: function (returnVal) {
                var result = returnVal.result;
                document.getElementById("lblMessage").value = result;

            },
            error: function (errMsg) {
                document.getElementById("lblMessage").value = errMsg;
            }
        });
    }

    function GetUserInfoByToken() {
        var tokenID = document.getElementById("lblMessage").value;

        if (tokenID != "") {
            var postData = { Func: "GetUserInfoByToken", tokenID: tokenID };
            $.ajax({
                type: "Post",
                url: "http://192.168.1.26:3001/SSO_Server/Handles/SSO_ServerCenter.ashx?returnUrl=http://192.168.1.26:3001/SSO_Test/Login.aspx",
                data: postData,
                dataType: "jsonp",
                jsonp: "jsoncallback",
                success: function (returnVal) {
                    var flg = returnVal.result;
                    if (flg != null) {
                        if (flg.errNum == 0) {
                            var item = flg.retData;
                            //$.cookie('TokenCookie_Cube', tokenID, { path: '/', secure: false });
                            //if (item.CreateTime != null) item.CreateTime = DateTimeConvert(item.CreateTime);
                            //if (item.EditTime != null) item.EditTime = DateTimeConvert(item.EditTime);
                            //if (item.Birthday != null) item.Birthday = DateTimeConvert(item.Birthday);

                            //lblINFO.value = item.userName;

                            //$(lblINFO).val(item.userName)
                            var lbl = document.getElementById("lblINFO");
                            lbl.value = JSON.stringify(item);
                            //$.cookie('LoginCookie_Cube', JSON.stringify(item), { expires: 7, path: '/', secure: false });
                            //if ($("#rem_paddword").is(":checked")) $.cookie('RememberCookie_Cube', $("#txt_passWord").val(), { expires: 7, path: '/', secure: false });
                            //if ($("#hidPreUrl").val() != "" && ($("#hidPreUrl").val().toLocaleLowerCase().indexOf("login.aspx") < -1 || $("#hidPreUrl").val().toLocaleLowerCase().indexOf("register.aspx") < -1)) window.location = $("#hidPreUrl").val();
                            //else window.location = "/Index.aspx";
                        } else {

                        }
                    }

                },
                error: function (errMsg) {
                    var lbl = document.getElementById("lblINFO");
                    lbl.value = flg;
                }
            });
        }
    }

</script>

