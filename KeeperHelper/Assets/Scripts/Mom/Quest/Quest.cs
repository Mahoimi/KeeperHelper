using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    public enum MomExtension
    {
        Base,
        Forest,
        Experiences,

        Count
    }

    [CreateAssetMenu(menuName = "Keeper Helper/Quest")]
    public class Quest : ScriptableObject
    {
        public string NameLocId = GlobalVariables.UnknownLocId;
        public MomExtension MomExension = MomExtension.Base;
        public RoomDescription[] Rooms = null;
        public QuestHistoryQuestion[] Questions = null;
    }
}