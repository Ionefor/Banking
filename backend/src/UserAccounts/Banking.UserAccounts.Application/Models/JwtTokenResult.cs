﻿namespace Banking.UserAccounts.Application.Models;

public record JwtTokenResult(string AccessToken, Guid Jti);