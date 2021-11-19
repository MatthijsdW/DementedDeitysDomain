using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainContoller : MonoBehaviour
{
    public GameObject planePrefab;
    public Texture2D[] biomeTextures;

    void Start()
    {
        for (int i = -4; i <= 4; i++)
            for (int j = -4; j <= 4; j++)
            {
                Texture2D newTexture = new Texture2D(64, 64);
                newTexture.wrapMode = TextureWrapMode.Clamp;
                newTexture.filterMode = FilterMode.Point;
                for (int x = 0; x < 64; x++)
                    for (int y = 0; y <  64; y++)
                    {
                        Biome biome = BiomeMap.instance.GetBiomeAt(x + 64 * i, y + 64 * j);
                        Color color = biomeTextures[(int)biome].GetPixel(x, y);
                        newTexture.SetPixel(x, y, color);
                    }
                newTexture.Apply();
                GameObject plane = Instantiate(planePrefab);
                plane.transform.position -= new Vector3(4 * i, 0, 4 * j);
                plane.GetComponent<MeshRenderer>().material.mainTexture = newTexture;
            }
    }
}
