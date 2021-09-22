using MediatR;
using Microsoft.EntityFrameworkCore;
using Protel.ExchangeRates.Core;
using Protel.ExchangeRates.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Data
{
    /// <summary>
    /// Represents the entity repository implementation
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPublisher _mediator;

        #endregion

        #region Ctor

        public EntityRepository(ApplicationDbContext applicationDbContext, IPublisher mediator)
        {
            _applicationDbContext = applicationDbContext;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _applicationDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int? id)
        {
            return await _applicationDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity, bool publishEvent = true)
        {
            await _applicationDbContext.Set<TEntity>().AddAsync(entity);

            //if (publishEvent)
            //{
            //    await _mediator.Publish(new EntityInsertedEvent(entity));
            //}
        }

        public async Task InsertAsync(IList<TEntity> entities, bool publishEvent = true)
        {
            await _applicationDbContext.Set<TEntity>().AddRangeAsync(entities);

            //if (publishEvent)
            //{
            //    foreach (var entity in entities)
            //    {
            //        await _mediator.Publish(new EntityInsertedEvent(entity));
            //    }
            //}
        }

        #endregion
    }
}
