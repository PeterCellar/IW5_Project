using Delivery.Common;
using Delivery.Web.DAL.Repositories.Interfaces;

namespace Delivery.Web.DAL.Repositories
{
    public abstract class RepositoryBase<TCreateModel, TDetailModel, TListModel> : IWebRepository<TCreateModel, TDetailModel, TListModel>
        where TCreateModel : IWithId
        where TDetailModel : IWithId
        where TListModel : IWithId
    {
        private readonly LocalDb localDb;
        public abstract string TableName { get; }

        protected RepositoryBase(LocalDb localDb)
        {
            this.localDb = localDb;
        }

        public async Task<IList<TListModel>> GetAllAsync()
        {
            return await localDb.GetAllAsync<TListModel>(TableName);
        }

        public async Task<IList<TDetailModel>> GetAllDetailAsync()
        {
            return await localDb.GetAllAsync<TDetailModel>(TableName);
        }

        public async Task<TDetailModel> GetByIdAsync(Guid id)
        {
            return await localDb.GetByIdAsync<TDetailModel>(TableName, id);
        }

        public async Task InsertAsync(TCreateModel entity)
        {
            await localDb.InsertAsync(TableName, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            await localDb.RemoveAsync(TableName, id);
        }
    }
}
