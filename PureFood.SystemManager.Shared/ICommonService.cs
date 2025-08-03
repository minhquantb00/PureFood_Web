using PureFood.SystemCommands.Queries;
using System.ServiceModel;

namespace PureFood.SystemManager.Shared
{
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        Task<string> GetNextCode(GetNextCodeQuery query);

        [OperationContract]
        Task<string[]> GetNextMultipleCode(GetNextCodeQuery query);

    }
}
