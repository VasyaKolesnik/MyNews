using Autofac;
using Autofac.Integration.Mvc;
using News.Interface;
using News.Service;
using System.Web.Mvc;

namespace News.App_Start
{
    public class AutofacConfig
    {
        public static void SetupContainer()
        {
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //регистрируем споставление типов
            builder.RegisterType<ViewService>().As<IViewService>().InstancePerHttpRequest();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}