<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login3.aspx.cs" Inherits="SSO_Test.Login3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        #lblMessage {
            width: 287px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <label for="male">请输入TokenCode：</label>
            <input id="tokenID" type="text" />
            <input type="button" style="margin-left: 20px;" value=" 获取用户信息 " onclick="GetUserInfo()" />

        </p>
        <p>
            <textarea id="lblINFO" style="width: 100%; height: 130px" cols="20" rows="2"></textarea>
        </p>

    </form>
</body>
</html>
<script type="text/javascript">

    function GetUserInfo() {
        var tokenID = document.getElementById("tokenID").value;
        if (tokenID != "") {

            var postData = { CMD: "GetUserInfo", tokenID: tokenID };
            $.ajax({
                type: "Post",
                url: "Login3.aspx",
                data: postData,
                dataType: "text",
                success: function (returnVal) {
                    returnVal = $.parseJSON(returnVal);
                    var flg = returnVal.Data;
                    var lbl = document.getElementById("lblMessage");
                    lbl.value = flg;

                },
                error: function (errMsg) {
                    var lbl = document.getElementById("lblMessage");
                    lbl.value = flg;
                }
            });
        }
    }

    //}
</script>
