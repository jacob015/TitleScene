using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "Matter", menuName = "Scriptable Object/Matters", order = int.MaxValue)]
public class Matter// : ScriptableObject
{
    public Matter(string name, string origin, string explanation, int[] flavor, Sprite matterSprite)
    {
        Name = name;
        Origin = origin;
        Explanation = explanation;
        Flavor = flavor;
        MatterSprite = matterSprite;
    }
    public string Name;
    public string Origin;
    public string Explanation;
    public int[] Flavor;
    public Sprite MatterSprite;
}
