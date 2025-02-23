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
    public string closestCheckpointID;
        
    public float lostCurrencyX;
    public float lostCurrencyY;
    public int lostCurrencyAmount;

    public GameData()
    {
        this.lostCurrencyX = 0;
        this.lostCurrencyY = 0;
        this.lostCurrencyAmount = 0;


        this.currency = 0;
        inventory = new SerializableDictionary<string, int>();
        equipmentId = new List<string>();
        leftSpellSlotID = string.Empty;
        rightSpellSlotID = string.Empty;
        dashSpellSlotID = string.Empty;
        qSpellSlotID = string.Empty;
        eSpellSlotID = string.Empty;
        closestCheckpointID = string.Empty;

        checkpoints = new SerializableDictionary<string, bool>();
    }
}
