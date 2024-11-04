using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace WebAPP.Layout
{
    public partial class Appbar
    {
        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }

    }
}