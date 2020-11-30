public struct AnimalCount
{
    public Animal Animal { get; private set; }
    public int Count { get; set; }

    public AnimalCount(Animal animal, int count = 0)
    {
        Animal = animal;
        Count = count;
    }
}
