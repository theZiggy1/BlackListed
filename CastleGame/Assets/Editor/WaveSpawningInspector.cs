using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework.Internal.Filters;

[CustomEditor (typeof(WaveSpawning))]
public class WaveSpawningInspector : Editor
{
    
    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        WaveSpawning spawner = (WaveSpawning)base.target;
        EditorGUILayout.LabelField("Current Waves");
        foreach (Wave item in spawner.waveScripts)
        {
            EditorGUILayout.LabelField(item.WaveInfo);
        }

        base.DrawDefaultInspector();
    }

}
