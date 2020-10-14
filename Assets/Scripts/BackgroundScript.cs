using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

    public void SetSky(GameObject sky)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject instance = Instantiate(sky, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(transform);
    }
}
