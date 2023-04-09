using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class RotationKeeper : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public List<int> rotations = new List<int>();

    private string key = "image1RotatingPuzzle";

    AsyncOperationHandle<Texture2D> targetImageHandle;

    // Start is called before the first frame update
    void Start()
    {
        StartButtons();
    }

    public void StartButtons()
    {
        buttons = FindObjectsOfType<Button>().ToList();
        buttons = buttons.OrderBy(go => go.name).ToList();
        for (int i = 0; i < buttons.Count; i++)
        {
            rotations.Add(0);
        }

        Addressables.LoadAssetAsync<Texture2D>("image1RotatingPuzzle").Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Addressables.InstantiateAsync(key);
                Debug.Log("Loaded key for image");
                Texture2D[] splitTextures = new Texture2D[9];

                

                Texture2D originalImage = op.Result;


// Calculate the width and height of each split
                int splitWidth = originalImage.width / 3;
                int splitHeight = originalImage.height / 3;

// Loop through each split and extract its pixels from the original texture
                for (int i = 0; i < 9; i++)
                {
                    int x = (i % 3) * splitWidth;
                    int y = (i / 3) * splitHeight;
                    Color[] pixels = originalImage.GetPixels(x, y, splitWidth, splitHeight);

                    // Create a new texture for the split and set its pixels
                    Texture2D splitTexture = new Texture2D(splitWidth, splitHeight);
                    splitTexture.SetPixels(pixels);
                    splitTexture.Apply();

                    // Add the split texture to the arr

                    splitTextures[i] = splitTexture;

                    for (int f = 0; f < splitTextures.Length; f++)
                    {
                        buttons[f].GetComponent<SpriteRenderer>().sprite = Sprite.Create(splitTextures[f],
                            new Rect(0, 0, splitTextures[f].width, splitTextures[f].height), new Vector2(0.5f, 0.5f));
                    }
                }
            }
        };
    }
}