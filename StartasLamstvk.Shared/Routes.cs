namespace StartasLamstvk.Shared
{
    public static class Routes
    {
        private const string BasePath = "/api";

        public static class Users
        {
            private const string basePath = "users";

            public const string Endpoint = BasePath + "/" + basePath;

            public static class UserId
            {
                public const string Endpoint = BasePath + "/" + basePath + "/" + Parameters.UserId;

                public static class Lasf
                {
                    private const string basePath = "lasf";

                    public static class LasfCategoryId
                    {
                        public const string Endpoint = BasePath + "/" + Users.basePath + "/" + Parameters.UserId + "/" +
                                                       basePath + "/" + Parameters.LasfCategoryId;
                    }
                }

                public static class Moto
                {
                    private const string basePath = "moto";

                    public static class MotoCategoryId
                    {
                        public const string Endpoint = BasePath + "/" + Users.basePath + "/" + Parameters.UserId + "/" +
                                                       basePath + "/" + Parameters.MotoCategoryId;
                    }
                }

                public static class Preference
                {
                    private const string basePath = "preference";

                    public const string Endpoint =
                        BasePath + "/" + Users.basePath + "/" + Parameters.UserId + "/" + basePath;

                    public static class PreferenceId
                    {
                        public const string Endpoint = BasePath + "/" + Users.basePath + "/" + Parameters.UserId + "/" +
                                                       basePath + "/" + Parameters.PreferenceId;
                    }

                    public static class RaceTypeId
                    {
                        public const string Endpoint = BasePath + "/" + Users.basePath + "/" + Parameters.UserId + "/" +
                                                       basePath + "/" + Parameters.RaceTypeId;
                    }
                }

                public static class RacePreference
                {
                    private const string basePath = "race-preference";

                    public const string Endpoint =
                        BasePath + "/" + Users.basePath + "/" + Parameters.UserId + "/" + basePath;

                    public static class PreferenceId
                    {
                        public const string Endpoint = BasePath + "/" + Users.basePath + "/" + Parameters.UserId + "/" +
                                                       basePath + "/" + Parameters.PreferenceId;
                    }
                }
            }
        }
    }
}