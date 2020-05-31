<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicTable.aspx.cs" Inherits="SelfAspNet.Traning.DynamicTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .tyohyoDiv {
        }

        .hokenDiv1 {
            float: left;
            height: 240px;
            margin-right: 5px;
            width: 720px;
        }

        .hokenDiv2 {
            float: none;
            width: auto;
            height: auto;
            padding: 5px;
            margin-left: 720px;
            background-color: yellow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 1200px;">
        <div style="width: 1110px; margin-left: 9px;">
            header
        </div>
        <div style="width: 1110px; height: 240px; margin-left: 9px; overflow: hidden; box-sizing: border-box;">
            <div  style="max-width: 720px; height: auto; float: left;">
                <table id="ChohyoDiv" style="width: 720px; height: 240px; border: none; box-sizing: border-box; padding: 0; margin: 0;">
                    <tr>
                        <td>piyopiyo</td>
                    </tr>
                </table>
            </div>
            <div id="HokenDiv" style="max-height: 240px; float: left; background-color: aqua; padding: 5px; box-sizing: border-box;">
                <asp:Label ID="lblReportSetting" runat="server" Text="帳票設定" Style="font-weight: bold; box-sizing: border-box;" />
                <div style="border: 1px solid; width: auto; height: auto; box-sizing: border-box;">
                    <ul style=" float: flex; list-style-type: none; padding: 5px; margin: 0; flex-wrap: wrap; box-sizing: border-box; overflow: hidden; justify-content: center;">
                        <li style="box-sizing: border-box; width: 350px; height: 40px;">
                            <asp:Label ID="test4" runat="server" Text="帳票:" Width="90px" Style="text-align: right;" />
                            <asp:Label ID="Label2" runat="server" Text="*" Width="20px" Style="text-align: left;" />
                            <asp:TextBox ID="Textbox1" runat="server" Width="120px" />
                        </li>
                        <li style="box-sizing: border-box; width: 350px; height: 40px;">
                            <asp:Label ID="test2" runat="server" Text="発行年月日:" Width="90px" Style="text-align: right;" />
                            <asp:Label ID="Label3" runat="server" Text="*" Width="20px" Style="text-align: left;" />
                            <asp:TextBox ID="Textbox4" runat="server" Style="vertical-align: middle;" Width="120px" />
                        </li>
                        <li style="box-sizing: border-box; width: 350px; height: 40px;">
                            <asp:Label ID="Label1" runat="server" Text="保険者:" Width="90px" Style="text-align: right;" />
                            <asp:Label ID="Label4" runat="server" Text="*" Width="20px" Style="text-align: left;" />
                            <asp:TextBox ID="Textbox2" runat="server" Width="90px" />
                            <asp:Button ID="Button4" runat="server" Width="100px" Height="30px" Text="選択" />
                        </li>
                        <li style="box-sizing: border-box; width: 350px; height: 40px; overflow: hidden;">
                            <div style="height: 40px; width: 350px; float: right">
                                <asp:Button ID="Button1" runat="server" Width="100px" Height="30px"
                                    Style="margin-right: 5px" Text="印刷" />
                                <asp:Button ID="Button2" runat="server" Width="100px" Height="30px"
                                    Style="margin-right: 5px" Text="プレビュー" />
                                <asp:Button ID="Button3" runat="server" Width="100px" Height="30px"
                                    Style="margin-right: 5px" Text="帳票設定" />
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div style="width: 1110px; margin-left: 9px;">
            footer
        </div>
        <%--        <div style="width: 1110px; height: 240px; margin-left: 9px;">
            <div id="Block1" class="tyohyoDiv hokenDiv1"></div>
            <div id="Block2" class="tyohyoDiv hokenDiv2">
                <div style="font-weight: bold">印刷設定</div>
                <div style="border: 1px solid; float: none; width: ">
                    <div>
                        <div style="height: 40px; width: 340px; float: left;">
                            <asp:Label ID="test4" runat="server" Text="帳票:" Width="90px" Style="text-align: right;" />
                            <asp:Label ID="Label2" runat="server" Text="*" Width="20px" Style="text-align: left;" />
                            <asp:TextBox ID="Textbox1" runat="server" Width="120px" />
                        </div>
                    </div>
                    <div>
                        <div style="width: 340px; height: 40px; float: left;">
                            <asp:Label ID="test2" runat="server" Text="発行年月日:" Width="90px" Style="text-align: right;" />
                            <asp:Label ID="Label3" runat="server" Text="*" Width="20px" Style="text-align: left;" />
                            <asp:TextBox ID="Textbox4" runat="server" Style="vertical-align: middle;" Width="120px" />

                        </div>
                    </div>
                    <div>
                        <div style="height: 40px; width: 390px; float: left">
                            <asp:Label ID="Label1" runat="server" Text="保険者:" Width="90px" Style="text-align: right;" />
                            <asp:Label ID="Label4" runat="server" Text="*" Width="20px" Style="text-align: left;" />
                            <asp:TextBox ID="Textbox2" runat="server" Width="90px" />

                        </div>
                    </div>
                    <div>
                        <div style="height: 40px; width: 390px; float: right">
                            <asp:Button ID="Button1" runat="server" Width="100px" Height="30px"
                                Style="margin-right: 5px" Text="印刷" />
                            <asp:Button ID="Button2" runat="server" Width="100px" Height="30px"
                                Style="margin-right: 5px" Text="プレビュー" />
                            <asp:Button ID="Button3" runat="server" Width="100px" Height="30px"
                                Style="margin-right: 5px" Text="帳票設定" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Button ID="Button4" runat="server" Width="100px" Height="30px" Text="選択" />--%>
        <script type="text/javascript">
            window.onload = function () {
                var hknsNo = this.sessionStorage.getItem('ss_hknsNo') 
                
                if (hknsNo == '2'){
                        var HokenDiv = this.document.getElementById('HokenDiv');
                        var ChohyoDiv = this.document.getElementById('ChohyoDiv');
                        HokenDiv.style.width = '1110px';
                        ChohyoDiv.style.displau = 'none';
                        return true;
                } else {
                    var HokenDiv = this.document.getElementById('HokenDiv');
                    HokenDiv.style.width = '390px';
                    return true;
                }
            }
        </script>
    </form>
</body>
</html>
