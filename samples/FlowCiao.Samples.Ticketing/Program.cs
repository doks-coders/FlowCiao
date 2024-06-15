using FlowCiao;
using FlowCiao.Interfaces;
using FlowCiao.Operators;
using FlowCiao.Samples.Ticketing.Flow;
using FlowCiao.Samples.Ticketing.Flow.Models;

var builder = WebApplication.CreateBuilder(args);

// Add FlowCiao to services
try
{
	builder.Services.AddFlowCiao(settings =>
	{
		/*  Use Persist() if you want to persist the states of your Flows to your desired database 
			or ignore it and use InMemory caching as long as the application is alive */
		settings
			.Persist(persistenceSettings =>
			{
				persistenceSettings.UseSqlServer(builder.Configuration.GetConnectionString("FlowCiao"));
			});
	});

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

var app = builder.Build();

// Call UseFlowCiao() if you are using Persistence
app.UseFlowCiao();

using (var scope = app.Services.CreateScope()) {
    // Build your desired Flow
    var flowBuilder = scope.ServiceProvider.GetRequiredService<IFlowBuilder>();
    var flow = flowBuilder.Build<TicketingFlow>();
    
    // Call CiaoAndTriggerAsync() to both initialize it using Ciao() and run it using Trigger()
    var flowOperator = scope.ServiceProvider.GetService<IFlowOperator>();
    var result = flowOperator.CiaoAndTriggerAsync(flow.Key, Triggers.Created)
        .GetAwaiter().GetResult();
    Console.WriteLine(result.Message);
}

app.Run();