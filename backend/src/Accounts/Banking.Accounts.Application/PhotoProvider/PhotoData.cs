using Banking.SharedKernel.ValueObjects;

namespace Banking.Accounts.Application.PhotoProvider;

public record PhotoData(Stream Stream, FilePath FilePath, string BucketName);