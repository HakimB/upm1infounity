using UnityEngine;
using System;

public class CameraSpherique : ICloneable
{
    private Camera cameraUnity;
    
    private float rayon = 10f;
    private float theta = 0f; // Angle azimutal (en degrés)
    private float phi = 45f;  // Angle polaire (en degrés)
    
    private Vector3 centre = Vector3.zero;

    public CameraSpherique(Camera cam)
    {
        cameraUnity = cam;
        if (cameraUnity != null)
        {
            if(!cam.name.StartsWith("Main")) {
                rayon = UnityEngine.Random.Range(5f, 15f);
                theta = UnityEngine.Random.Range(0f, 360f);
                phi = UnityEngine.Random.Range(1f, 179f);
            }
            DeplacerCamera();
        }
    }

    // Constructeur de copie
    public CameraSpherique(CameraSpherique autre)
    {
        if (autre != null)
        {
            this.cameraUnity = autre.cameraUnity;
            this.rayon = autre.rayon;
            this.theta = autre.theta;
            this.phi = autre.phi;
            this.centre = autre.centre;
        }
    }    

    private void Update()
    {
        if (IsActive())
        {
            DeplacerCamera();
        }
    }

    public void DeplacerCamera()
    {
        float thetaRad = theta * Mathf.Deg2Rad;
        float phiRad = phi * Mathf.Deg2Rad;

        float x = rayon * Mathf.Sin(phiRad) * Mathf.Cos(thetaRad);
        float y = rayon * Mathf.Cos(phiRad);
        float z = rayon * Mathf.Sin(phiRad) * Mathf.Sin(thetaRad);

        cameraUnity.transform.position = centre + new Vector3(x, y, z);
        cameraUnity.transform.LookAt(centre);
    }

    public void enable() { cameraUnity.enabled = true; }
    public void disable() { cameraUnity.enabled = false; }

    public bool IsActive() {
        return cameraUnity.enabled;
    }

    public void SetCoordonneesSperiques(float r, float t, float p)
    {
        rayon = r;
        theta = t;
        phi = p;
    }

    public void SetCentre(Vector3 newCentre) { centre = newCentre; }

    public void moveUp(float delta) { phi = Mathf.Clamp(phi - delta, 1f, 179f); }
    public void moveDown(float delta) { phi = Mathf.Clamp(phi + delta, 1f, 179f); }
    public void moveLeft(float delta) { theta += delta; }
    public void moveRight(float delta) { theta -= delta; }

    // Implémentation de ICloneable
    public object Clone()
    {
        return new CameraSpherique(this);
    }

    // Alternative : cloner avec une caméra différente
    public CameraSpherique CloneWithCamera(Camera newCamera)
    {
        CameraSpherique copie = new CameraSpherique(newCamera);
        copie.rayon = this.rayon;
        copie.theta = this.theta;
        copie.phi = this.phi;
        copie.centre = this.centre;
        return copie;
    }


    public  static CameraSpherique Lerp(CameraSpherique a, CameraSpherique b, float t)
    {
        if(t < 0f) t = 0f;
        if(t > 1f) t = 1f;
        CameraSpherique result = new CameraSpherique(b.cameraUnity);
        result.rayon = Mathf.Lerp(a.rayon, b.rayon, t);
        result.theta = Mathf.Lerp(a.theta, b.theta, t);
        result.phi = Mathf.Lerp(a.phi, b.phi, t);
        result.centre = Vector3.Lerp(a.centre, b.centre, t);
        return result;
    }

}