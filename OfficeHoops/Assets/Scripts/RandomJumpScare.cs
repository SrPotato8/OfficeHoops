using UnityEngine;
using UnityEngine.UI;

public class RandomJumpScare : MonoBehaviour
{
    private float RandomNumber;
    public Image David;
    public Image Marc;
    public Image Fukun;
    public Image Adrian;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomNumber = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        switch (RandomNumber)
        {
            case 1:
                David.gameObject.SetActive(true); break;
                case 2:
                Marc.gameObject.SetActive(true);
                break;
                case 3:
                Adrian.gameObject.SetActive(true);
                break ;
                case 4:
                Fukun.gameObject.SetActive(true);
                break ;
        }
        

       
    }
}
