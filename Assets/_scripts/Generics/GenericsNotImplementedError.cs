using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenericsNotImplementedError<T>
{
    public static T TryGet(T value, string name)
    {
        if (value != null)
            return value;

        Debug.LogError(typeof(T)+ "Not Implemented on "+ name); 
        return default;
    }
}
