namespace LogGateway.Extenssions
{
    public static class ConfigurationExtenssions
    {
        public static IConfigurationBuilder UseDotEnv(this IConfigurationBuilder builder, string envFilePath)
        {
            return builder.Add(new DotEnvConfigProvider(envFilePath));
        }
    }
}