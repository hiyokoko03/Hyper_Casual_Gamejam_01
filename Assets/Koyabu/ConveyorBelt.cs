using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorSpeed = 3.0f;  // —¬‚ê‚é‘¬“x


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
