using System.Threading.Tasks;

namespace FiiPrezent.Core.Interfaces
{
    public interface IParticipantsService
    {
        Task<ResultStatus> RegisterParticipantAsync(string code, string nameIdentifier);
    }
}