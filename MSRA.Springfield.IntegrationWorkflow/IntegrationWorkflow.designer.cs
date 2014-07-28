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
    partial class IntegrationWorkflow
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
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference2 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference3 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference4 = new System.Workflow.Activities.Rules.RuleConditionReference();
            this.AddPAActivity = new System.Workflow.Activities.CodeActivity();
            this.ExistedPAActivity = new System.Workflow.Activities.CodeActivity();
            this.ExistedApplicantActivity = new System.Workflow.Activities.CodeActivity();
            this.AddApplicantActivity = new System.Workflow.Activities.CodeActivity();
            this.ifElseBranchActivity4 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity3 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity1 = new System.Workflow.Activities.IfElseBranchActivity();
            this.FailedActivity = new System.Workflow.Activities.CodeActivity();
            this.UpdateEnumeratorActivity = new System.Workflow.Activities.CodeActivity();
            this.LogActivity = new System.Workflow.Activities.CodeActivity();
            this.JudgeHasPAActivity = new System.Workflow.Activities.IfElseActivity();
            this.IsApplicantExistActivity = new System.Workflow.Activities.IfElseActivity();
            this.EmailFieldCheckActivity = new System.Workflow.Activities.CodeActivity();
            this.PositionFieldCheckActivity = new System.Workflow.Activities.CodeActivity();
            this.RequiredFieldCheckActivity = new System.Workflow.Activities.CodeActivity();
            this.faultHandlerActivity1 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.sequenceActivity1 = new System.Workflow.Activities.SequenceActivity();
            this.sequenceActivity2 = new System.Workflow.Activities.SequenceActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.StatisticsActivity = new System.Workflow.Activities.CodeActivity();
            this.HasRecordActivity = new System.Workflow.Activities.WhileActivity();
            this.CheckActivity = new System.Workflow.Activities.WhileActivity();
            // 
            // AddPAActivity
            // 
            this.AddPAActivity.Name = "AddPAActivity";
            this.AddPAActivity.ExecuteCode += new System.EventHandler(this.AddApplicantPAActivity_ExecuteCode);
            // 
            // ExistedPAActivity
            // 
            this.ExistedPAActivity.Name = "ExistedPAActivity";
            this.ExistedPAActivity.ExecuteCode += new System.EventHandler(this.EmptyActivity_ExecuteCode);
            // 
            // ExistedApplicantActivity
            // 
            this.ExistedApplicantActivity.Description = "Get the applicant ID";
            this.ExistedApplicantActivity.Name = "ExistedApplicantActivity";
            this.ExistedApplicantActivity.ExecuteCode += new System.EventHandler(this.ExistedApplicantActivity_ExecuteCode);
            // 
            // AddApplicantActivity
            // 
            this.AddApplicantActivity.Name = "AddApplicantActivity";
            this.AddApplicantActivity.ExecuteCode += new System.EventHandler(this.AddApplicantActivity_ExecuteCode);
            // 
            // ifElseBranchActivity4
            // 
            this.ifElseBranchActivity4.Activities.Add(this.AddPAActivity);
            this.ifElseBranchActivity4.Name = "ifElseBranchActivity4";
            // 
            // ifElseBranchActivity3
            // 
            this.ifElseBranchActivity3.Activities.Add(this.ExistedPAActivity);
            ruleconditionreference1.ConditionName = "Cond_HasPA";
            this.ifElseBranchActivity3.Condition = ruleconditionreference1;
            this.ifElseBranchActivity3.Name = "ifElseBranchActivity3";
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Activities.Add(this.ExistedApplicantActivity);
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // ifElseBranchActivity1
            // 
            this.ifElseBranchActivity1.Activities.Add(this.AddApplicantActivity);
            ruleconditionreference2.ConditionName = "Cond_IsApplicantMatched";
            this.ifElseBranchActivity1.Condition = ruleconditionreference2;
            this.ifElseBranchActivity1.Name = "ifElseBranchActivity1";
            // 
            // FailedActivity
            // 
            this.FailedActivity.Name = "FailedActivity";
            this.FailedActivity.ExecuteCode += new System.EventHandler(this.FailedActivity_ExecuteCode);
            // 
            // UpdateEnumeratorActivity
            // 
            this.UpdateEnumeratorActivity.Name = "UpdateEnumeratorActivity";
            this.UpdateEnumeratorActivity.ExecuteCode += new System.EventHandler(this.UpdateEnumeratorActivity_ExecuteCode);
            // 
            // LogActivity
            // 
            this.LogActivity.Name = "LogActivity";
            this.LogActivity.ExecuteCode += new System.EventHandler(this.LogActivity_ExecuteCode);
            // 
            // JudgeHasPAActivity
            // 
            this.JudgeHasPAActivity.Activities.Add(this.ifElseBranchActivity3);
            this.JudgeHasPAActivity.Activities.Add(this.ifElseBranchActivity4);
            this.JudgeHasPAActivity.Name = "JudgeHasPAActivity";
            // 
            // IsApplicantExistActivity
            // 
            this.IsApplicantExistActivity.Activities.Add(this.ifElseBranchActivity1);
            this.IsApplicantExistActivity.Activities.Add(this.ifElseBranchActivity2);
            this.IsApplicantExistActivity.Name = "IsApplicantExistActivity";
            // 
            // EmailFieldCheckActivity
            // 
            this.EmailFieldCheckActivity.Name = "EmailFieldCheckActivity";
            this.EmailFieldCheckActivity.ExecuteCode += new System.EventHandler(this.EmailFieldCheckActivity_ExecuteCode);
            // 
            // PositionFieldCheckActivity
            // 
            this.PositionFieldCheckActivity.Name = "PositionFieldCheckActivity";
            this.PositionFieldCheckActivity.ExecuteCode += new System.EventHandler(this.PositionFieldCheckActivity_ExecuteCode);
            // 
            // RequiredFieldCheckActivity
            // 
            this.RequiredFieldCheckActivity.Name = "RequiredFieldCheckActivity";
            this.RequiredFieldCheckActivity.ExecuteCode += new System.EventHandler(this.RequiredFieldCheckActivity_ExecuteCode);
            // 
            // faultHandlerActivity1
            // 
            this.faultHandlerActivity1.Activities.Add(this.FailedActivity);
            this.faultHandlerActivity1.FaultType = typeof(System.Exception);
            this.faultHandlerActivity1.Name = "faultHandlerActivity1";
            // 
            // sequenceActivity1
            // 
            this.sequenceActivity1.Activities.Add(this.IsApplicantExistActivity);
            this.sequenceActivity1.Activities.Add(this.JudgeHasPAActivity);
            this.sequenceActivity1.Activities.Add(this.LogActivity);
            this.sequenceActivity1.Activities.Add(this.UpdateEnumeratorActivity);
            this.sequenceActivity1.Name = "sequenceActivity1";
            // 
            // sequenceActivity2
            // 
            this.sequenceActivity2.Activities.Add(this.RequiredFieldCheckActivity);
            this.sequenceActivity2.Activities.Add(this.PositionFieldCheckActivity);
            this.sequenceActivity2.Activities.Add(this.EmailFieldCheckActivity);
            this.sequenceActivity2.Name = "sequenceActivity2";
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerActivity1);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // StatisticsActivity
            // 
            this.StatisticsActivity.Name = "StatisticsActivity";
            this.StatisticsActivity.ExecuteCode += new System.EventHandler(this.StatisticsActivity_ExecuteCode);
            // 
            // HasRecordActivity
            // 
            this.HasRecordActivity.Activities.Add(this.sequenceActivity1);
            ruleconditionreference3.ConditionName = "Cond_IsCompleted";
            this.HasRecordActivity.Condition = ruleconditionreference3;
            this.HasRecordActivity.Name = "HasRecordActivity";
            // 
            // CheckActivity
            // 
            this.CheckActivity.Activities.Add(this.sequenceActivity2);
            ruleconditionreference4.ConditionName = "HasRecord";
            this.CheckActivity.Condition = ruleconditionreference4;
            this.CheckActivity.Name = "CheckActivity";
            // 
            // IntegrationWorkflow
            // 
            this.Activities.Add(this.CheckActivity);
            this.Activities.Add(this.HasRecordActivity);
            this.Activities.Add(this.StatisticsActivity);
            this.Activities.Add(this.faultHandlersActivity1);
            this.Name = "IntegrationWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private WhileActivity CheckActivity;
        private CodeActivity EmailFieldCheckActivity;
        private CodeActivity PositionFieldCheckActivity;
        private CodeActivity RequiredFieldCheckActivity;
        private SequenceActivity sequenceActivity2;
        private CodeActivity UpdateEnumeratorActivity;
        private CodeActivity StatisticsActivity;
        private CodeActivity ExistedPAActivity;
        private CodeActivity ExistedApplicantActivity;
        private IfElseBranchActivity ifElseBranchActivity4;
        private IfElseBranchActivity ifElseBranchActivity3;
        private IfElseActivity JudgeHasPAActivity;
        private IfElseBranchActivity ifElseBranchActivity2;
        private IfElseBranchActivity ifElseBranchActivity1;
        private IfElseActivity IsApplicantExistActivity;
        private CodeActivity AddApplicantActivity;
        private CodeActivity AddPAActivity;
        private SequenceActivity sequenceActivity1;
        private WhileActivity HasRecordActivity;
        private FaultHandlersActivity faultHandlersActivity1;
        private FaultHandlerActivity faultHandlerActivity1;
        private CodeActivity FailedActivity;
        private CodeActivity LogActivity;















































































    }
}
