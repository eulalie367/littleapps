<%@ Page Language="C#" MasterPageFile="~/MasterPages/HomePage.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="PotBroker.Listings.Search" Title="Medical Marijuana Listings" %>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <asp:ListView ID="rptListing" runat="server" ItemPlaceholderID="phListing" DataKeyNames="Name,OverAllRating,VendorRating,UnitType,UnitPrice">
        <LayoutTemplate>
            <div class="search clearfix">
                <ul class="listings">
                    <asp:PlaceHolder ID="phListing" runat="server" />
                </ul>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <li class="listing clearfix"><a class="view" title="Blue Dream 90% Strain Rating" href="Search.aspx">
                <ul>
                    <li class="clearfix">
                        <h2 id="name" runat="server">
                            Unknown
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
            </a></li>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
