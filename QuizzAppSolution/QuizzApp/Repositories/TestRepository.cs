using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuizzApp.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly QuizzAppContext _context;

        public TestRepository(QuizzAppContext context)
        {
            _context = context;
        }
        public Task<TestDTO> AddAsync(TestDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TestDTO>> AddTestListAsync(List<TestDTO> testDTOs)
        {
            try
            {
                var result = _context.tests.AddRangeAsync(testDTOs);
                await _context.SaveChangesAsync();
                return testDTOs;
            }
            catch (Exception ex) {
                throw new ErrorInConnectingRepository(ex.Message);
            }
        }

        public Task<TestDTO> DeleteAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TestDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TestDTO> GetAsync(int Key)
        {
            throw new NotImplementedException();
        }

        public async Task<TestDTO> GetTestByAssignmentNoAsync(int AssignmentNo)
        {
            try
            {
                var result = await _context.tests.FirstOrDefaultAsync(e => e.AssignmentNo == AssignmentNo);
                if (result == null)
                {
                    return null;
                }

                var testDto = new TestDTO();
                {
                    testDto.AssignmentNo = result.AssignmentNo;
                    testDto.TestId = result.TestId;
                    testDto.UserId = result.UserId;
                    testDto.QuestionsCount = result.QuestionsCount;
                    testDto.StatusOfTest = result.StatusOfTest;
                    testDto.TestStartDate = result.TestStartDate;
                    testDto.TestEndDate = result.TestEndDate;
                    testDto.TestType = result.TestType;
                }

                return testDto;
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository(ex.Message);
            }
        }

        public Task<TestDTO> GetTestByUserIdAndAssignemtnNumber(int UserId, int AssignmentNumber)
        {
            try
            {
                var result = _context.tests.FirstOrDefault(e => e.UserId == UserId && e.AssignmentNo == AssignmentNumber);
                if (result == null)
                {
                    return null;
                }
                var testDto = new TestDTO();
                {
                    testDto.AssignmentNo = result.AssignmentNo;
                    testDto.TestId = result.TestId;
                    testDto.UserId = result.UserId;
                    testDto.QuestionsCount = result.QuestionsCount;
                    testDto.StatusOfTest = result.StatusOfTest;
                    testDto.TestStartDate = result.TestStartDate;
                    testDto.TestEndDate = result.TestEndDate;
                    testDto.TestType = result.TestType;
                }
                return Task.FromResult(testDto);
            }
            catch (Exception ex)
            {
                throw new ErrorInConnectingRepository(ex.Message);
            }
        }

        public Task<TestDTO> UpdateAsync(TestDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
