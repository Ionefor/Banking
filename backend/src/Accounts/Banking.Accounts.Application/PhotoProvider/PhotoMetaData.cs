using Banking.SharedKernel.ValueObjects;

namespace Banking.Accounts.Application.PhotoProvider;

public record PhotoMetaData(string BucketName, FilePath FilePath);