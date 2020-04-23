using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRestEAV.Data_Access;
using WebApiRestEAV.Logic;
using WebApiRestEAV.Models;

namespace WebApiRestEAV.Controllers
{
    public class GPS_DATAController : ApiController
    {
        //GET    api/GPS_DATA/get_GPS_DATA                  Obtener todos los registros
        /// <summary>
        /// Obtiene todos los registros de la tabla GPS_DATA.
        /// </summary>
        /// <returns>Lista de objetos GPS_DATA</returns>
        [HttpGet]
        public List<GPS_DATA> get_GPS_DATA()
        {
            List<GPS_DATA> listGPS_DATA = null;

            try
            {
                return listGPS_DATA = GPS_DATA_LN.get_GPS_DATA();
            }
            catch
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
        }

        //GET    api/GPS_DATA/get_GPS_DATA_BY_ID/ID         Obtener registro especifico
        /// <summary>
        /// Obtiene un registro especifico por el ID de la tabla GPS_DATA.
        /// </summary>
        /// <param name="id">Idd del registro a buscar</param>
        /// <returns>Objeto GPS_DATA</returns>
        [HttpGet]
        public GPS_DATA get_GPS_DATA_BY_ID(string id)
        {
            List<GPS_DATA> listGPS_DATA = null;
            GPS_DATA oGPS_DATA = new GPS_DATA();

            try
            {
                listGPS_DATA = GPS_DATA_LN.get_GPS_DATA(id);
                if (listGPS_DATA != null && listGPS_DATA.Count > 0)
                {
                    oGPS_DATA = listGPS_DATA.FirstOrDefault();
                }
                return oGPS_DATA;
            }
            catch
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
        }

        //PUT    api/GPS_DATA/put_GPS_DATA/                            Editar un registro
        /// <summary>
        /// Edita un registro especifico en la tabla GPS_DATA.
        /// </summary>
        /// <param name="oGPS_DATA">Objeto GPS_DATA a editar</param>
        /// <returns>True, proceso correto, caso contrario False</returns>
        [HttpPut]
        public bool put_GPS_DATA(GPS_DATA oGPS_DATA)
        {
            try
            {
                if (!string.IsNullOrEmpty(oGPS_DATA.Id.ToString()) || oGPS_DATA.DateSystem == null)
                {
                    return GPS_DATA_LN.crud_GPS_DATA_LN(oGPS_DATA, "UP");
                }
                return false;
            }
            catch
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
        }

        //POST api/GPS_DATA/post_GPS_DATA/                              Insertar nuevo registro
        /// <summary>
        /// Inserta un nuevo registro en la tabla GPS_DATA.
        /// </summary>
        /// <param name="oGPS_DATA">Objeto GPS_DATA a insertar.</param>
        /// <returns>True, proceso correto, caso contrario False</returns>
        [HttpPost]
        public bool post_GPS_DATA(GPS_DATA oGPS_DATA)
        {
            try
            {
                if (oGPS_DATA.Id == 0 ||  oGPS_DATA.DateSystem != null)
                {

                    return GPS_DATA_LN.crud_GPS_DATA_LN(oGPS_DATA, "IN");
                }
                return false;
            }
            catch
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
        }

        //DELETE api/GPS_DATA/delete_GPS_DATA/                       Eliminar un registro especifico
        /// <summary>
        /// Elimina un registro especifico en la tabla GPS_DATA.
        /// </summary>
        /// <param name="id">Id del registro a eliminar.</param>
        /// <returns>True, proceso correto, caso contrario False</returns>
        [HttpDelete]
        public bool delete_GPS_DATA(string id)
        {
            try
            {
                if (id != "0" )
                {
                    GPS_DATA oGPS_DATA = new GPS_DATA();
                    oGPS_DATA.Id = Convert.ToInt32(id);
                    oGPS_DATA.DateSystem = new DateTime(1990,01,01);    //Solo para evitar error al ejecutar SP
                    oGPS_DATA.DateEvent  = new DateTime(1990, 01, 01);
                    return GPS_DATA_LN.crud_GPS_DATA_LN(oGPS_DATA, "DE");
                }
                return false;
            }
            catch
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
        }
    }

}
