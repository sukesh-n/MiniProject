using QuizzApp.Models;

namespace QuizzApp.Interfaces.Live
{
    public interface IUpdateDb
    {
        public Task<bool> UpdateAssignedTestEmail();
    }
}
