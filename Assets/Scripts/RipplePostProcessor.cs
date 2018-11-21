using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
    public Material RippleMaterial;
    public float MaxAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;

    void Update()
    {
    
        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }

    public void MakeRipple()
    {
        this.Amount = this.MaxAmount;
        this.RippleMaterial.SetFloat("_CenterX", transform.position.x);
        this.RippleMaterial.SetFloat("_CenterY", transform.position.y);

    }
}
