<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" Inherits="SSO_Test.Login2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.8.2.min.js"></script>
    <script src="Handles/LoginOut.ashx"></script>
    <title>Domain.SSO.Client</title>
</head>
<body>
    <form id="form1" runat="server">

        <p>
            <label for="male">提示消息</label>
            <input id="lblMessage" type="text" />
            <input type="button" style="margin-left: 20px;" value=" 注 销 " onclick="LoginOut()" />
        </p>
    </form>
</body>
</html>

<script type="text/javascript">
   <%-- var LoginMessage = '<%=LoginMessage%>';
    if (LoginMessage != null)
    {
       
    }--%>

    function LoginOut() {
        var loginID = '<%=LoginID%>';
        var postData = { CMD: "LoginOut", loginID: loginID };
        $.ajax({
            type: "Post",
            url: "Handles/LoginOut.ashx",
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

    //}
</script>
