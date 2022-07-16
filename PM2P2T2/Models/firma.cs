using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2P2T2.Models
{
    public class firma
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public Byte[] Firma { get; set; }
    }
}