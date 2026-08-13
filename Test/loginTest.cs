using System;
using System.Linq;
using System.Threading.Tasks;
using Mezon_sdk;
using Mezon_sdk.Models;
using DotNetEnv;

namespace Mezon_sdk.Test
{
    public class LoginTest
    {
        static LoginTest()
        {
            Env.Load();
        }

        public static async Task RunAsync(string[]? args = null)
        {
            var botId = Environment.GetEnvironmentVariable("MEZON_BOT_ID");
            var token = Environment.GetEnvironmentVariable("MEZON_BOT_TOKEN");

            if (string.IsNullOrEmpty(botId) || string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Error: Please set MEZON_BOT_ID and MEZON_BOT_TOKEN environment variables in .env");
                return;
            }

            var client = new MezonClient(botId, token);
            Console.WriteLine("Logging in...");
            await client.LoginAsync();
            Console.WriteLine($"Login OK: {client.Clans.Values().Count()} clans cached");

            foreach (var c in client.Clans.Values())
            {
                Console.WriteLine($"  clan {c.Id} '{c.Name}' welcome={c.WelcomeChannelId}");
            }

            var clanIdStr = Environment.GetEnvironmentVariable("MEZON_TEST_CLAN");
            Structures.Clan? clan = null;

            if (!string.IsNullOrEmpty(clanIdStr) && long.TryParse(clanIdStr, out var envClanId))
            {
                clan = client.Clans.Get(envClanId);
            }

            if (clan == null)
            {
                clan = client.Clans.Values().FirstOrDefault();
            }

            if (clan == null)
            {
                Console.WriteLine("Error: No clans available for testing.");
                return;
            }

            Console.WriteLine($"Using clan: {clan.Id} '{clan.Name}'");

            long categoryId = 0;
            var refChannelStr = Environment.GetEnvironmentVariable("MEZON_TEST_CHANNEL");
            if (!string.IsNullOrEmpty(refChannelStr) && long.TryParse(refChannelStr, out var refChannelId))
            {
                try
                {
                    var refChannel = await client.Channels.FetchAsync(refChannelId);
                    if (refChannel != null)
                    {
                        categoryId = refChannel.CategoryId;
                        Console.WriteLine($"Reference channel '{refChannel.Name}' category={categoryId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not fetch ref channel {refChannelId}: {ex.Message}");
                }
            }

            if (categoryId == 0)
            {
                try
                {
                    await clan.LoadChannelsAsync();
                    var firstCh = clan.Channels.Values().FirstOrDefault();
                    if (firstCh != null)
                    {
                        categoryId = firstCh.CategoryId;
                        Console.WriteLine($"Borrowed category from clan channel '{firstCh.Name}': category={categoryId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"LoadChannelsAsync error: {ex.Message}");
                }
            }

            var label = $"cs-sdk-hltest-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            try
            {
                var created = await clan.CreateChannelAsync(new ApiCreateChannelDescRequest
                {
                    ChannelLabel = label,
                    CategoryId = categoryId,
                    Type = (int)Constants.ChannelType.ChannelTypeChannel
                });

                if (created != null && created.ChannelId.HasValue && created.ChannelId.Value != 0)
                {
                    var chId = created.ChannelId.Value;
                    Console.WriteLine($"CREATED via Clan.CreateChannel: id={chId} label='{created.ChannelLabel}'");

                    if (clan.Channels.Get(chId) == null)
                    {
                        Console.WriteLine("WARNING: created channel not in clan cache");
                    }

                    await clan.DeleteChannelAsync(chId);
                    Console.WriteLine($"DELETED via Clan.DeleteChannel: id={chId}");

                    if (clan.Channels.Get(chId) != null)
                    {
                        Console.WriteLine("WARNING: deleted channel still in clan cache");
                    }
                }
                else
                {
                    Console.WriteLine("CreateChannel returned null or zero channel ID");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Clan channel create/delete test note: {ex.Message}");
            }

            Console.WriteLine("loginTest OK");
        }
    }
}
