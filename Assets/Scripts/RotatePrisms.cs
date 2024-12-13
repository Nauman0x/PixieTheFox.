using UnityEngine;
using System.Collections;

public class RotatePrisms : MonoBehaviour
{
    public GameObject prism1; 
    public GameObject prism2;
    public GameObject prism3;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    public GameObject panel; 
    public AudioSource rotationSound; 

    public Animator animator; 

    private float[] rotationAngles = new float[] {-112f, -300f, -18f };

    private int prism1AngleIndex = 0;
    private int prism2AngleIndex = 0;
    private int prism3AngleIndex = 0;

    public void RotatePrism1()
    {
        RotatePrism(prism1, ref prism1AngleIndex);
    }

    public void RotatePrism2()
    {
        RotatePrism(prism2, ref prism2AngleIndex);
    }

    public void RotatePrism3()
    {
        RotatePrism(prism3, ref prism3AngleIndex);
    }

    private void RotatePrism(GameObject prism, ref int angleIndex)
    {
        if (prism == prism2)
        {
            prism.transform.rotation = Quaternion.Euler(0f, -180f, rotationAngles[angleIndex]);
        }
        else
        {
            prism.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngles[angleIndex]);
        }

        angleIndex = (angleIndex + 1) % rotationAngles.Length;

        if (Mathf.Abs(prism1.transform.rotation.eulerAngles.z - 342f) < 1f &&
            Mathf.Abs(prism2.transform.rotation.eulerAngles.z - 342f) < 1f &&
            Mathf.Abs(prism3.transform.rotation.eulerAngles.z - 342f) < 1f)
        {
            Debug.Log("All prisms are aligned! Playing animation and waiting 3 seconds...");

            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);

            if (animator != null)
            {
                Debug.Log("Triggering animation: Item-feedback-Animation");
                animator.SetTrigger("isAlignment"); 
            }

            if (rotationSound != null)
            {
                rotationSound.Play();
            }

            StartCoroutine(ShowPanelAfterDelay());
        }
    }

    private IEnumerator ShowPanelAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        panel.SetActive(true);
    }
}
