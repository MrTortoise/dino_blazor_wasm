using System;
using System.Collections.Generic;
using EventBase.Client;

namespace DinoGame.Game
{
    public class Class1
    {
    }

    public interface IProjectEvents
    {
        public void Load(IEnumerable<StreamEvent> events);
    }

    public interface IReadModel : IProjectEvents
    {
        public void SubscribeTo(IEventStore eventStore);
    }

    public interface ITurnEventsIntoCommand : IReadModel
    {
    }

    public interface IHandleCommand<in T> : IProjectEvents where T : Command
    {
        public Event Handle(T command);
    }

    public interface IApply<in T> where T : Event
    {
        void Apply(T @event, long streamPosition);
    }

    public class HasId
    {
        public string GetId() => $"{this.GetType().Name}-{Guid.NewGuid()}";
    }

    public class Command : HasId
    {
        public Command()
        {
            Id = GetId();
        }


        protected Command(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
