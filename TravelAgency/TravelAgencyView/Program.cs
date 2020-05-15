using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplement.Implements;
using Unity;
using Unity.Lifetime;

namespace TravelAgencyView
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
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ITourLogic, TourLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITravelLogic, TravelLogic>(new
           HierarchicalLifetimeManager());          
            return currentContainer;
        }
    }
}