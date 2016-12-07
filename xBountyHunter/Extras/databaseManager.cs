using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xBountyHunter.Extras
{
    public class databaseManager
    {
        private static int DATABASE_VERSION = 1; // cada vez que se quiera hacer una nueva tabla settear este valor a 0 o cambiarlo
        private SQLiteConnection db;
        private int oldVersion = 0;

        public databaseManager()
        {
            db = DependencyService.Get<DependencyServices.ISQLite>().GetConnection();
            if (xBountyApp.Current.Properties.ContainsKey("DATABASE_VERSION"))
                oldVersion = (int)xBountyApp.Current.Properties["DATABASE_VERSION"];
            if (DATABASE_VERSION != oldVersion && oldVersion != 0)
                onUpgrade();
            if (oldVersion == 0)
                createTable();
        }


        public void createTable()
        {
            db.CreateTable<Models.mFugitivos>();
            xBountyHunter.xBountyApp.Current.Properties["DATABASE_VERSION"] = DATABASE_VERSION;
        }

        public void onUpgrade()
        {
            db.DropTable<Models.mFugitivos>();
            createTable();
        }

        public List<Models.mFugitivos> selectNoCaptured()
        {
            List<Models.mFugitivos> result = db.Query<Models.mFugitivos>("SELECT * FROM [mfugitivos] WHERE [Capturado] = 0 or [Capturado] is null");
            return result;
        }

        public List<Models.mFugitivos> selectCaptured()
        {
            List<Models.mFugitivos> result = db.Query<Models.mFugitivos>("SELECT * FROM [mfugitivos] WHERE [Capturado] = 1");
            return result;
        }

        public List<Models.mFugitivos> selectAll()
        {
            List<Models.mFugitivos> result = db.Query<Models.mFugitivos>("SELECT * FROM [mfugitivos]");
            return result;
        }

        public int insertItem(Models.mFugitivos item)
        {
            int result = db.Insert(item);
            return result;
        }

        public int updateItem(Models.mFugitivos item)
        {
            int result = db.Update(item);
            return result;
        }

        public int deleteItem(int id)
        {
            int result = db.Delete<Models.mFugitivos>((id));
            return result;
        }

        public void closeConnection()
        {
            db.Close();
        }
    }
}
