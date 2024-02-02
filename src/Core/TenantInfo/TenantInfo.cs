using System;

namespace MCIO.Core.TenantInfo
{
    public readonly struct TenantInfo
    {
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
        public static TenantInfo FromExistingCode(Guid code) => new TenantInfo(code);
    }
}
