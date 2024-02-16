using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace datos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        //lector, atributo privado, para leerlo (get) lo tengo que hacer publico creando la property
        public SqlDataReader Lector 
        { 
            get { return lector; }
        }

        public AccesoDatos()
        {
            // no se pasa por parametro contructor porque sino cada clase de "datos", cada metodo que llame  y cree una instancia necesita que este la cadena pero de esta manera no esta centralizado   
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true");
            comando = new SqlCommand();
        }

        public void setQuery(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        
        public void setProcedure(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp; 
        }

        public void setParameter(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void execRead()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //este metodo agrega, modifica o elimina un pokemon
        public void execAction()
        {
            comando.Connection = conexion; 
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void closeConnection()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }
    }
}
