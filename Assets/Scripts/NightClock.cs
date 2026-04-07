using UnityEngine;
using TMPro;

public class NightClock : MonoBehaviour
{
    public static NightClock instance { get; private set; }
    public TextMeshProUGUI clockText;
    public int currentHour = 12;
    public int nightNumber = 1;

    public float hourDuration = 60f; // seconds per in-game hour
    private float timer = 0f;

    void Start()
    {
        UpdateClockDisplay();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= hourDuration)
        {
            timer = 0f;
            AdvanceHour();
        }
    }
    public void Sync(int hour)
    {
        currentHour = hour;
    }
    void AdvanceHour()
    {
        // After 12 AM → 1 AM → ... → 6 AM
        if (currentHour == 12)
            currentHour = 1;
        else
            currentHour++;

        UpdateClockDisplay();

        if (currentHour > 6)
        {
            Debug.Log("Night Complete!");
            // You can trigger your win condition here
        }
    }

    void UpdateClockDisplay()
    {
        clockText.text = $"{currentHour} AM\nNight {nightNumber}";
    }
}
