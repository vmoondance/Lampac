using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Shared.Models;

namespace Shared
{
    public class Startup
    {
        public static bool IsShutdown { get; set; }

        public static INws Nws { get; set; }

        public static ISoks WS { get; set; }

        public static AppReload appReload { get; private set; }

        public static IServiceProvider ApplicationServices { get; private set; }

        public static IMemoryCache memoryCache { get; private set; }

        public static void Configure(AppReload reload, IApplicationBuilder app, IMemoryCache mem, INws nws, ISoks ws)
        {
            appReload = reload;
            Nws = nws;
            WS = ws;
            ApplicationServices = app.ApplicationServices;
            memoryCache = mem;
        }
    }


    public class AppReload
    {
        IHost _host;

        public AppReload(IHost _host)
        {
            this._host = _host;
        }

        public static bool _reload { get; set; } = true;

        public void Reload()
        {
            _reload = true;
            _host.StopAsync();
            AppInit.LoadModules();
        }
    }
}
