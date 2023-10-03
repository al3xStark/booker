using booker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Services
{
    public class SegmentRepository
    {
        static SegmentRepository instance;
        static SQLiteConnection database;
        private SegmentRepository(SQLiteConnection dataBase)
        {
            database = dataBase;
            database.CreateTable<Segment>();
        }
        public static SegmentRepository GetInstance(SQLiteConnection database)
        {
                if (instance == null)
                    instance = new SegmentRepository(database);
                return instance;
        }
        public static int DeleteSegment(int id)
        {
            return database.Delete<Segment>(id);
        }

        public static IEnumerable<Segment> GetSegments()
        {
            return database.Table<Segment>().ToList();
        }
        public static IEnumerable<Segment> GetAccountSegments(int accountID)
        {
            return database.Table<Segment>().Where(x => x.AccountID == accountID).ToList();
        }

        public static Segment GetSegment(int id)
        {
            return database.Get<Segment>(id);
        }

        public static int SaveSegment(Segment item)
        {
            if (item.ID != 0)
            {
                database.Update(item);
                return item.ID;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
