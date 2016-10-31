using A.Core.Attributes;
using A.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interface
{
    /// <summary>
    /// Has only readonly methods (get and get many)
    /// </summary>
    public interface IReadService<TEntity, TSearchObject, TSearchAdditionalData> : IService
        where TEntity : class, new()
        where TSearchAdditionalData : BaseAdditionalSearchRequestData, new()
        where TSearchObject : BaseSearchObject<TSearchAdditionalData>, new()
    {
        /// <summary>
        /// Lazy loading options, Row version etc inside additional data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="additionalData"></param>
        /// <returns></returns>
        [DefaultMethodBehaviour(BehaviourEnum.GetById)]
        TEntity Get(object id, TSearchAdditionalData additionalData = null);

        /// <summary>
        /// Returns data by specified search object
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [DefaultMethodBehaviour(BehaviourEnum.Get)]
        PagedResult<TEntity> GetPage(TSearchObject search);
    }
}
