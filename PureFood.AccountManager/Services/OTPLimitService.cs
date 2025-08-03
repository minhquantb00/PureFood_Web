using PureFood.AccountCommands.Queries;
using PureFood.AccountManager.Shared;
using PureFood.BaseApplication.Services;
using PureFood.BaseCommands;

namespace PureFood.AccountManager.Services
{
    public class OTPLimitService(
    ILogger<OTPLimitService> logger,
    ContextService contextService,
    ICacheService cacheService) : BaseService(logger, contextService), IOTPLimitService
    {
        public Task<BaseCommandResponse<DateTime[]>> GetOtpSendTimeByKeyword(OtpLimitGetByKeywordQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
