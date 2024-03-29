#pragma checksum "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\Pages\FetchData.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6e34d6aea10ddb26c5e18b5dfd9f58a419870aaa"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Blazor.Server.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using Blazor.Server;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\_Imports.razor"
using Blazor.Server.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\Pages\FetchData.razor"
using System.Threading;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\Pages\FetchData.razor"
using Blazor.Server.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\Pages\FetchData.razor"
using Blazor.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\Pages\FetchData.razor"
using Microsoft.Extensions.Caching.Memory;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/fetchdata")]
    public class FetchData : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 53 "C:\_TFS\PSDev\CDS\CDS\Spikes\DougM\Edmug\2019-11-13\Blazor\Blazor.Server\Pages\FetchData.razor"
       
    EventLog[] _eventLogs;
    private bool AutoUpdate { get; set; } = true;
    private Timer _timer;
    private int _cacheDelay;
    private int _cacheSize;

    private int CacheSize {
        get => _cacheSize;
        set {
            _cacheSize = value;
            Cache.Set(EventLogWorkerConstants.EventLogCacheSize, _cacheSize);
        }
    }

    private int CacheDelay {
        get => _cacheDelay;
        set {
            _cacheDelay = value;
            Cache.Set(EventLogWorkerConstants.EventLogCacheDelay, _cacheDelay);
            _timer?.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(_cacheDelay));
        }
    }

    private void RefreshLogs()
    {
        Cache.TryGetValue(EventLogWorkerConstants.EventLogCache, out _eventLogs);
    }

    private void StartTimer()
    {
        _timer = new Timer(new TimerCallback(_ =>
         {
             if (!AutoUpdate) return;

             Cache.TryGetValue(EventLogWorkerConstants.EventLogCache, out _eventLogs);

         //Inform blazor that the UI has changed
         InvokeAsync(StateHasChanged);
         }), null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(_cacheDelay));
    }

    protected override void OnInitialized()
    {
        CacheSize = 10;
        CacheDelay = 3;
        StartTimer();
    }

    public void Dispose()
    {
        //Prevent the timer from executing;
        _timer?.Change(Timeout.Infinite, Timeout.Infinite);

        //Should probably configure a WaitHandle for disposing timer.
        //_timer?.Dispose(MyWaitHandle);
        _timer?.Dispose();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMemoryCache Cache { get; set; }
    }
}
#pragma warning restore 1591
