using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Models.DTO.AnalyseServiceDTO;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces
{
    public interface IAdminService
    {
        
        Task<TestDTO> ConductCommonQuizAsync(List<User> candidates, QuestionSelectionDTO questionSelectionDTO);       
        Task<ResultDTO> ViewResultAnalysis();
    }
}
