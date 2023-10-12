using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;

    public void SetItemSprite(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
    
    public void SetItemName(string name)
    {
        itemName.text = name;
    }
    
}
