using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    public enum RoomElementType
    {
        Item,
        Weapon,
        Spell,
        Artefact,
        Clue,
        Obstacle,
        Lock,

        Count
    }

    [System.Serializable]
    public abstract class RoomElement : ScriptableObject
    {
        public string NameLocId = GlobalVariables.UnknownLocId;

        protected RoomElementType m_roomElementType = RoomElementType.Item;
    }
}