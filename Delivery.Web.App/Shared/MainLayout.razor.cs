using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class MainLayout
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
    }
}
