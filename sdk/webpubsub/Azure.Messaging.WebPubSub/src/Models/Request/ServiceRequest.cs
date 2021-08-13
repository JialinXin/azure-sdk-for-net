// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Net;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Web PubSub service request.
    /// </summary>
    public abstract class ServiceRequest
    {
        ///// <summary>
        ///// Flag to indicate whether it's an <see href="https://github.com/cloudevents/spec/blob/v1.0/http-webhook.md#4-abuse-protection">Abuse Protection</see> request.
        ///// </summary>
        //public bool IsValidationRequest { get; }
        //
        ///// <summary>
        ///// Flag to indicate whether request is valid.
        ///// </summary>
        //public bool Valid { get; }
        //
        ///// <summary>
        ///// Flag to indicate whether request is unauthorized.
        ///// </summary>
        //public bool Unauthorized { get; }
        //
        ///// <summary>
        ///// Flag to indicate whether request is invalid.
        ///// </summary>
        //public bool BadRequest { get; }
        //
        ///// <summary>
        ///// Detail error message to clients.
        ///// </summary>
        //public string ErrorMessage { get; }

        /// <summary>
        /// ConnectionContext.
        /// </summary>
        public ConnectionContext ConnectionContext { get; set;}

        /// <summary>
        /// Request name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="context"></param>
        public ServiceRequest(ConnectionContext context)
        {
            ConnectionContext = context;
        }

        ///// <summary>
        ///// Constrator for ServiceRequest.
        ///// </summary>
        ///// <param name="isValidationRequest"></param>
        ///// <param name="valid"></param>
        ///// <param name="unauthorized"></param>
        ///// <param name="badRequest"></param>
        ///// <param name="error"></param>
        //public ServiceRequest(bool isValidationRequest, bool valid, bool unauthorized, bool badRequest, string error = null)
        //{
        //    IsValidationRequest = isValidationRequest;
        //    Valid = valid;
        //    Unauthorized = unauthorized;
        //    BadRequest = badRequest;
        //    ErrorMessage = error;
        //}
        //
        //internal ServiceRequest(bool isValidationRequest, bool valid)
        //{
        //    IsValidationRequest = isValidationRequest;
        //    Valid = valid;
        //}
        //
        //internal ServiceRequest(HttpStatusCode status, string error = null)
        //{
        //    switch (status)
        //    {
        //        case HttpStatusCode.OK:
        //            Valid = true;
        //            break;
        //        case HttpStatusCode.Unauthorized:
        //            Unauthorized = true;
        //            break;
        //        case HttpStatusCode.BadRequest:
        //            BadRequest = true;
        //            break;
        //        default:
        //            BadRequest = true;
        //            break;
        //    }
        //    if (!string.IsNullOrEmpty(error))
        //    {
        //        ErrorMessage = error;
        //    }
        //}
    }
}
