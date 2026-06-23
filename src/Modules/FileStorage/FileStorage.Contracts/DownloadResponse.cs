namespace FileStorage.Contracts;

public record DownloadResponse(Stream Stream, string ContentType);