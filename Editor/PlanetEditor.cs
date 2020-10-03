﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    // Planet Editor 

    Planet planet;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdate)
    {
        using(var check = new EditorGUI.ChangeCheckScope())
        {
            Editor editor = CreateEditor(settings);
            editor.OnInspectorGUI();

            if (check.changed)
            {
                if (onSettingsUpdate != null)
                {
                    onSettingsUpdate();
                }
            }
        }
        
    }

    private void OnEnable() 
    {
        planet = (Planet)target;
    }
}
