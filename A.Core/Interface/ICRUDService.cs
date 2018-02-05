using A.Core.Attributes;
using A.Core.Model;

namespace A.Core.Interface
{
    /// <summary>
    /// Represents CRUD operations on some entity
    /// </summary>
    /// <typeparam name="TSearchObject"></typeparam>
    /// <typeparam name="TInsert"></typeparam>
    /// <typeparam name="TUpdate"></typeparam>
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
        [DefaultMethodBehaviour(BehaviourEnum.Insert)]
        TEntity Insert(TInsert request, bool saveChanges = true);

        /// <summary>
        /// Updates model by given request
        /// </summary>
        /// <param name="request"></param>
        [DefaultMethodBehaviour(BehaviourEnum.Update)]
        TEntity Update(object id, TUpdate request, bool saveChanges = true);

        /// <summary>
        /// Patches model by given request
        /// </summary>
        /// <param name="request"></param>
        [DefaultMethodBehaviour(BehaviourEnum.Patch)]
        TEntity Patch(object id, TUpdate request, bool saveChanges = true);
    }
}
