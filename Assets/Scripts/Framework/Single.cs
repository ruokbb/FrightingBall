using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class Single<T> : MonoBehaviour
    where T : MonoBehaviour
{

    private static T myInstance = null;

    public static T instance
    {
        get { return myInstance; }
    }

    protected virtual void Awake()
    {
        myInstance = this as T;
    }

}
