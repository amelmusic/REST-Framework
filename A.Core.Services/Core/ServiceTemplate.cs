






















#region A_Core generated code
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.Collections.Generic;
using A.Core.Interface;
using AutoMapper.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using A.Core.Validation;
using Autofac;
using Autofac.Extras.DynamicProxy;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using A.Core;

//A.Core.Interfaces
using A.Core.Model;
using A.Core.Model.Requests;


namespace A.Core.Services 
	{ 
	
	public partial class ServicesRegistration : A.Core.Interface.IServicesRegistration
	{
		public int Priority {get; set; }
		public ServicesRegistration()
		{
			Priority = 0; //This is root, If you want to override this. Add new class with higher priority
		}
		public void Register(ref Autofac.ContainerBuilder container)
		{
			}
	}



}
#endregion
