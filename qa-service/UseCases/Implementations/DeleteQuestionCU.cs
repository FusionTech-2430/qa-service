using qa_service.UseCases.Interfaces;
using qa_service.Entities;
using qa_service.Repositories.Interfaces;

namespace qa_service.UseCases.Implementations
{
    public class DeleteQuestionCU : IDeleteQuestionCU
    {
        private IQuestionRepository questionRepository;

        public DeleteQuestionCU(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public bool deleteQuestion(int id)
        {
            if (questionRepository.deleteQuestion(id))
            {
                return true;
            }
            throw new ApplicationException("The question with the given id, does not exists");
        }
    }
}
