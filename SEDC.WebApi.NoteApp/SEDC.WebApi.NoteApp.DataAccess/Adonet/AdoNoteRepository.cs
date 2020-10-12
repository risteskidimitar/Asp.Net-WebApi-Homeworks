using SEDC.WebApi.NoteApp.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SEDC.WebApi.NoteApp.DataAccess.Adonet
{
    public class AdoNoteRepository : IRepository<Note>
    {
        private readonly string _connectionString;
        public AdoNoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Note> GetAll()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM dbo.[Notes]";
            SqlDataReader dataReader = command.ExecuteReader();

            List<Note> notes = new List<Note>();

            while (dataReader.Read())
            {
                var note = new Note
                {
                    Id = dataReader.GetFieldValue<int>(0),
                    Text = dataReader.GetFieldValue<string>(1),
                    Color = dataReader.GetFieldValue<string>(2),
                    Tag = dataReader.GetFieldValue<int>(3),
                    UserId = dataReader.GetFieldValue<int>(4),
                    User = dataReader.GetFieldValue<User>(5)
                };

                notes.Add(note);
            }

            connection.Close();

            return notes;
        }

        public void Insert(Note entity)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;

            cmd.CommandText =
                         $"INSERT INTO dbo.[Notes] (Text, Color, Tag, UserId)" +
                         $" VALUES(@noteText, @noteColor, @noteTag, @noteUserId)";
            cmd.Parameters.AddWithValue("@noteText", entity.Text);
            cmd.Parameters.AddWithValue("@noteColor", entity.Color);
            cmd.Parameters.AddWithValue("@noteTag", entity.Tag);
            cmd.Parameters.AddWithValue("@noteUserId", entity.UserId);

            cmd.ExecuteNonQuery();
            connection.Close();

        }

        public void Remove(Note entity)
        {
            SqlConnection connection =
               new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;

            cmd.CommandText = "DELETE FROM dbo.[Notes] WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", entity.Id);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Note entity)
        {
            SqlConnection connection =
                new SqlConnection(_connectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "UPDATE dbo.[Notes] " +
                "SET Text = @noteText, Color = @noteColor, Tag = @noteTag, UserId = @noteUserId" +
                "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@noteText", entity.Text);
            cmd.Parameters.AddWithValue("@noteColor", entity.Color);
            cmd.Parameters.AddWithValue("@noteTag", entity.Tag);
            cmd.Parameters.AddWithValue("@noteUserId", entity.UserId);
            cmd.Parameters.AddWithValue("@id", entity.Id);

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
