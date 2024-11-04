using qa_service.Entities;

namespace qa_service.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        public Question createQuestion (QuestionRequest questionRequest);
        public Question? getQuestionById (int id);
        public List<Question> getAllQuestions();
        public Question? updateQuestion(int id, QuestionRequest questionRequest);
        public bool deleteQuestion (int id);
    }
}
