using PureFood.EnumDefine;

namespace PureFood.ServiceCMS.Models.Responses
{
    public record UserLoginResponse
    {
        public required string Id { get; set; }
        public required OtpTypeEnum OtpType { get; set; }
        public required string SessionId { get; set; }
        public required bool IsNeedOtpVerify { get; set; }
    }

    public record UserLoginCompletedResponse : UserLoginResponse
    {
        public required string Token { get; set; }
        public required int MinuteExpire { get; set; }
        public required string RefreshToken { get; set; }
        public string? ReturnUrl { get; set; }
        public string? AuthenCode { get; set; }
        public string? AuthenApplicationId { get; set; }
        public string? CurrentDealerId { get; set; }
    }
}
