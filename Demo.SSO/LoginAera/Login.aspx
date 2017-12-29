<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LoginAera.Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>登录</title>
    <link rel="stylesheet" href="/LoginAera/css/reset.css" />
    <link rel="stylesheet" href="/LoginAera/css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="/LoginAera/css/style.css" />
    <script type="text/javascript" src="/LoginAera/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/LoginAera/Scripts/jquery-ui.min.js"></script>
    <script src="/LoginAera/Scripts/Validform_v5.3.1.js"></script>
    <script src="/LoginAera/Scripts/layer/layer.js"></script>


  

<script src="/LoginAera/Scripts/md5.js"></script>
    <!--[if IE]>
			<script src="/Scripts/html5.js"></script>
		<![endif]-->
    <style type="text/css">
        .sortable {
            width: 300px;
            float: right;
        }

            .sortable li {
                height: 44px;
                margin-bottom: 12px;
            }

                .sortable li.ui-state-default {
                    background: none;
                    border: none;
                }
    </style>
</head>
<body>

    <div class="login_wraps">
        <div class="login_center clearfix">
            <div class="two_code fl">
                <img src="/LoginAera/images/ma.jpg" alt="" />
            </div>
            <div class="login_wrap fr">
                <form id="loginform" name="loginform">
                    <ul id="sortable1" class="sortable">
                        <li class="ui-state-default">
                            <div class="row">
                                <i class="iconfont icon-user"></i>
                                <input type="text" placeholder="请输入登录名" class="input1" id="txt_loginName" datatype="*" nullmsg="请输入登录名！" />
                            </div>
                        </li>
                        <li class="ui-state-default">
                            <div class="row">
                                <i class="iconfont icon-password"></i>
                                <input type="password" placeholder="请输入密码" class="input1" id="txt_passWord" datatype="*" nullmsg="请输入密码" />
                            </div>
                        </li>
                        <li class="ui-state-default">
                            <div class="row">
                                <i class="iconfont icon-code"></i>
                                <input type="hidden" id="hidCode" name="hidCode" />
                                <input type="text" placeholder="请输入验证码" class="input2" name="inpCode" id="inpCode" datatype="iCode" nullmsg="请输入验证码！" errormsg="验证码输入错误！"  />
                                <a href="#" onclick="createCode()">
                                    <input type="text" id="checkCode" class="code" style="width: 50px" /></a>
                               
                                <%--<span class="code">adasd</span>--%>
                            </div>
                        </li>
                    </ul>

                    <div class="row">
                        <input type="checkbox" name="" id="rem_paddword" value="" /><label for="rem_paddword">记住密码</label>
                        <a href="javascript:void(0);" class="fr forget_paddword">忘记密码？</a>
                    </div>
                    <div class="row">
                        <input type="button" name="BtnLogin" id="BtnLogin" value="登录>" class="btn" />
                    </div>
                    <div class="row">
                        <a href="/Register.aspx" class="resi">还没有账号？立即注册 ></a>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <div class="footer—wrap"></div>
    <script type="text/javascript">
        $(function () {
            $('.footer—wrap').load('/CommonPage/footer.html');
            $("#sortable1").sortable({
                placeholder: "ui-state-highlight"
            });
            $("#sortable1").disableSelection();

            //加载验证码
            createCode();
            //回车提交事件
            $("body").keydown(function () {
                if (event.keyCode == "13") {//keyCode=13是回车键
                    $("#BtnLogin").click();
                }
            });

            var valiNewForm = $("#loginform").Validform({
                datatype: {
                    "iCode": function (gets, obj, curform, regxp) {
                        /*参数gets是获取到的表单元素值，
                          obj为当前表单元素，
                          curform为当前验证的表单，
                          regxp为内置的一些正则表达式的引用。*/
                        var reg1 = regxp["*"];

                        var hidcode = curform.find("#hidCode");
                        if (reg1.test(gets)) { if (hidcode.val().toUpperCase() == gets.toUpperCase()) { return true; } }


                        return false;
                    }
                },
                btnSubmit: "#BtnLogin",
                tiptype: 3,
                showAllError: true,
                beforeSubmit: function (curform) {
                    //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                    //这里明确return false的话表单将不会提交;	
                    Login();
                }
            })
        });
        var code; //在全局 定义验证码
        function createCode() {
            code = "";
            var codeLength = 4;//验证码的长度
            var checkCode = document.getElementById("checkCode");
            checkCode.value = "";
            var selectChar = new Array(1, 2, 3, 4, 5, 6, 7, 8, 9, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');

            for (var i = 0; i < codeLength; i++) {
                var charIndex = Math.floor(Math.random() * 60);
                code += selectChar[charIndex];
            }
            if (code.length != codeLength) {
                createCode();
            }
            checkCode.value = code;
            $("#hidCode").val(code);
        }

        function Login() {
            var loginName = $("#txt_loginName").val();
            var passWord = $("#txt_passWord").val();
            var postData = { Func: "Login", userName: loginName, password: hex_md5(passWord) };
            $.ajax({
                type: "Post",
                url: 'http://192.168.1.89:3001/SSO_Server/Handles/SSO_ServerCenter.ashx' + "?returnUrl=" + getQueryString('ds'),
                data: postData,
                dataType: "text",
                success: function (returnVal) {
                    window.location.href = "http://www.baidu.com";
                    if (returnVal.length < 100) {
                        returnVal = $.parseJSON(returnVal);
                        var flg = returnVal.Data;
                        if (flg != "" || flg != null) {

                        }
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //console.log(errorThrown);
                }
            });
            //$.ajax({
            //    url: "/Common.ashx",
            //    async: false,
            //    dataType: "json",
            //    data: { "Func": "Login", "loginName": loginName, "passWord": passWord },
            //    success: OnSuccessLogin,
            //    error: OnErrorLogin

            //});
        }

        function OnSuccessLogin(json) {
            var cookie = json.result;
            debugger;
            if (cookie.errNum == "0") {
                $.cookie('LoginCookie_Cube', JSON.stringify(cookie.retData[0]), { path: '/', secure: false });
            } else if (cookie.errNum == "333") {
                layer.msg("帐号已被禁用请联系管理员！");
            } else if (cookie.errNum == "444") {
                layer.msg("帐号已被删除请重新注册！");
            } else if (cookie.errNum == "999") {
                layer.msg("用户名或密码错误！");
            }
            else {
                layer.msg(json.result.errMsg + "！");
            }
        }
        function OnErrorLogin(XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("登录名或密码错误！" + errorThrown);
        }


        //js 获取地址栏参数
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return decodeURI(r[2]);
            return null;
        }

    </script>
</body>
</html>
