using System;
using tohow.Domain.Enum;

namespace tohow.Domain.DTO
{
    public class UserProfileDetails: UserProfile
    {
        public Guid UserId { get; set; }
        public long ProfileId { get; set; }
        public Gender Sex { get; set; }
        public string UserName { get; set; }
        public Nullable<int> Age { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public int Credits { get; set; }
    }
}
