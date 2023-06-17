using SBToolsService.POCOs;

namespace SBToolsService.ServiceInterfaces
{
    public interface ITokenService
    {
        public bool IsTokenValid(STToken token);
    }
}
