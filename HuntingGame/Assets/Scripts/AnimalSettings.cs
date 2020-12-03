using System;
using System.Collections.Generic;

[Serializable]
public class AnimalSettings
{
    public AnimalType AnimalType;
    public int StartCount;
    public Animal Prefab;
    public SpawnType SpawnType;
    public int MinCountInGroup;
    public int MaxCountInGroup;
    public List<GroupPrefab> GroupPrefabs;
}
