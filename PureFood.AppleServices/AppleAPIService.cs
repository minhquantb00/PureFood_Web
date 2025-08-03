using PureFood.HttpClientBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AppleServices
{
    public class AppleAPIService : IAppleAPIService
    {
        private readonly string _sharedSecret;
        private readonly string _verifyReceiptUrl;
        private readonly IHttpClient _httpClient;

        public AppleAPIService(IHttpClient httpClient, string sharedSecret, string verifyReceiptUrl)
        {
            _httpClient = httpClient;
            _sharedSecret = sharedSecret;
            _verifyReceiptUrl = verifyReceiptUrl;
        }

        public async Task<bool> ValidatePurchase(string apple_transaction_id, string apple_receipt, string shared_secret,
            string verifyReceiptUrl)
        {
            AppleReceiptRequest request = new AppleReceiptRequest
            {
                password = shared_secret,
                receipt_data = apple_receipt,
                exclude_old_transactions = true
            };
            string c = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            c = c.Replace("receipt_data", "receipt-data").Replace("exclude_old_transactions", "exclude-old-transactions");
            HttpResponseMessage response =
                await _httpClient.Post(verifyReceiptUrl, new StringContent(c, Encoding.UTF8, "application/json"));
            string data = await response.Content.ReadAsStringAsync();
            try
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<AppleReceiptResponse>(data);
                if (result?.latest_receipt_info[0].in_app_ownership_type != "PURCHASED" &&
                    apple_transaction_id != result?.latest_receipt_info[0].original_transaction_id)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
