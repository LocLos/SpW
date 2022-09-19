using UnityEngine;
using Zenject;

public class Loot : MonoBehaviour
{
    private int _value;

    private LootData _lootData;

    [Inject]
    public void Construct(LootData lootData)
    {
        _lootData = lootData;
    }

    public void SetValue(int value) => 
        _value = value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _lootData.Collect(_value);
        Destroy(gameObject);
    }
}
