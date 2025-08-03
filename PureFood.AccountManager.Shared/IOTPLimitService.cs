using PureFood.AccountCommands.Queries;
using PureFood.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountManager.Shared
{
    [ServiceContract]
    public interface IOTPLimitService
    {
        [OperationContract]
        Task<BaseCommandResponse<DateTime[]>> GetOtpSendTimeByKeyword(OtpLimitGetByKeywordQuery query);
    }
}
