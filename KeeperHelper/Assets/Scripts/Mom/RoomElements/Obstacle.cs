using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    [CreateAssetMenu(menuName = "Keeper Helper/Room Element/Obstacle")]
    public class Obstacle : RoomElement
    {
        public Obstacle()
        {
            m_roomElementType = RoomElementType.Obstacle;
        }
    }
}