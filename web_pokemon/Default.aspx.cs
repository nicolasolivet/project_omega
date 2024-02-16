using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using datos;

namespace web_pokemon
{
    public partial class Default : System.Web.UI.Page
    {
        //public List<Pokemon> PokeList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonDatos data = new PokemonDatos(); 
            List<Pokemon> list = data.SPToList();

            repeater.DataSource = list;
            repeater.DataBind();
        }
    }
}