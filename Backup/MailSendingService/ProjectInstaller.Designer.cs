namespace Springfield.ExtendModule.EmailModule
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mailServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.mailServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // mailServiceProcessInstaller
            // 
            this.mailServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.mailServiceProcessInstaller.Password = null;
            this.mailServiceProcessInstaller.Username = null;
            // 
            // mailServiceInstaller
            // 
            this.mailServiceInstaller.ServiceName = "SpringfieldMailSender";
            this.mailServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.mailServiceProcessInstaller,
            this.mailServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller mailServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller mailServiceInstaller;
    }
}