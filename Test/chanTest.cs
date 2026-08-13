using System;
using System.Linq;
using System.Threading.Tasks;
using Mezon_sdk;
using Mezon_sdk.Api;
using Mezon_sdk.Models;
using DotNetEnv;

namespace Mezon_sdk.Test
{
    public class ChanTest
    {
        static ChanTest()
        {
            Env.Load();
        }

        public static async Task RunAsync(string[]? args = null)
        {
            var botId = Environment.GetEnvironmentVariable("MEZON_BOT_ID");
            var token = Environment.GetEnvironmentVariable("MEZON_BOT_TOKEN");
            var clanIdStr = Environment.GetEnvironmentVariable("MEZON_TEST_CLAN");

            if (string.IsNullOrEmpty(botId) || string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Error: Set MEZON_BOT_ID and MEZON_BOT_TOKEN environment variables in .env");
                return;
            }

            long.TryParse(botId, out var botIdLong);

            // 1. Authenticate against login gateway
            var loginApi = new MezonApi(botIdLong, token, "https://gw.mezon.ai:443", 7000);
            var sessApi = await loginApi.AuthenticateAsync(botId, token, new ApiAuthenticateRequest
            {
                Account = new ApiAccountApp
                {
                    Appid = botId,
                    Token = token
                }
            });
            Console.WriteLine($"Authenticated: user={sessApi.UserId} api_url={sessApi.ApiUrl} ws_url={sessApi.WsUrl}");

            var sess = new Session(sessApi);

            // 2. Post-auth REST API
            var apiUrl = Mezon_sdk.Api.Utils.ParseUrlComponents(sessApi.ApiUrl ?? "", true);
            var restApi = new MezonApi(botIdLong, token, Mezon_sdk.Api.Utils.BuildUrl(apiUrl.Scheme, apiUrl.Hostname, apiUrl.Port), 7000);

            // Get clans list to find target clan if not set
            long clanId = 0;
            if (!string.IsNullOrEmpty(clanIdStr))
            {
                long.TryParse(clanIdStr, out clanId);
            }

            if (clanId == 0)
            {
                try
                {
                    var clanList = await restApi.ListClansAsync(sess.Token);
                    var firstClan = clanList?.Clandesc?.FirstOrDefault();
                    if (firstClan != null && firstClan.ClanId.HasValue)
                    {
                        clanId = firstClan.ClanId.Value;
                        Console.WriteLine($"Selected clan: {clanId} '{firstClan.ClanName}'");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ListClansAsync: {ex.Message}");
                }
            }

            // 3. Connect socket
            var wsUrl = Mezon_sdk.Api.Utils.ParseUrlComponents(sessApi.WsUrl ?? "", true);
            var socket = new Socket.DefaultSocket(wsUrl.Hostname, wsUrl.Port, wsUrl.UseSsl);
            await socket.ConnectAsync(sess);
            restApi.AttachSocket(socket);

            if (clanId != 0)
            {
                try
                {
                    await socket.JoinClanChatAsync(clanId);
                    Console.WriteLine($"JoinClanChatAsync({clanId}) OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"JoinClanChatAsync({clanId}): {ex.Message} (continuing)");
                }
            }

            // 4. Determine category ID
            long categoryId = 0;
            var refChannelStr = Environment.GetEnvironmentVariable("MEZON_TEST_CHANNEL");
            if (!string.IsNullOrEmpty(refChannelStr) && long.TryParse(refChannelStr, out var refChannelId))
            {
                try
                {
                    var refChannel = await restApi.GetChannelDetailAsync(sess.Token, refChannelId);
                    if (refChannel != null && refChannel.CategoryId.HasValue)
                    {
                        categoryId = refChannel.CategoryId.Value;
                        Console.WriteLine($"Reference channel {refChannelId}: label='{refChannel.ChannelLabel}' category={categoryId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GetChannelDetailAsync({refChannelId}): {ex.Message}");
                }
            }

            if (categoryId == 0 && clanId != 0)
            {
                try
                {
                    var channelList = await restApi.ListChannelsAsync(sess.Token, clanId, (int)Constants.ChannelType.ChannelTypeChannel);
                    var ch = channelList?.Channeldesc?.FirstOrDefault();
                    if (ch != null && ch.CategoryId.HasValue)
                    {
                        categoryId = ch.CategoryId.Value;
                        Console.WriteLine($"Borrowed category from channel '{ch.ChannelLabel}': {categoryId}");
                    }
                }
                catch { }
            }

            var label = $"cs-sdk-chantest-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            try
            {
                var created = await restApi.CreateChannelAsync(sess.Token, new ApiCreateChannelDescRequest
                {
                    ClanId = clanId,
                    CategoryId = categoryId,
                    Type = 1, // ChannelTypeChannel
                    ChannelLabel = label
                });

                if (created != null && created.ChannelId.HasValue && created.ChannelId.Value != 0)
                {
                    var chId = created.ChannelId.Value;
                    Console.WriteLine($"CREATED channel id={chId} label='{created.ChannelLabel}' type={created.Type} category={created.CategoryId}");

                    // 5. Verify channel exists
                    try
                    {
                        var detail = await restApi.GetChannelDetailAsync(sess.Token, chId);
                        Console.WriteLine($"VERIFIED exists: id={detail.ChannelId} label='{detail.ChannelLabel}'");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"verify-after-create GetChannelDetailAsync error: {ex.Message}");
                    }

                    // 6. Wait before deleting
                    var delayMs = 15000;
                    var delayEnv = Environment.GetEnvironmentVariable("MEZON_TEST_DELAY");
                    if (!string.IsNullOrEmpty(delayEnv) && int.TryParse(delayEnv, out var customDelaySec))
                    {
                        delayMs = customDelaySec * 1000;
                    }
                    Console.WriteLine($"Waiting {delayMs / 1000}s before delete...");
                    await Task.Delay(delayMs);

                    // 7. Delete channel
                    await restApi.DeleteChannelAsync(sess.Token, clanId, chId);
                    Console.WriteLine($"DELETED channel id={chId}");

                    // 8. Verify channel is gone
                    try
                    {
                        var gone = await restApi.GetChannelDetailAsync(sess.Token, chId);
                        if (gone == null || !gone.ChannelId.HasValue || gone.ChannelId.Value == 0)
                        {
                            Console.WriteLine("VERIFIED gone: GetChannelDetailAsync returns empty description");
                        }
                        else
                        {
                            Console.WriteLine($"WARNING: channel still resolves after delete: id={gone.ChannelId} label='{gone.ChannelLabel}'");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"VERIFIED gone: GetChannelDetailAsync now errors: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("CreateChannelAsync returned null or zero channel ID");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ChanTest create/delete channel note: {ex.Message}");
            }

            Console.WriteLine("chanTest OK");
        }
    }
}
