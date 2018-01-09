// Used for versioning purposes
namespace Strike2D
{
    public static class Manifest
    {
        public static readonly string Version = "0.0.5 Alpha";
        public const string Release = "Release";
        public const string Develop = "Develop";
        
        #if DEBUG
            public static readonly string Environment = Develop;
        #else
            public static readonly string Environment = Release;
        #endif
    }
}