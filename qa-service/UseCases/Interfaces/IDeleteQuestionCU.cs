using qa_service.Entities;

namespace qa_service.UseCases.Interfaces
{
    public interface IDeleteQuestionCU
    {
        public bool deleteQuestion(int id);
    }
}
