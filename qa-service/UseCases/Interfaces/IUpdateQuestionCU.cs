using qa_service.Entities;
namespace qa_service.UseCases.Interfaces
{
    public interface IUpdateQuestionCU
    { 
       public Question? updateQuestion(int id, QuestionRequest questionRequest);
    }
}
