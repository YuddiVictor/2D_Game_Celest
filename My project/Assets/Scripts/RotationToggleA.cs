using UnityEngine;

public class RotationToggleA : MonoBehaviour
{
    private ScenarioRotator scenarioRotatorA;

    void Start()
    {
        scenarioRotatorA = FindObjectOfType<ScenarioRotator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Alterna o estado de rotação
            scenarioRotatorA.isRotationAEnabled = true;

            Debug.Log("Scenario rotation A toggled: " + scenarioRotatorA.isRotationAEnabled);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Alterna o estado de rotação
            scenarioRotatorA.isRotationAEnabled = false;

            Debug.Log("Scenario rotation A toggled: " + scenarioRotatorA.isRotationAEnabled);
        }
    }
}
