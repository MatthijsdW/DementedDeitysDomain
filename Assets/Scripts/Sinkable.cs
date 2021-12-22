using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinkable : MonoBehaviour
{
    Transform parent;
    GameObject spriteObject;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.root;
        spriteObject = GetComponentInChildren<SpriteRenderer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Biome biome = BiomeMap.instance.GetBiomeAt(parent.position.x, parent.position.z);
        switch (biome)
        {
            case Biome.Water:
                spriteObject.transform.localPosition = new Vector3(0, -0.2f, 0);
                break;
            case Biome.DeepWater:
                spriteObject.transform.localPosition = new Vector3(0, -1f, 0);
                break;
            default:
                spriteObject.transform.localPosition = new Vector3(0, 0, 0);
                break;
        }
    }
}
