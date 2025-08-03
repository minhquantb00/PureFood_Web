using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AppleServices
{
    public interface IAppleAPIService
    {
        Task<bool> ValidatePurchase(string apple_transaction_id, string apple_receipt, string shared_secret, string verifyReceiptUrl);
    }
}
