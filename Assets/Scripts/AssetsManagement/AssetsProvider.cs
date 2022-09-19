using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetsProvider
{//кеш завершенных операций
    private readonly Dictionary<string, AsyncOperationHandle> _completeCache = new Dictionary<string, AsyncOperationHandle>();
    private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();
    // лист иза того что обращение на один обьект может прийти 2жды но пока оно обрабатывается проверка на добавление еще не сработает

    public void Initialized()// прогреть аддресейблы 
    {
        Addressables.InitializeAsync();
       Load<GameObject>(AssetsAddress.Loot);

    }

    public async Task<T> Load<T>(AssetReference assetReference) where T : class
    {
        if (_completeCache.TryGetValue(assetReference.AssetGUID, out var completedHandle)) // если такое уже есть то его и вернуть
            return completedHandle.Result as T;

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
        return await RunWithCecheOnComplrte(assetReference.AssetGUID, handle);
    }

    public async Task<T> Load<T>(string address) where T : class
    {
        if (_completeCache.TryGetValue(address, out var completedHandle))
        {
            Debug.Log("load" + address);
            return completedHandle.Result as T;
        }

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
        Debug.Log("new_load");

        return await RunWithCecheOnComplrte(address, handle);
    }

    public void CleanUp()
    {
        foreach (List<AsyncOperationHandle> resourceHandle in _handles.Values)
            foreach (AsyncOperationHandle handle in resourceHandle)
                Addressables.Release(handle);
        _completeCache.Clear();
        _handles.Clear();
    }

    private async Task<T> RunWithCecheOnComplrte<T>(string cacheKey, AsyncOperationHandle<T> handle) where T : class
    {
        handle.Completed += h =>
        {
            _completeCache[cacheKey] = h;
        };
        AddHandle(handle, cacheKey);

        return await handle.Task;
    }
    private void AddHandle<T>(AsyncOperationHandle<T> handle, string key) where T : class
    {
        if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandle))
        {
            resourceHandle = new List<AsyncOperationHandle>();
            _handles[key] = resourceHandle;
        }
        resourceHandle.Add(handle);
    }
    public Task<GameObject> Instantiate(string address) =>
        Addressables.InstantiateAsync(address).Task;
}