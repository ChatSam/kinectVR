using UnityEngine;
using System.Collections;
using trinus;

namespace trinus{
	public class TrinusNotStreaming : MonoBehaviour {
		TrinusProcessor trinusProcessor;

		[Tooltip("This script will be enabled if Trinus is not streaming (and disabled otherwise). You can use it as a failover to activate a MouseLook script, for example")]
		public MonoBehaviour nonStreamingScript;

		void Awake(){
			trinusProcessor = GameObject.Find ("TrinusManager").GetComponent<TrinusProcessor> ();
		}

		void Update () {
			if (trinusProcessor != null && nonStreamingScript != null) {
				if (nonStreamingScript.enabled && trinusProcessor.isStreaming ())
					nonStreamingScript.enabled = false;
				else
					if (!nonStreamingScript.enabled && !trinusProcessor.isStreaming())
						nonStreamingScript.enabled = true;
			}
		}
	}
}
