using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using datos;
using dominio;

namespace web_pokemon
{
    public partial class PokeForm : System.Web.UI.Page
    {
        public List<Elemento> PokeList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            try
            {
                //configureta inicial, precarga de desplegables
                if (!IsPostBack)
                {
                    ElementoDatos elemData = new ElementoDatos();
                    //List<Elemento> list = elemData.ToList();
                    PokeList = elemData.ToList();
                    
                    ddlType.DataSource = PokeList;
                    //funca como key-value el id es lo que toma de referencia y desc es lo que muestra
                    ddlType.DataValueField = "Id";
                    ddlType.DataTextField = "Descripcion";
                    ddlType.DataBind();

                    ddlWeak.DataSource = PokeList;
                    ddlWeak.DataValueField = "Id";
                    ddlWeak.DataTextField = "Descripcion";
                    ddlWeak.DataBind();
                }

                //configuracion si modificamos un pokemon
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    PokemonDatos data = new PokemonDatos();
                    //List<Pokemon> list = data.ToList(id);
                    //Pokemon selected = list[0]; 
                    Pokemon selected = (data.ToList(id))[0];

                    txtId.Text = id;
                    txtName.Text = selected.Nombre;
                    txtNum.Text = selected.Numero.ToString();
                    txtDesc.Text = selected.Descripcion;
                    txtImage.Text = selected.UrlImagen;
       
                    ddlType.SelectedValue = selected.Tipo.Id.ToString();
                    ddlWeak.SelectedValue = selected.Debilidad.Id.ToString();

                    //forzamos la carga de la imagen, al traer un poke para modificar no cargaba la img
                    txtImage_TextChanged(sender, e);

                }
            }
            catch (Exception ex)
            {
                //en vez de lanzar el error lo agrego a la session 
                Session.Add("error", ex);
                throw;
            }
        }
        
        protected void LoadImg()
        {
            img.ImageUrl = txtImage.Text;    
        }

        protected void txtImage_TextChanged(object sender, EventArgs e)
        {
            LoadImg();   
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonDatos data = new PokemonDatos();
                Pokemon n = new Pokemon();
                n.Nombre = txtName.Text;
                n.Numero = int.Parse(txtNum.Text);
                n.Descripcion = txtDesc.Text;
                n.UrlImagen = txtImage.Text;

                n.Tipo = new Elemento();
                n.Tipo.Id = int.Parse(ddlType.SelectedValue);
                n.Debilidad = new Elemento();
                n.Debilidad.Id = int.Parse(ddlWeak.SelectedValue);

                if (txtId.Text != "")
                {
                    n.Id = int.Parse(txtId.Text);
                    data.ModifySP(n);
                }
                else
                {
                    data.AddSP(n);
                }
                Response.Redirect("PokeList.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
                //redireccion a otra pantalla 
            } 
                

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonDatos data = new PokemonDatos();
                data.Delete(int.Parse(txtId.Text));
                Response.Redirect("PokeList.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}