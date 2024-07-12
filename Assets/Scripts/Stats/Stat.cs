using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
[System.Serializable]
public class Stat 
{
    [SerializeField] private int baseValue;
    public List<int> modifiers;

    public int GetValue(){
        int finalValue = baseValue;
        foreach(int modifier in modifiers){
            finalValue += modifier;
        }
        return finalValue;
    }
    public void AddModifiers(int _modifier){
        modifiers.Add(_modifier);
    }
    public void RemoveModifiers(int _modifier){
        modifiers.RemoveAt(_modifier);
    }
}
