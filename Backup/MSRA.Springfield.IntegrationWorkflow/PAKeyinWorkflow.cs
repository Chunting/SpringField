﻿using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace MSRA.Springfield.IntegrationWorkflow
{
    public sealed partial class PAKeyinWorkflow : SequentialWorkflowActivity
    {
        public PAKeyinWorkflow()
        {
            InitializeComponent();
        }

        private void SavePAActivity_ExecuteCode(object sender, EventArgs e)
        {

        }
    }

}
