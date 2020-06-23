using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IT/ResourceHolder")]
public class ResourcesHolder : ScriptableObject
{
    static ResourcesHolder prefab;
    public static ResourcesHolder Prefab
    {
        get
        {
            if(prefab == null)
            {
                prefab = Resources.Load<ResourcesHolder>("IT/ResourcesHolder");
                if(prefab == null)
                {
                    Debug.LogError("Can't load ResourceHolder at Resources/IT/ResourcesHolder");
                }
            }
            return prefab;
        }
    }

    public Color HeaderButtonDisabledColor;

    public List<SpriteResourceData> sprites = new List<SpriteResourceData>();

    public Sprite GetSprite(string key)
    {
        var data = sprites.Find(s => s.Key.ToLower() == key.ToLower());
        if (data == null)
            return null;
        return data.sprite;
    }
    
    public Color GetTextOutlineColor(string Key)
    {
        var data = sprites.Find(s => s.Key.ToLower() == Key.ToLower());
        if (data == null)
            return Color.white;
        return data.textOutlineColor;
    }

}

[System.Serializable]
public class ResourceData
{
    public string Key;
}
[System.Serializable]
public class SpriteResourceData : ResourceData
{
    public Sprite sprite;
    public Color textOutlineColor;
}
