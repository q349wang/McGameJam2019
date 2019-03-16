using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypedPlayerSpawner : MonoBehaviour
{
    [SerializeField]
    Sprite healerSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Healer>();
        GetComponent<SpriteRenderer>().sprite = healerSprite;
    }

}
