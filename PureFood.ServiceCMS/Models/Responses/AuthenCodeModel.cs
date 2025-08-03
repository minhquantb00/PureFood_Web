using ProtoBuf;

namespace PureFood.ServiceCMS.Models.Responses
{
    [ProtoContract]
    public record AuthenCodeModel
    {
        [ProtoMember(1)] public required string AuthenApplicationId { get; set; }
        [ProtoMember(2)] public required string ExternalLoginConfigId { get; set; }
        [ProtoMember(3)] public required string SessionId { get; set; }
        [ProtoMember(4)] public required string AuthenCodeHashKey { get; set; }
        [ProtoMember(5)] public required string Token { get; set; }
    }
}
