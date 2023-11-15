using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Common;
using Delivery.Common.BL.Facades;
using Delivery.Web.BL.Options;
using Delivery.Web.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace Delivery.Web.BL.Facades
{
    public abstract class FacadeBase<TCreateModel, TDetailModel, TListModel> : IAppFacade
        where TCreateModel : IWithId
        where TDetailModel : IWithId
        where TListModel : IWithId
    {
        protected readonly RepositoryBase<TCreateModel, TDetailModel, TListModel> repository;
        private readonly IMapper mapper;
        private readonly LocalDbOptions localDbOptions;
        protected virtual string apiVersion => "3";

        protected FacadeBase(
            RepositoryBase<TCreateModel, TDetailModel, TListModel> repository,
            IMapper mapper,
            IOptions<LocalDbOptions> localDbOptions)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.localDbOptions = localDbOptions.Value;
        }

        public virtual async Task<List<TListModel>> GetAllAsync()
        {
            var itemsAll = new List<TListModel>();

            if (localDbOptions.IsLocalDbEnabled)
            {
                itemsAll.AddRange(await GetAllFromLocalDbAsync());
            }
            return itemsAll;
        }

        protected async Task<IList<TListModel>> GetAllFromLocalDbAsync()
        {
            var itemsLocal = await repository.GetAllAsync();
            return mapper.Map<IList<TListModel>>(itemsLocal);
        }

        public abstract Task<TDetailModel> GetByIdAsync(Guid id);

        public virtual async Task SaveAsync(TCreateModel data)
        {
            try
            {
                await SaveToApiAsync(data);
            }
            catch (HttpRequestException exception) when (exception.Message.Contains("Failed to fetch"))
            {
                if (localDbOptions.IsLocalDbEnabled)
                {
                    await repository.InsertAsync(data);
                }
            }
        }

        public abstract Task<Guid> SaveToApiAsync(TCreateModel data);
        public abstract Task DeleteAsync(Guid id);

        public abstract Task<bool> SynchronizeLocalDataAsync();
        public async Task<bool> Exist(Guid id)
        {
            var items = await GetAllAsync();
            foreach (var item in items)
            {
                if (item.Id == id)
                    return true;
            }
            return false;
        }
    }
}
