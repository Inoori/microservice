using Microsoft.AspNetCore.Http;

namespace FileStorage.Contracts;

/// <summary>
/// 提供文件处理功能的服务接口。
/// </summary>
public interface IFileService
{
    /// <summary>
    /// 异步上传文件到指定路径。
    /// </summary>
    /// <param name="filePath">文件保存的目标路径。</param>
    /// <param name="file">待上传的文件。</param>
    /// <param name="overwrite">如果目标路径已存在文件，是否覆盖现有文件。默认为 false。</param>
    /// <param name="tags">附加的键值对标记，用于文件的额外元数据信息。默认为 null。</param>
    /// <returns>返回包含上传后文件信息的 <see cref="UploadResponse"/> 对象。</returns>
    Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false,
        Dictionary<string, string>? tags = null);

    /// <summary>
    /// 异步下载指定文件。
    /// </summary>
    /// <param name="fileId">要下载的文件的唯一标识符。</param>
    /// <returns>返回一个包含文件数据流和内容类型的 <see cref="DownloadResponse"/> 对象。</returns>
    Task<DownloadResponse> DownloadFileAsync(string fileId);

    /// <summary>
    /// 异步尝试删除指定文件。
    /// </summary>
    /// <param name="fileId">要删除的文件的唯一标识符。</param>
    /// <returns>如果文件删除成功，返回 true；否则返回 false。</returns>
    Task<bool> TryDeleteFileAsync(string fileId);
}