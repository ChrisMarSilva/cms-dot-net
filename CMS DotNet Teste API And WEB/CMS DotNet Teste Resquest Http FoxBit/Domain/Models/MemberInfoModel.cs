namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;

public class MemberInfoModel
{
    public MemberInfoModel()
    {

    }

    public MemberInfoModel(string sn, string email, int level, DateTime created_at, bool disabled) : this()
    {
        this.sn = sn;
        this.email = email;
        this.level = level;
        this.created_at = created_at;
        this.disabled = disabled;
    }

    public string sn { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public int level { get; set; }
    public DateTime created_at { get; set; }
    public bool disabled { get; set; }

    //public override string ToString()
    //{
    //    return "";
    //}
}