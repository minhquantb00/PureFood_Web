

using FirebaseAdmin.Messaging;

namespace PureFood.GoogleService;

public interface IFirebaseMessagingClient
{
    Task SubscribeToTopic(string[] fcmTokens, string topic);
    Task UnsubscribeFromTopicAsync(string[] fcmTokens, string topic);

    Task<string> Send(string fcmToken, string title, string content, string? imageUrl,
        Dictionary<string, string> data, bool isCMSNotify);

    Task<BatchResponse> Send(string[] fcmTokens, string title, string content, string? imageUrl,
        Dictionary<string, string>? data, bool isCMSNotify);

    //Task<BatchResponse> Send(Message[] messages);

    Task<string> SendToTopic(string topic, string title, string content, string? imageUrl,
        Dictionary<string, string> data, bool isCMSNotify);

    Task<string> SendToTopic(string[] topics, string title, string content, string? imageUrl,
        Dictionary<string, string> data, bool isCMSNotify);

    Task<string> SendAll(string title, string content, string? imageUrl,
        Dictionary<string, string> data, bool isCMSNotify);
}
