﻿using Microsoft.Extensions.Logging;
using PureFood.BaseEvents;
using PureFood.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public class BaseEventHandler(ILogger<BaseEventHandler> logger)
    {
        private readonly List<IEvent> _events = [];
        private const bool IsUseQueue = false;
        private readonly ConcurrentQueue<IEvent?> _eventQueue = new();
        private readonly ConcurrentQueue<IEvent?> _triggerQueue = new();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void EventAdd(IEvent? @event)
        {
            if (@event == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(@event.Publisher))
            {
                var stackTrace = new StackTrace(new StackFrame(1, false));
                @event.Publisher =
                    $"{Environment.MachineName} - {CommonUtility.GetFullMethodName(stackTrace.GetFrame(0)?.GetMethod())}";
            }

            if (IsUseQueue)
            {
                Enqueue(@event);
            }
            else
            {
                _events.Add(@event);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void EventAdd(IEvent[] @events)
        {
            var stackTrace = new StackTrace(new StackFrame(1, false));
            var fullMethodName =
                $"{Environment.MachineName} - {CommonUtility.GetFullMethodName(stackTrace.GetFrame(0)?.GetMethod())}";
            foreach (var @event in @events)
            {
                if (String.IsNullOrEmpty(@event.Publisher))
                {
                    @event.Publisher = fullMethodName;
                }
            }

            if (IsUseQueue)
            {
                foreach (var @event in @events)
                {
                    Enqueue(@event);
                }
            }
            else
            {
                _events.AddRange(@events);
            }
        }

        private void EventRemove(IEvent @event)
        {
            _events.Remove(@event);
        }

        public void EventRemove(string id)
        {
            if (_events is not { Count: > 0 }) return;
            var events = _events.Where(p => p.EventId == id).ToArray();
            foreach (var @event in events)
            {
                EventRemove(@event);
            }
        }

        protected void EventRemoveAll(bool isTrigger = false)
        {
            if (IsUseQueue)
            {
                // After TryDequeue success, items are removed from queue
                // So no need to remove in queue
            }
            else
            {
                _events.RemoveAll(p => p.IsTrigger == isTrigger);
            }
        }

        public IEvent[] EventsGet(bool isTrigger = false)
        {
            if (IsUseQueue)
            {
                return TryDequeue(isTrigger);
            }
            else
            {
                return _events.Where(p => p.IsTrigger == isTrigger).ToArray();
            }
        }

        private void Enqueue(IEvent @event)
        {
            if (@event.IsTrigger)
            {
                _triggerQueue.Enqueue(@event);
            }
            else
            {
                _eventQueue.Enqueue(@event);
            }
        }

        private IEvent[] TryDequeue(bool isTrigger = false)
        {
            var events = new List<IEvent>();
            if (isTrigger)
            {
                while (!_triggerQueue.IsEmpty)
                {
                    if (_triggerQueue.TryDequeue(out IEvent? item) && item != null)
                    {
                        events.Add(item);
                    }
                }
            }
            else
            {
                while (!_eventQueue.IsEmpty)
                {
                    if (_triggerQueue.TryDequeue(out IEvent? item) && item != null)
                    {
                        events.Add(item);
                    }
                }
            }

            return events.ToArray();
        }

        #region LogError

        protected void LogError(Exception exception, string message)
        {
            logger.LogError(exception, "BaseEventHandler Exception {Message}", message);
        }

        protected void LogError(Exception exception)
        {
            logger.LogError(exception, "BaseEventHandler Exception {Message}", exception.Message);
        }

        protected void LogError(IEnumerable<string>? message)
        {
            if (message == null)
            {
                return;
            }

            logger.LogError("BaseEventHandler Error {Message}", message.ToArray().AsArrayJoin());
        }

        protected void LogError(string? message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            logger.LogError("BaseEventHandler Error {Message}", message);
        }

        protected void LogWarning(string? message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            logger.LogWarning("BaseEventHandler Warning {Message}", message);
        }

        protected void LogInformation(string? message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            logger.LogInformation("BaseEventHandler Information {Message}", message);
        }

        #endregion
    }
}
