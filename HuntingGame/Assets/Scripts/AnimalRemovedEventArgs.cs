public class AnimalRemovedEventArgs
{
    public Animal Animal { get; private set; }

    public AnimalRemovedEventArgs(Animal animal)
    {
        Animal = animal;
    }
}