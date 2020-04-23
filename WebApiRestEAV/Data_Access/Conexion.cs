using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiRestEAV.Data_Access
{
    public class Conexion
    {
        #region "PATRÓN SINGLETON"
        public static Conexion conexion = null;//creamos un obj de tipo Conexion

        private Conexion() { }//Constructor por defecto

        public static Conexion getInstanceConexion()
        {
            if (conexion == null)
            {
                conexion = new Conexion();//Instanciamos el obj
            }

            return conexion;
        }

        #endregion

        public SqlConnection ConexionBD()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings["urlConexion"];

            return conexion;
        }
    }
}