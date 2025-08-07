using UnityEngine;

public class centralizar : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
       transform.position += (transform.parent.position - transform.position) * 5 * Time.deltaTime;   
    }
}
