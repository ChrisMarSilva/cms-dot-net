﻿namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;

public class MemberInfoResponseDto
{
    public string sn { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public int level { get; set; }
    public DateTime created_at { get; set; }
    public bool disabled { get; set; }
}
