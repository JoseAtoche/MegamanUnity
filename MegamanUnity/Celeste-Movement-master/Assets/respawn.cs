using UnityEngine;

public class respawn : MonoBehaviour
{
    public GameObject dash1;
    public GameObject dash2;
    public GameObject dash3;

    public GameObject dashprefab;
    private Vector3 posiciondash1;
    private Vector3 posiciondash2;
    private Vector3 posiciondash3;

    private float time;

    // Start is called before the first frame update
    private void Start()
    {
        posiciondash1 = dash1.transform.position;
        posiciondash2 = dash2.transform.position;
        posiciondash3 = dash3.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (dash1 == null || dash2 == null || dash3 == null)
        {
            time += Time.deltaTime;

            if (time >= 5)
            {
                if (dash1 == null)
                {
                    dash1 = Instantiate(dashprefab, posiciondash1, new Quaternion(0, 0, 0, 0));
                }

                if (dash2 == null) { dash2 = Instantiate(dashprefab, posiciondash2, new Quaternion(0, 0, 0, 0)); }

                if (dash3 == null) { dash3 = Instantiate(dashprefab, posiciondash3, new Quaternion(0, 0, 0, 0)); }

                time -= 5;
            }
        }
    }
}