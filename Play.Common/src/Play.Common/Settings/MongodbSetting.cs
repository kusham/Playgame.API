﻿namespace Play.Common.Settings
{
    public class MongodbSetting
    {
        public string Host { get; init; }
        public int Port { get; init; }

        public string ConnectionString
        {
            get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }
    }
}
