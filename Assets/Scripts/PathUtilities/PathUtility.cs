using UnityEditor;
using System.IO;
using UnityEngine;

public static class PathUtility
{
    public static string GenerateUniqueAssetPath(string folderPath, string assetName, AssetType assetType)
    {
        string extension = GetExtensionForAssetType(assetType);
        folderPath = EnsureFolderPathEndsWithSlash(folderPath);
        string initialPath = $"{folderPath}{assetName}{extension}";
        return AssetDatabase.GenerateUniqueAssetPath(initialPath);
    }

    private static string GetExtensionForAssetType(AssetType assetType)
    {
        switch (assetType)
        {
            case AssetType.AnimationClip:
                return ".anim";
            case AssetType.Texture:
                return ".png"; // or ".asset" depending on how you save textures
            case AssetType.Material:
                return ".mat";
            default:
                throw new System.ArgumentOutOfRangeException(nameof(assetType), $"Unsupported asset type: {assetType}");
        }
    }

    private static string EnsureFolderPathEndsWithSlash(string folderPath)
    {
        if (!folderPath.EndsWith("/"))
        {
            folderPath += "/";
        }

        return folderPath;
    }

    public static void ValidateOrCreateDirectory(string folderPath)
    {
        folderPath = EnsureFolderPathEndsWithSlash(folderPath);
        
        string systemPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", folderPath));

        if (!Directory.Exists(systemPath))
        {
            Directory.CreateDirectory(systemPath);
            AssetDatabase.Refresh();
        }
    }
}

public enum AssetType
{
    AnimationClip,
    Texture,
    Material,
}