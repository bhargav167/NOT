using UnityEngine;

enum CharterToFind
{
    Police,
    Tero

}
public class Splat : MonoBehaviour
{
    public enum SplatLocation
    {
        Foreground,
        Background
    }
    public Color backgroundTint;
    public SpriteRenderer SpriteRenderer;
    public float minSizeMod=0.3f;
    public float maxSizeMod=0.5f;
    public Sprite[] sprites;
    private SplatLocation splatLocation;
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    public void Initilize(SplatLocation splatLocation){
        this.splatLocation=splatLocation;
        setSprite();
        setSize();
        setRotation();
        setLocationProperties();
    }
    private void setSprite(){
        int randomIndex=Random.Range(0,sprites.Length);
        spriteRenderer.sprite=sprites[randomIndex];
    }
    private void setSize(){
        float sizemod=Random.Range(minSizeMod,maxSizeMod);
        transform.localScale *= sizemod;
    }
    private void setRotation(){
        float randomRotation = Random.Range(-360f, 360f);
        transform.rotation=Quaternion.Euler(0f,0f,randomRotation);
    }
    private void setLocationProperties(){
        switch(splatLocation){
            case SplatLocation.Background:
                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                spriteRenderer.color=backgroundTint;
            spriteRenderer.sortingOrder=0;
            break;

            case SplatLocation.Foreground:
            spriteRenderer.maskInteraction=SpriteMaskInteraction.VisibleInsideMask;
            spriteRenderer.sortingOrder=146;
            break;
        }
    }
    private void FixedUpdate()
    {
        if (this.transform.localScale.y <= 0.90){
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y + 0.1f * Time.deltaTime, this.transform.localScale.z);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.1f * Time.deltaTime);
        }
    }
}