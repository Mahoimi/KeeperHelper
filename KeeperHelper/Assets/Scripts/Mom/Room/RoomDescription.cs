using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    [System.Serializable]
    public class RoomDescription
    {
        public string RoomLocId = GlobalVariables.UnknownLocId;
        public RoomVariation[] RoomVariations = null;
    }
}