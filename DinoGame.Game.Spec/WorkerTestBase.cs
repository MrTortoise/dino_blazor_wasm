using System;
using System.Collections.Generic;
using System.Linq;
using EventBase.Client;
using Xunit;

namespace DinoGame.Game.Spec
{
    public class WorkerTestBase
    {
        private readonly IEventStore _eventStore = new InMemoryEventStore();
        private ITurnEventsIntoCommand _worker;

        protected void Given(ITurnEventsIntoCommand worker)
        {
            Given(new List<EventInStream>(), worker);
        }
        protected void Given(List<EventInStream> events, ITurnEventsIntoCommand worker)
        {
            _worker = worker;
            _worker.SubscribeTo(_eventStore);

            var eventInStreams = events.ToList();
            foreach (var @event in eventInStreams)
            {
                var streamEvent = ToStreamEvent(@event);
                var createStreamEvent = new CreateStreamEvent(streamEvent.StreamName, streamEvent.StreamPosition, streamEvent.Event);
                _eventStore.Append(createStreamEvent);
            }
        }

        protected void When(CreateStreamEvent @event)
        {
            _eventStore.Append(@event);
        }

        protected void Then(string streamName, Action<StreamEvent> asserts)
        {
            var stream = _eventStore.ReadStream(streamName);
            Assert.NotEmpty(stream);
            var lastEvent = stream.Last();
            asserts(lastEvent);
        }

        private StreamEvent ToStreamEvent(EventInStream @event)
        {
            var events = _eventStore.ReadStream(@event.Stream);
            long streamPosition = 0;
            long globalPosition = 0;

            if (events.Any())
            {
                streamPosition = events.Count;
                globalPosition = events.Count;
            }

            var streamEvent = new StreamEvent(@event.Stream, streamPosition, globalPosition, DateTime.Now, @event.Event);
            return streamEvent;
        }
    }
}