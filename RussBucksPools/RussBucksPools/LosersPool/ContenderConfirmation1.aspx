﻿<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ContenderConfirmation1.aspx.vb" Inherits="RussBucksPools.ContenderConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:CStr(Session("dayNumber"))%></h2>
    <br />
    <br />
    <h2>Confirmation Number</h2>
    <h1><%:CStr(Session("confirmationNumber"))%></h1>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Height="35px" Text="Return" Width="120px" />
    
</asp:Content>
