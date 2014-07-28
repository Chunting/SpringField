namespace MSRA.SpringField.Components.Enumerations
{
    public enum InterviewStatusEnum
    {
        WaitingForInterviewFeedback,//0
        WaitingForMentorDecision,//1
        WaitingForGroupManagerDecision,//2
        Hired,//3
        Rejected,//4
        OfferDeclined,//5
        Default,//6
        /*
         * Add by Yuanqin, 2011.5.5
         * Add new status
         */  
        QualifiedButNotMatched//7
    }
}