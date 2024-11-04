using qa_service.Entities;
using qa_service.Repositories.Interfaces;
using qa_service.UseCases.Interfaces;

namespace qa_service.UseCases.Implementations
{
    public class CreateQuestionCU : ICreateQuestionCU
    {
        private IQuestionRepository questionRepository;

        public CreateQuestionCU(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public Question createQuestion(QuestionRequest questionRequest)
        {
            var question = questionRepository.createQuestion(questionRequest);
            if (question != null)
            {
                return question;
            }
            throw new ApplicationException("Error creating question");
        }
    }
}
