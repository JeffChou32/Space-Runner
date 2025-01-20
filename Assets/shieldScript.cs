using UnityEngine;

public class shieldScript : MonoBehaviour
{
    private ParticleSystem shield;
    public Color customBlue = new Color(0f, 78f / 255f, 188f / 255f, 1f);
    public Color customOrange = new Color(0f, 78f / 255f, 188f / 255f, 1f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shield = GetComponent<ParticleSystem>();
        shield.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (shipscript.shipIsAlive == false)
        {
            gameObject.SetActive(false);
        }
        //if (shipscript.multiplier == 2)
        //{
        //    if (!shield.isPlaying) shield.Play();
        //} else if (shipscript.multiplier == 3)
        //{
        //    if(!shield.isPlaying) shield.Play();
        //    ParticleSystem.color
        //}

        var colorOverLifetime = shield.colorOverLifetime;
        Gradient gradient = new Gradient();
        if (shipscript.multiplier > 3)
        {
            if (!shield.isPlaying) shield.Play();
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(Color.white, 0f),                    
                    new GradientColorKey(Color.black, 1f)},
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
                );
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
        }
        else if (shipscript.multiplier == 3)
        {
            if (!shield.isPlaying) shield.Play();
            gradient.SetKeys(
                new GradientColorKey[] {                    
                    new GradientColorKey(customBlue, 0f),
                    new GradientColorKey(Color.black, 1f)},
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
                );
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
        }
        
        else if (shipscript.multiplier == 2)
        {
            if (!shield.isPlaying) shield.Play();
            gradient.SetKeys(
                new GradientColorKey[] {                    
                    new GradientColorKey(customOrange, 0),
                    new GradientColorKey(Color.black, 1f)
                },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
            );
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
        }    
        else if (shipscript.multiplier == 1) if (shield.isPlaying) shield.Stop();
    }
}
