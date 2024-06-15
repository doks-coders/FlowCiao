using System.Linq;
using FlowCiao.Persistence.Providers.Rdbms;
using Microsoft.EntityFrameworkCore;

namespace FlowCiao.Studio.Persistence.Providers.Rdbms;

internal static class FlowCiaoDbInitializer
{
    public static void Initialize(FlowCiaoDbContext context)
    {
        context.Database.Migrate();
    }
}