using FlowCiao;
using FlowCiao.Interfaces;
using FlowCiao.Operators;
using FlowCiao.Samples.Phone.Flow;
using FlowCiao.Samples.Phone.Flow.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);


// Add FlowCiao to services

builder.Services.AddFlowCiao(settings =>
{
    /* Use Persist() if you want to persist the states of your Flows to your desired database
        or ignore it and use InMemory caching as long as the application is alive */
    settings
        .Persist(persistenceSettings =>
        {
            persistenceSettings.UseSqlServer(builder.Configuration.GetConnectionString("FlowCiao"));
        });
});

/*
var connection = builder.Configuration.GetConnectionString("FlowCiao");
builder.Services.AddDbContext<ApplicationDbContext>(u => u.UseSqlServer(connection));
*/
var app = builder.Build();


// Call UseFlowCiao() if you are using Persistence

app.UseFlowCiao();


using (var scope = app.Services.CreateScope()) {
    // Build your desired Flow
    var flowBuilder = scope.ServiceProvider.GetRequiredService<IFlowBuilder>();
    var flow = flowBuilder.Build<PhoneFlow>();
    
    // Call CiaoAndTriggerAsync() to both initialize it using Ciao() and run it using Trigger()
    var flowOperator = scope.ServiceProvider.GetService<IFlowOperator>();

	/*
     {
    "Key":"Creating Flow",
    "Name":"Creating Flow",
    "States":[{"Code":"2","Name":"Begin"},
                {"Code":"3","Name":"End"},
                {"Code":"4","Name":"ProgramFinished"}],

    "Triggers":[{"Code":"3","Name":"StartButton"},
                {"Code":"4","Name":"EndButton"}],

    "Initial":{
    "fromStateCode":"2",
    "allows":[{"allowedStateCode":"3","triggerCode":"3"}],
    "onEntry":null,"onExit":null},
    "Steps":[{"fromStateCode":"3","allows": [{"allowedStateCode":"4","triggerCode":"4" }],
    "onEntry":null,
    "onExit":null
    },
    {"fromStateCode":"4",
    "onEntry":null,
    "onExit":null
    }
    ]}
     */
	var data = new Dictionary<object, object>
    {
        { "CallerId", 12134664789 }
    };


    var result = await flowOperator.CiaoAndTriggerAsync(flow.Key, Triggers.Call, data);
    Console.WriteLine(result.Message);
}

app.Run();

