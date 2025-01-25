using UnityEngine;
using UnityEngine.UI;

public class speedtextscript : MonoBehaviour
{
    public Text speedText;
    private Color defaultColor;
    void Start()
    {
        defaultColor = speedText.color;
    }

    void Update()
    {        
        if (shipscript.multiplier == 2)
        {
            Color color = new Color(216f / 255f, 121f / 255f, 26f / 255f, 1);
            speedText.color = color;
                
        }
        else if (shipscript.multiplier == 3)
        {
            Color color = new Color(79f / 255f, 153f / 255f, 207f / 255f, 1);
            speedText.color = color;
                
        }
        else if (shipscript.multiplier > 3)
        {
            Color color = new Color(202f / 255f, 230f / 255f, 255f / 255f, 1);
            speedText.color = color;
                
        } else
        {
            speedText.color = defaultColor;
        }
            
        
    }
}
