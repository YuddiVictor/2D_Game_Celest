using UnityEngine;

public class ScenarioRotator : MonoBehaviour
{
    public float rotationSpeed = 200f;  // Velocidade da rotação
    private float targetRotationAngle = 0f;
    public bool isRotationEnabled = false;  // Controle de rotação
    public bool isRotationAEnabled = false;  // Controle de rotação
    private Transform playerTransform;  // Referência ao transform do jogador

    void Start()
    {
        // Encontra o jogador na cena
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isRotationEnabled)
        {
            // Calcula o centro da cena (média das posições dos objetos com a tag "Ground")
            Vector3 center = playerTransform.position;

            // Rotaciona suavemente o cenário em torno do centro da cena
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetRotationAngle, rotationSpeed * Time.deltaTime);
            transform.RotateAround(center, Vector3.forward, currentAngle - transform.eulerAngles.z);
        }

        if (isRotationAEnabled)
        {
            // Calcula o centro da cena (média das posições dos objetos com a tag "Ground")
            Vector3 center = playerTransform.position;

            // Rotaciona suavemente o cenário em torno do centro da cena
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetRotationAngle, rotationSpeed * Time.deltaTime);
            transform.RotateAround(center, Vector3.forward, currentAngle - transform.eulerAngles.z);
        }
    }

    public void RotateScene()
    {
        if (isRotationEnabled)
        {
            // Incrementa o ângulo de rotação desejado em 90°
            targetRotationAngle += 90f;
           if (targetRotationAngle >= 360f)
            {
                targetRotationAngle -= 360f;
            }
        }

        if (isRotationAEnabled)
        {
            // Incrementa o ângulo de rotação desejado em 90°
            targetRotationAngle -= 90f;
            if (targetRotationAngle >= 360f)
            {
                targetRotationAngle -= 360f;
            }
        }
    }
}
