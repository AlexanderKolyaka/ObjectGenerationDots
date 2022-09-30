using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UI;

public class TowerCreator : MonoBehaviour
{

    public GameObject prefab;
    public int length;
    public int width;
    public int height;
    public Text cubeCounterText;

    private Entity _cubeEntity;
    private EntityManager _entityManager;
    private BlobAssetStore _blobAssetStore;
    private GameObjectConversionSettings _settings;


   
    void Start()
    {

        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _blobAssetStore = new BlobAssetStore();
        _settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld,_blobAssetStore);

        _cubeEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, _settings);
        int cubeCount = 0;
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                for (int l = 0; l < length; l++)
                {
                    if (l != 0 && w != 0 && l != length - 1 && w != width - 1)
                    {
                        continue;
                    }



                    //Instantiate(prefab, new Vector3(l, h+0.5f, w), Quaternion.identity);
                    Entity cube = _entityManager.Instantiate(_cubeEntity);
                    Translation cubeTranslition = new Translation
                    {
                        Value = new float3(l, h + 0.5f, w)
                       
                    };
                    
                    _entityManager.SetComponentData(cube,cubeTranslition);
                    cubeCount++;
                }
            }
        }



        cubeCounterText.text = $"Cube count : {cubeCount}";
    }


    private void OnDisable()
    {
        _blobAssetStore.Dispose();
    }

}
