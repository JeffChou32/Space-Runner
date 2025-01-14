using UnityEngine;

public class starfieldscript : MonoBehaviour
{
    public ParticleSystem starfield;
    public float baseStartSpeed = 5;
    private Vector3 originalScale;
    public int mult=1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (starfield == null)
        {
            starfield = GetComponent<ParticleSystem>();
        }
        var mainModule = starfield.main;
        mainModule.startSpeed = baseStartSpeed;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (shipscript.boost) mult = shipscript.multiplier * 2;
        else mult = 1;
        var mainModule = starfield.main;
        mainModule.startSpeed = baseStartSpeed * shipscript.multiplier;
        
        transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y * mult,
            originalScale.z);
        
    }
}
