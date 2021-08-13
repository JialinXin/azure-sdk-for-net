// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.Messaging.WebPubSub.Tests
{
    public class WebPubSubRequestTests
    {
        [TestCase]
        public void TestJsonConvert()
        {
            var input = "{\"claims\":{\"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier\":[\"aaa\"],\"nbf\":[\"1628748162\"],\"exp\":[\"1628751762\"],\"iat\":[\"1628748162\"],\"aud\":[\"https://jixinwps.webpubsub.azure.com/client/hubs/chat\"],\"sub\":[\"aaa\"]},\"query\":{\"access_token\":[\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhYWEiLCJuYmYiOjE2Mjg3NDgxNjIsImV4cCI6MTYyODc1MTc2MiwiaWF0IjoxNjI4NzQ4MTYyLCJhdWQiOiJodHRwczovL2ppeGlud3BzLndlYnB1YnN1Yi5henVyZS5jb20vY2xpZW50L2h1YnMvY2hhdCJ9.ZFnoul-t1UTmI4qSkfsjmS6kWfnDqceAhghdeISNX6g\"]},\"subprotocols\":[],\"clientCertificates\":[]}";

            var result = JsonSerializer.Deserialize<TestReqeust>(input);
        }

        private sealed class TestReqeust : ServiceRequest
        {
            /// <summary>
            /// User Claims.
            /// </summary>
            [JsonPropertyName("claim")]
            public IDictionary<string, string[]> Claims { get; set; }

            /// <summary>
            /// Query.
            /// </summary>
            [JsonPropertyName("query")]
            public IDictionary<string, string[]> Query { get; set; }

            /// <summary>
            /// Supported subprotocols/
            /// </summary>
            [JsonPropertyName("subprotocols")]
            public string[] Subprotocols { get; set; }

            /// <summary>
            /// Client certificates.
            /// </summary>
            [JsonPropertyName("clientCertificates")]
            public ClientCertificateInfo[] ClientCertificates { get; set; }

            public override string Name => nameof(TestReqeust);

            public TestReqeust() : base(null)
            { }
        }
    }
}
