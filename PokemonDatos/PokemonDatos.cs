using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Cache;
using System.Data.SqlClient;
using dominio;

namespace datos
{
    public class PokemonDatos
    {
        //agrego el string id como parametro opcional por eso el ide me lo permitej
        public List<Pokemon> ToList(string id = "")
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion  = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, P.IdTipo, P.IdDebilidad, P.Id from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad And P.Activo = 1 ";
                if (id != "")
                {
                    comando.CommandText += " and P.Id = " + id;
                }
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                //agarramos la data del sqldatareader
                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)lector["Id"];
                    aux.Numero = (int)lector["Numero"];             
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    
                    if(!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)lector["UrlImagen"];

                    //hay que instanciar porque sino no existe un objeto de tipo elemento y tipo.descripcion devolveria nulo
                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];

                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];

                    //en cada vuelta que encuentre el lector.read() va a bajar y en cada vuelta, va a hacer  un nuevo objeto, creando una nueva instancia, reutilizando la variable aux, . 
                    // a esa nueva instancia le va guardando los datos (props) numero, nombre, descripcion. y la lista va haciendo referecnia a diferentes objetos

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;

            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Pokemon> SPToList() 
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //string consulta = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, P.IdTipo, P.IdDebilidad, P.Id from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad And P.Activo = 1";

                //datos.setQuery(consulta);
                // en vez de escribir la consulta en vs, hacemos el sp la db y llamamos 
                datos.setProcedure("storedToList"); 
                
                datos.execRead();

                //agarramos la data del sqldatareader
                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = (int)datos.Lector["Numero"];
                    //otra forma, esta es mas facil de leer
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    //casteo explicito: string
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    //dos formas de validar la lectura DBNull
                    
                    //if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                    //    aux.UrlImagen = (string)lector["UrlImagen"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    //hay que instanciar porque sino no existe un objeto de tipo elemento y tipo.descripcion devolveria nulo
                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];

                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    //en cada vuelta que encuentre el lector.read() va a bajar y en cada vuelta, va a crear un nuevo objeto, va reutilizar la variable aux creando un nuevo objeto, creando una nueva instancia. 
                    // a esa nueva instancia le va guardando los datos (props) numero, nombre, descripcion. y la lista va haciendo referecnia a diferentes objetos

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddSP(Pokemon nuevo)
        {
            AccesoDatos data = new AccesoDatos();
            //al insertar registros, no necesitamos una lista

            try
            {
                data.setProcedure("storedNewPoke");
                data.setParameter("nombre", nuevo.Nombre);
                data.setParameter("@numero", nuevo.Numero);
                data.setParameter("@desc", nuevo.Descripcion);
                data.setParameter("@img", nuevo.UrlImagen);
                data.setParameter("@idTipo", nuevo.Tipo.Id);
                data.setParameter("@idDebilidad", nuevo.Debilidad.Id);
                //data.setParameter("@idEvolucion", null);

                data.execAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.closeConnection();
            }
        }

        public void LoadImg()
        {
            
        } 

        public void Modify(Pokemon poke)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.setQuery("Update POKEMONS set Numero = @numero, Nombre = @nombre, Descripcion = @desc, Urlimagen = @img, IdTipo = @idTipo, IdDebilidad = @idDebilidad Where id = @id");

                data.setParameter("@numero", poke.Numero);
                data.setParameter("@nombre", poke.Nombre);
                data.setParameter("@desc", poke.Descripcion);
                data.setParameter("@img", poke.UrlImagen);
                data.setParameter("@idTipo", poke.Tipo.Id);
                data.setParameter("@idDebilidad", poke.Debilidad.Id);
                data.setParameter("@id", poke.Id);

                data.execAction();
            }
            catch (Exception ex)
            {
                 throw ex;
            }
        }
       
        public void ModifySP(Pokemon poke)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.setProcedure("storedModifyPoke");
                data.setParameter("@numero", poke.Numero);
                data.setParameter("@nombre", poke.Nombre);
                data.setParameter("@desc", poke.Descripcion);
                data.setParameter("@img", poke.UrlImagen);
                data.setParameter("@idTipo", poke.Tipo.Id);
                data.setParameter("@idDebilidad", poke.Debilidad.Id);
                data.setParameter("@id", poke.Id);

                data.execAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete (int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setQuery("delete from POKEMONS where id = @id");
                datos.setParameter("@id", id);
                datos.execAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSP (int id)
        {
            try
            {
                AccesoDatos data = new AccesoDatos();
                data.setProcedure("storedDeletePoke");
                data.setParameter("@id", id);
                data.execAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteLogic(int id)
        {
            try
            {
                AccesoDatos data = new AccesoDatos();
                data.setQuery("Update POKEMONS set ACtivo = 0 where id = @id");
                data.setParameter("@id", id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Pokemon> Filter(string campo, string criterio, string filtro)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, P.IdTipo, P.IdDebilidad, P.Id from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad And P.Activo = 1 And ";

                // evaluo los campos campo y criterio y depende de lo que tengas voy a terminar de completar este consulta

                switch (campo)
                {
                    case "Número":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "Numero > " + filtro;
                                break;
                            case "Menor a":
                                consulta += "Numero < " + filtro;
                                break;
                            default:
                                consulta += "Numero = " + filtro;
                                break;
                        }
                        break;

                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "Nombre like '" + filtro + "%' ";
                                break;
                            case "Termina con":
                                consulta += "Nombre like '%" + filtro + "' ";
                                break;
                            default:
                                consulta += "Nombre like '%" + filtro + "%' ";
                                break;
                        }
                        break;

                    default:
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "P.Descripcion like '" + filtro + "%' ";
                                break;
                            case "Termina con":
                                consulta += "P.Descripcion like '%" + filtro + "' ";
                                break;
                            default:
                                consulta += "P.Descripcion like '%" + filtro + "%' ";
                                break;
                        }
                        break;
                }

                datos.setQuery(consulta);
                datos.execRead();
               

                //agarramos la data del sqldatareader
                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = (int)datos.Lector["Numero"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    //casteo explicito: string
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    //dos formas de validar la lectura DBNull
                    //if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                    //    aux.UrlImagen = (string)lector["UrlImagen"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    //hay que instanciar porque sino no existe un objeto de tipo elemento y tipo.descripcion devolveria nulo
                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];

                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    //en cada vuelta que encuentre el lector.read() va a bajar y en cada vuelta, va a crear un nuevo objeto, va reutilizar la variable aux creando un nuevo objeto, creando una nueva instancia. 
                    // a esa nueva instancia le va guardando los datos (props) numero, nombre, descripcion. y la lista va haciendo referecnia a diferentes objetos

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }

}
