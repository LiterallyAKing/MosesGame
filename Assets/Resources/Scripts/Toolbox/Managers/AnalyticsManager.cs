using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour {



	public void LogEvent(string eventName, Object data){
		//Analytics.CustomEvent("loaded_Gameplay", new Dictionary<string, object> {"loaded_Gameplay",1});
	}
}
