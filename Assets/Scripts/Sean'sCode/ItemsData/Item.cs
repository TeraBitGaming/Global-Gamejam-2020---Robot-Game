using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField]
    private string iName;
    [SerializeField]
    private Sprite iSprite;

    public string GetName()
    {
        return iName;
    }

    public Sprite GetSprite()
    {
        return iSprite;
    }
}
