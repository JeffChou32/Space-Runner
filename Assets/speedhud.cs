using UnityEngine;
using UnityEngine.UI;

public class speedhud : MonoBehaviour
{
    public static int boosts = 0;    

    public Image[] speedBoosts;
    public Sprite speedBoost;

    private void Start()
    {
        boosts = 0;
    }
    private void Update()
    {
       for (int i=0; i<speedBoosts.Length; i++ )
        {
            if (i<boosts) speedBoosts[i].enabled = true;
            else speedBoosts[i].enabled = false;
        }
    }
}
