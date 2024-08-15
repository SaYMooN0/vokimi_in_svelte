﻿using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.users
{
    public class UnconfirmedAppUser
    {
        public UnconfirmedAppUserId Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }
        public string ConfirmationCode { get; init; }
        public DateTime RegistrationDate { get; init; }
        public static UnconfirmedAppUser CreateNew(string username, string email, string passwordHash, string confirmationCode) =>
            new()
            {
                Id = new(),
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                ConfirmationCode = confirmationCode,
                RegistrationDate = DateTime.UtcNow
            };

    }
}
