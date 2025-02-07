using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DrinkDB;
//[CreateAssetMenu(fileName = "Drink", menuName = "Scriptable Object/Drink", order = int.MaxValue)]
public class Drink// : ScriptableObject
{
    public string Name;
    public DrinkType drinkType;
    public List<Matter> RequiredMatter;
    public List<Matter> ExtraMatter;
    public string Feature;
    public Difficulty difficulty;
    public int Price;
    public int Bonus;
    public Satisfaction satisfaction;
    public int ProduceTime;

    public Drink(string name, DrinkType type, List<Matter> requiredMatter, List<Matter> extraMatter, string feature, Difficulty Difficulty,int price,int bonus,Satisfaction Satisfaction,int produceTime)
    {
        Name = name; drinkType = type; RequiredMatter = requiredMatter; ExtraMatter = extraMatter; Feature = feature; difficulty = Difficulty; Price = price; Bonus = bonus; satisfaction = Satisfaction; ProduceTime = produceTime;
    }
}