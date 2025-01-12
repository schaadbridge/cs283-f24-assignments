using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Dependencies.Sqlite;

public class ColorPickerControl : MonoBehaviour
{
    public float currentHue, currentSat, currentVal;
    [SerializeField]
    private RawImage hueImage, satValImage, outputImage;

    [SerializeField]
    private Slider hueSlider;

    [SerializeField]
    private Texture2D objectTexture;

    private Texture2D hueTexture, svTexture, outputTexture;

    [SerializeField]
    private SkinnedMeshRenderer changeThisColor;

    [SerializeField]
    private Material changeThisMaterial;

    [SerializeField]
    private bool customTexture;


    // Start is called before the first frame update
    void Start()
    {
        CreateHueImage();
        CreateSVImage();
        CreateOutputImage();

        UpdateOutputImage();
    }

    private void CreateHueImage()
    {
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++)
        {
            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 1));
        }

        hueTexture.Apply();
        currentHue = 0;

        hueImage.texture = hueTexture;
    }

    private void CreateSVImage()
    {
        svTexture = new Texture2D(16, 16);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SatValTexture";

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(
                    currentHue,
                    (float)x / svTexture.width,
                    (float)y / svTexture.height));
            }
        }

        svTexture.Apply();
        currentSat = 0;
        currentVal = 0;

        satValImage.texture = svTexture;
    }

    private void CreateOutputImage()
    {
        outputTexture = new Texture2D(1, 16);
        outputTexture.wrapMode = TextureWrapMode.Clamp;
        outputTexture.name = "HueTexture";

        Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColor);
        }

        hueTexture.Apply();

        outputImage.texture = outputTexture;
    }

    private void CreateObjectImage()
    {
        for (int i = 0; i < objectTexture.height; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                // there should be six different colors of increasing value
                objectTexture.SetPixel(j, i, Color.HSVToRGB(currentHue, currentSat, (float)(10-j / 2) / 5 * currentVal));
            }
        }

        objectTexture.Apply();
        changeThisMaterial.mainTexture = objectTexture;
    }

    private void UpdateOutputImage()
    {
        Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColor);
        }

        outputTexture.Apply();

        if (customTexture)
        {
            CreateObjectImage();
        }
        else
        {
            changeThisColor.GetComponent<SkinnedMeshRenderer>().material.color = currentColor;
        }
        MainManager.Instance.TeamColor = currentColor;
    }

    public void SetSV(float s, float v)
    {
        currentSat = s;
        currentVal = v;

        UpdateOutputImage();
    }

    public void UpdateSVImage()
    {
        currentHue = hueSlider.value;

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(
                    currentHue,
                    (float)x / svTexture.width,
                    (float)y / svTexture.height));
            }
        }

        svTexture.Apply();

        UpdateOutputImage();
    }

}
