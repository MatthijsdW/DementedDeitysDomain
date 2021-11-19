using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeMap : MonoBehaviour
{
    public static BiomeMap instance;

    public float amplitude;
    public float frequency;
    public float seed;

    private void Awake()
    {
        instance = this;
    }

    public Biome GetBiomeAt(float x, float y)
    {
        float perlin = amplitude * Mathf.PerlinNoise(x * frequency + seed, y * frequency + seed);

        if (perlin < 0.2)
            return Biome.DeepWater;
        if (perlin < 0.4)
            return Biome.Water;
        if (perlin < 0.5)
            return Biome.Beach;
        if (perlin < 0.7)
            return Biome.Grass;
        if (perlin < 0.8)
            return Biome.Stone;
        return Biome.Snow;
    }
}

public enum Biome
{
    DeepWater,
    Water,
    Beach,
    Grass,
    Stone,
    Snow,
}
