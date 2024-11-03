using qa_service.Entities;
namespace qa_service.UseCases.Interfaces
{
    public interface ICreateQuestionCU
    {
        public Question createQuestion(QuestionRequest questionRequest);
    }
}
