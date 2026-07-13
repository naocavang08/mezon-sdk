# Mezon .NET SDK (Mezon.Sdk)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Framework: .NET 8.0](https://img.shields.io/badge/Framework-.NET%208.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)

**Mezon.Sdk** is a software development kit (.NET SDK) designed for integrating applications, building chatbots, and interacting directly with the **Mezon** communication and collaboration platform. The SDK provides features to support real-time WebSocket connections (Real-time events), API calls using Protobuf/gRPC-web, and core data structures like Clan, Channel, User, and Message.

---

## 🚀 Key Features

- **Robust Real-time Connections**: Integrated WebSocket adapter with automatic reconnection (Auto Reconnect) using exponential backoff.
- **Comprehensive Interactions**: Fully supports message operations (Send, Reply, Update, React, Delete, Ephemeral).
- **Interactive Messages & Forms**: Provides `InteractiveBuilder` and `ButtonBuilder` to easily construct messages containing form inputs, dropdown selects, radio options, date pickers, and animations.
- **Smart Caching**: `CacheManager` supports lazy loading of users, clans, and channels from the API when needed, with built-in support for both **in-memory** and **Redis-backed** caching (`StackExchange.Redis`).
- **Local Storage**: Built-in SQLite database service for storing messages locally (`MessageDbService`).
- **Blockchain / On-Chain MMN Support**: Integrated MMN Wallet client allowing bot-to-user withdrawals and token transfers, utilizing ZK proofs (Zero-Knowledge) and cryptography.

---

## Project Directory Structure

```text
Mezon-sdk/
├── Api/               # HTTP RESTful API connection services (gRPC-web / Protobuf)
│   ├── MezonApi.cs    # Handles API requests and manages rate limiting
│   └── Utils.cs       # Utility tools for building URLs, encoding/decoding payloads
├── Socket/            # WebSocket management and real-time communication control
│   ├── WebSocketAdapter.cs  # Establishes & controls the underlying WebSocket connection
│   ├── DefaultSocket.cs     # Socket logic including ping/pong, heartbeats, and frame handling
│   └── MessageBuilder.cs    # Helper for packaging messages in binary/JSON format
├── Structrues/        # Core business domain models (Entities)
│   ├── Clan.cs        # Clan object (manages channels, roles, permission list)
│   ├── TextChannel.cs # Text channel object (sends messages, ephemeral messages)
│   ├── User.cs        # User object (sends direct messages/DMs)
│   ├── Message.cs     # Message object (reply, react, update, delete)
│   ├── ButtonBuilder.cs       # Interactive button builder
│   └── InteractiveMessage.cs  # Interactive forms and dynamic components builder
├── Models/            # Data Transfer Objects (DTO) mapped from Protobuf
├── Constants/         # Enums for channel types, message types, and socket event names
├── Messages/          # Local storage services (Database context, SQLite service)
└── Client.cs          # MezonClient - The primary entry point for the SDK
```

---

## Installation

The project targets **.NET 8.0** and relies on the following NuGet packages:
- `Google.Protobuf` (v3.34.0)
- `Grpc.Tools` (v2.78.0)
- `Microsoft.EntityFrameworkCore.Sqlite` (v8.0.11)
- `Microsoft.Data.SqlClient` (v7.0.0)
- `StackExchange.Redis` (v2.7.33)

To build the library, run the standard dotnet command:
```bash
dotnet build
```

---

## Usage Guide

### 1. Initialize and Login Client

`MezonClient` is the central class of your application. You can configure optional SQLite storage for messages or a Redis connection string for external caching.

```csharp
using System;
using System.Threading.Tasks;
using Mezon_sdk;

class Program
{
    static async Task Main(string[] args)
    {
        // Initialize the client with your Client ID and API Key provided by Mezon
        var client = new MezonClient(
            clientId: "YOUR_CLIENT_ID",
            apiKey: "YOUR_API_KEY",
            
            // Optional: Enable Redis caching for Users, Clans, and Channels
            redisConnectionString: "localhost:6379",
            
            // Optional: Persist messages to a physical SQLite database
            messageDbConnectionString: "Data Source=mezon_messages.db"
        );

        // Login and enable automatic reconnection
        await client.LoginAsync(enableAutoReconnect: true);
        Console.WriteLine("LoggedIn to Mezon successfully!");

        // Keep the application running to receive real-time events
        await Task.Delay(-1);
    }
}
```

### 2. Session and Authentication

You can retrieve the session metadata containing authentication tokens and API/WebSocket endpoints.

```csharp
// Retrieve the authenticated session
var session = await client.GetSessionAsync();

Console.WriteLine($"Session Token: {session.Token}");
Console.WriteLine($"ID Token: {session.IdToken}");
Console.WriteLine($"API Gateway Url: {session.ApiUrl}");
Console.WriteLine($"WebSocket Url: {session.WsUrl}");
```

### 3. Event Handling

`MezonClient` supports a wide array of event handlers for real-time WebSocket activities. You can subscribe to them to respond to user messages, UI interactions, voice statuses, and agent workflows.

```csharp
// Subscribe to new messages from channels
client.OnChannelMessage += async (protoMessage) =>
{
    long channelId = protoMessage.ChannelId;
    long senderId = protoMessage.SenderId;
    
    // Decode JSON content
    var channelMsg = Mezon_sdk.Models.ChannelMessage.FromProtobuf(protoMessage);
    if (channelMsg.Content != null && channelMsg.Content.TryGetValue("t", out var textObj))
    {
        string text = textObj?.ToString() ?? "";
        Console.WriteLine($"[Channel: {channelId}] User {senderId} sent: {text}");
    }
};

// Listen to button click interactions
client.OnMessageButtonClicked += async (btnEvent) =>
{
    Console.WriteLine($"Button {btnEvent.ButtonId} clicked by User {btnEvent.UserId} on Message {btnEvent.MessageId}");
    await Task.CompletedTask;
};

// Listen to dropdown select box choices
client.OnDropdownBoxSelected += async (dropdownEvent) =>
{
    Console.WriteLine($"Dropdown selected on Message: {dropdownEvent.MessageId}");
    await Task.CompletedTask;
};

// Listen to Voice / Meeting events
client.OnVoiceStarted += async (voiceEvent) =>
{
    Console.WriteLine($"Voice session started in Clan: {voiceEvent.ClanId}, Channel: {voiceEvent.ChannelId}");
};

client.OnVoiceJoined += async (joinEvent) =>
{
    Console.WriteLine($"User {joinEvent.UserId} joined voice channel {joinEvent.ChannelId}");
};

// Listen to SSE AI Agent lifecycle events
client.OnAIAgentSessionStarted += async (startedEvent) =>
{
    Console.WriteLine($"AI Agent Session Started for room: {startedEvent.RoomId}");
};

client.OnAIAgentSessionEnded += async (endedEvent) =>
{
    Console.WriteLine($"AI Agent Session Ended for room: {endedEvent.RoomId}");
};
```

### 4. Messaging Operations

When you obtain a channel or message object, you can perform standard messaging actions:

#### Send a message to a channel
```csharp
var channel = await client.GetChannelFromIdAsync(channelId);
var msgAck = await channel.SendAsync(new Mezon_sdk.Models.ChannelMessageContent 
{ 
    Text = "Hello, this is a standard message!" 
});
```

#### Reply to a message
```csharp
// "message" is a Message object
var replyAck = await message.ReplyAsync(new Mezon_sdk.Models.ChannelMessageContent 
{ 
    Text = "This is a direct reply to your message" 
});
```

#### React to a message
```csharp
// React to a message with a specific emoji (e.g. ❤️)
var reactionResult = await message.ReactAsync(emojiId: 0, emoji: "❤️", count: 1);
```

#### Edit/Update a message
```csharp
var updateAck = await message.UpdateAsync(new Mezon_sdk.Models.ChannelMessageContent 
{ 
    Text = "This content has been edited" 
});
```

#### Delete a message
```csharp
await message.DeleteAsync();
```

#### Send an Ephemeral Message
Ephemeral messages are private messages only visible to the specified recipient user(s):
```csharp
var receivers = new List<long> { 1967925734009737216L };
await channel.SendEphemeralAsync(
    receiverIds: receivers,
    content: new Mezon_sdk.Models.ChannelMessageContent { Text = "This is a secret message only you can see!" }
);
```

### 5. Clan Operations

Clans are workspaces containing text/voice channels and members. You can query and manage resources within a Clan.

```csharp
// Retrieve a clan by ID (will query API and cache the result)
var clan = await client.GetClanFromIdAsync(clanId);
Console.WriteLine($"Clan Name: {clan.Name}");

// Load all channels in the Clan into cache
await clan.LoadChannelsAsync();

// List users currently in a voice channel
var voiceUsers = await clan.ListChannelVoiceUsersAsync(
    channelId: voiceChannelId,
    limit: 100
);

// List roles defined in the Clan
var roles = await clan.ListRolesAsync(limit: 50);

// Update permissions or properties of a Clan Role
var updateRequest = new Mezon.Net.Internal.Api.UpdateRoleRequest
{
    RoleName = "New Admin Name",
    Color = "#FF5733"
};
bool success = await clan.UpdateRoleAsync(roleId: 12345, updateRequest);
```

### 6. Channel Operations

Channels can be text, voice, forums, threads, etc. Use `GetChannelFromIdAsync` to query details:

```csharp
// Retrieve a channel by ID (cached or fetched)
var channel = await client.GetChannelFromIdAsync(channelId);
Console.WriteLine($"Channel Name: {channel.Name}, Type: {channel.ChannelType}");

// Access messages of this channel cached locally in-memory
var cachedMessages = channel.Messages;
```

### 7. User & Direct Messages (DM)

You can look up user profiles and establish DM conversations.

```csharp
// Fetch user profile (cached or fetched)
var user = await client.GetUserFromIdAsync(userId);
Console.WriteLine($"Username: {user.Username}, DisplayName: {user.DisplayName}");

// Create a DM channel with the user explicitly
var dmChannelDesc = await user.CreateDmChannelAsync();

// Send a direct message to the user (automatically resolves or creates the DM channel)
await user.SendDmMessageAsync(new Mezon_sdk.Models.ChannelMessageContent
{
    Text = "Hello! This is a direct message from the bot."
});
```

### 8. Quick Menu Actions

Quick Menus are shortcut buttons shown in the chat interface that allow users to quickly trigger bot commands.

```csharp
// Add a quick menu action button
var quickMenu = await client.AddQuickMenuAccessAsync(
    channelId: (int)channelId,
    clanId: (int)clanId,
    menuType: 1,
    actionMsg: "/help",
    background: "#2ecc71",
    menuName: "Get Help"
);

// List quick menu actions configured for a specific channel
var menuList = await client.ListQuickMenuAccessAsync(
    botId: checked((int)long.Parse(client.ClientId)),
    channelId: (int)channelId
);

// Delete a quick menu shortcut
await client.DeleteQuickMenuAccessAsync(
    id: quickMenu.Id,
    clanId: (int)clanId,
    channelId: (int)channelId
);
```

### 9. Interactive Messages & Buttons

Interactive messages enhance chatbot responsiveness with form elements, buttons, and structured inputs.

#### Messages with Clickable Buttons
Define a control row with buttons having various styles (`Primary`, `Secondary`, `Success`, `Danger`, `Link`):

```csharp
using Mezon_sdk.Structures;
using Mezon_sdk.Models;
using Mezon_sdk.Constants;

// 1. Add buttons using ButtonBuilder
var buttons = new ButtonBuilder()
    .AddButton("btn_approve", "Approve", ButtonMessageStyle.Success)
    .AddButton("btn_reject", "Reject", ButtonMessageStyle.Danger)
    .AddButton("btn_info", "Read Wiki", ButtonMessageStyle.Link, url: "https://wiki.mezon.ai")
    .Build();

// 2. Convert button dictionary definitions to message ActionRow component structure
var actionRow = new MessageActionRow
{
    Components = buttons.ConvertAll(b => new MessageComponent
    {
        Id = b["id"]?.ToString() ?? "",
        Type = Convert.ToInt32(b["type"]),
        Component = b["component"] as Dictionary<string, object>
    })
};

// 3. Send the message containing the button components
await channel.SendAsync(new ChannelMessageContent
{
    Text = "Pending request: Please choose an action below.",
    Components = new List<MessageActionRow> { actionRow }
});
```

#### Interactive Forms (Inputs, Selects, Radios, and Date Pickers)
Use `InteractiveBuilder` to build forms containing text areas, dropdown lists, radio choices, date selection, or animation configs:

```csharp
using Mezon_sdk.Structures;
using Mezon_sdk.Models;
using System.Text.Json;

var form = new InteractiveBuilder(title: "Event Registration Form")
    .SetDescription("Provide your details to register for the upcoming hackathon:")
    .SetColor("#9b59b6")
    .SetThumbnail("https://example.com/hackathon_logo.png")
    
    // Add text input field
    .AddInputField(fieldId: "txt_teamname", name: "Team Name", placeholder: "Enter your team's name...")
    
    // Add dropdown select list
    .AddSelectField(fieldId: "sel_size", name: "Team Size", options: new List<SelectFieldOption>
    {
        new SelectFieldOption { Value = "2-3", Label = "2 to 3 members" },
        new SelectFieldOption { Value = "4-5", Label = "4 to 5 members" }
    })
    
    // Add radio group choices
    .AddRadioField(fieldId: "rad_track", name: "Choose Hackathon Track", options: new List<RadioFieldOption>
    {
        new RadioFieldOption { Value = "ai", Label = "Artificial Intelligence" },
        new RadioFieldOption { Value = "web3", Label = "Web3 & Blockchain" }
    })
    
    // Add date picker calendar field
    .AddDatepickerField(fieldId: "dt_arrival", name: "Arrival Date")
    .Build();

// Serialize and deserialize to the required DTO structure
var embedProps = JsonSerializer.Deserialize<InteractiveMessageProps>(JsonSerializer.Serialize(form));

await channel.SendAsync(new ChannelMessageContent
{
    Embed = new List<InteractiveMessageProps> { embedProps! }
});
```

### 10. Blockchain / Token Transfer (MMN Wallet)

The Mezon SDK has native client interfaces for interaction with the MMN Blockchain Node and Zero-Knowledge (ZK) Proof generators. This allows bots to send token transactions on-chain to user addresses (e.g. for payments or withdrawals).

```csharp
using Mezon_sdk.Models;

// 1. Get current session to access ID Token
var session = await client.GetSessionAsync();

// 2. Initialize the MMN Blockchain Client (generates cryptographic keypair, derives address, and fetches ZK Proofs)
await client.MmnInitializedAsync(session.IdToken);
Console.WriteLine($"Bot Wallet Address: {client.AddressMMN}");

// 3. Construct a token sending request
var withdrawRequest = new APISentTokenRequest
{
    SenderId = client.ClientId,
    SenderName = "Bot Payment Gateway",
    ReceiverId = "1967925734009737216", // Target user's Mezon ID
    Amount = 10.0m,                     // Amount in token units
    Note = "Weekly developer rewards payout"
};

// 4. Send token on-chain
var txResult = await client.SendTokenAsync(withdrawRequest);

if (txResult.Ok)
{
    Console.WriteLine("Blockchain transfer successful!");
    Console.WriteLine($"Transaction Hash: {txResult.TxHash}");
}
else
{
    Console.WriteLine($"Transfer failed: {txResult.Error}");
}
```

---

## 🔒 License

This project is licensed under the **MIT License**.
