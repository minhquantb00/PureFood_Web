using PureFood.BaseApplication.Services;
using PureFood.BaseCommands;
using PureFood.EmailCommands.Commands;
using PureFood.EmailDomains;
using PureFood.EmailManager.Shared;
using PureFood.EmailReadModels;

namespace PureFood.EmailManager.Services
{
    public class EmailQueueService (
        HttpClient httpClient,
        ILogger<EmailQueueService> logger,
        ContextService contextService
        ) : BaseService(logger, contextService), IEmailQueueService
    {
        public async Task<BaseCommandResponse<int>> CreateQueuedEmailAsync(EmailCreateQueueCommand command)
        {
            return await ProcessCommand<int>(async (response) =>
            {
                if(command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                var uri = "QueuedEmails";
                var data = await httpClient.PostAsJsonAsync(uri, command);
                data.EnsureSuccessStatusCode();
                response.Data = int.Parse(await data.Content.ReadAsStringAsync());

                response.SetSuccess();
            });
        }

        public  async Task<BaseCommandResponse<int>> CreateQueuedEmailFromTemplateAsync(EmailCreateQueuedFromTemplateCommand command)
        {
            return await ProcessCommand<int>(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                var uri = "QueuedEmails/createFromTemplate";
                var data = await httpClient.PostAsJsonAsync(uri, command);
                data.EnsureSuccessStatusCode();
                response.Data = int.Parse(await data.Content.ReadAsStringAsync());

                response.SetSuccess();
            });
        }
    }
}
