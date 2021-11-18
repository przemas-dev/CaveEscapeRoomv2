
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Sprite SpriteCrosshairOn;
    public Sprite SpriteCrosshairOff;
    public Canvas PadlockCanvas;
    
    private SpriteRenderer _spriteRenderer;
    private bool _isCarrying;
    private GameObject _carriedObject;

   
    private Camera _camera;


    private GameObject _prevObservedObject;


    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _spriteRenderer = _camera.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   


    private bool IsPickable(GameObject gameObject)
    {
        return gameObject.GetComponent<IPickable>() != null;
    }



    public void CrosshairOn()
    {
        _spriteRenderer.sprite = SpriteCrosshairOn;
    }

    public void CrosshairOff()
    {
        _spriteRenderer.sprite = SpriteCrosshairOff;
    }

    public void SetCrossHairColor(Color color)
    {
        _spriteRenderer.color = color;
    }
    

    public void SetUpCrosshair(GameObject observedObject)
    {
        if (_prevObservedObject == observedObject) return;
        
        if (observedObject != null && IsPickable(observedObject))
        {
            CrosshairOn();
        }
        else
        {
            CrosshairOff();
        }
        _prevObservedObject = observedObject;
    }

    public void EnablePadlockCanvas()
    {
        PadlockCanvas.enabled = true;
    }

    public void DisablePadlockCanvas()
    {
        PadlockCanvas.enabled = false;
    }
}
