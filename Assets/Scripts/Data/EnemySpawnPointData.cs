using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemySpawnPointData : MonoBehaviour// переделать на скриптабл обж.
{
    [Range(1, 100)]
    public int Hp;

    [Range(1, 30)]
    public int Damage;

    [Range(.5f, 1)]
    public int EffectiveDistance;

    [Range(1, 30)]
    public int MoveSpeed;

    public int MaxLoot;
    public int MinLoot;

    public EnemyType EnemyType;

    public AssetReferenceGameObject PrefabReference;
}
