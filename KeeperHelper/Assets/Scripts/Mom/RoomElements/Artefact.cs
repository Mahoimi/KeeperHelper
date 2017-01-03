using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    [CreateAssetMenu(menuName = "Keeper Helper/Room Element/Artefact")]
    public class Artefact : RoomElement
    {
        public Artefact()
        {
            m_roomElementType = RoomElementType.Artefact;
        }
    }
}