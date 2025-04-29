using RabbitMQ.Client;
using RabbitMqLibrary.Factories.Interfaces;
using RabbitMqLibrary.Models;

namespace RabbitMqLibrary.Factories;

public class ChannelFactory (ConnectionFactoryParameters factoryParameters) : IChannelFactory
{

    public async Task<IChannel?> GetChannel()
    {
        var factory = new ConnectionFactory
        {
            HostName = factoryParameters.HostName,
            UserName = factoryParameters.UserName,
            Password = factoryParameters.Password
        };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        return channel;
    }
}
