﻿using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace Code.Gameplay.Levels
{
#if UNITY_EDITOR
    public class LevelConfigGenerator : EditorWindow 
    {
        private string levelName = "NewLevel";
        private string savePath = "Assets/Resources/Configs/Levels/";
        private List<LevelObjectMarker> foundMarkers = new List<LevelObjectMarker>();

        [MenuItem("Tools/Level System/Level Config Generator")]
        public static void ShowWindow()
        {
            GetWindow<LevelConfigGenerator>("Level Config Generator");
        }

        void OnGUI()
        {
            GUILayout.Label("Level Config Generator", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            levelName = EditorGUILayout.TextField("Level Name:", levelName);
            savePath = EditorGUILayout.TextField("Save Path:", savePath);
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Scan Scene", GUILayout.Height(30))) 
                ScanScene();
            
            if (foundMarkers.Count > 0)
            {
                GUILayout.Label($"Found {foundMarkers.Count} objects");
                
                if (GUILayout.Button("Create Config", GUILayout.Height(30))) 
                    CreateConfig();
            }
        }

        private void ScanScene()
        {
            foundMarkers.Clear();
            foundMarkers.AddRange(FindObjectsOfType<LevelObjectMarker>());
            Repaint();
        }

        private void CreateConfig()
        {
            // Сначала проверяем валидность данных
            if (string.IsNullOrEmpty(levelName))
            {
                EditorUtility.DisplayDialog("Error", "Level name cannot be empty!", "OK");
                return;
            }
            
            if (foundMarkers.Count == 0)
            {
                EditorUtility.DisplayDialog("Error", "No markers found! Please scan scene first.", "OK");
                return;
            }
            
            // Создаем папку если не существует
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
                AssetDatabase.Refresh(); // Важно для обновления базы данных
            }
            
            // Генерируем уникальный путь
            string fileName = $"{levelName}.asset";
            string fullPath = Path.Combine(savePath, fileName);
            fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);
            
            // Создаем ScriptableObject
            LevelConfig config = CreateInstance<LevelConfig>();
            
            // Заполняем данными
            foreach (var marker in foundMarkers)
            {
                if (marker == null) continue;
                
                LevelObjectData objectData = new LevelObjectData();
                objectData.position = marker.transform.position;
                objectData.position.y = 0;
                objectData.towerFraction = marker.towerFractionsEnum;
                objectData.towerType = marker.towerType;
                objectData.initialTowerScore = marker.initialTowerScore;
                
                config.levelObjects.Add(objectData);
            }
            
            try
            {
                // Создаем ассет
                AssetDatabase.CreateAsset(config, fullPath);
                
                // КРИТИЧЕСКИ ВАЖНО: помечаем как dirty и сохраняем
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                // Дополнительная проверка что файл создался
                if (AssetDatabase.LoadAssetAtPath<LevelConfig>(fullPath) != null)
                {
                    EditorUtility.DisplayDialog("Success", $"Config created: {fullPath}", "OK");
                    EditorGUIUtility.PingObject(config);
                    Selection.activeObject = config;
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "Failed to create config file!", "OK");
                }
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("Error", $"Failed to create config: {e.Message}", "OK");
                Debug.LogError($"Error creating config: {e}");
                
                // Очищаем созданный объект в случае ошибки
                if (config != null)
                {
                    DestroyImmediate(config);
                }
            }
        }
    }
#endif
}