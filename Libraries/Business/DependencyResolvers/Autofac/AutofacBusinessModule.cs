using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BrandManager>().As<IBrandService>();
            builder.RegisterType<CarManager>().As<ICarService>();
            builder.RegisterType<ColorManager>().As<IColorService>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<RentalManager>().As<IRentalService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<CarImageManager>().As<ICarImageService>();

            builder.RegisterType<EfBrandDal>().As<IBrandDal>();
            builder.RegisterType<EfCarDal>().As<ICarDal>();
            builder.RegisterType<EfColorDal>().As<IColorDal>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<EfCarImageDal>().As <ICarImageDal>();
        }
    }
}
