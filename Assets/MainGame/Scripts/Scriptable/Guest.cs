using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GuestDB;
using static GuestSpeechDB;
public class Guest// : ScriptableObject
{
    public int ID;
    public string Name;
    public string Personality;
    public Flavor[] GoodFlavor;
    public Flavor BadFlavor;
    public string Feature;
    public string Explanation;
    public List<speechData> speechDatas = new List<speechData>();
    public Guest(int id, string name, string personality, Flavor[] goodFlavor, Flavor badFlavor, string feature, string explation)
    {
        ID = id; Name = name; Personality = personality; GoodFlavor = goodFlavor; BadFlavor = badFlavor; Feature = feature; Explanation = explation;
    }
    public class speechData
    {
        public speechData(SpeechType Type, SpeechCategory Category, string reaction, string explanation)
        {
            speechType = Type;
            speechCategory = Category;
            Reaction = reaction;
            Explanation = explanation;
        }
        public SpeechType speechType;
        public SpeechCategory speechCategory;
        public string Reaction;
        public string Explanation;
    }
}