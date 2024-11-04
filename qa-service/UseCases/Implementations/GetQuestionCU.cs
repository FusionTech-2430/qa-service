using qa_service.Entities;
using qa_service.UseCases.Interfaces;
using qa_service.Repositories.Interfaces;

namespace qa_service.UseCases.Implementations
{
    public class GetQuestionCU : IGetQuestionCU
    {
        private IQuestionRepository questionRepository;
        public GetQuestionCU(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
        public Question? getQuestionById(int id)
        {
            if (questionRepository.getQuestionById(id) != null)
            {
                return questionRepository.getQuestionById(id);
            }
            throw new ApplicationException("Question not found");
        }

        public List<Question> getQuestions()
        {
            if (questionRepository.getAllQuestions().Count > 0)
            {
                return questionRepository.getAllQuestions();
            }
            throw new ApplicationException("No questions found");
        }
    }
}
