using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class SetupTags
{
    [MenuItem("Tools/Setup Tags & Layers")]
    public static void SetupProjectTags()
    {
        Debug.Log("=== 设置项目标签 ===");

        // 获取TagManager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        // 添加必要的标签
        string[] requiredTags = { "Player", "Enemy", "Dialogue", "Ground", "Interactable" };

        foreach (string tag in requiredTags)
        {
            if (!TagExists(tag, tagsProp))
            {
                tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
                SerializedProperty newTag = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
                newTag.stringValue = tag;
                Debug.Log($"✅ 添加标签: {tag}");
            }
            else
            {
                Debug.Log($"✅ 标签已存在: {tag}");
            }
        }

        tagManager.ApplyModifiedProperties();

        // 设置层级
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        
        // 确保UI层级存在
        string uiLayer = "UI";
        bool uiLayerExists = false;
        for (int i = 0; i < layersProp.arraySize; i++)
        {
            SerializedProperty layer = layersProp.GetArrayElementAtIndex(i);
            if (layer.stringValue == uiLayer)
            {
                uiLayerExists = true;
                break;
            }
        }

        if (!uiLayerExists)
        {
            // 找到第一个空层级
            for (int i = 8; i < layersProp.arraySize; i++)
            {
                SerializedProperty layer = layersProp.GetArrayElementAtIndex(i);
                if (string.IsNullOrEmpty(layer.stringValue))
                {
                    layer.stringValue = uiLayer;
                    Debug.Log($"✅ 设置层级: {uiLayer} -> 层级 {i}");
                    break;
                }
            }
        }

        tagManager.ApplyModifiedProperties();
        Debug.Log("=== 标签设置完成 ===");
    }

    private static bool TagExists(string tag, SerializedProperty tagsProp)
    {
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty existingTag = tagsProp.GetArrayElementAtIndex(i);
            if (existingTag.stringValue == tag)
            {
                return true;
            }
        }
        return false;
    }
}
#endif