using UnityEngine;
using UnityEngine.U2D;
 
public class SpriteShapeUpdateBounds : MonoBehaviour
{
    int boundsHash = 0;
    SpriteShapeController sc;
    SpriteShapeRenderer sr;
 
    void Start()
    {
        sc = gameObject.GetComponent<SpriteShapeController>();
        sr = gameObject.GetComponent<SpriteShapeRenderer>();
    }
 
    // Update is called once per frame
    void Update()
    {
        Bounds bounds = new Bounds();
        for (var i = 0; i < sc.spline.GetPointCount(); ++i)
            bounds.Encapsulate(sc.spline.GetPosition(i));
        bounds.Encapsulate(transform.position);
     
        if (boundsHash != bounds.GetHashCode())
        {
            sr.SetLocalAABB(bounds);
            boundsHash = bounds.GetHashCode();
        }
    }
}