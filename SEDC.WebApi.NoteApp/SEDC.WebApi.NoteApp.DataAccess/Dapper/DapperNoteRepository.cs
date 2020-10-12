using Dapper;
using Dapper.Contrib.Extensions;
using SEDC.WebApi.NoteApp.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SEDC.WebApi.NoteApp.DataAccess.Dapper
{
    public class DapperNoteRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public DapperNoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Note> GetAll()
        {
            using(var connection =
                new SqlConnection(_connectionString))
            {
                connection.Open();

                var notes = connection
                    .Query<Note>("SELECT * FROM dbo.[Notes]")
                    .ToList();

                return notes;
            }
        }

        public void Insert(Note entity)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            connection.Insert(entity);
            connection.Close();
        }

        public void Remove(Note entity)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Delete(entity);
            }
        }

        public void Update(Note entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Update(entity);
            }
        }
    }
}
