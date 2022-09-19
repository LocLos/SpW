using Assets.Scripts;
using Assets.Scripts.Player;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class GameFactory
{
    DiContainer _container;
    AssetsProvider _assetProvider;

    [Inject]
    public void Construct(DiContainer container, AssetsProvider assetProvider)
    {
        _container = container;
        _assetProvider = assetProvider;
    }

    public async Task CreateHudAsync()
    {
        var pref = await _assetProvider.Instantiate(AssetsAddress.Hud);
        _container.InjectGameObject(pref);
    }



    public async Task<GameObject> CreatePlayerAsync()
    {
        var pref = await _assetProvider.Instantiate(AssetsAddress.Player);
        _container.InjectGameObject(pref);
        return pref;
    }


    public async void CreateEnemySpawner()
    {
        var pref = await _assetProvider.Load<GameObject>(AssetsAddress.EnemySpawner);
        _container.InstantiatePrefab(pref);
    }

    public async void CreateEnemyAsync(EnemySpawnPointData enemyData)
    {

        GameObject prefab = await _assetProvider.Load<GameObject>(enemyData.PrefabReference);
        GameObject enemy = _container.InstantiatePrefab(prefab);

        enemy.GetComponentInChildren<IHealth>().Construct(enemyData.Hp);
        enemy.GetComponent<MoveToPlayer>().Construct(enemyData.MoveSpeed);
        enemy.GetComponent<LootSpawner>().SetLoot(enemyData.MaxLoot, enemyData.MinLoot);
        enemy.transform.position = enemyData.transform.position;
    }

    /* public async Task WarmUp()
     {
         await _assetProvider.Load<GameObject>(AssetsAddress.Loot);
     }*/

    public async Task<GameObject> CreateLootAsync()
    {
        GameObject pref = await _assetProvider.Load<GameObject>(AssetsAddress.Loot);

        return _container.InstantiatePrefab(pref);
    }
}
