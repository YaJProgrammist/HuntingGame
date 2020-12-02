﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<AnimalSettings> animalsSettings;
    [SerializeField] private Transform animalsSpawnPivot;
    [SerializeField] private double distanceBetweenPoints;

    private System.Random rand;
    private const float FIELD_WIDTH = 100;
    private const float FIELD_HEIGHT = 100;
    private Dictionary<AnimalType, List<Animal>> animalLists;

    public Dictionary<AnimalType, int> NeededCount { get; set; }
    public event EventHandler<AnimalCountChangedEventArgs> OnAnimalCountChanged;

    void Awake()
    {
        rand = new System.Random();

        animalLists = new Dictionary<AnimalType, List<Animal>>();
        NeededCount = new Dictionary<AnimalType, int>();

        foreach (AnimalSettings animalSettings in animalsSettings)
        {
            animalLists.Add(animalSettings.AnimalType, new List<Animal>());
            NeededCount.Add(animalSettings.AnimalType, animalSettings.StartCount);
        }
    }

    void Start()
    {
        RespawnEverything();
    }

    void Update()
    {
        foreach (KeyValuePair<AnimalType, int> neededCountPair in NeededCount)
        {
            AdjustAnimalCount(neededCountPair.Key, neededCountPair.Value);
        }
    }

    public void RespawnEverything()
    {
        foreach(AnimalSettings animalSettings in animalsSettings)
        {
            DeleteAllAnimalKind(animalSettings.AnimalType);
            NeededCount[animalSettings.AnimalType] = animalSettings.StartCount;
        }
    }

    private void AdjustAnimalCount(AnimalType animal, int newAnimalCount)
    {
        if (newAnimalCount == animalLists[animal].Count)
        {
            return;
        }

        if (newAnimalCount < animalLists[animal].Count)
        {
            DeleteRandomAnimals(animalLists[animal], animalLists[animal].Count - newAnimalCount);
        }
        else
        {
            List<Vector2> positionsOfNewAnimals = GetRandomFreePoints(newAnimalCount - animalLists[animal].Count);

            AnimalSettings animalSettings = animalsSettings.Find(animSett => animSett.AnimalType == animal);

            foreach (Vector2 position in positionsOfNewAnimals)
            {
                Animal spawnedAnimal = Instantiate(animalSettings.Prefab);
                spawnedAnimal.transform.position = position;
                spawnedAnimal.transform.SetParent(animalsSpawnPivot, false);
                animalLists[animalSettings.AnimalType].Add(spawnedAnimal);
                spawnedAnimal.OnAnimalRemoved += (a, ea) => 
                    {
                        animalLists[animalSettings.AnimalType].Remove(ea.Animal);
                        NeededCount[animalSettings.AnimalType]--;
                    };
            }
        }

        OnAnimalCountChanged?.Invoke(this, new AnimalCountChangedEventArgs(animal));
    }

    public int GetAnimalCount(AnimalType animalType)
    {
        return animalLists[animalType].Count;
    }

    private void DeleteAllAnimalKind(AnimalType animalType)
    {
        foreach (Animal animal in animalLists[animalType])
        {
            Destroy(animal.gameObject);
        }

        animalLists[animalType].Clear();
    }

    private void DeleteRandomAnimals(List<Animal> animalList, int deleteCount)
    {
        for (int i = 0; i < deleteCount; i++)
        {
            int animalToDeleteInd = rand.Next(animalList.Count);
            Destroy(animalList[animalToDeleteInd].gameObject);
            animalList.RemoveAt(animalToDeleteInd);
        }
    }

    private List<Vector2> GetRandomFreePoints(int pointsCount)
    {
        double squareDistanceBetweenPoints = Math.Pow(distanceBetweenPoints, 2);

        List<Vector2> points = new List<Vector2>();

        for (int i = 0; i < pointsCount; i++)
        {
            Vector2 newPoint;

            while (true)
            {
                newPoint = GetRandomPointOnField();

                bool isDistantFromOtherPoints = true;

                for (int j = 0; j < points.Count; j++)
                {
                    double squareDistance = GetSquareDistanceBetweenPoints(newPoint, points[j]);
                    if (squareDistance < squareDistanceBetweenPoints)
                    {
                        isDistantFromOtherPoints = false;
                        break;
                    }
                }

                if (isDistantFromOtherPoints && !PointIsCloseToExistingAnimals(newPoint))
                {
                    break;
                }
            }

            points.Add(newPoint);
        }

        return points;
    }

    private bool PointIsCloseToExistingAnimals(Vector2 point)
    {
        bool isCloseToExistingAnimals = false;

        double squareDistanceBetweenPoints = Math.Pow(distanceBetweenPoints, 2);

        foreach (List<Animal> animalList in animalLists.Values)
        {
            foreach (Animal animal in animalList)
            {
                Vector2 position = animal.transform.position;

                double squareDistance = GetSquareDistanceBetweenPoints(point, position);
                if (squareDistance < squareDistanceBetweenPoints)
                {
                    isCloseToExistingAnimals = true;
                    break;
                }
            }

            if (isCloseToExistingAnimals)
            {
                break;
            }
        }

        return isCloseToExistingAnimals;
    }

    private Vector2 GetRandomPointOnField()
    {
        float x = rand.Next((int)(FIELD_WIDTH * 1000)) / 1000.0f;
        float y = rand.Next((int)(FIELD_HEIGHT * 1000)) / 1000.0f;

        return new Vector2(x, y);
    }

    private double GetSquareDistanceBetweenPoints(Vector2 point1, Vector2 point2)
    {
        return Math.Pow(point1.x - point2.x, 2) + Math.Pow(point1.y - point2.y, 2);
    }
}
