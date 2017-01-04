using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    public enum GameExtension
    {
        Base,
        ForbiddenAlchemy,
        CallOfTheWild,

        Count
    }

    [CreateAssetMenu(menuName = "Keeper Helper/Quest")]
    public class Quest : ScriptableObject
    {
        public string NameLocId = GlobalVariables.UnknownLocId;
        public GameExtension GameExtension = GameExtension.Base;
        public uint QuestNumber = 0;
        public RoomDescription[] Rooms = null;
        public QuestHistoryQuestion[] Questions = null;
    }
}