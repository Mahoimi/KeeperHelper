using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    [CreateAssetMenu(menuName = "Keeper Helper/Room Element/Item")]
    public class Item : RoomElement
    {
        public Item()
        {
            m_roomElementType = RoomElementType.Item;
        }
    }
}