using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

public class thrusterscript : MonoBehaviour
{
    private ParticleSystem thrusterParticles;
    private float baseMinLifetime; 
    private float baseMaxLifetime;
    private float baseSimulationSpeed;
    private float baseShapeRadius;    
    public Color customBlue = new Color(0f, 78f/255f, 188f/255f, 1f);
    public Color customOrange = new Color(0f, 78f / 255f, 188f / 255f, 1f);

    void Start()
    {
        thrusterParticles = GetComponent<ParticleSystem>();
        baseMinLifetime = thrusterParticles.main.startLifetime.constantMin;
        baseMaxLifetime = thrusterParticles.main.startLifetime.constantMax;
        baseSimulationSpeed = thrusterParticles.main.simulationSpeed;
        baseShapeRadius = thrusterParticles.shape.radius;  
    }
       
    void Update()
    {
        if (shipscript.shipIsAlive == false)
        {
            gameObject.SetActive(false);
        }
        
        var main = thrusterParticles.main;
        main.startLifetime = new ParticleSystem.MinMaxCurve(
            baseMinLifetime * shipscript.multiplier, 
            baseMaxLifetime * shipscript.multiplier);

        var shape = thrusterParticles.shape;
        if (shipscript.multiplier > 1) shape.radius = baseShapeRadius * shipscript.multiplier/2;        
        
        main.simulationSpeed = baseSimulationSpeed * shipscript.multiplier;

        var colorOverLifetime = thrusterParticles.colorOverLifetime;
        Gradient gradient = new Gradient();        
        if (shipscript.multiplier > 3)
        {            
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(Color.white, 0f), 
                    new GradientColorKey(Color.black, 1f)},
                new GradientAlphaKey[] {new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f)}
                );
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
        }
        else if (shipscript.multiplier > 2)
        {            
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(Color.white, 0f), 
                    new GradientColorKey(customBlue, 0.5f),
                    new GradientColorKey(Color.black, 1f)}, 
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f)}
                );
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
        } else
        {
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(Color.yellow, 0f), 
                    new GradientColorKey(customOrange, 0.5f),
                new GradientColorKey(Color.black, 1f)
                },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
            );
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
        }      
    }
}
