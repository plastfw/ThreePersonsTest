using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using VFavorites.Libs;

public static class AddressablesDependencyChecker
{
    [MenuItem("Tools/Check Addressables Dependencies")]
    private static void CheckDependencies()
    {
        var keys = Addressables.ResourceLocators
            .SelectMany(x => x.Keys)
            .Distinct()
            .ToList();

        var found = new HashSet<string>();

        foreach (var key in keys)
        {
            Addressables.LoadResourceLocationsAsync(key).WaitForCompletion()
                .ForEach(loc => CollectDependencies(loc, found));
        }

        foreach (var path in found.OrderBy(x => x))
            if (!path.StartsWith("Packages/") && !path.StartsWith("Assets/TextMesh Pro/Resources/"))
                Debug.Log(path);
    }

    private static void CollectDependencies(IResourceLocation location, HashSet<string> paths)
    {
        if (location.Data is string path &&
            (path.EndsWith(".asset") || path.EndsWith(".prefab") || path.EndsWith(".shader")))
            paths.Add(path);

        if (location.Dependencies != null)
            foreach (var dep in location.Dependencies)
                CollectDependencies(dep, paths);
    }
}