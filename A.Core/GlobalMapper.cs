using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace A.Core
{
    /// <summary>
    /// Reference to mapper populated at runtime from profiles
    /// </summary>
    public static class GlobalMapper
    {
        /// <summary>
        /// Reference to mapper populated at runtime from profiles
        /// </summary>
        public static IMapper Mapper = null;

        public static void Init(List<Profile> profileList)
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profileList)
                {
                    cfg.AddProfile(profile);
                }
            });

            Mapper = config.CreateMapper();
        }
    }
}
