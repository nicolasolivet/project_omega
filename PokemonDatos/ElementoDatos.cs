using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace datos
{
    public class ElementoDatos
    {
        public List<Elemento> ToList()
        {
			List<Elemento> lista = new List<Elemento>();	
			//al instanciar AccesoDatos, nace un objeto, tiene un lector que tiene un comando y una conexion, el comando ya tiene instancia igual que la conexion, y tiene la cadena de conexion configurada
			AccesoDatos data = new AccesoDatos();	
			
			try
			{
				data.setQuery("Select Id, Descripcion From ELEMENTOS");
				data.execRead();

				while (data.Lector.Read())
				{
					Elemento aux = new Elemento();
					aux.Id = (int)data.Lector["Id"];
					aux.Descripcion = (string)data.Lector["Descripcion"];
					lista.Add(aux);
				}
				return lista;
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				//cierra el lector, si existe
				data.closeConnection();
			}
        }
    }
}
