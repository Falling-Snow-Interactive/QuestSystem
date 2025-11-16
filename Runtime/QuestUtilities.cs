using System;

namespace Fsi.QuestSystem
{
    public static class QuestUtilities
    {
        // public static QuestID StringToQuestID(string s)
        // {
        //     s = s.ToLower();
        //     return s switch
        //     {
        //         "quest_00" => QuestID.Quest_00,
        //         _ => QuestID.None,
        //     };
        // }

        public static QuestStatus StringToStatus(string s)
        {
            s = s.ToLower();
            return s switch
            {
                "none" => QuestStatus.None,
                "active" => QuestStatus.Active,
                "complete" => QuestStatus.Complete,
                "failed" => QuestStatus.Failed,
                "abandoned" => QuestStatus.Abandoned,
                _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
            };
        }
    }
}