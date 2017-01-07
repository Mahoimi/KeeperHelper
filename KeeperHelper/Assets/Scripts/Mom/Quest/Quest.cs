using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace KeeperHelper
{
    public enum GameExtension
    {
        Base,
        ForbiddenAlchemy,
        CallOfTheWild,
        Custom,

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

        public static Quest[] GetAllQuests()
        {
            Quest[] quests = Resources.LoadAll<Quest>(GlobalVariables.ScriptableObjectsRoot);
            Assert.IsTrue(quests.Length > 0, string.Format("No quests found at location {0}", GlobalVariables.ScriptableObjectsPath));
            return quests;
        }
    }
}