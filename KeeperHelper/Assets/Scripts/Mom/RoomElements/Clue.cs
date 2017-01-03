using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    [CreateAssetMenu(menuName = "Keeper Helper/Room Element/Clue")]
    public class Clue : RoomElement
    {
        public Clue()
        {
            m_roomElementType = RoomElementType.Clue;
        }
    }
}