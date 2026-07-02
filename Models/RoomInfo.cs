namespace Mezon_sdk.Models
{
    using System.Text.Json.Serialization;

    public class RoomInfo
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; } = string.Empty;

        [JsonPropertyName("room_name")]
        public string RoomName { get; set; } = string.Empty;
    }
}
