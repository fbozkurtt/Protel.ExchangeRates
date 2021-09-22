namespace Protel.ExchangeRates.Core.Events
{
    /// <summary>
    /// A container for entities that have been inserted.
    /// </summary>
    public class EntityInsertedEvent : DomainEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entity">Entity</param>
        public EntityInsertedEvent(object entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Entity
        /// </summary>
        public object Entity { get; }
    }
}
