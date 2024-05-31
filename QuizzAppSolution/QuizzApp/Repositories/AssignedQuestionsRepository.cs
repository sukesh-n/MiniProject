using QuizzApp.Context;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO;

namespace QuizzApp.Repositories
{
    public class AssignedQuestionsRepository : IAssignedQuestionRepository
    {
        private readonly QuizzAppContext _context;
        public AssignedQuestionsRepository(QuizzAppContext context)
        {
            _context = context;
        }
        public Task<AssignedQuestions> AddAsync(AssignedQuestions entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AssignedQuestions>> AddQuestionsForTest(int AssignmentNo, List<QuestionSolutionDTO> GetQuestionsWithSolution)
        {
            throw new NotImplementedException(); new NotImplementedException();
        }

        public Task<AssignedQuestions> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AssignedQuestions>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AssignedQuestions> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedQuestions> UpdateAsync(AssignedQuestions entity)
        {
            throw new NotImplementedException();
        }
    }
}
