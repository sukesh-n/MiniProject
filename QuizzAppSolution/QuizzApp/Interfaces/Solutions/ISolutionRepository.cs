﻿using QuizzApp.Models;


namespace QuizzApp.Interfaces.Solutions
{
    public interface ISolutionRepository : IRepository<int, Solution>
    {
        public Task<List<Solution>> GetSolutions(List<int> QuestionId);

        public Task<List<Solution>> GetSolutionForQuestions(List<int> QuestionIds);
    }
}
