using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework.Internal.Filters;
/******************
 * Anton Ziegler s1907905
 * ****************/

[CustomEditor (typeof(WaveSpawning))]
public class WaveSpawningInspector : Editor
{
    //This edits the inspector gui, to take the string information so that waves can be easily checked, so we know which wave is in which order on a room. 
    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        WaveSpawning spawner = (WaveSpawning)base.target;
        EditorGUILayout.LabelField("Current Waves");
        foreach (Wave item in spawner.waveScripts)
        {
            if (item != null) { EditorGUILayout.LabelField(item.WaveInfo); }
        }

        base.DrawDefaultInspector();
    }

}
