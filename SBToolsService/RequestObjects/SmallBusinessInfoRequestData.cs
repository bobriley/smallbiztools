using SBToolsService.POCOs;

namespace SBToolsService.RequestObjects
{
    public class SmallBusinessInfoRequestData
    {
        public STToken Token { get; set; } = new STToken();
        public SmallBusinessInfo SmallBusinessInfo { get; set; } = new SmallBusinessInfo();     
    }
}
