<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PokeForm.aspx.cs" Inherits="web_pokemon.PokeForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox ID="txtId" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtName" class="form-label">Nombre</label>
                <asp:TextBox ID="txtName" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNum" class="form-label">Número</label>
                <asp:TextBox ID="txtNum" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtDesc" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDesc" CssClass="form-control" TextMode="MultiLine" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlTipo" class="form-label">Tipo</label>
                <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlWeak" class="form-label">Debilidad</label>
                <asp:DropDownList ID="ddlWeak" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAccept" runat="server" CssClass="btn btn-success" OnClick="btnAccept_Click" />
                <a href="PokeList.aspx" class="btn btn-primary">Ir al listado de pokemons</a>
            </div>
            <div class="mb-3">
                <asp:Button Text="Eliminar" ID="btnDelete" runat="server" CssClass="btn btn-warning" OnClick="btnDelete_Click" />
            </div>
        </div>

        <div class="col-6">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtImage" class="form-label">Imagen</label>
                        <asp:TextBox runat="server" ID="txtImage" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtImage_TextChanged" />
                    </div>
                    <asp:Image ImageUrl="https://storage.googleapis.com/proudcity/mebanenc/uploads/2021/03/placeholder-image.png" ID="img" Width="80%" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
