namespace OrderManagementSystem
{
    using IocServiceStack;

    public static class IocServiceStackConfig
    {
        public static void Register()
        {
            /* IocServiceStack Documentation: https://rjinaga.github.io/IocServiceStack/
             * nuget: https://www.nuget.org/packages/IocServiceStack/
             * source code: https://github.com/rjinaga/IocServiceStack */

            var container = IocServicelet.Configure(config =>
            {
                config.AddServices((business) =>
                {
                    business.Assemblies = new[] { "Navtech.Oms.Business" };

                    business.AddDependencies((data) =>
                    {
                        data.Assemblies = new[] {
                            "Navtech.Oms.Communication",
                            "Navtech.Oms.DataValidators",
                            "Navtech.Oms.Data"
                        };
                    });
                    business.StrictMode = true;
                });
            });
        }
    }
}