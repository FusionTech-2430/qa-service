using qa_service.Entities;

namespace qa_service.UseCases.Interfaces
{
    public interface IGetQuestionCU
    {
        public List<Question> getQuestions();
        public Question? getQuestionById(int id);
    }
}
