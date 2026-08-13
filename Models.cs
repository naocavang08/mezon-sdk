// Auto-generated: All models merged into one file
// Source: Mezon_sdk.Models

namespace Mezon_sdk.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Google.Protobuf;
    using Mezon.Net.Internal.Api;
    using Mezon_sdk.Utils;
    using static Mezon_sdk.Utils.Helper;
    using ApiPb = Mezon.Net.Internal.Api;

    // ===== From: ApiSentTokenRequest.cs =====
    public class APISentTokenRequest
    {
        [JsonPropertyName("sender_name")]
        public string? SenderName { get; set; }

        [JsonPropertyName("sender_id")]
        public string? SenderId { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [JsonPropertyName("extra_attribute")]
        public string? ExtraAttribute { get; set; }

        [JsonPropertyName("mmn_extra_info")]
        public Dictionary<string, string>? MmnExtraInfo { get; set; }

        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }

        [JsonPropertyName("receiver_id")]
        public string ReceiverId { get; set; } = string.Empty;
    }

    // ===== From: AddUsers.cs =====

    public class AddUsers
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

    }

    // ===== From: AIAgentSessionEvents.cs =====
    public class AIAgentSessionStartedEvent : RoomMetadataEvent { }
    public class AIAgentSessionEndedEvent : RoomMetadataEvent { }
    public class AIAgentSessionSummaryDoneEvent : RoomMetadataEvent { }

    // ===== From: AnimationConfig.cs =====

    public class AnimationConfig
    {
        [JsonPropertyName("url_image")]
        public string? UrlImage { get; set; }

        [JsonPropertyName("url_position")]
        public string? UrlPosition { get; set; }

        [JsonPropertyName("pool")]
        public List<string>? Pool { get; set; }

        [JsonPropertyName("repeat")]
        public int? Repeat { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

    }

    // ===== From: ApiAccountApp.cs =====

    public class ApiAccountApp : MezonBaseModel<ApiAccountApp>
    {
        [JsonPropertyName("appid")]
        public string? Appid { get; set; }

        [JsonPropertyName("appname")]
        public string? Appname { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("vars")]
        public Dictionary<string, string>? Vars { get; set; }

    }

    // ===== From: ApiAuthenticateRequest.cs =====

    public class ApiAuthenticateRequest : MezonBaseModel<ApiAuthenticateRequest>
    {
        [JsonPropertyName("account")]
        public ApiAccountApp? Account { get; set; }

    }

    // ===== From: ApiChannelDescList.cs =====

    public class ApiChannelDescList : MezonBaseModel<ApiChannelDescList>
    {
        [JsonPropertyName("channeldesc")]
        public List<ApiChannelDescription>? Channeldesc { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        public static ApiChannelDescList FromProtobuf(ChannelDescList message)
        {
            var result = Mezon_sdk.Utils.ProtoUtils.FromProtobuf<ApiChannelDescList>(message)
                ?? new ApiChannelDescList();

            if (result.Channeldesc is { Count: > 0 } && string.IsNullOrWhiteSpace(result.Cursor))
            {
                result.Cursor = $"cursor-{result.Channeldesc.Count}";
            }

            return result;
        }
    }

    // ===== From: ApiChannelDescription.cs =====

    public class ApiChannelDescription : MezonBaseModel<ApiChannelDescription>
    {
        [JsonPropertyName("active")]
        public int? Active { get; set; }

        [JsonPropertyName("avatars")]
        public List<string>? Avatars { get; set; }

        [JsonPropertyName("category_id")]
        public long? CategoryId { get; set; }

        [JsonPropertyName("category_name")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("channel_avatar")]
        public List<string>? ChannelAvatar { get; set; }

        [JsonPropertyName("channel_id")]
        public long? ChannelId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("channel_private")]
        public int? ChannelPrivate { get; set; }

        [JsonPropertyName("clan_id")]
        public long? ClanId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("count_mess_unread")]
        public int? CountMessUnread { get; set; }

        [JsonPropertyName("create_time_seconds")]
        public int? CreateTimeSeconds { get; set; }

        [JsonPropertyName("creator_id")]
        public long? CreatorId { get; set; }

        [JsonPropertyName("creator_name")]
        public string? CreatorName { get; set; }

        [JsonPropertyName("display_names")]
        public List<string>? DisplayNames { get; set; }

        [JsonPropertyName("last_pin_message")]
        public string? LastPinMessage { get; set; }

        [JsonPropertyName("last_seen_message")]
        public ApiChannelMessageHeader? LastSeenMessage { get; set; }

        [JsonPropertyName("last_sent_message")]
        public ApiChannelMessageHeader? LastSentMessage { get; set; }

        [JsonPropertyName("meeting_code")]
        public string? MeetingCode { get; set; }

        [JsonPropertyName("meeting_uri")]
        public string? MeetingUri { get; set; }

        [JsonPropertyName("onlines")]
        public List<bool>? Onlines { get; set; }

        [JsonPropertyName("parent_id")]
        public long? ParentId { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("type")]
        public int? Type { get; set; }

        [JsonPropertyName("update_time_seconds")]
        public int? UpdateTimeSeconds { get; set; }

        [JsonPropertyName("user_id")]
        public List<long>? UserId { get; set; }

        [JsonPropertyName("user_ids")]
        public List<long>? UserIds { get; set; }

        [JsonPropertyName("usernames")]
        public List<string>? Usernames { get; set; }

        public static ApiChannelDescription FromProtobuf(ChannelDescription message)
        {
            return ProtoUtils.FromProtobuf<ApiChannelDescription>(message)
                ?? new ApiChannelDescription();
        }
    }

    // ===== From: ApiChannelMessageHeader.cs =====

    public class ApiChannelMessageHeader : MezonBaseModel<ApiChannelMessageHeader>
    {
        [JsonPropertyName("attachment")]
        public string? Attachment { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("mention")]
        public string? Mention { get; set; }

        [JsonPropertyName("reaction")]
        public string? Reaction { get; set; }

        [JsonPropertyName("referece")]
        public string? Referece { get; set; }

        [JsonPropertyName("sender_id")]
        public long? SenderId { get; set; }

        [JsonPropertyName("timestamp_seconds")]
        public int? TimestampSeconds { get; set; }

    }

    // ===== From: ApiClanDesc.cs =====

    public class ApiClanDesc : MezonBaseModel<ApiClanDesc>
    {
        [JsonPropertyName("banner")]
        public string? Banner { get; set; }

        [JsonPropertyName("clan_id")]
        public long? ClanId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("creator_id")]
        public long? CreatorId { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("badge_count")]
        public int? BadgeCount { get; set; }

        [JsonPropertyName("is_onboarding")]
        public bool? IsOnboarding { get; set; }

        [JsonPropertyName("welcome_channel_id")]
        public long? WelcomeChannelId { get; set; }

        [JsonPropertyName("onboarding_banner")]
        public string? OnboardingBanner { get; set; }

    }

    public class ApiClanDescList : MezonBaseModel<ApiClanDescList>
    {
        [JsonPropertyName("clandesc")]
        public List<ApiClanDesc>? Clandesc { get; set; }

    }

    // ===== From: ApiCreateChannelDescRequest.cs =====

    public class ApiCreateChannelDescRequest : MezonBaseModel<ApiCreateChannelDescRequest>
    {
        [JsonPropertyName("category_id")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("channel_id")]
        public int? ChannelId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("channel_private")]
        public int? ChannelPrivate { get; set; }

        [JsonPropertyName("clan_id")]
        public int? ClanId { get; set; }

        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        [JsonPropertyName("type")]
        public int? Type { get; set; }

        [JsonPropertyName("user_ids")]
        public List<long>? UserIds { get; set; }

    }

    // ===== From: ApiDeleteChannelDescRequest.cs =====

    public class ApiDeleteChannelDescRequest : MezonBaseModel<ApiDeleteChannelDescRequest>
    {
        [JsonPropertyName("clan_id")]
        public long? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public long? ChannelId { get; set; }
    }

    // ===== From: ApiUpdateChannelDescRequest.cs =====

    public class ApiUpdateChannelDescRequest : MezonBaseModel<ApiUpdateChannelDescRequest>
    {
        [JsonPropertyName("clan_id")]
        public long? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public long? ChannelId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("category_id")]
        public long? CategoryId { get; set; }

        [JsonPropertyName("app_id")]
        public long? AppId { get; set; }

        [JsonPropertyName("topic")]
        public string? Topic { get; set; }

        [JsonPropertyName("age_restricted")]
        public int? AgeRestricted { get; set; }

        [JsonPropertyName("e2ee")]
        public int? E2ee { get; set; }

        [JsonPropertyName("channel_avatar")]
        public string? ChannelAvatar { get; set; }
    }

    // ===== From: ApiChangeChannelPrivateRequest.cs =====

    public class ApiChangeChannelPrivateRequest : MezonBaseModel<ApiChangeChannelPrivateRequest>
    {
        [JsonPropertyName("clan_id")]
        public long? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public long? ChannelId { get; set; }

        [JsonPropertyName("channel_private")]
        public int? ChannelPrivate { get; set; }

        [JsonPropertyName("user_ids")]
        public List<long>? UserIds { get; set; }

        [JsonPropertyName("role_ids")]
        public List<long>? RoleIds { get; set; }
    }

    // ===== From: ApiMessageAttachment.cs =====

    public class ApiMessageAttachment : MezonBaseModel<ApiMessageAttachment>
    {
        [JsonPropertyName("filename")]
        public string? Filename { get; set; }

        [JsonPropertyName("filetype")]
        public string? Filetype { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }

        [JsonPropertyName("size")]
        public int? Size { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        [JsonPropertyName("channel_id")]
        public int? ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int? Mode { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("message_id")]
        public int? MessageId { get; set; }

        [JsonPropertyName("sender_id")]
        public int? SenderId { get; set; }

    }

    // ===== From: ApiMessageDeleted.cs =====

    public class ApiMessageDeleted : MezonBaseModel<ApiMessageDeleted>
    {
        [JsonPropertyName("deletor")]
        public string? Deletor { get; set; }

        [JsonPropertyName("message_id")]
        public int? MessageId { get; set; }

    }

    // ===== From: ApiMessageMention.cs =====

    public class ApiMessageMention : MezonBaseModel<ApiMessageMention>
    {
        [JsonPropertyName("create_time")]
        public string? CreateTime { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("role_id")]
        public int? RoleId { get; set; }

        [JsonPropertyName("rolename")]
        public string? Rolename { get; set; }

        [JsonPropertyName("s")]
        public int? S { get; set; }

        [JsonPropertyName("e")]
        public int? E { get; set; }

        [JsonPropertyName("channel_id")]
        public int? ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int? Mode { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("message_id")]
        public int? MessageId { get; set; }

        [JsonPropertyName("sender_id")]
        public int? SenderId { get; set; }

    }

    // ===== From: ApiMessageReaction.cs =====

    public class ApiMessageReaction : MezonBaseModel<ApiMessageReaction>
    {
        [JsonPropertyName("action")]
        public bool? Action { get; set; }

        [JsonPropertyName("emoji_id")]
        public int? EmojiId { get; set; }

        [JsonPropertyName("emoji")]
        public string? Emoji { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("sender_id")]
        public int? SenderId { get; set; }

        [JsonPropertyName("sender_name")]
        public string? SenderName { get; set; }

        [JsonPropertyName("sender_avatar")]
        public string? SenderAvatar { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("channel_id")]
        public int? ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int? Mode { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("message_id")]
        public int? MessageId { get; set; }

    }

    // ===== From: ApiMessageRef.cs =====

    public class ApiMessageRef : MezonBaseModel<ApiMessageRef>
    {
        [JsonPropertyName("message_id")]
        public int? MessageId { get; set; }

        [JsonPropertyName("message_ref_id")]
        public int MessageRefId { get; set; }

        [JsonPropertyName("ref_type")]
        public int? RefType { get; set; }

        [JsonPropertyName("message_sender_id")]
        public int MessageSenderId { get; set; }

        [JsonPropertyName("message_sender_username")]
        public string? MessageSenderUsername { get; set; }

        [JsonPropertyName("message_sender_avatar")]
        public string? MessageSenderAvatar { get; set; }

        [JsonPropertyName("message_sender_clan_nick")]
        public string? MessageSenderClanNick { get; set; }

        [JsonPropertyName("message_sender_display_name")]
        public string? MessageSenderDisplayName { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("has_attachment")]
        public bool? HasAttachment { get; set; }

        [JsonPropertyName("channel_id")]
        public int? ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int? Mode { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

    }

    // ===== From: ApiPermission.cs =====

    public class ApiPermission : MezonBaseModel<ApiPermission>
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("active")]
        public int? Active { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("level")]
        public int? Level { get; set; }

        [JsonPropertyName("scope")]
        public int? Scope { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

    }

    // ===== From: ApiPermissionList.cs =====

    public class ApiPermissionList : MezonBaseModel<ApiPermissionList>
    {
        [JsonPropertyName("max_level_permission")]
        public int? MaxLevelPermission { get; set; }

        [JsonPropertyName("permissions")]
        public List<ApiPermission>? Permissions { get; set; }

    }

    // ===== From: ApiQuickMenuAccess.cs =====

    public class ApiQuickMenuAccess : MezonBaseModel<ApiQuickMenuAccess>
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("bot_id")]
        public int? BotId { get; set; }

        [JsonPropertyName("clan_id")]
        public int? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int? ChannelId { get; set; }

        [JsonPropertyName("menu_name")]
        public string? MenuName { get; set; }

        [JsonPropertyName("background")]
        public string? Background { get; set; }

        [JsonPropertyName("action_msg")]
        public string? ActionMsg { get; set; }

        [JsonPropertyName("menu_type")]
        public int? MenuType { get; set; }

    }

    // ===== From: ApiQuickMenuAccessList.cs =====

    public class ApiQuickMenuAccessList : MezonBaseModel<ApiQuickMenuAccessList>
    {
        [JsonPropertyName("list_menus")]
        public List<ApiQuickMenuAccess>? ListMenus { get; set; }

        public static ApiQuickMenuAccessList FromProtobuf (QuickMenuAccessList message)
        {
            var menus = new List<ApiQuickMenuAccess>();

            foreach (var menu in message.ListMenus)
            {
                var item = ApiQuickMenuAccess.FromProtobuf(menu);
                if (item is not null)
                {
                    menus.Add(item);
                }
            }

            return new ApiQuickMenuAccessList
            {
                ListMenus = menus.Count > 0 ? menus : null
            };
        }
    }

    // ===== From: ApiRole.cs =====

    public class ApiRole : MezonBaseModel<ApiRole>
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("role_icon")]
        public string? RoleIcon { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("creator_id")]
        public int? CreatorId { get; set; }

        [JsonPropertyName("clan_id")]
        public int? ClanId { get; set; }

        [JsonPropertyName("active")]
        public int? Active { get; set; }

        [JsonPropertyName("display_online")]
        public int? DisplayOnline { get; set; }

        [JsonPropertyName("allow_mention")]
        public int? AllowMention { get; set; }

        [JsonPropertyName("max_level_permission")]
        public int? MaxLevelPermission { get; set; }

        [JsonPropertyName("order_role")]
        public int? OrderRole { get; set; }

        [JsonPropertyName("channel_ids")]
        public List<int>? ChannelIds { get; set; }

        [JsonPropertyName("permission_list")]
        public ApiPermissionList? PermissionList { get; set; }

        [JsonPropertyName("role_user_list")]
        public ApiRoleUserList? RoleUserList { get; set; }

        [JsonPropertyName("role_channel_active")]
        public int? RoleChannelActive { get; set; }

    }

    // ===== From: ApiRoleList.cs =====

    public class ApiRoleList : MezonBaseModel<ApiRoleList>
    {
        [JsonPropertyName("cacheable_cursor")]
        public string? CacheableCursor { get; set; }

        [JsonPropertyName("next_cursor")]
        public string? NextCursor { get; set; }

        [JsonPropertyName("prev_cursor")]
        public string? PrevCursor { get; set; }

        [JsonPropertyName("roles")]
        public List<ApiRole>? Roles { get; set; }

    }

    // ===== From: ApiRoleListEventResponse.cs =====

    public class ApiRoleListEventResponse : MezonBaseModel<ApiRoleListEventResponse>
    {
        [JsonPropertyName("clan_id")]
        public int? ClanId { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        [JsonPropertyName("limit")]
        public string? Limit { get; set; }

        [JsonPropertyName("roles")]
        public ApiRoleList? Roles { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

    }

    // ===== From: ApiRoleUserList.cs =====

    public class ApiRoleUserList : MezonBaseModel<ApiRoleUserList>
    {
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        [JsonPropertyName("role_users")]
        public List<RoleUserListRoleUser>? RoleUsers { get; set; }

    }

    // ===== From: ApiSentTokenRequest.cs =====

    public class ApiSentTokenRequest : MezonBaseModel<ApiSentTokenRequest>
    {
        [JsonPropertyName("receiver_id")]
        public int ReceiverId { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("sender_id")]
        public int? SenderId { get; set; }

        [JsonPropertyName("sender_name")]
        public string? SenderName { get; set; }

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [JsonPropertyName("extra_attribute")]
        public string? ExtraAttribute { get; set; }

        [JsonPropertyName("mmn_extra_info")]
        public Dictionary<string, object>? MmnExtraInfo { get; set; }

        [JsonPropertyName("timestamp")]
        public int? Timestamp { get; set; }

    }

    // ===== From: ApiSession.cs =====

    public class ApiSession : MezonBaseModel<ApiSession>
    {
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

        [JsonPropertyName("api_url")]
        public string? ApiUrl { get; set; }

        [JsonPropertyName("id_token")]
        public string? IdToken { get; set; }

        [JsonPropertyName("ws_url")]
        public string? WsUrl { get; set; }

        [JsonPropertyName("session_id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("tcp_url")]
        public string? TcpUrl { get; set; }

    }

    // ===== From: ApiVoiceChannelUser.cs =====

    public class ApiVoiceChannelUser : MezonBaseModel<ApiVoiceChannelUser>
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("channel_id")]
        public int? ChannelId { get; set; }

        [JsonPropertyName("participant")]
        public string? Participant { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

    }

    // ===== From: ApiVoiceChannelUserList.cs =====

    public class ApiVoiceChannelUserList : MezonBaseModel<ApiVoiceChannelUserList>
    {
        [JsonPropertyName("voice_channel_users")]
        public List<ApiVoiceChannelUser>? VoiceChannelUsers { get; set; }

    }

    // ===== From: ButtonMessage.cs =====

    public class ButtonMessage
    {
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("disable")]
        public bool? Disable { get; set; }

        [JsonPropertyName("style")]
        public int? Style { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

    }

    // ===== From: ButtonMessageStyle.cs =====
    public enum ButtonMessageStyle
    {
        Primary = 1,
        Secondary = 2,
        Success = 3,
        Danger = 4,
        Link = 5,
    }

    // ===== From: Channel.cs =====

    public class Channel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("chanel_label")]
        public string? ChanelLabel { get; set; }

        [JsonPropertyName("presences")]
        public List<Presence>? Presences { get; set; }

        [JsonPropertyName("self")]
        public Presence? SelfPresence { get; set; }

        [JsonPropertyName("clan_logo")]
        public string? ClanLogo { get; set; }

        [JsonPropertyName("category_name")]
        public string? CategoryName { get; set; }

    }

    // ===== From: ChannelCreatedEvent.cs =====

    public class ChannelCreatedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("category_id")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("creator_id")]
        public int? CreatorId { get; set; }

        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("channel_private")]
        public int? ChannelPrivate { get; set; }

        [JsonPropertyName("channel_type")]
        public int? ChannelType { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("app_id")]
        public int? AppId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("channel_avatar")]
        public string? ChannelAvatar { get; set; }

    }

    // ===== From: ChannelDeletedEvent.cs =====

    public class ChannelDeletedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("category_id")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        [JsonPropertyName("deletor")]
        public string? Deletor { get; set; }

    }

    // ===== From: ChannelJoin.cs =====

    public class ChannelJoin
    {
        [JsonPropertyName("channel_join")]
        public Dictionary<string, object>? Channel_join { get; set; }

    }

    // ===== From: ChannelLeave.cs =====

    public class ChannelLeave
    {
        [JsonPropertyName("channel_leave")]
        public Dictionary<string, object>? Channel_leave { get; set; }

    }

    // ===== From: ChannelMessage.cs =====

    public class ChannelMessage
    {
        [JsonIgnore]
        public int Id => MessageId;
        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("sender_id")]
        public int SenderId { get; set; }

        [JsonPropertyName("content")]
        public Dictionary<string, object>? Content { get; set; }

        [JsonPropertyName("mentions")]
        public List<ApiMessageMention>? Mentions { get; set; }

        [JsonPropertyName("attachments")]
        public List<ApiMessageAttachment>? Attachments { get; set; }

        [JsonPropertyName("reactions")]
        public List<ApiMessageReaction>? Reactions { get; set; }

        [JsonPropertyName("references")]
        public List<ApiMessageRef>? References { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("clan_nick")]
        public string? ClanNick { get; set; }

        [JsonPropertyName("clan_avatar")]
        public string? ClanAvatar { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("clan_logo")]
        public string? ClanLogo { get; set; }

        [JsonPropertyName("category_name")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("create_time_seconds")]
        public int? CreateTimeSeconds { get; set; }

        [JsonPropertyName("update_time_seconds")]
        public int? UpdateTimeSeconds { get; set; }

        [JsonPropertyName("mode")]
        public int? Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool? IsPublic { get; set; }

        [JsonPropertyName("hide_editted")]
        public bool? HideEditted { get; set; }

        [JsonPropertyName("topic_id")]
        public int? TopicId { get; set; }

        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("referenced_message")]
        public byte[]? ReferencedMessage { get; set; }

        public static ChannelMessage FromProtobuf(ApiPb.ChannelMessage message)
        {
            return new ChannelMessage
            {
                MessageId = ToInt(message.MessageId) ?? 0,
                ClanId = ToInt(message.ClanId) ?? 0,
                ChannelId = ToInt(message.ChannelId) ?? 0,
                SenderId = ToInt(message.SenderId) ?? 0,

                Content = SafeJsonParse<Dictionary<string, object>>(message.Content, new()),

                Mentions = DecodeMentions(message.Mentions),
                Attachments = DecodeAttachments(message.Attachments),
                Reactions = DecodeReactions(message.Reactions),
                References = DecodeReferences(message.References),

                Username = message.Username,
                Avatar = message.Avatar,
                DisplayName = message.DisplayName,
                ClanNick = message.ClanNick,
                ClanAvatar = message.ClanAvatar,
                ChannelLabel = message.ChannelLabel,
                ClanLogo = message.ClanLogo,
                CategoryName = message.CategoryName,

                CreateTimeSeconds = ToInt(message.CreateTimeSeconds),
                UpdateTimeSeconds = ToInt(message.UpdateTimeSeconds),
                Mode = ToInt(message.Mode),
                IsPublic = message.IsPublic,
                HideEditted = message.HideEditted,
                TopicId = ToInt(message.TopicId),
                Code = ToInt(message.Code),
                ReferencedMessage = message.ReferencedMessage.ToByteArray()
            };
        }

        private static T SafeJsonParse<T>(string? json, T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(json))
                return defaultValue;

            try
            {
                return JsonSerializer.Deserialize<T>(json) ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        private static List<ApiMessageMention> DecodeMentions(Google.Protobuf.ByteString data)
        {
            if (data == null || data.Length == 0) return new();

            try
            {
                var list = ApiPb.MessageMentionList.Parser.ParseFrom(data);

                return list.Mentions.Select(m => new ApiMessageMention
                {
                    Id = ToInt(m.Id) ?? 0,
                    UserId = ToInt(m.UserId) ?? 0,
                    Username = m.Username,
                    RoleId = ToInt(m.RoleId) ?? 0,
                    Rolename = m.Rolename,
                    S = ToInt(m.S) ?? 0,
                    E = ToInt(m.E) ?? 0
                }).ToList();
            }
            catch
            {
                return new();
            }
        }

        private static List<ApiMessageAttachment> DecodeAttachments(Google.Protobuf.ByteString data)
        {
            if (data == null || data.Length == 0) return new();

            try
            {
                var list = ApiPb.MessageAttachmentList.Parser.ParseFrom(data);

                return list.Attachments.Select(a => new ApiMessageAttachment
                {
                    Filename = a.Filename,
                    Filetype = a.Filetype,
                    Height = a.Height,
                    Size = a.Size,
                    Url = a.Url,
                    Width = a.Width,
                    Thumbnail = a.Thumbnail,
                    Duration = a.Duration
                }).ToList();
            }
            catch
            {
                return new();
            }
        }

        private static List<ApiMessageReaction> DecodeReactions(Google.Protobuf.ByteString data)
        {
            if (data == null || data.Length == 0) return new();

            try
            {
                var list = ApiPb.MessageReactionList.Parser.ParseFrom(data);

                return list.Reactions.Select(r => new ApiMessageReaction
                {
                    Action = r.Action,
                    EmojiId = ToInt(r.EmojiId) ?? 0,
                    Emoji = r.Emoji,
                    Id = ToInt(r.Id) ?? 0,
                    SenderId = ToInt(r.SenderId) ?? 0,
                    SenderName = r.SenderName,
                    SenderAvatar = r.SenderAvatar,
                    Count = r.Count
                }).ToList();
            }
            catch
            {
                return new();
            }
        }

        private static List<ApiMessageRef> DecodeReferences(Google.Protobuf.ByteString data)
        {
            if (data == null || data.Length == 0) return new();

            try
            {
                var list = ApiPb.MessageRefList.Parser.ParseFrom(data);


                return list.Refs.Select(r => new ApiMessageRef
                {
                    MessageId = ToInt(r.MessageId) ?? 0,
                    MessageRefId = ToInt(r.MessageRefId) ?? 0,
                    RefType = r.RefType,
                    MessageSenderId = ToInt(r.MessageSenderId) ?? 0,
                    MessageSenderUsername = r.MessageSenderUsername,
                    MessageSenderDisplayName = r.MessageSenderDisplayName,
                    MessageSenderAvatar = r.MessageSenderAvatar,
                    HasAttachment = r.HasAttachment,
                    MessageSenderClanNick = r.MessageSenderClanNick,
                    Content = r.Content
                }).ToList();
            }
            catch
            {
                return new();
            }
        }

        public Dictionary<string, object?> ToMessageDict()
        {
            var json = JsonSerializer.Serialize(this);

            return JsonSerializer.Deserialize<Dictionary<string, object?>>(json)
                   ?? new Dictionary<string, object?>();
        }

        public Dictionary<string, object> ToDbDict()
        {
            return new Dictionary<string, object>
            {
                ["message_id"] = MessageId,
                ["clan_id"] = ClanId,
                ["channel_id"] = ChannelId,
                ["sender_id"] = SenderId,
                ["content"] = Content ?? new(),
                ["reactions"] = Reactions?.Select(r => r).ToList() ?? new(),
                ["mentions"] = Mentions?.Select(m => m).ToList() ?? new(),
                ["attachments"] = Attachments?.Select(a => a).ToList() ?? new(),
                ["references"] = References?.Select(r => r).ToList() ?? new(),
                ["create_time_seconds"] = CreateTimeSeconds ?? 0
            };
        }

        public static ChannelMessage FromDictionary(Dictionary<string, object> dict)
        {
            var reactionsData = ParseJson<List<object>>(dict, "reactions") ?? new List<object>();
            var mentionsData = ParseJson<List<object>>(dict, "mentions") ?? new List<object>();
            var attachmentsData = ParseJson<List<object>>(dict, "attachments") ?? new List<object>();
            var referencesData = ParseJson<List<object>>(dict, "msg_references") ?? new List<object>();
            var contentData = ParseJson<object>(dict, "content") ?? new Dictionary<string, object>();

            return new ChannelMessage
            {
                MessageId = ToInt(dict["id"]) ?? 0,
                ClanId = ToInt(dict["clan_id"]) ?? 0,
                ChannelId = ToInt(dict["channel_id"]) ?? 0,
                SenderId = ToInt(dict["sender_id"]) ?? 0,

                Content = contentData as Dictionary<string, object>,

                Reactions = reactionsData
                    .Select(r => SafeConvert<ApiMessageReaction>(r))
                    .Where(r => r != null)
                    .ToList()!,

                Mentions = mentionsData
                    .Select(m => SafeConvert<ApiMessageMention>(m))
                    .Where(m => m != null)
                    .ToList()!,

                Attachments = attachmentsData
                    .Select(a => SafeConvert<ApiMessageAttachment>(a))
                    .Where(a => a != null)
                    .ToList()!,

                References = referencesData
                    .Select(r => SafeConvert<ApiMessageRef>(r))
                    .Where(r => r != null)
                    .ToList()!,

                CreateTimeSeconds = ToInt(dict["create_time_seconds"]) ?? 0,
                TopicId = ToInt(dict["topic_id"]) ?? 0
            };
        }

        // =========================
        // JSON PARSER (matches the Python logic)
        // =========================
        private static T? ParseJson<T>(Dictionary<string, object> dict, string key)
        {
            if (!dict.ContainsKey(key) || dict[key] == null)
                return default;

            try
            {
                var val = dict[key];

                // If the value is a string, parse it as JSON.
                if (val is string str)
                {
                    if (string.IsNullOrWhiteSpace(str))
                        return default;

                    return JsonSerializer.Deserialize<T>(str);
                }

                // If the value is already an object, convert it through JSON.
                var json = JsonSerializer.Serialize(val);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return default;
            }
        }

        // =========================
        // SAFE CONVERT (matches model_validate)
        // =========================
        private static T? SafeConvert<T>(object obj)
        {
            try
            {
                var json = JsonSerializer.Serialize(obj);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return default;
            }
        }
    }

    // ===== From: ChannelMessageAck.cs =====

    public class ChannelMessageAck
    {
        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int? Mode { get; set; }

        [JsonPropertyName("message_id")]
        public int? MessageId { get; set; }

        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("create_time")]
        public string? CreateTime { get; set; }

        [JsonPropertyName("update_time")]
        public string? UpdateTime { get; set; }

        [JsonPropertyName("persistence")]
        public bool? Persistence { get; set; }

        [JsonPropertyName("clan_id")]
        public int? ClanId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("is_public")]
        public bool? IsPublic { get; set; }

    }

    // ===== From: ChannelMessageContent.cs =====

    public class ChannelMessageContent
    {
        [JsonPropertyName("t")]
        public string? Text { get; set; }

        [JsonPropertyName("contentThread")]
        public string? ContentThread { get; set; }

        [JsonPropertyName("hg")]
        public List<HashtagOnMessage>? Hashtags { get; set; }

        [JsonPropertyName("ej")]
        public List<EmojiOnMessage>? Emojis { get; set; }

        [JsonPropertyName("lk")]
        public List<LinkOnMessage>? Links { get; set; }

        [JsonPropertyName("mk")]
        public List<MarkdownOnMessage>? Markdown { get; set; }

        [JsonPropertyName("vk")]
        public List<LinkVoiceRoomOnMessage>? VoiceLinks { get; set; }

        [JsonPropertyName("embed")]
        public List<InteractiveMessageProps>? Embed { get; set; }

        [JsonPropertyName("components")]
        public List<MessageActionRow>? Components { get; set; }

    }

    // ===== From: ChannelMessageRemove.cs =====

    public class ChannelMessageRemove
    {
        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

    }

    // ===== From: ChannelMessageSend.cs =====

    public class ChannelMessageSend
    {
        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("content")]
        public object? Content { get; set; }

        [JsonPropertyName("mentions")]
        public List<ApiMessageMention>? Mentions { get; set; }

        [JsonPropertyName("attachments")]
        public List<ApiMessageAttachment>? Attachments { get; set; }

        [JsonPropertyName("references")]
        public List<ApiMessageRef>? References { get; set; }

    }

    // ===== From: ChannelMessageUpdate.cs =====

    public class ChannelMessageUpdate
    {
        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("content")]
        public object? Content { get; set; }

    }

    // ===== From: ChannelUpdatedEvent.cs =====

    public class ChannelUpdatedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("category_id")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("creator_id")]
        public int? CreatorId { get; set; }

        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("channel_type")]
        public int? ChannelType { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("meeting_code")]
        public string? MeetingCode { get; set; }

        [JsonPropertyName("is_error")]
        public bool? IsError { get; set; }

        [JsonPropertyName("channel_private")]
        public bool? ChannelPrivate { get; set; }

        [JsonPropertyName("app_id")]
        public int? AppId { get; set; }

        [JsonPropertyName("e2ee")]
        public int? E2Ee { get; set; }

        [JsonPropertyName("topic")]
        public string? Topic { get; set; }

        [JsonPropertyName("age_restricted")]
        public int? AgeRestricted { get; set; }

        [JsonPropertyName("active")]
        public int? Active { get; set; }

        [JsonPropertyName("count_mess_unread")]
        public int? CountMessUnread { get; set; }

        [JsonPropertyName("user_ids")]
        public List<int>? UserIds { get; set; }

        [JsonPropertyName("role_ids")]
        public List<int>? RoleIds { get; set; }

        [JsonPropertyName("channel_avatar")]
        public string? ChannelAvatar { get; set; }

    }

    // ===== From: ClanDesc.cs =====

    public class ClanDesc
    {
        [JsonPropertyName("banner")]
        public string? Banner { get; set; }

        [JsonPropertyName("clan_id")]
        public int? ClanId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("creator_id")]
        public int? CreatorId { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

    }

    // ===== From: ClanJoin.cs =====

    public class ClanJoin
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

    }

    // ===== From: ClanNameExistedEvent.cs =====

    public class ClanNameExistedEvent
    {
        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("exist")]
        public bool Exist { get; set; }

    }

    // ===== From: ClanProfileUpdatedEvent.cs =====

    public class ClanProfileUpdatedEvent
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("clan_nick")]
        public string? ClanNick { get; set; }

        [JsonPropertyName("clan_avatar")]
        public string? ClanAvatar { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

    }

    // ===== From: ClanUpdatedEvent.cs =====

    public class ClanUpdatedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("clan_logo")]
        public string? ClanLogo { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("banner")]
        public string? Banner { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("is_onboarding")]
        public bool? IsOnboarding { get; set; }

        [JsonPropertyName("welcome_channel_id")]
        public string? WelcomeChannelId { get; set; }

        [JsonPropertyName("onboarding_banner")]
        public string? OnboardingBanner { get; set; }

        [JsonPropertyName("community_banner")]
        public string? CommunityBanner { get; set; }

        [JsonPropertyName("is_community")]
        public bool? IsCommunity { get; set; }

        [JsonPropertyName("about")]
        public string? About { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("prevent_anonymous")]
        public bool? PreventAnonymous { get; set; }

    }

    // ===== From: CustomStatusEvent.cs =====

    public class CustomStatusEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("time_reset")]
        public int? TimeReset { get; set; }

        [JsonPropertyName("no_clear")]
        public bool? NoClear { get; set; }

    }

    // ===== From: DropdownBoxSelected.cs =====

    public class DropdownBoxSelected
    {
        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("selectbox_id")]
        public string? SelectboxId { get; set; }

        [JsonPropertyName("sender_id")]
        public int SenderId { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("values")]
        public List<string>? Values { get; set; }

    }

    // ===== From: EMarkdownType.cs =====

    public enum EMarkdownType
    {
        Triple,
        Single,
        Pre,
        Code,
        Bold,
        Link,
        VoiceLink,
        LinkYoutube,
    }

    public static class EMarkdownTypeExtensions
    {
        public static string ToWireValue(this EMarkdownType t) => t switch
        {
            EMarkdownType.Triple => "t",
            EMarkdownType.Single => "s",
            EMarkdownType.Pre => "pre",
            EMarkdownType.Code => "c",
            EMarkdownType.Bold => "b",
            EMarkdownType.Link => "lk",
            EMarkdownType.VoiceLink => "vk",
            EMarkdownType.LinkYoutube => "lk_yt",
            _ => ""
        };
    }

    public sealed class EMarkdownTypeWireConverter : JsonConverter<EMarkdownType?>
    {
        public override EMarkdownType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                return value switch
                {
                    "t" => EMarkdownType.Triple,
                    "s" => EMarkdownType.Single,
                    "pre" => EMarkdownType.Pre,
                    "c" => EMarkdownType.Code,
                    "b" => EMarkdownType.Bold,
                    "lk" => EMarkdownType.Link,
                    "vk" => EMarkdownType.VoiceLink,
                    "lk_yt" => EMarkdownType.LinkYoutube,
                    _ => null
                };
            }

            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var enumValue))
            {
                if (Enum.IsDefined(typeof(EMarkdownType), enumValue))
                {
                    return (EMarkdownType)enumValue;
                }
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, EMarkdownType? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStringValue(value.Value.ToWireValue());
        }
    }

    // ===== From: EmojiOnMessage.cs =====

    public class EmojiOnMessage : StartEndIndex
    {
        [JsonPropertyName("emojiid")]
        public int? EmojiId { get; set; }

    }

    // ===== From: EphemeralMessageData.cs =====

    public class EphemeralMessageData
    {
        [JsonPropertyName("receiver_id")]
        public int ReceiverId { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("content")]
        public object? Content { get; set; }

        [JsonPropertyName("mentions")]
        public List<ApiMessageMention>? Mentions { get; set; }

        [JsonPropertyName("attachments")]
        public List<ApiMessageAttachment>? Attachments { get; set; }

        [JsonPropertyName("references")]
        public List<ApiMessageRef>? References { get; set; }

        [JsonPropertyName("anonymous_message")]
        public bool? AnonymousMessage { get; set; }

        [JsonPropertyName("mention_everyone")]
        public bool? MentionEveryone { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("topic_id")]
        public int? TopicId { get; set; }

        [JsonPropertyName("message_id")]
        public int? MessageId { get; set; }

    }

    // ===== From: FCMTokens.cs =====

    public class FCMTokens
    {
        [JsonPropertyName("device_id")]
        public string? DeviceId { get; set; }

        [JsonPropertyName("token_id")]
        public string? TokenId { get; set; }

        [JsonPropertyName("platform")]
        public string? Platform { get; set; }

    }

    // ===== From: GiveCoffeeEvent.cs =====

    public class GiveCoffeeEvent
    {
        [JsonPropertyName("sender_id")]
        public int SenderId { get; set; }

        [JsonPropertyName("receiver_id")]
        public int ReceiverId { get; set; }

        [JsonPropertyName("token_count")]
        public int TokenCount { get; set; }

        [JsonPropertyName("message_ref_id")]
        public int MessageRefId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

    }

    // ===== From: HashtagOnMessage.cs =====

    public class HashtagOnMessage : StartEndIndex
    {
        [JsonPropertyName("channelid")]
        public int? ChannelId { get; set; }

    }

    // ===== From: InputFieldOption.cs =====

    public class InputFieldOption
    {
        [JsonPropertyName("defaultValue")]
        public object? Defaultvalue { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("textarea")]
        public bool? Textarea { get; set; }

        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }

    }

    // ===== From: InteractiveMessageAuthor.cs =====

    public class InteractiveMessageAuthor
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

    }

    // ===== From: InteractiveMessageField.cs =====

    public class InteractiveMessageField
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("inline")]
        public bool? Inline { get; set; }

        [JsonPropertyName("options")]
        public List<object>? Options { get; set; }

        [JsonPropertyName("inputs")]
        public Dictionary<string, object>? Inputs { get; set; }

        [JsonPropertyName("max_options")]
        public int? MaxOptions { get; set; }

    }

    // ===== From: InteractiveMessageFooter.cs =====

    public class InteractiveMessageFooter
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; set; }

    }

    // ===== From: InteractiveMessageMedia.cs =====

    public class InteractiveMessageMedia
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("width")]
        public string? Width { get; set; }

        [JsonPropertyName("height")]
        public string? Height { get; set; }

    }

    // ===== From: InteractiveMessageProps.cs =====

    public class InteractiveMessageProps
    {
        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("author")]
        public InteractiveMessageAuthor? Author { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("thumbnail")]
        public InteractiveMessageMedia? Thumbnail { get; set; }

        [JsonPropertyName("fields")]
        public List<InteractiveMessageField>? Fields { get; set; }

        [JsonPropertyName("image")]
        public InteractiveMessageMedia? Image { get; set; }

        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; set; }

        [JsonPropertyName("footer")]
        public InteractiveMessageFooter? Footer { get; set; }

    }

    // ===== From: LastPinMessageEvent.cs =====

    public class LastPinMessageEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("operation")]
        public int Operation { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("timestamp_seconds")]
        public int? TimestampSeconds { get; set; }

        [JsonPropertyName("message_sender_avatar")]
        public string? MessageSenderAvatar { get; set; }

        [JsonPropertyName("message_sender_id")]
        public string? MessageSenderId { get; set; }

        [JsonPropertyName("message_sender_username")]
        public string? MessageSenderUsername { get; set; }

        [JsonPropertyName("message_content")]
        public string? MessageContent { get; set; }

        [JsonPropertyName("message_attachment")]
        public string? MessageAttachment { get; set; }

        [JsonPropertyName("message_created_time")]
        public string? MessageCreatedTime { get; set; }

    }

    // ===== From: LastSeenMessageEvent.cs =====

    public class LastSeenMessageEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("timestamp_seconds")]
        public string? TimestampSeconds { get; set; }

        [JsonPropertyName("badge_count")]
        public int? BadgeCount { get; set; }

    }

    // ===== From: LinkOnMessage.cs =====

    public class LinkOnMessage : StartEndIndex
    {
    }

    // ===== From: LinkVoiceRoomOnMessage.cs =====

    public class LinkVoiceRoomOnMessage : StartEndIndex
    {
    }

    // ===== From: MarkdownOnMessage.cs =====

    public class MarkdownOnMessage : StartEndIndex
    {
        [JsonPropertyName("type")]
        [JsonConverter(typeof(EMarkdownTypeWireConverter))]
        public EMarkdownType? Type { get; set; }

    }

    // ===== From: MessageActionRow.cs =====

    public class MessageActionRow
    {
        [JsonPropertyName("components")]
        public List<MessageComponent>? Components { get; set; }

    }

    // ===== From: MessageComponent.cs =====

    public class MessageComponent
    {
        [JsonPropertyName("type")]
        public object? Type { get; set; }

        [JsonPropertyName("id")]
        public string? ComponentId { get; set; }

        [JsonPropertyName("component")]
        public Dictionary<string, object>? Component { get; set; }

    }

    // ===== From: MessageComponentType.cs =====

    public enum MessageComponentType
    {
        Button = 1,
        Select = 2,
        Input = 3,
        Datepicker = 4,
        Radio = 5,
        Animation = 6,
        Grid = 7,
    }

    // ===== From: MessagePayLoad.cs =====

    public class MessagePayLoad
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("msg")]
        public ChannelMessageContent? Msg { get; set; }

        [JsonPropertyName("mentions")]
        public List<ApiMessageMention>? Mentions { get; set; }

        [JsonPropertyName("attachments")]
        public List<ApiMessageAttachment>? Attachments { get; set; }

        [JsonPropertyName("ref")]
        public List<ApiMessageRef>? Ref { get; set; }

        [JsonPropertyName("hideEditted")]
        public bool? Hideeditted { get; set; }

        [JsonPropertyName("topic_id")]
        public int? TopicId { get; set; }

    }

    // ===== From: MessageSelectType.cs =====

    public enum MessageSelectType
    {
        Text = 1,
        User = 2,
        Role = 3,
        Channel = 4,
    }

    // ===== From: MessageTypingEvent.cs =====

    public class MessageTypingEvent
    {
        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("sender_id")]
        public int SenderId { get; set; }

        [JsonPropertyName("sender_username")]
        public string? SenderUsername { get; set; }

        [JsonPropertyName("sender_display_name")]
        public string? SenderDisplayName { get; set; }

        [JsonPropertyName("mode")]
        public int? Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool? IsPublic { get; set; }

        [JsonPropertyName("clan_id")]
        public int? ClanId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("topic_id")]
        public string? TopicId { get; set; }

    }

    // ===== From: MessageUserPayLoad.cs =====

    public class MessageUserPayLoad
    {
        [JsonPropertyName("userId")]
        public int Userid { get; set; }

        [JsonPropertyName("msg")]
        public string? Msg { get; set; }

        [JsonPropertyName("messOptions")]
        public Dictionary<string, object>? Messoptions { get; set; }

        [JsonPropertyName("attachments")]
        public List<ApiMessageAttachment>? Attachments { get; set; }

        [JsonPropertyName("refs")]
        public List<ApiMessageRef>? Refs { get; set; }

    }

    // ===== From: MezonBaseModel.cs =====

    public class MezonBaseModel
    {
    }

    public abstract class MezonBaseModel<TSelf> : MezonBaseModel
        where TSelf : class
    {
        public static TSelf? FromProtobuf(IMessage message)
        {
            return ProtoUtils.FromProtobuf<TSelf>(message);
        }
    }

    // ===== From: NotificationEvent.cs =====

    public class NotificationEvent
    {
    }

    // ===== From: Ping.cs =====

    public class Ping
    {
    }

    // ===== From: Pong.cs =====

    public class Pong
    {
    }

    // ===== From: Presence.cs =====

    public class Presence
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("session_id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("node")]
        public string? Node { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

    }

    // ===== From: RadioFieldOption.cs =====

    public class RadioFieldOption
    {
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("style")]
        public int? Style { get; set; }

        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }

    }

    // ===== From: ReactMessageData.cs =====

    public class ReactMessageData
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("emoji_id")]
        public int EmojiId { get; set; }

        [JsonPropertyName("emoji")]
        public string? Emoji { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("message_sender_id")]
        public int MessageSenderId { get; set; }

        [JsonPropertyName("action_delete")]
        public bool? ActionDelete { get; set; }

    }

    // ===== From: ReactMessagePayload.cs =====

    public class ReactMessagePayload
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("emoji_id")]
        public int EmojiId { get; set; }

        [JsonPropertyName("emoji")]
        public string? Emoji { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("action_delete")]
        public bool? ActionDelete { get; set; }

    }

    // ===== From: RemoveMessageData.cs =====

    public class RemoveMessageData
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("topic_id")]
        public int? TopicId { get; set; }

    }

    // ===== From: ReplyMessageData.cs =====

    public class ReplyMessageData
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("content")]
        public ChannelMessageContent? Content { get; set; }

        [JsonPropertyName("mentions")]
        public List<ApiMessageMention>? Mentions { get; set; }

        [JsonPropertyName("attachments")]
        public List<ApiMessageAttachment>? Attachments { get; set; }

        [JsonPropertyName("references")]
        public List<ApiMessageRef>? References { get; set; }

        [JsonPropertyName("anonymous_message")]
        public bool? AnonymousMessage { get; set; }

        [JsonPropertyName("mention_everyone")]
        public bool? MentionEveryone { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("topic_id")]
        public int? TopicId { get; set; }

    }

    // ===== From: RoleUserListRoleUser.cs =====

    public class RoleUserListRoleUser
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("lang_tag")]
        public string? LangTag { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("online")]
        public bool? Online { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

    }

    // ===== From: RoomInfo.cs =====

    public class RoomInfo
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; } = string.Empty;

        [JsonPropertyName("room_name")]
        public string RoomName { get; set; } = string.Empty;
    }

    // ===== From: RoomMetadataEvent.cs =====

    public class RoomMetadataEvent
    {
        [JsonPropertyName("event_id")]
        public string EventId { get; set; } = string.Empty;

        [JsonPropertyName("event_type")]
        public string EventType { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;

        [JsonPropertyName("room")]
        public RoomInfo Room { get; set; } = new();

        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = new();
    }

    // ===== From: Rpc.cs =====

    public class Rpc
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("payload")]
        public object? Payload { get; set; }

    }

    // ===== From: SelectFieldOption.cs =====

    public class SelectFieldOption
    {
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

    }

    // ===== From: SendTokenData.cs =====

    public class SendTokenData
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [JsonPropertyName("extra_attribute")]
        public string? ExtraAttribute { get; set; }

    }

    // ===== From: SocketError.cs =====

    public class SocketError
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

    }

    // ===== From: SocketMessage.cs =====

    public class SocketMessage
    {
        [JsonPropertyName("cid")]
        public string? Cid { get; set; }

    }

    // ===== From: StartEndIndex.cs =====

    public class StartEndIndex
    {
        [JsonPropertyName("s")]
        public int? Start { get; set; }

        [JsonPropertyName("e")]
        public int? End { get; set; }

    }

    // ===== From: StreamingJoinedEvent.cs =====

    public class StreamingJoinedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("participant")]
        public string? Participant { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("streaming_channel_label")]
        public string? StreamingChannelLabel { get; set; }

        [JsonPropertyName("streaming_channel_id")]
        public int StreamingChannelId { get; set; }

    }

    // ===== From: StreamingLeavedEvent.cs =====

    public class StreamingLeavedEvent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("streaming_channel_id")]
        public string? StreamingChannelId { get; set; }

        [JsonPropertyName("streaming_user_id")]
        public string? StreamingUserId { get; set; }

    }

    // ===== From: TokenSentEvent.cs =====

    public class TokenSentEvent
    {
        [JsonPropertyName("receiver_id")]
        public int ReceiverId { get; set; }

        [JsonPropertyName("sender_id")]
        public int? SenderId { get; set; }

        [JsonPropertyName("sender_name")]
        public string? SenderName { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [JsonPropertyName("extra_attribute")]
        public string? ExtraAttribute { get; set; }

        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

    }

    // ===== From: UpdateMessageData.cs =====

    public class UpdateMessageData
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("content")]
        public object? Content { get; set; }

        [JsonPropertyName("mentions")]
        public List<ApiMessageMention>? Mentions { get; set; }

        [JsonPropertyName("attachments")]
        public List<ApiMessageAttachment>? Attachments { get; set; }

        [JsonPropertyName("hideEditted")]
        public bool? Hideeditted { get; set; }

        [JsonPropertyName("topic_id")]
        public int? TopicId { get; set; }

        [JsonPropertyName("is_update_msg_topic")]
        public bool? IsUpdateMsgTopic { get; set; }

    }

    // ===== From: UserChannelAddedEvent.cs =====

    public class UserChannelAddedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("channel_desc")]
        public ApiChannelDescription? ChannelDesc { get; set; }

        [JsonPropertyName("users")]
        public List<UserProfileRedis>? Users { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("caller")]
        public UserProfileRedis? Caller { get; set; }

        [JsonPropertyName("create_time_seconds")]
        public int? CreateTimeSeconds { get; set; }

        [JsonPropertyName("active")]
        public int? Active { get; set; }

    }

    // ===== From: UserChannelRemoved.cs =====

    public class UserChannelRemoved
    {
        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("user_ids")]
        public List<int>? UserIds { get; set; }

        [JsonPropertyName("channel_type")]
        public int ChannelType { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("badge_counts")]
        public List<int>? BadgeCounts { get; set; }

    }

    // ===== From: UserClanRemovedEvent.cs =====

    public class UserClanRemovedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("user_ids")]
        public List<int>? UserIds { get; set; }

    }

    // ===== From: UserInitData.cs =====

    public class UserInitData
    {
        [JsonPropertyName("sender_id")]
        public long Id { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("clan_nick")]
        public string? ClanNick { get; set; }

        [JsonPropertyName("clan_avatar")]
        public string? ClanAvatar { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("dmChannelId")]
        public long DmChannelId { get; set; }

        public static UserInitData FromProtobuf (ApiPb.ChannelMessage message, long dmChannelId = 0)
        {
            return new UserInitData
            {
                Id = message.SenderId,
                Username = message.Username ?? "",
                ClanNick = message.ClanNick ?? "",
                ClanAvatar = message.ClanAvatar ?? "",
                Avatar = message.Avatar ?? "",
                DisplayName = message.DisplayName ?? "",
                DmChannelId = dmChannelId
            };
        }

        public Dictionary<string, object?> ToUserDict()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var json = JsonSerializer.Serialize(this, options);

            return JsonSerializer.Deserialize<Dictionary<string, object?>>(json, options)
                   ?? new Dictionary<string, object?>();
        }
    }

    // ===== From: UserProfileRedis.cs =====

    public class UserProfileRedis
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("user_status")]
        public string? UserStatus { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("online")]
        public bool? Online { get; set; }

        [JsonPropertyName("fcm_tokens")]
        public List<FCMTokens>? FcmTokens { get; set; }

        [JsonPropertyName("joined_clans")]
        public List<int>? JoinedClans { get; set; }

        [JsonPropertyName("app_token")]
        public string? AppToken { get; set; }

        [JsonPropertyName("create_time_second")]
        public int? CreateTimeSecond { get; set; }

        [JsonPropertyName("app_url")]
        public string? AppUrl { get; set; }

        [JsonPropertyName("is_bot")]
        public bool? IsBot { get; set; }

        [JsonPropertyName("voip_token")]
        public string? VoipToken { get; set; }

    }

    // ===== From: UserProfileUpdatedEvent.cs =====

    public class UserProfileUpdatedEvent
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("about_me")]
        public string? AboutMe { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("encrypt_private_key")]
        public string? EncryptPrivateKey { get; set; }

    }

    // ===== From: VoiceEndedEvent.cs =====

    public class VoiceEndedEvent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("voice_channel_id")]
        public string? VoiceChannelId { get; set; }

    }

    // ===== From: VoiceJoinedEvent.cs =====

    public class VoiceJoinedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("voice_channel_id")]
        public int VoiceChannelId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("participant")]
        public string? Participant { get; set; }

        [JsonPropertyName("voice_channel_label")]
        public string? VoiceChannelLabel { get; set; }

        [JsonPropertyName("last_screenshot")]
        public string? LastScreenshot { get; set; }

    }

    // ===== From: VoiceLeavedEvent.cs =====

    public class VoiceLeavedEvent
    {
        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("voice_channel_id")]
        public int VoiceChannelId { get; set; }

        [JsonPropertyName("voice_user_id")]
        public int VoiceUserId { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

    }

    // ===== From: VoiceStartedEvent.cs =====

    public class VoiceStartedEvent
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("clan_id")]
        public int ClanId { get; set; }

        [JsonPropertyName("voice_channel_id")]
        public int VoiceChannelId { get; set; }

    }


    // ===== From: ScreenShareEvent.cs =====

    public class ScreenShareEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("voice_channel_id")]
        public string? VoiceChannelId { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("is_sharing")]
        public bool? IsSharing { get; set; }
    }

    // ===== From: TopicInMessageEvent.cs =====

    public class TopicInMessageEvent
    {
        [JsonPropertyName("message_id")]
        public string? MessageId { get; set; }

        [JsonPropertyName("rpl")]
        public int? Rpl { get; set; }

        [JsonPropertyName("lsnt")]
        public string? Lsnt { get; set; }

        [JsonPropertyName("tp_id")]
        public string? TpId { get; set; }
    }

    // ===== From: ApiRequestEvent.cs =====

    public class ApiRequestEvent
    {
        [JsonPropertyName("api_index")]
        public int? ApiIndex { get; set; }

        [JsonPropertyName("api_name")]
        public string? ApiName { get; set; }

        [JsonPropertyName("body")]
        public byte[]? Body { get; set; }
    }

    // ===== From: FollowEvent.cs =====

    public class FollowEvent
    {
    }

    // ===== From: BannedUserEvent.cs =====

    public class BannedUserEvent
    {
        [JsonPropertyName("user_ids")]
        public List<string>? UserIds { get; set; }

        [JsonPropertyName("action")]
        public int? Action { get; set; }

        [JsonPropertyName("banner_id")]
        public string? BannerId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("ban_time")]
        public int? BanTime { get; set; }
    }

    // ===== From: ListChannelUsersBannedEvent.cs =====

    public class ListChannelUsersBannedEvent
    {
        [JsonPropertyName("banned_user_ids")]
        public List<string>? BannedUserIds { get; set; }
    }

    // ===== From: ChannelCanvas.cs =====

    public class ChannelCanvas
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("creator_id")]
        public string? CreatorId { get; set; }

        [JsonPropertyName("editor_id")]
        public string? EditorId { get; set; }

        [JsonPropertyName("is_default")]
        public bool? IsDefault { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }
    }

    // ===== From: IncomingCallPush.cs =====

    public class IncomingCallPush
    {
        [JsonPropertyName("receiver_id")]
        public string? ReceiverId { get; set; }

        [JsonPropertyName("json_data")]
        public string? JsonData { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("caller_id")]
        public string? CallerId { get; set; }
    }

    // ===== From: WebrtcSignalingFwd.cs =====

    public class WebrtcSignalingFwd
    {
        [JsonPropertyName("receiver_id")]
        public string? ReceiverId { get; set; }

        [JsonPropertyName("data_type")]
        public int? DataType { get; set; }

        [JsonPropertyName("json_data")]
        public string? JsonData { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("caller_id")]
        public string? CallerId { get; set; }
    }

    // ===== From: SFUSignalingFwd.cs =====

    public class SFUSignalingFwd
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("data_type")]
        public int? DataType { get; set; }

        [JsonPropertyName("json_data")]
        public string? JsonData { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }
    }

    // ===== From: AddClanUserEvent.cs =====

    public class AddClanUserEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("user")]
        public UserProfileRedis? User { get; set; }

        [JsonPropertyName("invitor")]
        public string? Invitor { get; set; }
    }

    // ===== From: RoleAssignedEvent.cs =====

    public class RoleAssignedEvent
    {
        [JsonPropertyName("ClanId")]
        public string? ClanId { get; set; }

        [JsonPropertyName("role_id")]
        public string? RoleId { get; set; }

        [JsonPropertyName("user_ids_assigned")]
        public List<string>? UserIdsAssigned { get; set; }

        [JsonPropertyName("user_ids_removed")]
        public List<string>? UserIdsRemoved { get; set; }
    }

    // ===== From: PermissionRoleChannel.cs =====

    public class PermissionRoleChannel
    {
        [JsonPropertyName("permission_id")]
        public string? PermissionId { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }
    }

    // ===== From: HashtagDm.cs =====

    public class HashtagDm
    {
        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("meeting_code")]
        public string? MeetingCode { get; set; }

        [JsonPropertyName("type")]
        public int? Type { get; set; }

        [JsonPropertyName("channel_private")]
        public int? ChannelPrivate { get; set; }

        [JsonPropertyName("parent_id")]
        public string? ParentId { get; set; }
    }

    // ===== From: ClanEmoji.cs =====

    public class ClanEmoji
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("src")]
        public string? Src { get; set; }

        [JsonPropertyName("shortname")]
        public string? Shortname { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("creator_id")]
        public string? CreatorId { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }
    }

    // ===== From: ChannelPresenceEvent.cs =====

    public class ChannelPresenceEvent
    {
        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("joins")]
        public List<UserPresence>? Joins { get; set; }

        [JsonPropertyName("leaves")]
        public List<UserPresence>? Leaves { get; set; }

        [JsonPropertyName("clan_logo")]
        public string? ClanLogo { get; set; }

        [JsonPropertyName("category_name")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("mode")]
        public int? Mode { get; set; }
    }

    // ===== From: UserPresence.cs =====

    public class UserPresence
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("session_id")]
        public int? SessionId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("is_mobile")]
        public bool? IsMobile { get; set; }

        [JsonPropertyName("user_status")]
        public string? UserStatus { get; set; }
    }

    // ===== From: Status.cs =====

    public class Status
    {
        [JsonPropertyName("presences")]
        public List<UserPresence>? Presences { get; set; }
    }

    // ===== From: StatusFollow.cs =====

    public class StatusFollow
    {
        [JsonPropertyName("user_ids")]
        public List<string>? UserIds { get; set; }

        [JsonPropertyName("usernames")]
        public List<string>? Usernames { get; set; }
    }

    // ===== From: StatusPresenceEvent.cs =====

    public class StatusPresenceEvent
    {
        [JsonPropertyName("joins")]
        public List<UserPresence>? Joins { get; set; }

        [JsonPropertyName("leaves")]
        public List<UserPresence>? Leaves { get; set; }
    }

    // ===== From: StatusUnfollow.cs =====

    public class StatusUnfollow
    {
        [JsonPropertyName("user_ids")]
        public List<string>? UserIds { get; set; }
    }

    // ===== From: StatusUpdate.cs =====

    public class StatusUpdate
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }

    // ===== From: Stream.cs =====

    public class Stream
    {
        [JsonPropertyName("mode")]
        public int? Mode { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }
    }

    // ===== From: StreamData.cs =====

    public class StreamData
    {
        [JsonPropertyName("stream")]
        public Stream? StreamInfo { get; set; }

        [JsonPropertyName("sender")]
        public UserPresence? Sender { get; set; }

        [JsonPropertyName("data")]
        public string? Data { get; set; }

        [JsonPropertyName("reliable")]
        public bool? Reliable { get; set; }
    }

    // ===== From: StreamPresenceEvent.cs =====

    public class StreamPresenceEvent
    {
        [JsonPropertyName("stream")]
        public Stream? StreamInfo { get; set; }

        [JsonPropertyName("joins")]
        public List<UserPresence>? Joins { get; set; }

        [JsonPropertyName("leaves")]
        public List<UserPresence>? Leaves { get; set; }
    }

    // ===== From: AddFriend.cs =====

    public class AddFriend
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
    }

    // ===== From: RemoveFriend.cs =====

    public class RemoveFriend
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }
    }

    // ===== From: BlockFriend.cs =====

    public class BlockFriend
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }
    }

    // ===== From: UnblockFriend.cs =====

    public class UnblockFriend
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("user_status")]
        public string? UserStatus { get; set; }
    }

    // ===== From: ClanDeletedEvent.cs =====

    public class ClanDeletedEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("deletor")]
        public string? Deletor { get; set; }
    }

    // ===== From: ClanCreatedEvent.cs =====

    public class ClanCreatedEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("creator_id")]
        public string? CreatorId { get; set; }

        [JsonPropertyName("welcome_channel_id")]
        public string? WelcomeChannelId { get; set; }
    }

    // ===== From: StickerCreateEvent.cs =====

    public class StickerCreateEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("shortname")]
        public string? Shortname { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("creator_id")]
        public string? CreatorId { get; set; }

        [JsonPropertyName("sticker_id")]
        public string? StickerId { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }
    }

    // ===== From: StickerUpdateEvent.cs =====

    public class StickerUpdateEvent
    {
        [JsonPropertyName("shortname")]
        public string? Shortname { get; set; }

        [JsonPropertyName("sticker_id")]
        public string? StickerId { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }
    }

    // ===== From: StickerDeleteEvent.cs =====

    public class StickerDeleteEvent
    {
        [JsonPropertyName("sticker_id")]
        public string? StickerId { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }
    }

    // ===== From: RoleEvent.cs =====

    public class RoleEvent
    {
        [JsonPropertyName("role")]
        public ApiRole? Role { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("user_add_ids")]
        public List<string>? UserAddIds { get; set; }

        [JsonPropertyName("user_remove_ids")]
        public List<string>? UserRemoveIds { get; set; }

        [JsonPropertyName("active_permission_ids")]
        public List<string>? ActivePermissionIds { get; set; }

        [JsonPropertyName("remove_permission_ids")]
        public List<string>? RemovePermissionIds { get; set; }
    }

    // ===== From: EventEmoji.cs =====

    public class EventEmoji
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("short_name")]
        public string? ShortName { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("action")]
        public int? Action { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("clan_name")]
        public string? ClanName { get; set; }

        [JsonPropertyName("is_for_sale")]
        public bool? IsForSale { get; set; }
    }

    // ===== From: PermissionSetEvent.cs =====

    public class PermissionSetEvent
    {
        [JsonPropertyName("caller")]
        public string? Caller { get; set; }

        [JsonPropertyName("role_id")]
        public string? RoleId { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("permission_updates")]
        public List<PermissionRoleChannel>? PermissionUpdates { get; set; }
    }

    // ===== From: PermissionChangedEvent.cs =====

    public class PermissionChangedEvent
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("add_permissions")]
        public List<PermissionRoleChannel>? AddPermissions { get; set; }

        [JsonPropertyName("remove_permissions")]
        public List<PermissionRoleChannel>? RemovePermissions { get; set; }

        [JsonPropertyName("default_permissions")]
        public List<PermissionRoleChannel>? DefaultPermissions { get; set; }
    }

    // ===== From: UnmuteEvent.cs =====

    public class UnmuteEvent
    {
        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("category_id")]
        public string? CategoryId { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }
    }

    // ===== From: ListActivity.cs =====

    public class ListActivity
    {
        [JsonPropertyName("acts")]
        public List<Dictionary<string, object>>? Acts { get; set; }
    }

    // ===== From: SdTopicEvent.cs =====

    public class SdTopicEvent
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("message_id")]
        public string? MessageId { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("last_sent_message")]
        public ApiChannelMessageHeader? LastSentMessage { get; set; }

        [JsonPropertyName("message")]
        public ChannelMessage? Message { get; set; }
    }

    // ===== From: ChannelAppEvent.cs =====

    public class ChannelAppEvent
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("action")]
        public int? Action { get; set; }
    }

    // ===== From: UserStatusEvent.cs =====

    public class UserStatusEvent
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("custom_status")]
        public string? CustomStatus { get; set; }
    }

    // ===== From: JoinChannelAppData.cs =====

    public class JoinChannelAppData
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("hash")]
        public string? Hash { get; set; }
    }

    // ===== From: UnpinMessageEvent.cs =====

    public class UnpinMessageEvent
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("message_id")]
        public string? MessageId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }
    }

    // ===== From: CategoryEvent.cs =====

    public class CategoryEvent
    {
        [JsonPropertyName("creator_id")]
        public string? CreatorId { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("category_name")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }
    }

    // ===== From: HandleParticipantMeetStateEvent.cs =====

    public class HandleParticipantMeetStateEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("state")]
        public int? State { get; set; }

        [JsonPropertyName("room_name")]
        public string? RoomName { get; set; }
    }

    // ===== From: DeleteAccountEvent.cs =====

    public class DeleteAccountEvent
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }
    }

    // ===== From: ListDataSocket.cs =====

    public class ListDataSocket
    {
        [JsonPropertyName("api_name")]
        public string? ApiName { get; set; }

        [JsonPropertyName("list_channel_badge_count_req")]
        public Dictionary<string, object>? ListChannelBadgeCountReq { get; set; }

        [JsonPropertyName("channel_badge_count")]
        public Dictionary<string, object>? ChannelBadgeCount { get; set; }

        [JsonPropertyName("clan_badge_count")]
        public Dictionary<string, object>? ClanBadgeCount { get; set; }

        [JsonPropertyName("list_loged_device")]
        public Dictionary<string, object>? ListLogedDevice { get; set; }

        [JsonPropertyName("list_user_online_req")]
        public Dictionary<string, object>? ListUserOnlineReq { get; set; }

        [JsonPropertyName("user_online_list")]
        public Dictionary<string, object>? UserOnlineList { get; set; }
    }

    // ===== From: MeetParticipantEvent.cs =====

    public class MeetParticipantEvent
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("room_name")]
        public string? RoomName { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("action")]
        public int? Action { get; set; }
    }

    // ===== From: TransferOwnershipEvent.cs =====

    public class TransferOwnershipEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("prev_owner")]
        public string? PrevOwner { get; set; }

        [JsonPropertyName("curr_owner")]
        public string? CurrOwner { get; set; }
    }

    // ===== From: ActiveArchivedThread.cs =====

    public class ActiveArchivedThread
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }
    }

    // ===== From: AllowAnonymousEvent.cs =====

    public class AllowAnonymousEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("allow")]
        public bool? Allow { get; set; }
    }

    // ===== From: AIAgentEnabledEvent.cs =====

    public class AIAgentEnabledEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("room_name")]
        public string? RoomName { get; set; }

        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }
    }

    // ===== From: ChannelArchiveEvent.cs =====

    public class ChannelArchiveEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("category_id")]
        public string? CategoryId { get; set; }

        [JsonPropertyName("creator_id")]
        public string? CreatorId { get; set; }

        [JsonPropertyName("parent_id")]
        public string? ParentId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("channel_label")]
        public string? ChannelLabel { get; set; }

        [JsonPropertyName("channel_type")]
        public int? ChannelType { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("meeting_code")]
        public string? MeetingCode { get; set; }

        [JsonPropertyName("is_error")]
        public bool? IsError { get; set; }

        [JsonPropertyName("channel_private")]
        public bool? ChannelPrivate { get; set; }

        [JsonPropertyName("app_id")]
        public string? AppId { get; set; }

        [JsonPropertyName("e2ee")]
        public int? E2Ee { get; set; }

        [JsonPropertyName("topic")]
        public string? Topic { get; set; }

        [JsonPropertyName("age_restricted")]
        public int? AgeRestricted { get; set; }

        [JsonPropertyName("active")]
        public int? Active { get; set; }

        [JsonPropertyName("count_mess_unread")]
        public int? CountMessUnread { get; set; }

        [JsonPropertyName("user_ids")]
        public List<string>? UserIds { get; set; }

        [JsonPropertyName("role_ids")]
        public List<string>? RoleIds { get; set; }

        [JsonPropertyName("channel_avatar")]
        public string? ChannelAvatar { get; set; }
    }

    // ===== From: VoiceReactionSend.cs =====

    public class VoiceReactionSend
    {
        [JsonPropertyName("emojis")]
        public List<string>? Emojis { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("sender_id")]
        public string? SenderId { get; set; }

        [JsonPropertyName("media_type")]
        public int? MediaType { get; set; }
    }

    // ===== From: MarkAsRead.cs =====

    public class MarkAsRead
    {
        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("category_id")]
        public string? CategoryId { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }
    }

    // ===== From: QuickMenuDataEvent.cs =====

    public class QuickMenuDataEvent
    {
        [JsonPropertyName("menu_name")]
        public string? MenuName { get; set; }

        [JsonPropertyName("sender_id")]
        public string? SenderId { get; set; }

        [JsonPropertyName("message_sender_id")]
        public string? MessageSenderId { get; set; }
    }

    // ===== From: StreamingStartedEvent.cs =====

    public class StreamingStartedEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("streaming_url")]
        public string? StreamingUrl { get; set; }

        [JsonPropertyName("is_streaming")]
        public bool? IsStreaming { get; set; }
    }

    // ===== From: StreamingEndedEvent.cs =====

    public class StreamingEndedEvent
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }
    }

    // ===== From: GotifyMessage.cs =====

    public class GotifyMessage
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("channel_id")]
        public string? ChannelId { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }

        [JsonPropertyName("users")]
        public List<string>? Users { get; set; }

        [JsonPropertyName("extras")]
        public Dictionary<string, string>? Extras { get; set; }

        [JsonPropertyName("app_id")]
        public int? AppId { get; set; }

        [JsonPropertyName("sender_id")]
        public string? SenderId { get; set; }
    }

    // ===== From: FcmDataPayload.cs =====

    public class FcmDataPayload
    {
        [JsonPropertyName("command_type")]
        public int? CommandType { get; set; }

        [JsonPropertyName("receiver_id")]
        public string? ReceiverId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("body")]
        public byte[]? Body { get; set; }

        [JsonPropertyName("user_role_ids")]
        public List<string>? UserRoleIds { get; set; }

        [JsonPropertyName("user_sent_ids")]
        public List<string>? UserSentIds { get; set; }

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }

        [JsonPropertyName("is_e2ee")]
        public bool? IsE2Ee { get; set; }

        [JsonPropertyName("is_dm")]
        public bool? IsDm { get; set; }

        [JsonPropertyName("mention_here")]
        public bool? MentionHere { get; set; }
    }

    // ===== From: ConfirmLinkMezonOTPData.cs =====

    public class ConfirmLinkMezonOTPData
    {
        [JsonPropertyName("type")]
        public int? Type { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }

    // ===== From: UserChannelAdded.cs =====

    public class UserChannelAdded
    {
        [JsonPropertyName("channel_desc")]
        public ApiChannelDescription? ChannelDesc { get; set; }

        [JsonPropertyName("users")]
        public List<UserProfileRedis>? Users { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("caller")]
        public UserProfileRedis? Caller { get; set; }

        [JsonPropertyName("create_time_seconds")]
        public int? CreateTimeSeconds { get; set; }

        [JsonPropertyName("active")]
        public int? Active { get; set; }
    }

    // ===== From: UserClanRemoved.cs =====

    public class UserClanRemoved
    {
        [JsonPropertyName("clan_id")]
        public string? ClanId { get; set; }

        [JsonPropertyName("user_ids")]
        public List<string>? UserIds { get; set; }
    }

}
