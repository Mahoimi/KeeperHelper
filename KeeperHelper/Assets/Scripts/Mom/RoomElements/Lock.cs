using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    [CreateAssetMenu(menuName = "Keeper Helper/Room Element/Lock")]
    public class Lock : RoomElement
    {
        public Lock()
        {
            m_roomElementType = RoomElementType.Lock;
        }
    }
}