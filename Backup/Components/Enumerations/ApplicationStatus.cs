namespace MSRA.SpringField.Components.Enumerations
{
    public enum ApplicationStatusEnum
    {
        //When Cadt has got his/her passport
        //His/her application is un-finished.
        //ApplicationStarted,//0
        //WaitforAdvisorsAppraisal,//1
        //ApplicationComplete,//2
        //InterviewProgressing,//3
        //Hired,//4
        //Rejected,//5
        //Pending,//6
        //OfferDeclined,//7
        //PersonalReferring,//8
        //URReferring,//9
        //KeyReferring//10
        ApplicationIncomplete,//0
        WaitforAdvisorsAppraisal,//1
        Available,//2
        InterviewinProcess,//3
        Hired,//4
        Rejected,//5
        OfferDeclined,//6
        KeyReferring,//7
        OnBoard,//8
        /*
         * Add by Yuanqin, 2011.5.5
         * Add new status
         */ 
        QualifiedButNotMatched//9
    }
}