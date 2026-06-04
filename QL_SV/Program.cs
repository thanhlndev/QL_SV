using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
//using QL_SV.Service;

namespace QL_SV
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //var serviceProvider = ConfigureServices();
            //var facultyService = serviceProvider.GetRequiredService<IFacultyService>();
            Application.Run(new FacultyForm(
                //facultyService
                ));
        }
        //private static IServiceProvider ConfigureServices()
        //{
        //    var services = new ServiceCollection();
        //    services.AddScoped<IFacultyService, FacultyService>();

        //    return services.BuildServiceProvider();
        //}
    }
}
