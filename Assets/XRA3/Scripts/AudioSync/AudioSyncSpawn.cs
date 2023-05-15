using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioSyncSpawn : AudioSyncer
{
	public UnityEvent onBeatEvent;
	public override void OnBeat()
	{
		base.OnBeat();

		// hint: do this if the game isn't over
		onBeatEvent.Invoke();
			
	}
}
