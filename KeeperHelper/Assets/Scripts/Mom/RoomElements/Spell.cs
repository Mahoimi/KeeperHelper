using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    [CreateAssetMenu(menuName = "Keeper Helper/Room Element/Spell")]
    public class Spell : RoomElement
    {
        public Spell()
        {
            m_roomElementType = RoomElementType.Spell;
        }
    }
}