namespace MSRA.SpringField.Components.Enumerations
{
    public enum MailType
    {
        //hire
        //no link
        HireApplicant,  //0
        //reject
        //no link
        RejectApplicant, //1

        //application complete
        //no link
        ApplicationComplete, //2 should be deleted.

        //ask for group manager approval(to group manager)
        //link: "GMApproval.aspx?interview="
        AskForApproval, //3

        //arrange a interview(to interviewer)
        //link: "InterviewFeedback.aspx?feedback="
        ArrangeInterview, //4

        //change interviewer
        //link: "InterviewerChangeApproval.aspx?feedback="
        InterviewerChange, //5

        HireReferral, //6
        RejectReferral, //7

        //feedback complete(to hiring manager)
        //link: "InterviewDetail.aspx?interview="
        FeedbackComplete, //8

        //approval complete(to hiring manager)
        //link: "InterviewDetail.aspx?interview="
        ApprovalComplete, //9 should check

        RejectMailToHM, // 10 

        ReferTo, //11 //Referral Request Email Alias// should be instead.

        // MIATS send email to applicant to tell him to register 
        RequestRegister, // 12

        Interviewreminder, //13
        OnBoardReminder = 15,
        PAReminder = 16,//send to mentor to notice them to Submit PA after intern finished thier PA
        PANotice = 17, //Send it when the mentor submitted the PA result for his/her intern. (Or send it when Person close the PE process, and change the student¡¯s status to ¡°available¡± or ¡°on-board¡±)
        DailyPAReport = 18,//Daily Intern Performance Assessment Report to MSRA Intern Support

        /*
         * Send when the pa approval was rejected
         * Modified By: Yin.P
         */
        PAApprovalRejected = 19,

        /*
         * Send PA Incomplete Remind when intern finish and mentor doesn't finish
         * Add by Yuanqin, 2011.5.27
         */
        PAIncompleteRemind = 20,

        /*
         * Send CheckoutSurvey report when intern finish checkout survey
         * Add by Yuanqin, 2011.6.2
         */
        CheckoutSurvey = 21,
        /*
         * Send Notice email to the applicant out
         * Add by bin, 2011.12.30
         */
        NoticeApplicant = 22,

        /*
         * Send CheckOutReminder email to the applicant out
         * Add by LMM, 2012.11.23
         */
        CheckOutReminder = 23

    }
}
