using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items")]
public class ItemSO : ScriptableObject
{
    public Sprite sprite;
    public string objectName;
    [TextArea] public string objectDescription;
}
