using System;
using System.Collections.Generic;
using UnityEngine;

public static class InputExtension
{
    private static readonly KeyCode[] _numbersKeyCodes =
    {
        KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
    };

    private static readonly KeyCode[] _letterKeyCodes =
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I,
        KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.R, KeyCode.S,
        KeyCode.T, KeyCode.Q, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z
    };

    public static int GetNumber(KeyCode? keyCode)
    {
        return (int)(keyCode - KeyCode.Alpha0);
    }
        
    public static KeyCode? GetKeyUp(KeyCodeType keyCodeType = KeyCodeType.All)
    {
        return GetKey(Input.GetKeyUp, keyCodeType);
    }
    public static KeyCode? GetKeyDown(KeyCodeType keyCodeType=KeyCodeType.All)
    {
        return GetKey(Input.GetKeyDown, keyCodeType);
    }
        
        
        
    private static KeyCode? GetKey(Func<KeyCode,bool> getKeyFunc, KeyCodeType keyCodeType)
    {
        switch (keyCodeType)
        {
            case KeyCodeType.Numbers:
                return GetKeyWithFunc(getKeyFunc, _numbersKeyCodes);
            case KeyCodeType.Letters:
                return GetKeyWithFunc(getKeyFunc, _letterKeyCodes);
            case KeyCodeType.LettersAndNumbers:
                return GetKeyWithFunc(getKeyFunc, _numbersKeyCodes) ?? GetKeyWithFunc(getKeyFunc, _letterKeyCodes);
            case KeyCodeType.All:
                return GetKeyWithFunc(getKeyFunc);
            default:
                throw new ArgumentOutOfRangeException("keyCodeType", keyCodeType, null);
        }
    }
        
    private static KeyCode? GetKeyWithFunc(Func<KeyCode,bool> getKeyFunction)
    {
        var keyCodes = Enum.GetValues(typeof(KeyCode));
        foreach (KeyCode keyCode in keyCodes)
        {
            if (getKeyFunction(keyCode)) return keyCode;
        }
        return null;
    }
        
    private static KeyCode? GetKeyWithFunc(Func<KeyCode,bool> getKeyFunction,IEnumerable<KeyCode> array)
    {
        foreach (var keyCode in array)
        {
            if (getKeyFunction(keyCode)) return keyCode;
        }
        return null;
    }
        
}
public enum KeyCodeType
{
    Numbers,
    Letters,
    LettersAndNumbers,
    All,
}