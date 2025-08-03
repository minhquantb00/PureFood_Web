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
    public interface IEmailService
    {
        [OperationContract]
        Task<string> SendEmail(SendMessageCommand message);
    }

}
