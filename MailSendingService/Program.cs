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
            //    // ͬһ�����п������ж���û�������Ҫ��
            //    //��һ��������ӵ��˽��̣����������
            //    // �Դ�����һ������������磬
            //    //
            //    //   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
            //    //
            //    ServicesToRun = new System.ServiceProcess.ServiceBase[] { x };
            //    System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            //}

        }
    }
}