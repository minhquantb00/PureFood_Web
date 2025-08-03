using PureFood.BaseCommands;
using PureFood.EmailCommands.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailManager.Shared
{
    [ServiceContract]
    public interface IEmailQueueService
    {
        [OperationContract]
        Task<BaseCommandResponse<int>> CreateQueuedEmailAsync(EmailCreateQueueCommand command);
        [OperationContract]
        Task<BaseCommandResponse<int>> CreateQueuedEmailFromTemplateAsync(EmailCreateQueuedFromTemplateCommand command);
    }
}
