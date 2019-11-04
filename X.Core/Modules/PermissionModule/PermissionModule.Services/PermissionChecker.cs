using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using PermissionModule.Interfaces;
using PermissionModule.Model;
using PermissionModule.Model.SearchObjects;
using X.Core.Interceptors;
using X.Core.Interface;
using X.Core.Model;

namespace PermissionModule.Services
{
    public class PermissionChecker : X.Core.Interface.IPermissionChecker, Interfaces.IPermissionChecker
    {
        static ILog _logger = log4net.LogManager.GetLogger(typeof(PermissionChecker));

        public bool BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public void DisposeTransaction()
        {
            throw new NotImplementedException();
        }

        public Lazy<IActionContext> ActionContext { get; set; }

        public Lazy<IRoleService> RoleService { get; set; }

        public Lazy<IPermissionService> PermissionService { get; set; }

        [Log]
        [Cache(ExpirationType.ExpiresIn, "00:03:00", isUserContextAware: true)]
        public virtual async Task<PermissionCheckResult> IsAllowed(Model.Requests.PermissionCheckRequest request)
        {
            PermissionCheckResult isAllowed = null;
            string[] roleList = null;
            object roleListTmp;
            if (ActionContext.Value.Data.TryGetValue("RoleList", out roleListTmp))
            {
                roleList = (string[])roleListTmp;
            }
            //load hierarchy for all roles
            roleList = await GetAllRolesByHierarchy(roleList);
            isAllowed = await IsAllowedByRole(request, roleList);
            isAllowed.IsAuthorized = ActionContext.Value.Data.ContainsKey("UserName");

            if (!isAllowed.IsAllowed)
            {
                ActionContext.Value.Data.TryGetValue("UserId", out object userId);

                _logger.Warn($"Permission denied: {request.Permission} for user: {userId}");
            }
            return isAllowed;
        }

        protected virtual async Task<string[]> GetAllRolesByHierarchy(IList<string> currentRoleNameList)
        {
            IList<string> roleList = new List<string>();

            if (currentRoleNameList?.Count > 0)
            {
                var currentRoleList = await this.RoleService.Value.GetPageAsync(new RoleSearchObject()
                {
                    NameList = currentRoleNameList.ToList(),
                    RetrieveAll = true
                });

                foreach (var role in currentRoleList.ResultList)
                {
                    roleList.Add(role.Name);
                    var childRoles = await this.RoleService.Value.ChildrenRoles(role.Name);
                    foreach (var childRole in childRoles)
                    {
                        roleList.Add(childRole);
                    }
                }

                roleList = roleList.Distinct().ToList();
            }

            return roleList.ToArray();
        }

        protected virtual async Task<PermissionCheckResult> IsAllowedByRole(Model.Requests.PermissionCheckRequest request, string[] roleList)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Permission))
            {
                throw new ApplicationException("Permission must be set");
            }
            request.Permission = request.Permission.ToLower();

            PermissionCheckResult checkResult = new PermissionCheckResult();

            List<string> permissionList = new List<string>();
            permissionList.Add(request.Permission);
            if (!request.IsExactMatchRequired)
            {
                string[] permissionParts = request.Permission.Split('.');
                StringBuilder previousPermissionPart = new StringBuilder();
                for (int i = 0; i < permissionParts.Length - 1; i++)
                {
                    string permissionPart = permissionParts[i];
                    previousPermissionPart.Append(permissionPart + ".");
                    string permissionTemp = previousPermissionPart.ToString() + "*";
                    permissionList.Add(permissionTemp);
                }
                //add root permission to list
                permissionList.Add("*");
            }

            bool isHandled = false;

            if (roleList != null && roleList.Length > 0)
            {
                checkResult.RequestedPermission = request.Permission;
                checkResult.PermissionResolveMode = PermissionResolveMode.Role;
                RoleSearchObject search = new RoleSearchObject();
                search.AdditionalData.IncludeList.Add("RolePermission");
                search.AdditionalData.IncludeList.Add("RolePermission.Permission");

                foreach (var role in roleList)
                {
                    search.NameList.Add(role);
                }

                search.PermissionName = request.Permission;

                var result = await RoleService.Value.GetPageAsync(search);

                foreach (var currentPermission in permissionList.OrderByDescending(x => x.Length))
                {
                    var permissionSelect = result.ResultList
                        .SelectMany(x => x.RolePermission.Where(y =>
                        !string.IsNullOrWhiteSpace(y.Permission.OperationType) && !string.IsNullOrWhiteSpace(request.OperationType)
                            && y.Permission.OperationType.Equals(request.OperationType, StringComparison.InvariantCultureIgnoreCase)
                            && y.Permission.Name.Equals(currentPermission, StringComparison.InvariantCultureIgnoreCase))).ToList();

                    if (permissionSelect.Count == 0)
                    {
                        permissionSelect = result.ResultList
                        .SelectMany(x => x.RolePermission.Where(y => y.Permission.Name.Equals(currentPermission, StringComparison.InvariantCultureIgnoreCase)
                                                                    && ((!string.IsNullOrWhiteSpace(y.Permission.OperationType) && request.OperationType == y.Permission.OperationType)
                                                                            || y.Permission.OperationType == null
                                                                        )
                                                                )).ToList();
                    }


                    //first check is this permission disabled in any role
                    if (permissionSelect.Any(x => x.IsAllowed == false))
                    {
                        checkResult.IsAllowed = false;
                        checkResult.ResolvedByPermission = currentPermission;
                        isHandled = true;
                        break;
                    }
                    //is this method allowed in any role
                    else if (permissionSelect.Any(x => x.IsAllowed == true))
                    {
                        checkResult.IsAllowed = true;
                        checkResult.ResolvedByPermission = currentPermission;
                        isHandled = true;
                        break;
                    }
                }
            }

            if (!isHandled && !request.IsDefaultResolveModeDisabled)
            {
                checkResult = await IsAllowedByPermission(request, permissionList);
            }
            return checkResult;
        }

        protected virtual async Task<PermissionCheckResult> IsAllowedByPermission(Model.Requests.PermissionCheckRequest request, List<string> permissionList)
        {
            PermissionCheckResult checkResult = new PermissionCheckResult();
            checkResult.RequestedPermission = request.Permission;
            checkResult.PermissionResolveMode = PermissionResolveMode.Default;

            PermissionSearchObject permissionSearch = new PermissionSearchObject();
            permissionSearch.NameWithHierarchy = request.Permission;
            permissionSearch.RetrieveAll = true;
            var permissionResult = await PermissionService.Value.GetPageAsync(permissionSearch);

            foreach (var currentPermission in permissionList.OrderByDescending(x => x.Length))
            {
                var permissionSelect = permissionResult.ResultList.Where(y =>
                    !string.IsNullOrWhiteSpace(y.OperationType) && !string.IsNullOrWhiteSpace(request.OperationType)
                    && y.OperationType.Equals(request.OperationType, StringComparison.InvariantCultureIgnoreCase)
                    && y.Name.Equals(currentPermission, StringComparison.InvariantCultureIgnoreCase)).ToList();

                if (permissionSelect.Count == 0)
                {
                    permissionSelect = permissionResult.ResultList.Where(y => y.Name.Equals(currentPermission, StringComparison.InvariantCultureIgnoreCase)
                        && string.IsNullOrWhiteSpace(y.OperationType)).ToList();
                }

                //first check is this permission disabled in any role
                if (permissionSelect.Any(x => x.IsAllowed == false))
                {
                    checkResult.IsAllowed = false;
                    checkResult.ResolvedByPermission = currentPermission;
                    break;
                }
                //is this method allowed in any role
                else if (permissionSelect.Any(x => x.IsAllowed == true))
                {
                    checkResult.IsAllowed = true;
                    checkResult.ResolvedByPermission = currentPermission;
                    break;
                }
            }

            return checkResult;
        }

        public async Task<bool> IsAllowed(PermissionCheckRequest permission)
        {
            var request = new Model.Requests.PermissionCheckRequest();
            request.Permission = permission.Permission;
            request.OperationType = permission.OperationType;

            var isAllowed = await IsAllowed(request);
            return isAllowed.IsAllowed;
        }

        public async void ThrowExceptionIfNotAllowed(PermissionCheckRequest permission)
        {
            if (!await IsAllowed(permission))
            {
                throw new ApplicationException(string.Format("Permission: {0} not allowed!", permission));
            }
        }
    }
}
