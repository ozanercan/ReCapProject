﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


            builder.RegisterType<EfBrandDal>().As<IBrandDal>();
            builder.RegisterType<EfCarDal>().As<ICarDal>();
            builder.RegisterType<EfColorDal>().As<IColorDal>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<EfCarImageDal>().As<ICarImageDal>();
            builder.RegisterType<EfPaymentDal>().As<IPaymentDal>();
            builder.RegisterType<EfCarCreditScoreDal>().As<ICarCreditScoreDal>();
            builder.RegisterType<EfCustomerCreditCardDal>().As<ICustomerCreditCardDal>();
            builder.RegisterType<EfGearTypeDal>().As<IGearTypeDal>();
            builder.RegisterType<EfFuelTypeDal>().As<IFuelTypeDal>();


            builder.RegisterType<BrandManager>().As<IBrandService>();
            builder.RegisterType<CarManager>().As<ICarService>();
            builder.RegisterType<ColorManager>().As<IColorService>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<RentalManager>().As<IRentalService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<CarImageManager>().As<ICarImageService>();
            builder.RegisterType<PaymentManager>().As<IPaymentService>();
            builder.RegisterType<CarCreditScoreManager>().As<ICarCreditScoreService>();
            builder.RegisterType<CustomerCreditScoreManager>().As<ICustomerCreditScoreService>();
            builder.RegisterType<CustomerCreditCardManager>().As<ICustomerCreditCardService>();
            builder.RegisterType<GearTypeManager>().As<IGearTypeService>();
            builder.RegisterType<FuelTypeManager>().As<IFuelTypeService>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
