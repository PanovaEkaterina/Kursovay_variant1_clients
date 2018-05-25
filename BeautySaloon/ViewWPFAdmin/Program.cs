using BeautySaloonService;
using BeautySaloonService.ImplementationsList;
using BeautySaloonService.Interfaces;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace ViewAdmin
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, SaloonDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IKlientService, KlientService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProcedureService, ProcedureService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IZakazService, ZakazService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainService>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
