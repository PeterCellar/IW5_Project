using Delivery.Common;

namespace Delivery.Web.DAL.Repositories.Interfaces
{
    public interface IWebRepository<TCreateModel, TDetailModel, TListModel>
        where TCreateModel : IWithId
        where TDetailModel : IWithId
        where TListModel : IWithId
    {
        string TableName { get; }
        Task<IList<TListModel>> GetAllAsync();
        Task<TDetailModel> GetByIdAsync(Guid id);
        Task InsertAsync(TCreateModel entity);
        Task RemoveAsync(Guid id);
    }
}
