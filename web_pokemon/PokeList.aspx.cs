using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using datos;

namespace web_pokemon
{
    public partial class PokeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {   
                PokemonDatos data = new PokemonDatos();
                if (!IsPostBack)
                {
                    //Session.Add("PokeList", data.SPToList());
                    //dgvPoke.DataSource = Session["PokeList"];
                    dgvPoke.DataSource = data.ToList();
                    dgvPoke.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //redireccionar y mostrar el error en pantalla
            }
            
        }

        protected void dgvPoke_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PokemonDatos data =  new PokemonDatos();
            dgvPoke.DataSource = data.ToList();
            dgvPoke.PageIndex = e.NewPageIndex;
            dgvPoke.DataBind();
        }

        protected void dgvPoke_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvPoke.SelectedDataKey.Value.ToString();
            Response.Redirect($"PokeForm.aspx?id={id}");
            
        }
    }
}