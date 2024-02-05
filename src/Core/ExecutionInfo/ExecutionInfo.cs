using MCIO.OutputEnvelop;
using MCIO.OutputEnvelop.Models;
using System;
using System.Collections.Generic;

namespace MCIO.Core.ExecutionInfo
{
    public readonly struct ExecutionInfo
    {
        // Constants
        public const int MAX_MESSAGE_COUNT = 4;

        public const string CORRELATION_ID_SHOULD_REQUIRED_MESSAGE_CODE = "ExecutionInfo.CorrelationId.Should.Required";
        public const string CORRELATION_ID_SHOULD_REQUIRED_MESSAGE_DESCRIPTION = "CorrelationId should required";

        public const string TENANT_INFO_SHOULD_VALID_MESSAGE_CODE = "ExecutionInfo.TenantInfo.Should.Valid";
        public const string TENANT_INFO_SHOULD_VALID_MESSAGE_DESCRIPTION = "TenantInfo should valid";

        public const string EXECUTION_USER_SHOULD_REQUIRED_MESSAGE_CODE = "ExecutionInfo.ExecutionUser.Should.Required";
        public const string EXECUTION_USER_SHOULD_REQUIRED_MESSAGE_DESCRIPTION = "ExecutionUser should required";

        public const string ORIGIN_SHOULD_REQUIRED_MESSAGE_CODE = "ExecutionInfo.Origin.Should.Required";
        public const string ORIGIN_SHOULD_REQUIRED_MESSAGE_DESCRIPTION = "Origin should required";

        // Properties
        public Guid CorrelationId { get; }
        public TenantInfo.TenantInfo TenantInfo { get; }
        public string ExecutionUser { get; }
        public string Origin { get; }
        public bool IsValid { get; }

        // Constructors
        private ExecutionInfo(
            Guid correlationId,
            TenantInfo.TenantInfo tenantInfo,
            string executionUser,
            string origin
        )
        {
            CorrelationId = correlationId;
            TenantInfo = tenantInfo;
            ExecutionUser = executionUser;
            Origin = origin;
            IsValid = true;
        }

        // Builders
        public static OutputEnvelop<ExecutionInfo?> Create(
            Guid correlationId,
            TenantInfo.TenantInfo tenantInfo,
            string executionUser,
            string origin
        )
        {
            // Validation
            var outputMessageCollection = new List<OutputMessage>(capacity: MAX_MESSAGE_COUNT);

            if (correlationId == Guid.Empty)
                outputMessageCollection.Add(
                    OutputMessage.CreateError(
                        CORRELATION_ID_SHOULD_REQUIRED_MESSAGE_CODE, 
                        CORRELATION_ID_SHOULD_REQUIRED_MESSAGE_DESCRIPTION
                    )
                );

            if (!tenantInfo.IsValid)
                outputMessageCollection.Add(
                    OutputMessage.CreateError(
                        TENANT_INFO_SHOULD_VALID_MESSAGE_CODE,
                        TENANT_INFO_SHOULD_VALID_MESSAGE_DESCRIPTION
                    )
                );

            if (string.IsNullOrWhiteSpace(executionUser))
                outputMessageCollection.Add(
                    OutputMessage.CreateError(
                        EXECUTION_USER_SHOULD_REQUIRED_MESSAGE_CODE,
                        EXECUTION_USER_SHOULD_REQUIRED_MESSAGE_DESCRIPTION
                    )
                );

            if (string.IsNullOrWhiteSpace(origin))
                outputMessageCollection.Add(
                    OutputMessage.CreateError(
                        ORIGIN_SHOULD_REQUIRED_MESSAGE_CODE,
                        ORIGIN_SHOULD_REQUIRED_MESSAGE_DESCRIPTION
                    )
                );

            // Return
            return outputMessageCollection.Count == 0
                ? OutputEnvelop<ExecutionInfo?>.CreateSuccess(
                    output: new ExecutionInfo(correlationId, tenantInfo, executionUser, origin)
                )
                : OutputEnvelop<ExecutionInfo?>.CreateError(
                    output: null,
                    outputMessageCollection: outputMessageCollection.ToArray(),
                    exceptionCollection: null
                );
        }
    }
}
