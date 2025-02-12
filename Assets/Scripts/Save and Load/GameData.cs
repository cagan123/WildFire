using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int currency;
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentId;
    public string leftSpellSlotID;
    public string rightSpellSlotID;
    public string dashSpellSlotID;
    public string qSpellSlotID;
    public string eSpellSlotID;

    public SerializableDictionary<string, bool> checkpoints;
        

    public GameData()
    {
        this.currency = 0;
        inventory = new SerializableDictionary<string, int>();
        equipmentId = new List<string>();
        leftSpellSlotID = "";
        rightSpellSlotID = "";
        dashSpellSlotID = "";
        qSpellSlotID = "";
        eSpellSlotID = "";

        checkpoints = new SerializableDictionary<string, bool>();
    }
}
