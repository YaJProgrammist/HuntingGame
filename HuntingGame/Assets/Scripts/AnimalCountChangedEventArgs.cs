public class AnimalCountChangedEventArgs
{
    public AnimalType AnimalType { get; private set; }

    public AnimalCountChangedEventArgs(AnimalType animalType)
    {
        AnimalType = animalType;
    }
}
