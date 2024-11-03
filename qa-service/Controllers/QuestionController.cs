using Microsoft.AspNetCore.Mvc;
using qa_service.UseCases.Interfaces;
using qa_service.Entities;


namespace qa_service.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/questions")]
    public class QuestionController : ControllerBase
    {
        private readonly ICreateQuestionCU createQuestionCU;
        private readonly IDeleteQuestionCU deleteQuestionCU;
        private readonly IGetQuestionCU getQuestionCU;
        private readonly IUpdateQuestionCU updateQuestionCU;

        public QuestionController(ICreateQuestionCU createQuestionCU, IDeleteQuestionCU deleteQuestionCU, IGetQuestionCU getQuestionCU, IUpdateQuestionCU updateQuestionCU)
        {
            this.createQuestionCU = createQuestionCU;
            this.deleteQuestionCU = deleteQuestionCU;
            this.getQuestionCU = getQuestionCU;
            this.updateQuestionCU = updateQuestionCU;
        }

        [HttpGet]
        public IActionResult GetQuestions()
        {
            try
            {
                var questions = getQuestionCU.getQuestions();
                return Ok(questions);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
        {
            try
            {
                var question = getQuestionCU.getQuestionById(id);
                return Ok(question);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateQuestion([FromBody] QuestionRequest questionRequest)
        {
            try
            {
                var question = createQuestionCU.createQuestion(questionRequest);
                return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody] QuestionRequest questionRequest)
        {
            try
            {
                var question = updateQuestionCU.updateQuestion(id, questionRequest);
                return Ok(question);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            try
            {
                var question = deleteQuestionCU.deleteQuestion(id);
                return Ok(question);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
