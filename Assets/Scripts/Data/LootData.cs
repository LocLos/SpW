using System;

[Serializable]
public class LootData
{
    public int Collected;
    public Action Changed;

    public void Collect(int loot)
    {
        Collected += loot;
        Changed?.Invoke();
    }
}