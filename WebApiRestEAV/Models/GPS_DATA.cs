using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiRestEAV.Models
{
    public class GPS_DATA
    {
        public int Id { get; set; }
        public DateTime DateSystem { get; set; }
        public DateTime DateEvent { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Battery { get; set; }
        public int Source { get; set; }
        public int Type { get; set; }

        public GPS_DATA() { }
        public GPS_DATA(DataRow fila)
        {
            Id         = Convert.ToInt32(fila["Id"]);
            DateSystem = Convert.ToDateTime(fila["DateSystem"]);
            DateEvent  = Convert.ToDateTime(fila["DateEvent"]);
            Latitude   = Convert.ToInt64(fila["Latitude"]);
            Longitude  = Convert.ToInt64(fila["Longitude"]);
            Battery    = Convert.ToInt32(fila["Battery"]);
            Source     = Convert.ToInt32(fila["Source"]);
            Type       = Convert.ToInt32(fila["Type"]);
        }
    }
}