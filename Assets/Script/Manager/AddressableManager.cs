using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class AddressableManager : MonoBehaviour
{
    void Awake()
    {
        
    }
    public void DownLoadDeoendenciesAsync(object key)
    {
        Addressables.GetDownloadSizeAsync(key).Completed += (opSize) =>
        {
            if (opSize.Status == AsyncOperationStatus.Succeeded && opSize.Result > 0)
            {
                Addressables.DownloadDependenciesAsync(key, true).Completed += (opDownload) =>
                {
                    if (((AsyncOperationHandle)opDownload).Status != AsyncOperationStatus.Succeeded)
                    {
                        return;
                    }
                };
            }
            else
            {
                Debug.Log("어드레서블 다운로드 되어있음");
            }
        };
    }

    public void LoadAssetAsync(object key)
    {
        try
        {
            Addressables.LoadAssetAsync<GameObject>(key).Completed += (op) =>
            {
                if(((AsyncOperationHandle<GameObject>)op).Status == AsyncOperationStatus.Succeeded)
                {
                    return;
                }
            };
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
