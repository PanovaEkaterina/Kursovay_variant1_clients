using BeautySaloonService;
using BeautySaloonService.ImplementationsList;
using BeautySaloonService.Interfaces;
using System;
using System.Data.Entity;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace ViewWPFKlient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
         static void Main()
        {
            var container = BuildUnityContainer();

            var application = new App();
            application.Run(container.Resolve<FormAuthorization>());

        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, SaloonDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProcedureService, ProcedureService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IKlientService, KlientService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IZakazService, ZakazService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportService>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
