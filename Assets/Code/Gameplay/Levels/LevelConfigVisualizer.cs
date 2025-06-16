using Code.Gameplay.Towers;
using UnityEditor;
using UnityEngine;

namespace Code.Gameplay.Levels
{
#if UNITY_EDITOR
    public class LevelConfigVisualizer : EditorWindow
    {
        private LevelConfig selectedLevelConfig;
        private GameObject visualizationRoot;
        private Material cubeMaterial;
        private const float CUBE_SCALE = 0.1f;
        private const string VISUALIZATION_ROOT_NAME = "[Level Visualization]";
        
        [MenuItem("Tools/Level System/Level Config Visualizer")]
        public static void ShowWindow()
        {
            GetWindow<LevelConfigVisualizer>("Level Config Visualizer");
        }

        private void OnGUI()
        {
            GUILayout.Label("Level Config Visualizer", EditorStyles.boldLabel);
            
            EditorGUI.BeginChangeCheck();
            selectedLevelConfig = (LevelConfig)EditorGUILayout.ObjectField(
                "Level Config", 
                selectedLevelConfig, 
                typeof(LevelConfig), 
                false
            );
            
            if (EditorGUI.EndChangeCheck() && selectedLevelConfig != null)
            {
                ClearVisualization();
            }
            
            EditorGUILayout.Space();
            
            if (selectedLevelConfig == null)
            {
                EditorGUILayout.HelpBox("Please select a Level Config asset to visualize.", MessageType.Info);
                return;
            }
            
            EditorGUILayout.LabelField($"Objects Count: {selectedLevelConfig.levelObjects.Count}");
            
            EditorGUILayout.Space();
            
            cubeMaterial = (Material)EditorGUILayout.ObjectField(
                "Cube Material", 
                cubeMaterial, 
                typeof(Material), 
                false
            );
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Visualize Level", GUILayout.Height(30)))
            {
                VisualizeLevel();
            }
            
            if (GUILayout.Button("Clear Visualization", GUILayout.Height(30)))
            {
                ClearVisualization();
            }
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Update from Scene Markers", GUILayout.Height(30)))
            {
                UpdateConfigFromSceneMarkers();
            }
        }

        private void VisualizeLevel()
        {
            if (selectedLevelConfig == null) return;
            
            ClearVisualization();
            
            // Create root object for visualization
            visualizationRoot = new GameObject(VISUALIZATION_ROOT_NAME);
            
            // Create cubes for each level object
            for (int i = 0; i < selectedLevelConfig.levelObjects.Count; i++)
            {
                var levelObjectData = selectedLevelConfig.levelObjects[i];
                CreateVisualizationCube(levelObjectData, i);
            }
            
            // Focus on created objects
            if (visualizationRoot.transform.childCount > 0)
            {
                Selection.activeGameObject = visualizationRoot;
                SceneView.lastActiveSceneView?.FrameSelected();
            }
        }

        private void CreateVisualizationCube(LevelObjectData data, int index)
        {
            // Create cube
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = $"LevelObject_{index}_[{data.towerFraction}_{data.towerType}]";
            cube.transform.SetParent(visualizationRoot.transform);
            
            // Set position and scale
            cube.transform.position = data.position;
            cube.transform.localScale = Vector3.one * CUBE_SCALE;
            
            // Add marker component
            var marker = cube.AddComponent<LevelObjectMarker>();
            marker.towerFractionsEnum = data.towerFraction;
            marker.towerType = data.towerType;
            marker.initialTowerScore = data.initialTowerScore;
            
            // Apply material if set
            if (cubeMaterial != null)
            {
                var renderer = cube.GetComponent<Renderer>();
                renderer.sharedMaterial = cubeMaterial;
            }
            
            // Color based on fraction (optional)
            ApplyFractionColor(cube, data.towerFraction);
        }

        private void ApplyFractionColor(GameObject cube, TowerFractionsEnum fraction)
        {
            var renderer = cube.GetComponent<Renderer>();
            if (renderer == null || cubeMaterial != null) return;
            
            // Create temporary material with color based on fraction
            Material tempMaterial = new Material(Shader.Find("Standard"));
            
            // You can customize colors based on your fractions
            Color color = fraction switch
            {
                TowerFractionsEnum.Blue => Color.blue,
                TowerFractionsEnum.Red => Color.red,
                TowerFractionsEnum.Neutral => Color.gray,
                _ => Color.white
            };
            
            tempMaterial.color = color;
            renderer.sharedMaterial = tempMaterial;
        }

        private void ClearVisualization()
        {
            if (visualizationRoot != null)
            {
                DestroyImmediate(visualizationRoot);
            }
            
            // Also find and remove any orphaned visualization roots
            GameObject[] roots = FindObjectsOfType<GameObject>();
            foreach (var root in roots)
            {
                if (root.name == VISUALIZATION_ROOT_NAME)
                {
                    DestroyImmediate(root);
                }
            }
        }

        private void UpdateConfigFromSceneMarkers()
        {
            if (selectedLevelConfig == null)
            {
                EditorUtility.DisplayDialog("Error", "Please select a Level Config first.", "OK");
                return;
            }
            
            // Find all markers in scene
            LevelObjectMarker[] markers = FindObjectsOfType<LevelObjectMarker>();
            
            if (markers.Length == 0)
            {
                EditorUtility.DisplayDialog("Info", "No Level Object Markers found in the scene.", "OK");
                return;
            }
            
            // Confirm action
            if (!EditorUtility.DisplayDialog(
                    "Update Level Config", 
                    $"This will replace all {selectedLevelConfig.levelObjects.Count} objects in the config with {markers.Length} markers from the scene. Continue?", 
                    "Yes", 
                    "Cancel"))
            {
                return;
            }
            
            // Update config
            Undo.RecordObject(selectedLevelConfig, "Update Level Config from Scene");
            
            selectedLevelConfig.levelObjects.Clear();
            
            foreach (var marker in markers)
            {
                var data = new LevelObjectData
                {
                    position = marker.transform.position,
                    towerFraction = marker.towerFractionsEnum,
                    towerType = marker.towerType,
                    initialTowerScore = marker.initialTowerScore
                };
                
                selectedLevelConfig.levelObjects.Add(data);
            }
            
            EditorUtility.SetDirty(selectedLevelConfig);
            AssetDatabase.SaveAssets();
            
            Debug.Log($"Updated Level Config with {markers.Length} objects from scene.");
        }

        private void OnDestroy()
        {
            ClearVisualization();
        }
    }
    
    // Custom Property Drawer for LevelObjectData (optional enhancement)
    [CustomPropertyDrawer(typeof(LevelObjectData))]
    public class LevelObjectDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            // Calculate rects
            float lineHeight = EditorGUIUtility.singleLineHeight;
            float spacing = EditorGUIUtility.standardVerticalSpacing;
            
            Rect foldoutRect = new Rect(position.x, position.y, position.width, lineHeight);
            
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);
            
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                
                float y = position.y + lineHeight + spacing;
                
                // Position
                Rect positionRect = new Rect(position.x, y, position.width, lineHeight);
                EditorGUI.PropertyField(positionRect, property.FindPropertyRelative("position"));
                y += lineHeight + spacing;
                
                // Tower Fraction
                Rect fractionRect = new Rect(position.x, y, position.width, lineHeight);
                EditorGUI.PropertyField(fractionRect, property.FindPropertyRelative("towerFraction"));
                y += lineHeight + spacing;
                
                // Tower Type
                Rect typeRect = new Rect(position.x, y, position.width, lineHeight);
                EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("towerType"));
                y += lineHeight + spacing;
                
                // Initial Score
                Rect scoreRect = new Rect(position.x, y, position.width, lineHeight);
                EditorGUI.PropertyField(scoreRect, property.FindPropertyRelative("initialTowerScore"));
                
                EditorGUI.indentLevel--;
            }
            
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;
            
            if (property.isExpanded)
            {
                height += (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 4;
            }
            
            return height;
        }
    }
#endif
}