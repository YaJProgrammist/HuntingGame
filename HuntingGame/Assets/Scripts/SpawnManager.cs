using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int startBunnyCount;
    [SerializeField] private int startDoeCount;
    [SerializeField] private int startWolfCount;
    private System.Random rand;
    private float FIELD_WIDTH = 500;
    private float FIELD_HEIGHT = 500;
    private Dictionary<AnimalType, int> animalCounts;

    void Awake()
    {
        rand = new System.Random();

        animalCounts = new Dictionary<AnimalType, int>();
        animalCounts.Add(AnimalType.Bunny, 0);
        animalCounts.Add(AnimalType.Doe, 0);
        animalCounts.Add(AnimalType.Wolf, 0);
    }

    void Start()
    {
        RespawnEverything();

    }

    void Update()
    {
        
    }

    public void RespawnEverything()
    {

    }

    public void AdjustAnimalCount(AnimalType animal, int newAnimalCount)
    {
        if (newAnimalCount == animalCounts[animal])
        {
            return;
        }

        if (newAnimalCount < animalCounts[animal])
        {

        }
        else
        {
            GetRandomFreePoints(newAnimalCount - animalCounts[animal]);
        }

        animalCounts[animal] = newAnimalCount;
    }

    private List<Point> GetRandomFreePoints(int pointsCount)
    {
        double distanceBetweenPoints = 5;
        double squareDistanceBetweenPoints = Math.Pow(distanceBetweenPoints, 2);

        List<Point> points = new List<Point>();

        for (int i = 0; i < pointsCount; i++)
        {
            Point newPoint;

            while (true)
            {
                newPoint = GetRandomPointOnField();

                bool isDistantFromOtherPoints = true;

                for (int j = 0; j < points.Count; j++)
                {
                    double squareDistance = Math.Pow(points[j].X - newPoint.X, 2) + Math.Pow(points[j].Y - newPoint.Y, 2);
                    if (squareDistance < squareDistanceBetweenPoints)
                    {
                        isDistantFromOtherPoints = false;
                        break;
                    }
                }

                if (isDistantFromOtherPoints)
                {
                    break;
                }
            }

            points.Add(newPoint);
        }

        return points;
    }

    private Point GetRandomPointOnField()
    {
        double x = rand.Next((int)(FIELD_WIDTH * 1000)) / 1000.0;
        double y = rand.Next((int)(FIELD_HEIGHT * 1000)) / 1000.0;

        return new Point(x, y);
    }
}
