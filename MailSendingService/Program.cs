using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System;

namespace Springfield.ExtendModule.EmailModule
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            ServicesToRun = new ServiceBase[] { new SpringfieldMailSender() };

            ServiceBase.Run(ServicesToRun);

            //SpringfieldMailSender x = new SpringfieldMailSender();
            //if (args.Length > 0)
            //{
            //    Console.WriteLine("Console");
            //    x.OnStart(null);
            //    Console.ReadLine();
            //}
            //else
            //{
            //    System.ServiceProcess.ServiceBase[] ServicesToRun;
            //    // 同一进程中可以运行多个用户服务。若要将
            //    //另一个服务添加到此进程，请更改下行
            //    // 以创建另一个服务对象。例如，
            //    //
            //    //   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
            //    //
            //    ServicesToRun = new System.ServiceProcess.ServiceBase[] { x };
            //    System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            //}

        }
    }
}