using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    public static class GlobalVariables
    {
        #region Default values
        public const string UnknownGUID = "UnknownGUID";
        public const string UnknownLocId = "UnknownLocId";
        #endregion

        #region Paths
        public const string ScriptableObjectsRoot = "ScriptableObjects";
        public static Dictionary<GameExtension, string> ScriptableObjectsPath = new Dictionary<GameExtension, string>(){
            { GameExtension.Base,               ScriptableObjectsRoot + "/00_Base" },
            { GameExtension.ForbiddenAlchemy,   ScriptableObjectsRoot + "/01_ForbiddenAlchemy" },
            { GameExtension.CallOfTheWild,      ScriptableObjectsRoot + "/02_CallOfTheWild" },
            { GameExtension.Custom,             ScriptableObjectsRoot + "/03_Custom" },
        };
        #endregion
    }
}