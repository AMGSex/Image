using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ImageSaveApp.DataBase;

namespace ImageSaveApp.DataBase
{
    class ImageStorage
    {
        SQLiteConnection database;
        public ImageStorage(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Image>();
        }

        public IEnumerable<Image> GetItems()
        {
            return database.Table<Image>().ToList();
        }

        public Image GetItem(int id)
        {
            return database.Get<Image>(id);
        }

        public int DeleteItem(int id)
        {
            return database.Delete<Image>(id);
        }

        public int SaveItem(Image item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
