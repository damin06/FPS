using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ssss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("a", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void a()
    {
        gameObject.SetActive(false);
    }
}
