using UnityEngine;
using UnityEngine.UI;

public class AnimalNumberViewer : MonoBehaviour
{
    [SerializeField] private AnimalType animalType;
    [SerializeField] private InputField inputField;
    [SerializeField] private Slider slider;
    [SerializeField] private SpawnManager spawnManager;

    void Start()
    {
        spawnManager.OnAnimalCountChanged += AnimalCountChanged;
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void AnimalCountChanged(object sender, AnimalCountChangedEventArgs eventArgs)
    {
        if (eventArgs.AnimalType == animalType)
        {
            UpdateValue();
        }
    }

    private void OnInputFieldValueChanged(string value)
    {
        if (int.TryParse(value, out int intValue))
        {
            if (intValue < 0)
            {
                intValue = 0;
                inputField.text = "0";
            }

            slider.value = intValue;
            AdjustAnimalCount(intValue);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        int intValue = (int)value;

        inputField.text = intValue.ToString();
        AdjustAnimalCount(intValue);
    }

    private void AdjustAnimalCount(int newCount)
    {
        spawnManager.NeededCount[animalType] = newCount;
    }

    private void UpdateValue()
    {
        int animalCount = spawnManager.GetAnimalCount(animalType);
        inputField.text = animalCount.ToString();
        slider.value = animalCount;
    }
}
