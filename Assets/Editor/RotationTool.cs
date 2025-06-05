using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RotationTool : EditorWindow
{
    private Vector3 rotationEuler = Vector3.zero;
    private Quaternion rotationQuaternion = Quaternion.identity;
    private bool useEulerAngles = true;
    
    // Словарь для хранения начальных позиций и поворотов объектов
    private Dictionary<Transform, TransformData> originalTransforms = new Dictionary<Transform, TransformData>();
    
    // Ключи для EditorPrefs (постоянное хранение)
    private const string PREFS_KEY_COUNT = "RotationTool_TransformCount";
    private const string PREFS_KEY_TRANSFORM = "RotationTool_Transform_";
    private const string PREFS_KEY_POSITION = "RotationTool_Position_";
    private const string PREFS_KEY_ROTATION = "RotationTool_Rotation_";
    
    [System.Serializable]
    private struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        
        public TransformData(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
    
    [MenuItem("Tools/Rotation Tool")]
    public static void ShowWindow()
    {
        GetWindow<RotationTool>("Rotation Tool");
    }
    
    private void OnEnable()
    {
        // Загружаем сохранённые состояния при открытии окна
        LoadRememberedStates();
    }
    
    private void OnDisable()
    {
        // Сохраняем состояния при закрытии окна
        SaveRememberedStates();
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Rotation Tool", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();
        
        // Переключатель между углами Эйлера и кватернионом
        useEulerAngles = EditorGUILayout.Toggle("Use Euler Angles", useEulerAngles);
        
        EditorGUILayout.Space();
        
        if (useEulerAngles)
        {
            // Ввод углов Эйлера
            rotationEuler = EditorGUILayout.Vector3Field("Rotation (Euler)", rotationEuler);
            rotationQuaternion = Quaternion.Euler(rotationEuler);
        }
        else
        {
            // Ввод кватерниона напрямую
            Vector4 quatVector = new Vector4(rotationQuaternion.x, rotationQuaternion.y, rotationQuaternion.z, rotationQuaternion.w);
            quatVector = EditorGUILayout.Vector4Field("Rotation (Quaternion)", quatVector);
            rotationQuaternion = new Quaternion(quatVector.x, quatVector.y, quatVector.z, quatVector.w);
        }
        
        EditorGUILayout.Space();
        
        // Информация о выделенных объектах
        if (Selection.gameObjects.Length > 0)
        {
            EditorGUILayout.LabelField($"Selected objects: {Selection.gameObjects.Length}");
            EditorGUILayout.LabelField($"Remembered objects: {originalTransforms.Count}");
            
            EditorGUILayout.Space();
            
            // Кнопка сохранения текущего состояния как начального
            if (GUILayout.Button("Remember Current State"))
            {
                RememberCurrentState();
            }
            
            EditorGUILayout.Space();
            
            // Кнопка применения поворота
            if (GUILayout.Button("Apply Rotation"))
            {
                ApplyRotation();
            }
            
            // Кнопка применения поворота относительно начального состояния
            GUI.enabled = originalTransforms.Count > 0;
            if (GUILayout.Button("Apply Rotation from Original"))
            {
                ApplyRotationFromOriginal();
            }
            GUI.enabled = true;
            
            // Кнопка возврата к начальному состоянию
            GUI.enabled = originalTransforms.Count > 0;
            if (GUILayout.Button("Reset to Original State"))
            {
                ResetToOriginalState();
            }
            GUI.enabled = true;
            
            // Кнопка сброса поворота выделенных объектов
            if (GUILayout.Button("Reset Selected Rotation"))
            {
                ResetRotation();
            }
            
            EditorGUILayout.Space();
            
            // Кнопка очистки запомненных состояний
            if (GUILayout.Button("Clear Remembered States"))
            {
                ClearRememberedStates();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Select one or more GameObjects to apply rotation", MessageType.Info);
            if (originalTransforms.Count > 0)
            {
                EditorGUILayout.LabelField($"Remembered objects: {originalTransforms.Count}");
            }
        }
        
        EditorGUILayout.Space();
        
        // Быстрые кнопки для часто используемых поворотов
        GUILayout.Label("Quick Rotations:", EditorStyles.boldLabel);
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("90° X"))
        {
            rotationQuaternion = Quaternion.Euler(90, 0, 0);
            if (useEulerAngles) rotationEuler = new Vector3(90, 0, 0);
        }
        if (GUILayout.Button("90° Y"))
        {
            rotationQuaternion = Quaternion.Euler(0, 90, 0);
            if (useEulerAngles) rotationEuler = new Vector3(0, 90, 0);
        }
        if (GUILayout.Button("90° Z"))
        {
            rotationQuaternion = Quaternion.Euler(0, 0, 90);
            if (useEulerAngles) rotationEuler = new Vector3(0, 0, 90);
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("180° Y"))
        {
            rotationQuaternion = Quaternion.Euler(0, 180, 0);
            if (useEulerAngles) rotationEuler = new Vector3(0, 180, 0);
        }
        if (GUILayout.Button("Reset"))
        {
            rotationQuaternion = Quaternion.identity;
            if (useEulerAngles) rotationEuler = Vector3.zero;
        }
        GUILayout.EndHorizontal();
    }
    
    private void RememberCurrentState()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Transform t = obj.transform;
            originalTransforms[t] = new TransformData(t.position, t.rotation);
        }
        
        Debug.Log($"Remembered state for {Selection.gameObjects.Length} objects");
    }
    
    private void ApplyRotation()
    {
        // Записываем операцию для Undo
        Undo.RecordObjects(Selection.transforms, "Apply Rotation");
        
        foreach (GameObject obj in Selection.gameObjects)
        {
            // Умножаем текущий поворот объекта на желаемый кватернион
            obj.transform.rotation = obj.transform.rotation * rotationQuaternion;
        }
        
        Debug.Log($"Applied rotation {rotationQuaternion} to {Selection.gameObjects.Length} objects");
    }
    
    private void ApplyRotationFromOriginal()
    {
        // Записываем операцию для Undo
        Undo.RecordObjects(Selection.transforms, "Apply Rotation from Original");
        
        int appliedCount = 0;
        foreach (GameObject obj in Selection.gameObjects)
        {
            Transform t = obj.transform;
            if (originalTransforms.ContainsKey(t))
            {
                // Применяем поворот к начальному состоянию объекта
                t.rotation = originalTransforms[t].rotation * rotationQuaternion;
                appliedCount++;
            }
            else
            {
                Debug.LogWarning($"No original state remembered for {obj.name}");
            }
        }
        
        Debug.Log($"Applied rotation from original state to {appliedCount} objects");
    }
    
    private void ResetToOriginalState()
    {
        // Записываем операцию для Undo
        Undo.RecordObjects(Selection.transforms, "Reset to Original State");
        
        int resetCount = 0;
        foreach (GameObject obj in Selection.gameObjects)
        {
            Transform t = obj.transform;
            if (originalTransforms.ContainsKey(t))
            {
                // Восстанавливаем начальное состояние
                t.position = originalTransforms[t].position;
                t.rotation = originalTransforms[t].rotation;
                resetCount++;
            }
            else
            {
                Debug.LogWarning($"No original state remembered for {obj.name}");
            }
        }
        
        Debug.Log($"Reset {resetCount} objects to original state");
    }
    
    private void ResetRotation()
    {
        // Записываем операцию для Undo
        Undo.RecordObjects(Selection.transforms, "Reset Rotation");
        
        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.transform.rotation = Quaternion.identity;
        }
        
        Debug.Log($"Reset rotation for {Selection.gameObjects.Length} objects");
    }
    
    private void ClearRememberedStates()
    {
        originalTransforms.Clear();
        Debug.Log("Cleared all remembered states");
    }
    
    private void OnSelectionChange()
    {
        // Обновляем интерфейс при изменении выделения
        Repaint();
    }
    
    private void SaveRememberedStates()
    {
        // Очищаем старые данные
        ClearSavedPrefs();
        
        // Сохраняем количество объектов
        EditorPrefs.SetInt(PREFS_KEY_COUNT, originalTransforms.Count);
        
        int index = 0;
        foreach (var kvp in originalTransforms)
        {
            Transform t = kvp.Key;
            TransformData data = kvp.Value;
            
            if (t != null) // Проверяем, что объект ещё существует
            {
                // Сохраняем ID объекта
                EditorPrefs.SetInt(PREFS_KEY_TRANSFORM + index, t.GetInstanceID());
                
                // Сохраняем позицию
                EditorPrefs.SetString(PREFS_KEY_POSITION + index, 
                    $"{data.position.x},{data.position.y},{data.position.z}");
                
                // Сохраняем поворот
                EditorPrefs.SetString(PREFS_KEY_ROTATION + index, 
                    $"{data.rotation.x},{data.rotation.y},{data.rotation.z},{data.rotation.w}");
                
                index++;
            }
        }
        
        // Обновляем реальное количество сохранённых объектов
        EditorPrefs.SetInt(PREFS_KEY_COUNT, index);
    }
    
    private void LoadRememberedStates()
    {
        originalTransforms.Clear();
        
        int count = EditorPrefs.GetInt(PREFS_KEY_COUNT, 0);
        
        for (int i = 0; i < count; i++)
        {
            int instanceID = EditorPrefs.GetInt(PREFS_KEY_TRANSFORM + i, 0);
            
            if (instanceID != 0)
            {
                Transform t = EditorUtility.InstanceIDToObject(instanceID) as Transform;
                
                if (t != null)
                {
                    // Загружаем позицию
                    string posStr = EditorPrefs.GetString(PREFS_KEY_POSITION + i, "");
                    if (!string.IsNullOrEmpty(posStr))
                    {
                        string[] posValues = posStr.Split(',');
                        if (posValues.Length == 3)
                        {
                            Vector3 position = new Vector3(
                                float.Parse(posValues[0]),
                                float.Parse(posValues[1]),
                                float.Parse(posValues[2])
                            );
                            
                            // Загружаем поворот
                            string rotStr = EditorPrefs.GetString(PREFS_KEY_ROTATION + i, "");
                            if (!string.IsNullOrEmpty(rotStr))
                            {
                                string[] rotValues = rotStr.Split(',');
                                if (rotValues.Length == 4)
                                {
                                    Quaternion rotation = new Quaternion(
                                        float.Parse(rotValues[0]),
                                        float.Parse(rotValues[1]),
                                        float.Parse(rotValues[2]),
                                        float.Parse(rotValues[3])
                                    );
                                    
                                    originalTransforms[t] = new TransformData(position, rotation);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
    private void ClearSavedPrefs()
    {
        int count = EditorPrefs.GetInt(PREFS_KEY_COUNT, 0);
        
        for (int i = 0; i < count; i++)
        {
            EditorPrefs.DeleteKey(PREFS_KEY_TRANSFORM + i);
            EditorPrefs.DeleteKey(PREFS_KEY_POSITION + i);
            EditorPrefs.DeleteKey(PREFS_KEY_ROTATION + i);
        }
        
        EditorPrefs.DeleteKey(PREFS_KEY_COUNT);
    }
}