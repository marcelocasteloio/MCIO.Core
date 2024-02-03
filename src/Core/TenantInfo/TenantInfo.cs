using MCIO.OutputEnvelop;
using System;

namespace MCIO.Core.TenantInfo
{
    public readonly struct TenantInfo
    {
        // Constants
        public const string TENANT_INFO_CODE_SHOULD_REQUIRED_MESSAGE_CODE = "TenantInfo.Code.Should.Required";
        public const string TENANT_INFO_CODE_SHOULD_REQUIRED_MESSAGE_DESCRIPTION = "Code should required";


        // Properties
        public Guid Code { get; }
        public bool IsValid { get; }

        // Constructors
        private TenantInfo(Guid code)
        {
            Code = code;
            IsValid = true;
        }

        // Operators
        public static implicit operator Guid(TenantInfo value) => value.Code;
        public static implicit operator TenantInfo(Guid value) => new TenantInfo(value);

        // Builders
        public static OutputEnvelop<TenantInfo?> FromExistingCode(Guid code)
        {
            return code == Guid.Empty
                ? OutputEnvelop<TenantInfo?>.CreateError(
                    output: null,
                    TENANT_INFO_CODE_SHOULD_REQUIRED_MESSAGE_CODE,
                    TENANT_INFO_CODE_SHOULD_REQUIRED_MESSAGE_DESCRIPTION
                )
                : OutputEnvelop<TenantInfo?>.CreateSuccess(
                    output: new TenantInfo(code)
                );
        }
    }
}
