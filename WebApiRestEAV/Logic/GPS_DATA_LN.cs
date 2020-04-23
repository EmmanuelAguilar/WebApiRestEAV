using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using WebApiRestEAV.Data_Access;
using WebApiRestEAV.Models;

namespace WebApiRestEAV.Logic
{
    //Creado por: Ing.Emmanuel Aguilar.    
    //Des.Breve: Code Behind encargado de la lógica de negocio
    public class GPS_DATA_LN
    {
        #region Global Variables
        #endregion
        #region Class Fields
        #endregion
        #region Constructors
        #endregion
        #region Constants
        #endregion

        /// <summary>
        /// Método que Obtiene todos o un registro especifico, según el valor de Id.
        /// Id = NULL , Lista todos los registros.
        /// Id != NULL, Busca el registro especifico según el valor de ID
        /// </summary>
        /// <param name="Id">Id de registro a buscar</param>
        /// <returns>Lista de objetos GPS_DATA</returns>
        public static List<GPS_DATA> get_GPS_DATA(string Id = "NULL")
        {
            List<GPS_DATA> listGPS_DATA = null;
            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataAdapter render = null;
            DataSet dS = new DataSet();

            try
            {
                listGPS_DATA = new List<GPS_DATA>();
                conexion = Conexion.getInstanceConexion().ConexionBD();
                conexion.Open();
                cmd = new SqlCommand("SELECT * FROM FN_GET_GPS_DATA(" + Id + ")", conexion);
                render = new SqlDataAdapter();
                render.SelectCommand = cmd;
                render.Fill(dS);

                foreach (DataRow fila in dS.Tables[0].Rows)
                {
                    listGPS_DATA.Add(new GPS_DATA(fila));
                }
            }
            catch 
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            finally
            {
                conexion.Close();
            }

            return listGPS_DATA;
        }

        /// <summary>
        /// Método que Iserta, edita o elimina un registro especifico de la tabla GPS_DATA.
        /// action = IN, Inserta un nuevo registro.
        /// action = UP, Actualiza un registro.
        /// action = DE, Elimina un registro.
        /// </summary>
        /// <param name="oGPS_DATA">Objeto GPS_DATA</param>
        /// <param name="action">Acción a realizar</param>
        /// <returns>True, proceso correcto, False proceso incorrecto</returns>
        public static bool crud_GPS_DATA_LN(GPS_DATA oGPS_DATA, string action)
        {
            SqlConnection conexion = null;
            SqlCommand cmd = new SqlCommand(); 
            SqlDataAdapter adapter ;
            SqlParameter param, paramR;
            DataSet dS = new DataSet();

            try
            {
                conexion = Conexion.getInstanceConexion().ConexionBD();
                conexion.Open();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GPS_DATA";

                //Params IN
                param = new SqlParameter("@Action", SqlDbType.Char);
                param.Direction = ParameterDirection.Input;
                param.Value = action;
                cmd.Parameters.Add(param);
                param = null;

                param = new SqlParameter("@Id", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = oGPS_DATA.Id;
                cmd.Parameters.Add(param);
                param = null;


                param = new SqlParameter("@DateSystem", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = oGPS_DATA.DateSystem;
                cmd.Parameters.Add(param);
                param = null;
               
                param = new SqlParameter("@DateEvent", SqlDbType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = oGPS_DATA.DateEvent;
                cmd.Parameters.Add(param);
                param = null;
                
                param = new SqlParameter("@Latitude", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = oGPS_DATA.Latitude;
                cmd.Parameters.Add(param);
                param = null;

                param = new SqlParameter("@Longitude", SqlDbType.Float);
                param.Direction = ParameterDirection.Input;
                param.Value = oGPS_DATA.Longitude;
                cmd.Parameters.Add(param);
                param = null;
                

                param = new SqlParameter("@Battery", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = oGPS_DATA.Battery;
                cmd.Parameters.Add(param);
                param = null;
               
                param = new SqlParameter("@Source", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = oGPS_DATA.Source;
                cmd.Parameters.Add(param);
                param = null;
                
                param = new SqlParameter("@Type", SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = oGPS_DATA.Type;
                cmd.Parameters.Add(param);
                param = null;

                paramR = new SqlParameter("@Result", SqlDbType.VarChar);
                paramR.Direction = System.Data.ParameterDirection.Output;
                paramR.Value = "";
                paramR.Size = 100;
                cmd.Parameters.Add(paramR);

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dS);

                string varf = cmd.Parameters["@Result"].Value.ToString();

                return varf == "1" ? true : false;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}