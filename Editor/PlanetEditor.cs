using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    // Planet Editor 

    Planet planet;
    Editor shapeEditor;

    public override void OnInspectorGUI()
    {
        // check if stuff changed so update scene
        using(var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.GeneratePlanet();
            }
        }
        if (GUILayout.Button("Generate Planet"))
        {
            planet.GeneratePlanet();
        }

        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdate, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            // Inspetortitbar is a separator, to separate the settings from the rest of the inspector, foldout is the var if show or not
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using(var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
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
        }
        
    }

    private void OnEnable() 
    {
        planet = (Planet)target;
    }
}
