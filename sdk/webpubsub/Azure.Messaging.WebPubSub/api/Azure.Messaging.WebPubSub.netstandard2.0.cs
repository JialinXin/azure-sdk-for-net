namespace Azure.Messaging.WebPubSub
{
    public sealed partial class ClientCertificateInfo
    {
        internal ClientCertificateInfo() { }
        public string Thumbprint { get { throw null; } }
    }
    public partial class ConnectedEventRequest : Azure.Messaging.WebPubSub.ServiceRequest
    {
        internal ConnectedEventRequest() : base (default(Azure.Messaging.WebPubSub.ConnectionContext)) { }
        public override string Name { get { throw null; } }
    }
    public sealed partial class ConnectEventRequest : Azure.Messaging.WebPubSub.ServiceRequest
    {
        internal ConnectEventRequest() : base (default(Azure.Messaging.WebPubSub.ConnectionContext)) { }
        public System.Collections.Generic.IDictionary<string, string[]> Claims { get { throw null; } }
        public Azure.Messaging.WebPubSub.ClientCertificateInfo[] ClientCertificates { get { throw null; } }
        public override string Name { get { throw null; } }
        public System.Collections.Generic.IDictionary<string, string[]> Query { get { throw null; } }
        public string[] Subprotocols { get { throw null; } }
    }
    public partial class ConnectionContext
    {
        public ConnectionContext() { }
        public string ConnectionId { get { throw null; } }
        public string EventName { get { throw null; } }
        public Azure.Messaging.WebPubSub.WebPubSubEventType EventType { get { throw null; } }
        public System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues> Headers { get { throw null; } }
        public string Hub { get { throw null; } }
        public string Origin { get { throw null; } }
        public string Signature { get { throw null; } }
        public System.Collections.Generic.Dictionary<string, object> States { get { throw null; } }
        public string UserId { get { throw null; } }
    }
    public partial class ConnectResponse : Azure.Messaging.WebPubSub.ServiceResponse
    {
        public ConnectResponse() { }
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("groups")]
        public string[] Groups { get { throw null; } set { } }
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("roles")]
        public string[] Roles { get { throw null; } set { } }
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("subprotocol")]
        public string Subprotocol { get { throw null; } set { } }
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("userId")]
        public string UserId { get { throw null; } set { } }
    }
    public sealed partial class DisconnectedEventRequest : Azure.Messaging.WebPubSub.ServiceRequest
    {
        internal DisconnectedEventRequest() : base (default(Azure.Messaging.WebPubSub.ConnectionContext)) { }
        public override string Name { get { throw null; } }
        public string Reason { get { throw null; } }
    }
    public partial class ErrorResponse : Azure.Messaging.WebPubSub.ServiceResponse
    {
        public ErrorResponse(Azure.Messaging.WebPubSub.WebPubSubErrorCode code, string message = null) { }
        public Azure.Messaging.WebPubSub.WebPubSubErrorCode Code { get { throw null; } set { } }
        public string ErrorMessage { get { throw null; } set { } }
    }
    public partial class InvalidRequest : Azure.Messaging.WebPubSub.ServiceRequest
    {
        internal InvalidRequest() : base (default(Azure.Messaging.WebPubSub.ConnectionContext)) { }
        public string ErrorMessage { get { throw null; } }
        public override string Name { get { throw null; } }
    }
    public partial interface IServiceRequestHandler
    {
        System.Threading.Tasks.Task HandleRequest<THub>(Microsoft.AspNetCore.Http.HttpContext context, THub hub) where THub : Azure.Messaging.WebPubSub.ServiceHub;
    }
    public enum MessageDataType
    {
        Binary = 0,
        Json = 1,
        Text = 2,
    }
    public sealed partial class MessageEventRequest : Azure.Messaging.WebPubSub.ServiceRequest
    {
        internal MessageEventRequest() : base (default(Azure.Messaging.WebPubSub.ConnectionContext)) { }
        public Azure.Messaging.WebPubSub.MessageDataType DataType { get { throw null; } }
        public System.BinaryData Message { get { throw null; } }
        public override string Name { get { throw null; } }
    }
    public partial class MessageResponse : Azure.Messaging.WebPubSub.ServiceResponse
    {
        public MessageResponse(System.BinaryData message, Azure.Messaging.WebPubSub.MessageDataType dataType = Azure.Messaging.WebPubSub.MessageDataType.Text) { }
        public MessageResponse(string message, Azure.Messaging.WebPubSub.MessageDataType dataType = Azure.Messaging.WebPubSub.MessageDataType.Text) { }
        public Azure.Messaging.WebPubSub.MessageDataType DataType { get { throw null; } }
        public System.BinaryData Message { get { throw null; } }
    }
    public abstract partial class ServiceHub : System.IDisposable
    {
        protected ServiceHub() { }
        public abstract System.Threading.Tasks.Task<Azure.Messaging.WebPubSub.ServiceResponse> Connect(Azure.Messaging.WebPubSub.ConnectEventRequest request);
        public virtual System.Threading.Tasks.Task Connected(Azure.Messaging.WebPubSub.ConnectedEventRequest request) { throw null; }
        public virtual System.Threading.Tasks.Task Disconnected(Azure.Messaging.WebPubSub.DisconnectedEventRequest request) { throw null; }
        public void Dispose() { }
        public abstract System.Threading.Tasks.Task<Azure.Messaging.WebPubSub.ServiceResponse> Message(Azure.Messaging.WebPubSub.MessageEventRequest request);
    }
    public abstract partial class ServiceRequest
    {
        public ServiceRequest(Azure.Messaging.WebPubSub.ConnectionContext context) { }
        public Azure.Messaging.WebPubSub.ConnectionContext ConnectionContext { get { throw null; } set { } }
        public abstract string Name { get; }
    }
    public static partial class ServiceRequestExtensions
    {
        public static System.Collections.Generic.Dictionary<string, object> DecodeConnectionState(this string connectionStates) { throw null; }
        public static bool IsValidationRequest(this Microsoft.AspNetCore.Http.HttpRequest request, out Azure.Messaging.WebPubSub.ValidationRequest validationRequest) { throw null; }
        public static bool IsValidSignature(this Azure.Messaging.WebPubSub.ConnectionContext connectionContext, Azure.Messaging.WebPubSub.WebPubSubValidationOptions options) { throw null; }
        public static string ToContentType(this Azure.Messaging.WebPubSub.MessageDataType dataType) { throw null; }
        public static string ToHeaderStates(this System.Collections.Generic.Dictionary<string, object> value) { throw null; }
        public static System.Threading.Tasks.Task<Azure.Messaging.WebPubSub.ServiceRequest> ToServiceRequest(this Microsoft.AspNetCore.Http.HttpRequest request, Azure.Messaging.WebPubSub.ConnectionContext context = null) { throw null; }
        public static int ToStatusCode(this Azure.Messaging.WebPubSub.WebPubSubErrorCode errorCode) { throw null; }
        public static bool TryParseCloudEvents(this Microsoft.AspNetCore.Http.HttpRequest request, out Azure.Messaging.WebPubSub.ConnectionContext connection) { throw null; }
        public static System.Collections.Generic.Dictionary<string, object> UpdateStates(this System.Collections.Generic.Dictionary<string, object> existValue, System.Collections.Generic.Dictionary<string, object> newValue) { throw null; }
    }
    public abstract partial class ServiceResponse
    {
        protected ServiceResponse() { }
    }
    public static partial class ServiceResponseExtensions
    {
        public static void ClearStates(this Azure.Messaging.WebPubSub.ServiceResponse response) { }
        public static void SetState(this Azure.Messaging.WebPubSub.ServiceResponse response, string key, object value) { }
    }
    public sealed partial class ValidationRequest : Azure.Messaging.WebPubSub.ServiceRequest
    {
        public ValidationRequest(bool valid, System.Collections.Generic.List<string> requestHosts) : base (default(Azure.Messaging.WebPubSub.ConnectionContext)) { }
        public override string Name { get { throw null; } }
        public bool Valid { get { throw null; } }
    }
    public enum WebPubSubErrorCode
    {
        Unauthorized = 0,
        UserError = 1,
        ServerError = 2,
    }
    public enum WebPubSubEventType
    {
        System = 0,
        User = 1,
    }
    public enum WebPubSubPermission
    {
        SendToGroup = 1,
        JoinLeaveGroup = 2,
    }
    public partial class WebPubSubRequestBuilder
    {
        public WebPubSubRequestBuilder() { }
        public Azure.Messaging.WebPubSub.WebPubSubRequestBuilder AddValidationOptions(Azure.Messaging.WebPubSub.WebPubSubValidationOptions options) { throw null; }
        public Azure.Messaging.WebPubSub.IServiceRequestHandler Build() { throw null; }
    }
    public partial class WebPubSubServiceClient
    {
        protected WebPubSubServiceClient() { }
        public WebPubSubServiceClient(string connectionString, string hub) { }
        public WebPubSubServiceClient(string connectionString, string hub, Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions options) { }
        public WebPubSubServiceClient(System.Uri endpoint, string hub, Azure.AzureKeyCredential credential) { }
        public WebPubSubServiceClient(System.Uri endpoint, string hub, Azure.AzureKeyCredential credential, Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions options) { }
        public virtual System.Uri Endpoint { get { throw null; } }
        public virtual string Hub { get { throw null; } }
        public virtual Azure.Core.Pipeline.HttpPipeline Pipeline { get { throw null; } }
        public virtual Azure.Response AddConnectionToGroup(string group, string connectionId, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> AddConnectionToGroupAsync(string group, string connectionId, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response AddUserToGroup(string group, string userId, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> AddUserToGroupAsync(string group, string userId, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response<bool> CheckPermission(Azure.Messaging.WebPubSub.WebPubSubPermission permission, string connectionId, string targetName = null, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response<bool>> CheckPermissionAsync(Azure.Messaging.WebPubSub.WebPubSubPermission permission, string connectionId, string targetName = null, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response CloseConnection(string connectionId, string reason = null, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> CloseConnectionAsync(string connectionId, string reason = null, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response<bool> ConnectionExists(string connectionId, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response<bool>> ConnectionExistsAsync(string connectionId, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Uri GenerateClientAccessUri(System.DateTimeOffset expiresAt, string userId = null, params string[] roles) { throw null; }
        public virtual System.Uri GenerateClientAccessUri(System.TimeSpan expiresAfter = default(System.TimeSpan), string userId = null, params string[] roles) { throw null; }
        public virtual Azure.Response GrantPermission(Azure.Messaging.WebPubSub.WebPubSubPermission permission, string connectionId, string targetName = null, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> GrantPermissionAsync(Azure.Messaging.WebPubSub.WebPubSubPermission permission, string connectionId, string targetName = null, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response<bool> GroupExists(string group, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response<bool>> GroupExistsAsync(string group, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response RemoveConnectionFromGroup(string group, string connectionId, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> RemoveConnectionFromGroupAsync(string group, string connectionId, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response RemoveUserFromAllGroups(string userId, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> RemoveUserFromAllGroupsAsync(string userId, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response RemoveUserFromGroup(string group, string userId, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> RemoveUserFromGroupAsync(string group, string userId, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response RevokePermission(Azure.Messaging.WebPubSub.WebPubSubPermission permission, string connectionId, string targetName = null, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> RevokePermissionAsync(Azure.Messaging.WebPubSub.WebPubSubPermission permission, string connectionId, string targetName = null, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response SendToAll(Azure.Core.RequestContent content, Azure.Core.ContentType contentType, System.Collections.Generic.IEnumerable<string> excluded = null, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response SendToAll(string content, Azure.Core.ContentType contentType = default(Azure.Core.ContentType)) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> SendToAllAsync(Azure.Core.RequestContent content, Azure.Core.ContentType contentType, System.Collections.Generic.IEnumerable<string> excluded = null, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> SendToAllAsync(string content, Azure.Core.ContentType contentType = default(Azure.Core.ContentType)) { throw null; }
        public virtual Azure.Response SendToConnection(string connectionId, Azure.Core.RequestContent content, Azure.Core.ContentType contentType, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response SendToConnection(string connectionId, string content, Azure.Core.ContentType contentType = default(Azure.Core.ContentType)) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> SendToConnectionAsync(string connectionId, Azure.Core.RequestContent content, Azure.Core.ContentType contentType, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> SendToConnectionAsync(string connectionId, string content, Azure.Core.ContentType contentType = default(Azure.Core.ContentType)) { throw null; }
        public virtual Azure.Response SendToGroup(string group, Azure.Core.RequestContent content, Azure.Core.ContentType contentType, System.Collections.Generic.IEnumerable<string> excluded = null, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response SendToGroup(string group, string content, Azure.Core.ContentType contentType = default(Azure.Core.ContentType)) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> SendToGroupAsync(string group, Azure.Core.RequestContent content, Azure.Core.ContentType contentType, System.Collections.Generic.IEnumerable<string> excluded = null, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> SendToGroupAsync(string group, string content, Azure.Core.ContentType contentType = default(Azure.Core.ContentType)) { throw null; }
        public virtual Azure.Response SendToUser(string userId, Azure.Core.RequestContent content, Azure.Core.ContentType contentType, Azure.RequestOptions options = null) { throw null; }
        public virtual Azure.Response SendToUser(string userId, string content, Azure.Core.ContentType contentType = default(Azure.Core.ContentType)) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> SendToUserAsync(string userId, Azure.Core.RequestContent content, Azure.Core.ContentType contentType, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response> SendToUserAsync(string userId, string content, Azure.Core.ContentType contentType = default(Azure.Core.ContentType)) { throw null; }
        public virtual Azure.Response<bool> UserExists(string userId, Azure.RequestOptions options = null) { throw null; }
        public virtual System.Threading.Tasks.Task<Azure.Response<bool>> UserExistsAsync(string userId, Azure.RequestOptions options = null) { throw null; }
    }
    public partial class WebPubSubServiceClientOptions : Azure.Core.ClientOptions
    {
        public WebPubSubServiceClientOptions(Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions.ServiceVersion version = Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions.ServiceVersion.V2021_05_01_preview) { }
        public System.Collections.Generic.List<string> AccessKeys { get { throw null; } set { } }
        public System.Collections.Generic.List<string> AllowedHosts { get { throw null; } set { } }
        public System.Uri ReverseProxyEndpoint { get { throw null; } set { } }
        public enum ServiceVersion
        {
            V2021_05_01_preview = 1,
        }
    }
    public partial class WebPubSubValidationOptions
    {
        public WebPubSubValidationOptions(params string[] connectionStrings) { }
    }
}
namespace Microsoft.Extensions.Azure
{
    public static partial class WebPubSubServiceClientBuilderExtensions
    {
        public static Azure.Core.Extensions.IAzureClientBuilder<Azure.Messaging.WebPubSub.WebPubSubServiceClient, Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions> AddWebPubSubServiceClient<TBuilder>(this TBuilder builder, string connectionString, string hub) where TBuilder : Azure.Core.Extensions.IAzureClientFactoryBuilder { throw null; }
        public static Azure.Core.Extensions.IAzureClientBuilder<Azure.Messaging.WebPubSub.WebPubSubServiceClient, Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions> AddWebPubSubServiceClient<TBuilder>(this TBuilder builder, System.Uri endpoint, string hub, Azure.AzureKeyCredential credential) where TBuilder : Azure.Core.Extensions.IAzureClientFactoryBuilder { throw null; }
        public static Azure.Core.Extensions.IAzureClientBuilder<Azure.Messaging.WebPubSub.WebPubSubServiceClient, Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions> AddWebPubSubServiceClient<TBuilder, TConfiguration>(this TBuilder builder, TConfiguration configuration) where TBuilder : Azure.Core.Extensions.IAzureClientFactoryBuilderWithConfiguration<TConfiguration> { throw null; }
    }
}
