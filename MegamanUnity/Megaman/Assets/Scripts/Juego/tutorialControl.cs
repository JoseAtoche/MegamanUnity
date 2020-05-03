using UnityEngine;

public class tutorialControl : MonoBehaviour
{
    private bool primera = false;
    public GameObject flecha;
    public GameObject tutorial1;
    public GameObject tutorial2;

    /// <summary>
    /// Si tocas el botón cambia al siguiente tutorial
    /// </summary>
    private void Update()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
        tutorial1.SetActive(true);
        flecha.SetActive(true);

        if (Input.GetButtonDown("Fire3") && !primera)
        {
            if (!primera)
            {
                primera = true;
                flecha.SetActive(false);
            }
            tutorial2.SetActive(true);
            flecha.SetActive(true);
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
            this.gameObject.SetActive(false);
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
            flecha.SetActive(false);
        }
    }
}