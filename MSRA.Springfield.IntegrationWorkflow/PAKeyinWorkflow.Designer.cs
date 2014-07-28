using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace MSRA.Springfield.IntegrationWorkflow
{
    partial class PAKeyinWorkflow
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.SavePAActivity = new System.Workflow.Activities.CodeActivity();
            // 
            // SavePAActivity
            // 
            this.SavePAActivity.Name = "SavePAActivity";
            this.SavePAActivity.ExecuteCode += new System.EventHandler(this.SavePAActivity_ExecuteCode);
            // 
            // PAKeyinWorkflow
            // 
            this.Activities.Add(this.SavePAActivity);
            this.Name = "PAKeyinWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity SavePAActivity;

    }
}
