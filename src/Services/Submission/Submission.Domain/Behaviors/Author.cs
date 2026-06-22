using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class Author
{
    /// <summary>
    /// 创建一个新的 Author 实例。
    /// </summary>
    /// <param name="email">作者的电子邮件地址。</param>
    /// <param name="firstName">作者的名字。</param>
    /// <param name="lastName">作者的姓氏。</param>
    /// <param name="title">作者的头衔，若无可为空。</param>
    /// <param name="affiliation">作者的所属机构，若无可为空。</param>
    /// <returns>返回创建的 Author 实例，若失败则返回 null。</returns>
    public static Author? Create(string email, string firstName, string lastName,
        string? title, string? affiliation) => new()
    {
        EmailAddress = EmailAddress.Create(email),
        FirstName = firstName,
        LastName = lastName,
        Title = title,
        Affiliation = affiliation
    };

    //todo: add author created domain event
}