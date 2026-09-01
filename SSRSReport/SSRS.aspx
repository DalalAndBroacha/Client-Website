
<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master"
    CodeBehind="~/SSRS.aspx.cs" Inherits="SSRSReport.SSRS" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%--<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Main.Master" CodeBehind="SSRS.aspx.cs" Inherits="SSRSReport.SSRS" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <rsweb:ReportViewer  ID="rptViewer"  Width="100%" Height="600px" runat="server" 
            CssClass="rpt-viewer">
        </rsweb:ReportViewer>
        <br />
    </div>
    </form>
</body>
</html>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server" AutoPostBack="true">
<script type="text/javascript">
    $(document).ready(function () {
        $('#dvTitle').addClass('hide');
    });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" AutoPostBack="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <rsweb:ReportViewer   ID="rptViewer"  Width="100%" Height="600px" runat="server" 
            CssClass="rpt-viewer">
        </rsweb:ReportViewer>
    </div>
    <label ID="Label1"  runat="server" ></label>
</asp:Content>
