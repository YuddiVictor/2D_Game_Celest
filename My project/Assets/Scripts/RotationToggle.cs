using UnityEngine;

public class RotationToggle : MonoBehaviour
{
    private ScenarioRotator scenarioRotator;

    void Start()
    {
        scenarioRotator = FindObjectOfType<ScenarioRotator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Alterna o estado de rotação
            scenarioRotator.isRotationEnabled = true;
           
            Debug.Log("Scenario rotation toggled: " + scenarioRotator.isRotationEnabled);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Alterna o estado de rotação
            scenarioRotator.isRotationEnabled = false;
          
            Debug.Log("Scenario rotation toggled: " + scenarioRotator.isRotationEnabled);
        }
    }
}
