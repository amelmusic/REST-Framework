using X.Core.Attributes;
using X.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.Core.Interface
{
    /// <summary>
    /// Represents CRUD operations on some entity
    /// </summary>
    /// <typeparam name="TSearchObject"></typeparam>
    /// <typeparam name="TInsert"></typeparam>
    /// <typeparam name="TUpdate"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICRUDService<TEntity, TSearchObject, TSearchAdditionalData, TInsert, TUpdate> : IReadService<TEntity, TSearchObject, TSearchAdditionalData>
        where TEntity : class, new()
        where TSearchAdditionalData : BaseAdditionalSearchRequestData, new()
        where TSearchObject : BaseSearchObject<TSearchAdditionalData>, new()
    {
        /// <summary>
        /// Inserts and returns model that has been inserted
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[DefaultMethodBehaviour(BehaviourEnum.Insert)]
        Task<TEntity> InsertAsync(TInsert request, bool saveChanges = true);

        /// <summary>
        /// Updates model by given request
        /// </summary>
        /// <param name="request"></param>
        //[DefaultMethodBehaviour(BehaviourEnum.Update)]
        Task<TEntity> UpdateAsync(object id, TUpdate request, bool saveChanges = true);

        //[DefaultMethodBehaviour(BehaviourEnum.Patch)]
        Task<TEntity> PatchAsync(object id, TUpdate request, bool saveChanges = true);
        Task<bool> DeleteAsync(object id, bool saveChanges = true);
    }
}
