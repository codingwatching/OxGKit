﻿using UnityEditor;
using UnityEngine;

namespace OxGKit.LoggingSystem.Editor
{
    [CustomEditor(typeof(LoggingLauncher))]
    public class LoggingLauncherEditor : UnityEditor.Editor
    {
        private LoggingLauncher _target = null;
        private UnityEditor.Editor _editor;
        private bool _isDirty = false;

        public override void OnInspectorGUI()
        {
            this._target = (LoggingLauncher)target;

            base.OnInspectorGUI();

            // Draw logging setting view
            this.DrawLoggingSettingView();
        }

        protected void DrawLoggingSettingView()
        {
            serializedObject.Update();

            var setting = this._target.loggerSetting;

            // Draw Runtime reload setting button
            if (setting != null)
            {
                EditorGUILayout.Space(20);
                this.DrawLine(new Color32(0, 255, 168, 255));

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                // Reload button
                Color bc = GUI.backgroundColor;
                GUI.backgroundColor = new Color32(102, 255, 153, 255);
                EditorGUI.BeginDisabledGroup(!this._isDirty);
                if (GUILayout.Button("Runtime Reload Setting", GUILayout.MaxWidth(225f)))
                {
                    LoggingLauncher.TryLoadLoggerSetting();
                    this._isDirty = false;
                }
                GUI.backgroundColor = bc;
                EditorGUI.EndDisabledGroup();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                this.DrawLine(new Color32(0, 255, 222, 255));
                EditorGUILayout.Space(20);
            }

            // Draw setting on inspector
            EditorGUI.BeginChangeCheck();
            if (setting != null)
            {
                // Create setting view on inspector
                if (this._editor == null) this._editor = CreateEditor(setting);
                this._editor.OnInspectorGUI();
            }

            if (EditorGUI.EndChangeCheck())
            {
                if (Application.isPlaying) this._isDirty = true;
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(setting);
                AssetDatabase.SaveAssets();
            }

            // Draw select all and deselect all buttons
            if (setting != null)
            {
                EditorGUILayout.Space(2.5f);

                // Select all button
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                Color bc = GUI.backgroundColor;
                GUI.backgroundColor = new Color32(164, 227, 255, 255);
                if (GUILayout.Button("Select All", GUILayout.MaxWidth(150f)))
                {
                    foreach (var logger in this._target.loggerSetting.loggerConfigs)
                    {
                        logger.logActive = true;
                    }
                    if (Application.isPlaying) this._isDirty = true;
                    EditorUtility.SetDirty(setting);
                    AssetDatabase.SaveAssets();
                }
                GUI.backgroundColor = bc;
                // Deselect all button
                bc = GUI.backgroundColor;
                GUI.backgroundColor = new Color32(164, 227, 255, 255);
                if (GUILayout.Button("Deselect All", GUILayout.MaxWidth(150f)))
                {
                    foreach (var logger in this._target.loggerSetting.loggerConfigs)
                    {
                        logger.logActive = false;
                    }
                    if (Application.isPlaying) this._isDirty = true;
                    EditorUtility.SetDirty(setting);
                    AssetDatabase.SaveAssets();
                }
                GUI.backgroundColor = bc;
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
        }

        protected void DrawLine(Color color, int thickness = 1, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }
    }
}
