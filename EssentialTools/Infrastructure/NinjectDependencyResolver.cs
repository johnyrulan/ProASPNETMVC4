using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel { get; set; }

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IValueCalc>().To<LinqValueCalc>();

            _kernel.Bind<IDiscountHelper>()
                .To<DefaultDiscountHelper>()
                .WithPropertyValue("DiscountSize", 50M);
            // This binds after the class is created. 
            // This can also be passed in as a constructor value using the WithConstructorArgument() method.

            _kernel.Bind<IDiscountHelper>()
                   .To<FlexibleDiscountHelper>()
                   .WhenInjectedInto<LinqValueCalc>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}