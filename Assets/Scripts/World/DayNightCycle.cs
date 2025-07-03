using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0f, 24f)] public float timeOfDay = 12f; // 0 = midnight, 12 = noon
    public float dayDurationInMinutes = 1f; // real-time duration for a full day

    [Header("Sun Settings")]
    public Light sun;
    public Gradient lightColor;
    public AnimationCurve lightIntensity;

    private float timeSpeed;

    void Start()
    {
        timeSpeed = 24f / (dayDurationInMinutes * 60f); // hours per real-time second
    }

    void Update()
    {
        // Progress time
        timeOfDay += Time.deltaTime * timeSpeed;
        if (timeOfDay >= 24f) timeOfDay = 0f;

        // Rotate the sun (map 0–24h to 360 degrees)
        float sunAngle = (timeOfDay / 24f) * 360f;
        sun.transform.rotation = Quaternion.Euler(new Vector3(sunAngle - 90f, 170f, 0));

        // Change light color/intensity based on time
        float t = timeOfDay / 24f;
        sun.color = lightColor.Evaluate(t);
        sun.intensity = lightIntensity.Evaluate(t);
    }
}
