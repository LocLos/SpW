using TMPro;
using UnityEngine;
using Zenject;

public class LootCounter : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _counter;
    private LootData _lootData;

    [Inject]
    public void Construct(LootData lootData)
    {
        _lootData = lootData;
        _lootData.Changed += UpdateLootInfo;
    }

    private void UpdateLootInfo() => 
        _counter.text = _lootData.Collected.ToString();
}