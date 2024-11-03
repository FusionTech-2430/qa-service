using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using qa_service.Entities;
using qa_service.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace questions_service.Repositories.Implementations
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly string _connectionString;

        public QuestionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Question createQuestion(QuestionRequest question)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    INSERT INTO question_db.question (pregunta, respuesta) 
                    OUTPUT INSERTED.id 
                    VALUES (@Pregunta, @Respuesta)";

                int generatedId = connection.ExecuteScalar<int>(sql, new { Pregunta = question.pregunta, Respuesta = question.respuesta });

                Question questionResponse = new Question
                {
                    Id = generatedId,
                    pregunta = question.pregunta,
                    respuesta = question.respuesta
                };
                return questionResponse;
            }
        }

        public bool deleteQuestion(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM question_db.question WHERE id = @Id";
                int rowsAffected = connection.Execute(sql, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public List<Question> getAllQuestions()
        {
            var questions = new List<Question>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM question_db.question";
                questions = connection.Query<Question>(sql).AsList();
            }

            return questions;
        }

        public Question? getQuestionById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM question_db.question WHERE id = @Id";
                return connection.QueryFirstOrDefault<Question>(sql, new { Id = id });
            }
        }

        public Question? updateQuestion(int id, QuestionRequest questionRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    UPDATE question_db.question 
                    SET pregunta = @Pregunta, respuesta = @Respuesta 
                    WHERE id = @Id";

                int rowsAffected = connection.Execute(sql, new { Pregunta = questionRequest.pregunta, Respuesta = questionRequest.respuesta, Id = id });

                if (rowsAffected == 0) return null;

                return getQuestionById(id); 
            }
        }
    }
}
