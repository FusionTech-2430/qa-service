using qa_service.Repositories.Interfaces;
using qa_service.UseCases.Interfaces;
using qa_service.Entities;

namespace qa_service.UseCases.Implementations
{
    public class UpdateQuestionCU : IUpdateQuestionCU
    {
        private IQuestionRepository questionRepository;

        public UpdateQuestionCU(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public Question? updateQuestion(int id, QuestionRequest questionRequest)
        {
            var question = questionRepository.updateQuestion(id, questionRequest);
            if (question != null) return question;
            throw new ApplicationException("Error updating question");
        }
    }
}
