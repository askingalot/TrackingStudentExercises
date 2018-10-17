using Microsoft.Data.Sqlite;

namespace TrackinggStudentExercises {
    public static class DatabaseInterface {
        public static SqliteConnection Connection {
            get {
                return new SqliteConnection("Data Source=./StudentExercises.db");
            }
        }
    }
}