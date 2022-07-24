using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility {
    [Serializable]
    public class WeightedValue<T> : MonoBehaviour
    {
        public float weight;
        public T value;

        public WeightedValue(T val, float wei) {
            value = val;
            weight = wei;
        }
    }
}