using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public enum FishRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    [Serializable]
    public class FishDB
    {
        public FishRarity rarity;
        public string fishName;
    }

    [CreateAssetMenu(fileName = "FishSO", menuName = "FishingGame/FishSO")]
    public class FishSO : ScriptableObject
    {
        public FishDB[] fishDBs;
    }
}

