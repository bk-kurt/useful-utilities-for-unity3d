using UnityEditor;
using UnityEngine;


public static class AnimationClipUtility
{
    public static void CopyAnimationClips(GameObject sourceGameObject, GameObject targetGameObject,
        string assetFolderPath)
    {
        if (sourceGameObject == null || targetGameObject == null)
        {
            Debug.LogError("Source or Target GameObject is null.");
            return;
        }

        var sourceAnimator = sourceGameObject.GetComponent<Animator>();
        var targetAnimator = targetGameObject.GetComponent<Animator>();

        if (sourceAnimator == null || targetAnimator == null)
        {
            Debug.LogError("Animator component missing in one of the GameObjects.");
            return;
        }

        if (sourceAnimator.runtimeAnimatorController == null)
        {
            Debug.LogError("Source Animator does not have a RuntimeAnimatorController set.");
            return;
        }
        
        PathUtility.ValidateOrCreateDirectory(assetFolderPath);


        foreach (AnimationClip clip in sourceAnimator.runtimeAnimatorController.animationClips)
        {
            AnimationClip newClip = new AnimationClip();
            EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);

            foreach (EditorCurveBinding binding in curveBindings)
            {
                AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
                newClip.SetCurve(binding.path, binding.type, binding.propertyName, curve);
            }
            
            string assetPath = PathUtility.GenerateUniqueAssetPath(assetFolderPath, clip.name, AssetType.AnimationClip);
            AssetDatabase.CreateAsset(newClip, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Animation clips copied successfully.");
    }
}