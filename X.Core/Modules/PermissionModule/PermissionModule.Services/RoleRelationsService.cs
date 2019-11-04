using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PermissionModule.Interfaces;

namespace PermissionModule.Services
{
    partial class RoleRelationsService
    {
        public Lazy<IRoleService> RoleService { get; set; }

        public async Task<IList<string>> ChildrenRoles(int roleId, short? level)
        {
            IList<string> list = new List<string>();

            string filterByLevel = "";
            if (level.HasValue)
            {
                filterByLevel = $"WHERE Level <= {level.Value}";
            }

            var rawSqlString = $@";with RolesChildrenCTE as
	                            (
		                            SELECT *, 1 AS Level
		                            FROM permission.RoleRelations
		                            where ParentRoleId = {roleId} and IsDeleted = 0
		                            UNION ALL
		                            SELECT C.*, Level + 1
		                            FROM permission.RoleRelations AS C
		                            JOIN RolesChildrenCTE p on C.ParentRoleId = P.RoleId
                                    WHERE  C.IsDeleted = 0
	                            )
                                SELECT
                                *
                                FROM
                                RolesChildrenCTE 
                                {filterByLevel}
                                ORDER BY Level
                                OPTION (MAXRECURSION 1000)
                                    ";
            var result = Context.RoleRelations.FromSql(rawSqlString).AsNoTracking().ToList();

            foreach (var item in result)
            {
                var childRole = await RoleService.Value.GetAsync(item.RoleId);
                list.Add(childRole.Name);
            }

            return list;
        }

        public async Task<IList<string>> ParentRoles(int roleId, short? level)
        {
            IList<string> list = new List<string>();

            string filterByLevel = "";
            if (level.HasValue)
            {
                filterByLevel = $"WHERE Level <= {level.Value}";
            }

            var rawSqlString = $@";with RolesParentCTE as
	                            (
		                            SELECT *, 1 AS Level
		                            FROM permission.RoleRelations
		                            where RoleId = {roleId} and IsDeleted = 0
		                            UNION ALL
		                            SELECT C.*, Level + 1
		                            FROM permission.RoleRelations AS C
		                            JOIN RolesParentCTE p on C.RoleId = P.ParentRoleId
                                    WHERE  C.IsDeleted = 0
	                            )
                                SELECT
                                *
                                FROM
                                RolesParentCTE 
                                {filterByLevel}
                                ORDER BY Level
                                OPTION (MAXRECURSION 1000)
                                    ";
            var result = Context.RoleRelations.FromSql(rawSqlString).AsNoTracking().ToList();

            foreach (var item in result)
            {
                var parentRole = await RoleService.Value.GetAsync(item.ParentRoleId);
                list.Add(parentRole.Name);
            }

            return list;
        }
    }
}
