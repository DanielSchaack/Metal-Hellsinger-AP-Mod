using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Randomizer
{
    public static class AssetUtil
    {
        public static T GetAssetByName<T>(string assetName)
            where T : Object
        {
            var assets = Resources.FindObjectsOfTypeAll<T>();

            foreach (var asset in assets)
            {
                if (asset != null && asset.name == assetName)
                {
                    return asset;
                }
            }

            return null;
        }
    }

    public static class ImageCache
    {
        private static readonly Dictionary<string, Image> Cache = new();

        public static void RefreshCache()
        {
            Cache.Clear();
            var assets = Resources.FindObjectsOfTypeAll<Image>();

            foreach (var asset in assets)
            {
                if (asset != null && !Cache.ContainsKey(asset.name))
                {
                    Cache[asset.name] = asset;
                }
            }
        }

        public static Image Get(string name)
        {
            if (Cache.TryGetValue(name, out var asset) && asset != null)
            {
                return asset;
            }

            asset = AssetUtil.GetAssetByName<Image>(name);
            if (asset != null)
            {
                Cache[name] = asset;
            }

            return asset;
        }
    }

    public static class SpriteCache
    {
        private static readonly Dictionary<string, Sprite> Cache = new();

        public static void RefreshCache()
        {
            Cache.Clear();
            var assets = Resources.FindObjectsOfTypeAll<Sprite>();

            foreach (var asset in assets)
            {
                if (asset != null && !Cache.ContainsKey(asset.name))
                {
                    Cache[asset.name] = asset;
                }
            }
        }

        public static Sprite Get(string name)
        {
            if (Cache.TryGetValue(name, out var asset) && asset != null)
            {
                return asset;
            }

            asset = AssetUtil.GetAssetByName<Sprite>(name);
            if (asset != null)
            {
                Cache[name] = asset;
            }

            return asset;
        }
    }

    public static class WeaponDataConfigurationCache
    {
        private static readonly Dictionary<string, WeaponDataConfiguration> Cache = new();

        public static void RefreshCache()
        {
            Cache.Clear();
            var assets = Resources.FindObjectsOfTypeAll<WeaponDataConfiguration>();

            foreach (var asset in assets)
            {
                if (asset != null && !Cache.ContainsKey(asset.name))
                {
                    Cache[asset.name] = asset;
                }
            }
        }

        public static WeaponDataConfiguration Get(string name)
        {
            if (Cache.TryGetValue(name, out var asset) && asset != null)
            {
                return asset;
            }

            asset = AssetUtil.GetAssetByName<WeaponDataConfiguration>(name);
            if (asset != null)
            {
                Cache[name] = asset;
            }

            return asset;
        }
    }

    public static class RandomVoDataCache
    {
        private static readonly Dictionary<string, RandomVoData> Cache = new();

        public static void RefreshCache()
        {
            Cache.Clear();
            var assets = Resources.FindObjectsOfTypeAll<RandomVoData>();

            foreach (var asset in assets)
            {
                if (asset != null && !Cache.ContainsKey(asset.name))
                {
                    Cache[asset.name] = asset;
                }
            }
        }

        public static RandomVoData Get(string name)
        {
            if (Cache.TryGetValue(name, out var asset) && asset != null)
            {
                return asset;
            }

            asset = AssetUtil.GetAssetByName<RandomVoData>(name);
            if (asset != null)
            {
                Cache[name] = asset;
            }

            return asset;
        }
    }

    public static class ChallengeVoDataCache
    {
        private static readonly Dictionary<string, ChallengeVoData> Cache = new();

        public static void RefreshCache()
        {
            Cache.Clear();
            var assets = Resources.FindObjectsOfTypeAll<ChallengeVoData>();

            foreach (var asset in assets)
            {
                if (asset != null && !Cache.ContainsKey(asset.name))
                {
                    Cache[asset.name] = asset;
                }
            }
        }

        public static ChallengeVoData Get(string name)
        {
            if (Cache.TryGetValue(name, out var asset) && asset != null)
            {
                return asset;
            }

            asset = AssetUtil.GetAssetByName<ChallengeVoData>(name);
            if (asset != null)
            {
                Cache[name] = asset;
            }

            return asset;
        }
    }
}
