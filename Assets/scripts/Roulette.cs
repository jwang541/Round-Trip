using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility {
    public class Roulette<T>
    {
        public List<Tuple<T, float>> items;

        public Roulette() {
            items = new List<Tuple<T, float>>();
        }

        public void Add(T val, float weight) { items.Add(new Tuple<T, float>(val, weight)); }

        public T WeightedSelection() 
        {
            float target = 0.0f;
            foreach (var item in items) target += item.Item2;
            target *= UnityEngine.Random.Range(0.0f, 1.0f);

            float sum = 0.0f;
            foreach (var item in items) {
                sum += item.Item2;
                if (sum > target) return item.Item1;
            }
            return default(T);
        }

    }

}