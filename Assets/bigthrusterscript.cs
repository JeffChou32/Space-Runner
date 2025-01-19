using UnityEngine;

public class bigthrusterscript : MonoBehaviour
{
    private ParticleSystem thrusterParticles;
    private float baseMinLifetime;
    private float baseMaxLifetime;
    private float baseSimulationSpeed;
    private float baseShapeRadius;
    
    void Start()
    {
        thrusterParticles = GetComponent<ParticleSystem>();
        baseMinLifetime = thrusterParticles.main.startLifetime.constantMin;
        baseMaxLifetime = thrusterParticles.main.startLifetime.constantMax;
        baseSimulationSpeed = thrusterParticles.main.simulationSpeed;
        baseShapeRadius = thrusterParticles.shape.radius;
        thrusterParticles.Stop();
    }
        
    void Update()
    {        
        if (shipscript.shipIsAlive == false)
        {
            gameObject.SetActive(false);
        }

        var main = thrusterParticles.main;
        var shape = thrusterParticles.shape;
        if (shipscript.multiplier > 3)
        {
            if (!thrusterParticles.isPlaying) thrusterParticles.Play();
            shape.radius = baseShapeRadius * shipscript.multiplier / 4;
            main.startLifetime = new ParticleSystem.MinMaxCurve(
            baseMinLifetime * shipscript.multiplier / 4,
            baseMaxLifetime * shipscript.multiplier / 4);
            main.simulationSpeed = baseSimulationSpeed * shipscript.multiplier / 4;
                     
        }
        else if (thrusterParticles.isPlaying) thrusterParticles.Stop();


    }
}
