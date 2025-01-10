using Discord;
using Discord.WebSocket;
using EmporerSteve.Services;

class Program
{
    private static DiscordSocketClient _client;

    static async Task Main(string[] args)
    {
        _client = new DiscordSocketClient();
        _client.Log += LogAsync;
        _client.Ready += ClientReady;
        _client.SlashCommandExecuted += SlashCommandHandler;
        var token = Environment.GetEnvironmentVariable("EMPORERSTEVE_DISCORD_TOKEN");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    static async Task LogAsync(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
    }

    static async Task ClientReady()
    {    
        var globalTraveller2eCharacteristicGenerator = new SlashCommandBuilder()
            .WithName(SlashCommandConstants.Traveller2eCharacteristicsGenerator)
            .WithDescription("Generates a set of valid starting characteristics for Traveller 2e");

        try
        {
            await _client.CreateGlobalApplicationCommandAsync(globalTraveller2eCharacteristicGenerator.Build());
            Console.WriteLine("Installed Traveller2e Commands!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task SlashCommandHandler(SocketSlashCommand command)
    {
        if (command.Data.Name == SlashCommandConstants.Traveller2eCharacteristicsGenerator)
        {
            var service = new Traveller2eService();
            var characteristics = service.GetValidStartingCharacteristics();
            var message = string.Join(", ", characteristics);
            var embed = new EmbedBuilder()
                .WithTitle("Traveller 2e Starting Characteristics")
                .AddField("Characteristics", message)
                .AddField("Total", characteristics.Sum())
                .AddField("Total Modifiers", characteristics.Sum(c => service.GetCharacteristicModifier(c))).Build();
            await command.RespondAsync(embed: embed);
        }
    }
}

class SlashCommandConstants
{
    public static string Traveller2eCharacteristicsGenerator = "generate-characteristics";
}