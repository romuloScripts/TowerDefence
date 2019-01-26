using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ObservedInt{
    
    [SerializeField]
    private int currentValue;
    
    [SerializeField]
    private StringEvent onValueChanged;

    [Serializable]
    public class StringEvent : UnityEvent<string>{ }

    private int CurrentValue
    {
        get => currentValue;
        set{
            currentValue = value;
            ValueChanged();
        }
    }

    public void ValueChanged()
    {
        onValueChanged.Invoke(currentValue.ToString()); 
    }

    public static implicit operator int(ObservedInt observedFloat)
    {
        return observedFloat.CurrentValue;
    }
    
    public static ObservedInt operator +(ObservedInt o1,int o2)
    {
        o1.CurrentValue += o2;
        return o1;
    }
    
    public static ObservedInt operator -(ObservedInt o1,int o2)
    {
        o1.CurrentValue -= o2;
        return o1;
    }
    
    public static ObservedInt operator --(ObservedInt o1)
    {
        o1.CurrentValue -= 1;
        return o1;
    }
    
    public static ObservedInt operator ++(ObservedInt o1)
    {
        o1.CurrentValue += 1;
        return o1;
    }
}