using Microsoft.Data.Sqlite;

namespace TrackingStudentExercises {
    public static class DatabaseInterface {
        public static SqliteConnection Connection {
            get {
                return new SqliteConnection("Data Source=./StudentExercises.db");
            }
        }
    }
}