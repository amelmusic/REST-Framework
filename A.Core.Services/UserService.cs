using A.Core.Attributes;
using A.Core.Interfaces;
using A.Core.Services.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Services
{
    public partial class UserService : IUserService
    {
        
        public Model.User Current()
        {
            var userIdstr = this.ActionContext.Value.Data["UserId"].ToString();
            //TODO: Get this ID from action context
            Guid userId = new Guid(userIdstr);
            return Get(userId);
        }
    }

}
