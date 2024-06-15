using FlowCiao.Models.Core;
using FlowCiao.Models.Execution;
using FlowCiao.Studio.Persistence.Providers.Rdbms;
using Microsoft.EntityFrameworkCore;


namespace FlowCiao.Studio.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<Transition> Transitions { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<FlowInstance> FlowInstances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.HasDefaultSchema("FlowCiao");

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.NoAction;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowCiaoDbInitializer).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
