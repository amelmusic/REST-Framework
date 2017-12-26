using A.Core.Interceptors;
using A.Core.Interface;
using A.Core.PermissionModule.Interfaces;
using A.Core.PermissionModule.Model;
using A.Core.PermissionModule.Model.Requests;
using A.Core.PermissionModule.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using A.Core.Model;

namespace A.Core.PermissionModule.Services
{
    public partial class PermissionChecker : A.Core.PermissionModule.Interfaces.IPermissionChecker, A.Core.Interface.IPermissionChecker, IService
    {
        
        public Lazy<IActionContext> ActionContext { get; set; }

        public Lazy<IRoleService> RoleService { get; set; }

        public Lazy<IPermissionService> PermissionService { get; set; }

        [Log]
        [Cache(ExpirationType.ExpiresIn, "00:15:00", isUserContextAware: true)]
        public virtual PermissionCheckResult IsAllowed(Model.Requests.PermissionCheckRequest request)
        {
            PermissionCheckResult isAllowed = null;
            string[] roleList = null;
            object roleListTmp;
            if (ActionContext.Value.Data.TryGetValue("RoleList", out roleListTmp))
            {
                roleList = (string[])roleListTmp;
            }
            isAllowed = IsAllowedByRole(request, roleList);
            isAllowed.IsAuthorized = ActionContext.Value.Data.ContainsKey("UserId");

            return isAllowed;
        }

        protected virtual PermissionCheckResult IsAllowedByRole(Model.Requests.PermissionCheckRequest request, string[] roleList)
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
                foreach (var role in roleList)
                {
                    search.NameList.Add(role);
                }

                search.PermissionName = request.Permission;

                var result = RoleService.Value.GetPage(search);

                foreach (var currentPermission in permissionList.OrderByDescending(x => x.Length))
                {
                    var permissionSelect = result.ResultList
                        .SelectMany(x => x.RolePermissions.Where(y =>
                        !string.IsNullOrWhiteSpace(y.Permission.OperationType) && !string.IsNullOrWhiteSpace(request.OperationType)
                            && y.Permission.OperationType.Equals(request.OperationType, StringComparison.InvariantCultureIgnoreCase)
                            && y.Permission.Name.Equals(currentPermission, StringComparison.InvariantCultureIgnoreCase))).ToList();

                    if (permissionSelect.Count == 0)
                    {
                        permissionSelect = result.ResultList
                        .SelectMany(x => x.RolePermissions.Where(y => y.Permission.Name.Equals(currentPermission, StringComparison.InvariantCultureIgnoreCase))).ToList();
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
                checkResult = IsAllowedByPermission(request, permissionList);
            }
            return checkResult;
        }

        protected virtual PermissionCheckResult IsAllowedByPermission(Model.Requests.PermissionCheckRequest request, List<string> permissionList)
        {
            PermissionCheckResult checkResult = new PermissionCheckResult();
            checkResult.RequestedPermission = request.Permission;
            checkResult.PermissionResolveMode = PermissionResolveMode.Default;

            PermissionSearchObject permissionSearch = new PermissionSearchObject();
            permissionSearch.NameWithHierarchy = request.Permission;
            permissionSearch.RetrieveAll = true;
            var permissionResult = PermissionService.Value.GetPage(permissionSearch);

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

        public bool IsAllowed(string permission)
        {
            var request = new Model.Requests.PermissionCheckRequest();
            request.Permission = permission;

            return IsAllowed(request).IsAllowed;
        }

        public void ThrowExceptionIfNotAllowed(string permission)
        {
            if (!IsAllowed(permission))
            {
                throw new ApplicationException(string.Format("Permission: {0} not allowed!", permission));
            }
        }

        public bool BeginTransaction()
        {
            return false;
        }

        public void CommitTransaction()
        {

        }

        public void RollbackTransaction()
        {

        }

        public void DisposeTransaction()
        {

        }

        public bool IsAllowed(A.Core.Model.PermissionCheckRequest permission)
        {
            var request = new Model.Requests.PermissionCheckRequest();
            request.Permission = permission.Permission;
            request.OperationType = permission.OperationType;

            return IsAllowed(request).IsAllowed;
        }

        public void ThrowExceptionIfNotAllowed(A.Core.Model.PermissionCheckRequest permission)
        {
            throw new NotImplementedException();
        }
    }
}
