using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using PM2P2T2.Models;
using System.IO;

namespace PM2P2T2.Controllers
{
    public class FirmaController
    {
        readonly SQLiteAsyncConnection db;

        public FirmaController(string pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<firma>().Wait();
        }

        public Task<List<firma>> GetFirmasList()
        {
            return db.Table<firma>().ToListAsync();
        }

        public Task<firma> GetFirmaID(int pcodigo)
        {
            return db.Table<firma>()
                .Where(i => i.Id == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> GuadarFirma(firma firma)
        {
            if (firma.Id != 0)
            {
                return db.UpdateAsync(firma);
            }
            else
            {
                return db.InsertAsync(firma);
            }
        }

        public Task<int> EliminarFirma(firma firma)
        {
            return db.DeleteAsync(firma);
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
