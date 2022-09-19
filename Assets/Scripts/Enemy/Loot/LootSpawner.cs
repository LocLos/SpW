using UnityEngine;
using Zenject;

public class LootSpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyDeath _enemyDeath;

    private GameFactory _gameFactory;

    private int _maxValue;
    private int _minValue;

    [Inject]
    public void Construct(GameFactory gameFactory) =>
        _gameFactory = gameFactory;

    private void Start() =>
       _enemyDeath.Happend += SpawnLoot;

    public void SetLoot(int max, int min)
    {
        _maxValue = max;
        _minValue = min;
    }

    private async void SpawnLoot()
    {
        var pref = await _gameFactory.CreateLootAsync();
        Loot loot = pref.GetComponent<Loot>();
        loot.transform.position = transform.position;
        loot.SetValue(GenerateLoot());
    }

    private int GenerateLoot() =>
        Random.Range(_minValue, _maxValue);
}
