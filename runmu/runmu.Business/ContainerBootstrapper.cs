﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;

namespace runmu.Business
{
    public class ContainerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterSingleton<IService, TeacherService>();
            container.RegisterSingleton<IService, CourseService>();
            container.RegisterSingleton<IService, StudentsService>();
            container.RegisterSingleton<IService, AssistantService>();
            container.RegisterSingleton<IService, PlatformService>();
            container.RegisterSingleton<IService, LearnStatusService>();
            container.RegisterSingleton<IService, SignUpService>();
            container.RegisterSingleton<IService, PaymentService>();
        }
    }
}
