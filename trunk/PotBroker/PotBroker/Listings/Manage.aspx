<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="PotBroker.Listings.Manage" MasterPageFile="~/MasterPages/HomePage.master" %>
<%@ Register Namespace="EditableControls" TagPrefix="EditableControls" Assembly="envato" %>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div class="search clearfix">
        <ul class="listings">
            <li class="listing clearfix">
                <ul>
                    <li class="clearfix">
                        <h2 id="name" runat="server">
                            <EditableControls:DropDownList ID="ddName" runat="server" EditMode="true">
                                <asp:ListItem Text="Blue Dream" Value="1" Selected="True" />
                            </EditableControls:DropDownList>
                        </h2>
                    </li>
                    <li class="clearfix">
                        <div class="image">
                            <img class="clearfix" src="/Images/blue%20dream%20001.jpg" alt="Blue Dream 90% Strain Rating" />
                        </div>
                        <ul class="ratings">
                            <li class="overall">
                                <h3 id="overallRating" runat="server">
                                    90% Overall
                                </h3>
                                <ul>
                                    <li>2002 High Times Winners</li>
                                </ul>
                            </li>
                            <li class="vendor">
                                <h3 id="vendorRating" runat="server">
                                    90% Vendor
                                </h3>
                            </li>
                        </ul>
                    </li>
                    <li class="clearfix"><a class="view" href="Search.aspx" title="View Details For Blue Dream">
                        <ul class="details clearfix">
                            <li>
                                <h2 id="unitType" runat="server">
                                    1 gram
                                </h2>
                            </li>
                            <li>
                                <h2 id="unitPrice" runat="server">
                                    $20.00
                                </h2>
                            </li>
                        </ul>
                    </a></li>
                </ul>
            </li>
        </ul>
    </div>
</asp:Content>
