using System.Linq;
using FlowCiao.Studio.Data;
using Microsoft.EntityFrameworkCore;

namespace FlowCiao.Studio.Persistence.Providers.Rdbms;

internal static class FlowCiaoDbInitializer
{
    public static void Initialize(DataContext context)
    {
        context.Database.Migrate();
    }
}