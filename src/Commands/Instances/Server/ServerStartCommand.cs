﻿namespace Tgstation.Server.CommandLineInterface.Commands.Instances.Server;

using CliFx.Attributes;
using Middlewares;
using Models;
using Services;
using Sessions;

[Command("instance server start")]
public class ServerStartCommand : BaseSessionCommand
{
    private readonly IInstanceManager instances;

    public ServerStartCommand(IInstanceManager instances) => this.instances = instances;

    [CommandParameter(0, Converter = typeof(InstanceSelectorConverter))]
    public required InstanceSelector Instance { get; init; }

    protected override async ValueTask RunCommandAsync(ICommandContext context)
    {
        var client = await this.instances.RequestInstanceClient(this.Instance, context.CancellationToken);
        await client.DreamDaemon.Start(context.CancellationToken);
    }
}
