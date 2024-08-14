namespace AutonomyApi
{
    public static class Constants
    {
        public static class Name
        {
            public const int MinLength = 2;
            public const int MaxLength = 60;
        }

        public static class Description
        {
            public const int MinLength = 0;
            public const int MaxLength = 500;
        }

        public static class Password
        {
            public const int MinLength = 8;
            public const int MaxLength = 72;
            public const int HashLength = 60;
        }

        public static class Document
        {
            public const int MinLength = 3;
            public const int MaxLength = 20;
        }
    }
}
