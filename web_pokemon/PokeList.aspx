<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PokeList.aspx.cs" Inherits="web_pokemon.PokeList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>PokeList</h1>
    <asp:GridView ID="dgvPoke" DataKeyNames="Id" CssClass="table" AutoGenerateColumns="false" AllowPaging="True" PageSize="4" runat="server" OnPageIndexChanging="dgvPoke_PageIndexChanging" OnSelectedIndexChanged="dgvPoke_SelectedIndexChanged">
       <Columns>
           <asp:BoundField HeaderText="Nombre" Datafield="Nombre"/>
           <asp:BoundField HeaderText="Numero" DataField="Numero"/>
           <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion"/>
           <asp:CommandField HeaderText="Accion" ShowSelectButton="true" SelectText="Seleccionar"/>
       </Columns>
    </asp:GridView>
    <a href="PokeForm.aspx" class="btn btn-success">Agregar Pokemon</a>

</asp:Content>
