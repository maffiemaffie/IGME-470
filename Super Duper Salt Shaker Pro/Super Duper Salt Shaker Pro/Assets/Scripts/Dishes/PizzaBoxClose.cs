using UnityEngine;

public class PizzaBoxClose : MonoBehaviour
{
    public void Awake()
    {
        GetComponent<Animator>().ResetTrigger("Close");
    }

    public void Close()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }
}
