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

        /// <summary>
        /// Use this one if there is no profile mapping
        /// </summary>
        public static IMapper DefaultMapper = null;

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

            var defaultCfg = new MapperConfiguration(cfg =>
            {
                cfg.ForAllPropertyMaps(pm => true,
                    (pm, c) =>
                    {
                        c.Condition((s, d, sVal) => sVal != null);
                    });

                cfg.CreateMissingTypeMaps = true;
            });
            DefaultMapper = defaultCfg.CreateMapper();
        }

    }
}
