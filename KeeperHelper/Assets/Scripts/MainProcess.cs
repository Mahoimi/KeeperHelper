using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MainProcess : SingletonMonoBehaviour<MainProcess>
{
    #region Monobehaviour
    public override void Awake() {
        Debug.Log("Hello world !");
	}
	
	void Update () {
		
	}
    #endregion
}
