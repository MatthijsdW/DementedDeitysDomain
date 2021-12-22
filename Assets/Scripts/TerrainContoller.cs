using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainContoller : MonoBehaviour
{
    public GameObject planePrefab;
    public Texture2D[] biomeTextures;
    public GameObject player;
    public int tileRadius;
    public float updateSeconds;

    private Dictionary<(int, int), GameObject> planes = new Dictionary<(int, int), GameObject>();

    void Start()
    {
        StartCoroutine(UpdateTerrain());
    }

    private IEnumerator UpdateTerrain()
    {
        while (true)
        {
            Dictionary<(int, int), GameObject> planesToDestroy = new Dictionary<(int, int), GameObject>(planes);
            int playerPlaneX = (int)(player.transform.position.x) / 4;
            int playerPlaneZ = (int)(player.transform.position.z) / 4;
            for (int i = -playerPlaneX - tileRadius - 1; i <= -playerPlaneX + tileRadius; i++)
                for (int j = -playerPlaneZ - tileRadius - 1; j <= -playerPlaneZ + tileRadius; j++)
                {
                    if (planesToDestroy.ContainsKey((i, j)))
                    {
                        planesToDestroy.Remove((i, j));
                        continue;
                    }

                    Texture2D newTexture = new Texture2D(64, 64);
                    newTexture.wrapMode = TextureWrapMode.Clamp;
                    newTexture.filterMode = FilterMode.Point;
                    for (int x = 0; x < 64; x++)
                        for (int y = 0; y < 64; y++)
                        {
                            Biome biome = BiomeMap.instance.GetBiomeAt(-(x + 64 * i) / 16f, -(y + 64 * j) / 16f);
                            Color color = biomeTextures[(int)biome].GetPixel(x, y);
                            newTexture.SetPixel(x, y, color);
                        }
                    newTexture.Apply();
                    GameObject plane = Instantiate(planePrefab);
                    plane.transform.position -= new Vector3(4 * i + 2, 0, 4 * j + 2);
                    plane.GetComponent<MeshRenderer>().material.mainTexture = newTexture;
                    planes.Add((i, j), plane);
                }

            foreach ((int, int) key in planesToDestroy.Keys)
            {
                Destroy(planes[key]);
                planes.Remove(key);
            }

            yield return new WaitForSeconds(updateSeconds);
        }
    }
}
