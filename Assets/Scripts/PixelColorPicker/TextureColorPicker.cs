using UnityEngine;

public class TextureColorPicker : IColorPicker
{
    private readonly Texture2D _texture;

    public TextureColorPicker(Texture2D texture)
    {
        if (texture == null)
        {
            throw new System.ArgumentNullException(nameof(texture), "Texture must not be null.");
        }

        _texture = texture;
    }

    public Color PickColor(Vector2 position)
    {
        if (!_texture.isReadable)
        {
            Debug.LogError("Texture is not readable. Ensure 'Read/Write Enabled' is set in the texture import settings.");
            return Color.clear;
        }

        int x = Mathf.Clamp((int)position.x, 0, _texture.width - 1);
        int y = Mathf.Clamp((int)position.y, 0, _texture.height - 1);
        return _texture.GetPixel(x, y);
    }
}