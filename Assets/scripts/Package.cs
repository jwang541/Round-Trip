using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
struct Package<T>
{
    public int Index0;
    public int Index1;
    public T Element;
    public Package(int idx0, int idx1, T element)
    {
        Index0 = idx0;
        Index1 = idx1;
        Element = element;
    }
}