﻿namespace PureFood.BaseApplication.Services
{
    public class LogAction : ILogAction
    {
        public async Task Push(string serviceName, string action, object? request, object? response, object? userInfo,
            TimeSpan executeTime,
            string requestUrl = "", string connectionId = "", string requestId = "", object? trackingProcessTime = null)
        {
        }

        public async Task PushError(string serviceName, string action, object? request, Exception exception,
            object? userInfo,
            TimeSpan executeTime, string requestUrl = "", string connectionId = "", string requestId = "",
            object? trackingProcessTime = null)
        {
        }

        public async Task PushError(string serviceName, string action, object? request, object? response,
            Exception exception,
            object? userInfo, TimeSpan executeTime, string requestUrl = "", string connectionId = "", string requestId = "",
            object? trackingProcessTime = null)
        {
        }

        public void Tracking((string Key, string Value)[]? parameters, int minuteCacheExpire, TimeSpan executeTime,
            Exception? exception, string? keyCache, string requestUrl, string requestPath, string connectionId,
            string requestId, object? tracking)
        {
        }

        public void Tracking(string componentId, int pageIndex, int pageSize, (string Key, string Value)[]? parameters,
            int minuteCacheExpire, TimeSpan executeTime, Exception? exception, string? keyCache, string requestUrl,
            string requestPath, string connectionId, string requestId, object? tracking)
        {
        }

        public void Tracking(string pageId, int deviceType, Dictionary<string, string>? conditions, long? minuteCacheExpire,
            TimeSpan executeTime,
            Exception? exception, int httpStatusCode, string? keyCache, string requestUrl, string connectionId,
            string requestId)
        {
        }
    }
}
