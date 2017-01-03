using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    [System.Serializable]
    public class RoomVariation
    {
        public HistoryMarker HistoryMarker = HistoryMarker.One_A;
        public RoomElement[] RoomElements = null;
    }
}