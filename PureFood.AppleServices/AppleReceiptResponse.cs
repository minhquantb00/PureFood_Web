using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PureFood.AppleServices
{
    public class AppleReceiptRequest
    {
        public string password { get; set; }
        [JsonPropertyName("receipt-data")]
        public string receipt_data { get; set; }
        [JsonPropertyName("exclude-old-transactions")]
        public bool exclude_old_transactions { get; set; }
    }
    public class AppleReceiptResponse
    {
        public AppleReceiptModel receipt { get; set; }
        public string environment { get; set; }
        public List<AppleReceiptInAppModel> latest_receipt_info { get; set; }
        public string latest_receipt { get; set; }
        public int status { get; set; }

    }
    public class AppleReceiptModel
    {
        public string receipt_type { get; set; }
        public string adam_id { get; set; }
        public string app_item_id { get; set; }
        public string bundle_id { get; set; }
        public string application_version { get; set; }
        public string download_id { get; set; }
        public string version_external_identifier { get; set; }
        public string receipt_creation_date { get; set; }
        public string receipt_creation_date_ms { get; set; }
        public string receipt_creation_date_pst { get; set; }
        public string request_date { get; set; }
        public string request_date_ms { get; set; }
        public string request_date_pst { get; set; }
        public string original_purchase_date { get; set; }
        public string original_purchase_date_ms { get; set; }
        public string original_purchase_date_pst { get; set; }
        public string original_application_version { get; set; }
        public List<AppleReceiptInAppModel> in_app { get; set; }

    }
    public class AppleReceiptInAppModel
    {
        public string quantity { get; set; }
        public string product_id { get; set; }
        public string transaction_id { get; set; }
        public string original_transaction_id { get; set; }
        public string purchase_date { get; set; }
        public string purchase_date_ms { get; set; }
        public string purchase_date_pst { get; set; }
        public string original_purchase_date { get; set; }
        public string original_purchase_date_ms { get; set; }
        public string original_purchase_date_pst { get; set; }
        public string is_trial_period { get; set; }
        public string in_app_ownership_type { get; set; }
    }
}
