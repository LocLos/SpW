using UnityEngine;
using Zenject;

public class EnemeySpawner : MonoBehaviour
{
    private GameFactory _gameFactory;

    [Inject]
    public void Construct(GameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    private void Start()
    {
        EnemySpawnPointData[] father = GetComponentsInChildren<EnemySpawnPointData>();
        foreach (EnemySpawnPointData point in father)
        {
              _gameFactory.CreateEnemyAsync(point);
        }
    }
}
 