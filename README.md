# Mezon .NET SDK (Mezon.Sdk)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Framework: .NET 8.0](https://img.shields.io/badge/Framework-.NET%208.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)

**Mezon.Sdk** is a software development kit (.NET SDK) designed for integrating applications, building chatbots, and interacting directly with the **Mezon** communication and collaboration platform. The SDK provides features to support real-time WebSocket connections (Real-time events), API calls using Protobuf/gRPC-web, and core data structures like Clan, Channel, User, and Message.

---

## 🚀 Key Features

- **Robust Real-time Connections**: Integrated WebSocket adapter with automatic reconnection (Auto Reconnect) using exponential backoff.
- **Comprehensive Interactions**: Fully supports message operations (Send, Reply, Update, React, Delete, Ephemeral).
- **Interactive Messages & Forms**: Provides `InteractiveBuilder` and `ButtonBuilder` to easily construct messages containing form inputs, dropdown selects, radio options, date pickers, and animations.
- **Smart Caching**: `CacheManager` supports lazy loading of users, clans, and channels from the API when needed.
- **Local Storage**: Built-in SQLite database service for storing messages locally (`MessageDbService`).

---

## 📂 Project Directory Structure

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

## 🛠 Installation

The project targets **.NET 8.0** and relies on the following NuGet packages:
- `Google.Protobuf` (v3.34.0)
- `Grpc.Tools` (v2.78.0)
- `Microsoft.EntityFrameworkCore.Sqlite` (v8.0.11)
- `Microsoft.Data.SqlClient` (v7.0.0)

To build the library, run the standard dotnet command:
```bash
dotnet build
```

---

## 📖 Usage Guide

### 1. Initialize and Login Client

`MezonClient` is the central class to start your application. Calling `LoginAsync` automatically authenticates the session, opens the WebSocket connection, and starts the data managers.

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
            apiKey: "YOUR_API_KEY"
        );

        // Login and enable auto-reconnection
        await client.LoginAsync(enableAutoReconnect: true);
        Console.WriteLine("LoggedIn to Mezon successfully!");

        // Keep the application running to receive real-time events
        await Task.Delay(-1);
    }
}
```

### 2. Event Handling

You can listen to new messages or user interactions through events on the `MezonClient`:

```csharp
// Subscribe to new messages from channels
client.OnChannelMessage += async (protoMessage) =>
{
    long channelId = protoMessage.ChannelId;
    long senderId = protoMessage.SenderId;
    
    // Use the Model helper to decode the JSON content easily
    var channelMsg = Mezon_sdk.Models.ChannelMessage.FromProtobuf(protoMessage);
    
    if (channelMsg.Content != null && channelMsg.Content.TryGetValue("t", out var textObj))
    {
        string text = textObj?.ToString() ?? "";
        Console.WriteLine($"[Channel: {channelId}] User {senderId} sent: {text}");

        // Echo bot mechanism (avoiding responding to itself)
        if (senderId != long.Parse(client.ClientId))
        {
            var channel = await client.GetChannelFromIdAsync(channelId);
            await channel.SendAsync(new Mezon_sdk.Models.ChannelMessageContent
            {
                Text = $"Bot received: {text}"
            });
        }
    }
};

// Subscribe to button click interactions
client.OnMessageButtonClicked += async (btnEvent) =>
{
    Console.WriteLine($"Button clicked: {btnEvent.ButtonId} by user: {btnEvent.UserId}");
    await Task.CompletedTask;
};
```

### 3. Common Message Operations

Once you have a channel or message object, you can perform various actions:

#### Send a new message to a channel
```csharp
var channel = await client.GetChannelFromIdAsync(channelId);
await channel.SendAsync(new ChannelMessageContent { Text = "Hello world!" });
```

#### Reply, Update, React, and Delete Messages
```csharp
// Replying to a message
await message.ReplyAsync(new ChannelMessageContent { Text = "I agree with this!" });

// React to a message (emoji: ❤️)
await message.ReactAsync(emojiId: 0, emoji: "❤️", count: 1);

// Editing a message
await message.UpdateAsync(new ChannelMessageContent { Text = "Updated content" });

// Deleting a message
await message.DeleteAsync();
```

#### Send an Ephemeral Message
Ephemeral messages are only visible to the specified recipient user(s):
```csharp
var receivers = new List<long> { 1234567890L }; // Recipient user IDs
await channel.SendEphemeralAsync(
    receiverIds: receivers,
    content: new ChannelMessageContent { Text = "This is a secret message only you can see!" }
);
```

---

## 🎨 Building Interactive Messages

### 1. Messages with Buttons
Use `ButtonBuilder` to define a control bar with one or more buttons, selecting from styles like `ButtonMessageStyle`:

```csharp
using Mezon_sdk.Structures;
using Mezon_sdk.Models;
using Mezon_sdk.Constants;

// 1. Build a list of buttons
var buttons = new ButtonBuilder()
    .AddButton("btn_accept", "Accept", ButtonMessageStyle.Success)
    .AddButton("btn_decline", "Decline", ButtonMessageStyle.Danger)
    .AddButton("btn_info", "Details", ButtonMessageStyle.Primary, url: "https://mezon.ai")
    .Build();

// 2. Convert to message ActionRow component structure
var actionRow = new MessageActionRow
{
    Components = buttons.ConvertAll(b => new MessageComponent
    {
        Id = b["id"]?.ToString() ?? "",
        Type = Convert.ToInt32(b["type"]),
        Component = b["component"] as Dictionary<string, object>
    })
};

// 3. Send the message with buttons
await channel.SendAsync(new ChannelMessageContent
{
    Text = "Do you agree to join this project?",
    Components = new List<MessageActionRow> { actionRow }
});
```

### 2. Building Rich Interactive Forms
`InteractiveBuilder` allows you to show messages with inputs, select dropdowns, radio options, date pickers, or animations:

```csharp
var form = new InteractiveBuilder(title: "Project Feedback Survey")
    .SetDescription("Please fill out the form below:")
    .SetColor("#3498db") // Embed color
    .SetThumbnail("https://example.com/logo.png")
    
    // Add text input field
    .AddInputField(fieldId: "txt_feedback", name: "Feedback", placeholder: "Type your comment here...")
    
    // Add single selection dropdown (Select)
    .AddSelectField(fieldId: "sel_rating", name: "Satisfaction Level", options: new List<SelectFieldOption>
    {
        new SelectFieldOption { Value = "5", Label = "Very Satisfied" },
        new SelectFieldOption { Value = "3", Label = "Neutral" },
        new SelectFieldOption { Value = "1", Label = "Not Satisfied" }
    })
    
    // Add radio group selection
    .AddRadioField(fieldId: "rad_join", name: "Will you attend the next event?", options: new List<RadioFieldOption>
    {
        new RadioFieldOption { Value = "yes", Label = "Yes, I will attend" },
        new RadioFieldOption { Value = "no", Label = "No, I am busy" }
    })
    
    // Add a datepicker component
    .AddDatepickerField(fieldId: "dt_date", name: "Suggested Date")
    .Build();

// Wrap the form into embed properties and send
var embedProps = JsonSerializer.Deserialize<InteractiveMessageProps>(JsonSerializer.Serialize(form));
await channel.SendAsync(new ChannelMessageContent
{
    Embed = new List<InteractiveMessageProps> { embedProps! }
});
```

---

## 🔒 License

This project is licensed under the **MIT License**.
