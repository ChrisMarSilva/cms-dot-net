unit Unit1;

interface

// function EnviaMensagemURL(
// ISPB: integer;
// TpAmbiente: PAnsiChar;
// DominioURL: PAnsiChar;
// CdLegado: PAnsiChar;
// CdUsuario: PAnsiChar;
// Senha: PAnsiChar;
// ISPBOrigem: integer;
// ISPBDestino: integer;
// ConteudoXML: PAnsiChar;
// var NumCabSeq: PAnsiChar;
// var CdRetorno: PAnsiChar;
// var DscRetorno: PAnsiChar
// ): integer; stdcall;

function EnviaMensagemURL(
  ISPB: integer;
  TpAmbiente: PAnsiChar;
  DominioURL: PAnsiChar;
  CdLegado: PAnsiChar;
  CdUsuario: PAnsiChar;
  Senha: PAnsiChar;
  ISPBOrigem: integer;
  ISPBDestino: integer;
  ConteudoXML: PAnsiChar;
  NumCabSeq: PAnsiChar;
  CdRetorno: PAnsiChar;
  DscRetorno: PAnsiChar
  ): integer; stdcall;

implementation

Uses
  JDTraceApp,
  // JDFuncoesXML,
  JDFuncoes,
  Classes,
  SysUtils;

procedure MontarTraceApp(var pTraceAPI: TJDTraceApp);
const
  NOME_ARQUIVO_TRACE = '~JDSPB_IntgrLeg_App.log';
var
  slConfTrace: TStringList;
  sNomeArquivoCfg: String;
begin
  if not Assigned(pTraceAPI) then
    pTraceAPI := TJDTraceApp.Create(nil);

  pTraceAPI.Sistema := 'API';
  pTraceAPI.Arquivo := ExtractFilePath(ParamStr(0)) + NOME_ARQUIVO_TRACE;

  sNomeArquivoCfg := ExtractFilePath(ParamStr(0)) + 'JDTrace_On.cfg';

  if FileExists(sNomeArquivoCfg) then
  begin
    pTraceAPI.Ativo := True;

    slConfTrace := TStringList.Create;
    try
      slConfTrace.LoadFromFile(sNomeArquivoCfg);

      if slConfTrace.Values['CaminhoTrace'] <> '' then
        pTraceAPI.Arquivo := ExcludeTrailingPathDelimiter(slConfTrace.Values['CaminhoTrace']) + '\' + NOME_ARQUIVO_TRACE;

    finally
      slConfTrace.Free;
    end;
  end;
end;

function EnviaMensagem_Padrao(
  TraceAPI: TJDTraceApp;
  ISPB: integer;
  TpAmbiente: string;
  DominioURL: string;
  CdLegado: string;
  CdUsuario: string;
  Senha: string;
  ISPBOrigem: integer;
  ISPBDestino: integer;
  ConteudoXML: String;
  var NumCabSeq: string;
  var CdRetorno: string;
  var DscRetorno: string): integer;
var
  TempoProc: TDateTime;
  Proc, Param: string;

  RetEnvioMsg: string;
  RetNumCabSeq: string;
  wsEnvConteudo: string;

Begin
  Proc := 'EnviaMensagem_Padrao';
  Param :=
  // 'ISPB: ' + InttoStr(ISPB) +
  // '; TpAmbiente: ' + TpAmbiente +
  // '; DominioURL: ' + DominioURL +
  // '; CdLegado: ' + CdLegado +
  // '; CdUsuario: ' + CdUsuario +
  // '; ISPBOrigem: ' + InttoStr(ISPBOrigem) +
  // '; ISPBDestino: ' + InttoStr(ISPBDestino) +
  // '; ConteudoXML: ' + ConteudoXML +
    '; var NumCabSeq: ' + NumCabSeq +
    '; var CdRetorno: ' + CdRetorno +
    '; var DscRetorno: ' + DscRetorno;
  try
    try
      TraceAPI.LogApp(TempoProc, Proc, True, Param);
      // MÉTODO - Início

      wsEnvConteudo := ConteudoXML;
      RetNumCabSeq := 'WSCABLEG000000042404';

      Result := 9;
      CdRetorno := 'ACS99';
      DscRetorno := 'Envio da Mensagem do Legado realizado com Sucesso'; // 'Realizado com Sucesso'; //
      NumCabSeq := RetNumCabSeq;

      // MÉTODO - Fim
    except
      on E: Exception do
      begin
        Result := 888;
        TraceAPI.LogErro(TempoProc, Proc, E.Message);
        // raise;
      end;
    end;
  finally
    TraceAPI.LogApp(TempoProc, Proc, False,
      'Result: ' + InttoStr(Result) +
      // '; ISPB: ' + InttoStr(ISPB) +
      // '; TpAmbiente: ' + TpAmbiente +
      // '; DominioURL: ' + DominioURL +
      // '; CdLegado: ' + CdLegado +
      // '; CdUsuario: ' + CdUsuario +
      // '; ISPBOrigem: ' + InttoStr(ISPBOrigem) +
      // '; ISPBDestino: ' + InttoStr(ISPBDestino) +
      // '; ConteudoXML: ' + ConteudoXML +
      '; var NumCabSeq: ' + NumCabSeq +
      '; var CdRetorno: ' + CdRetorno +
      '; var DscRetorno: ' + DscRetorno);
  end;
End;

function NicksStrPCopy(const Source: AnsiString; Dest: PAnsiChar): PAnsiChar;
begin
  Move(PAnsiChar(Source)^, Dest^, Length(Source) + 1); // +1 for the 0 char
  Result := Dest;
end;

function EnviaMensagemURL(
  ISPB: integer;
  TpAmbiente: PAnsiChar;
  DominioURL: PAnsiChar;
  CdLegado: PAnsiChar;
  CdUsuario: PAnsiChar;
  Senha: PAnsiChar;
  ISPBOrigem: integer;
  ISPBDestino: integer;
  ConteudoXML: PAnsiChar;
  NumCabSeq: PAnsiChar;
  CdRetorno: PAnsiChar;
  DscRetorno: PAnsiChar): integer; stdcall;

var
  TraceAPI: TJDTraceApp;
  TempoProc: TDateTime;
  Proc, Param: string;

  RetResult: integer;
  EnvNumCabSeq: string;
  EnvCdRetorno: string;
  EnvDscRetorno: string;
Begin

  TraceAPI := nil;
  MontarTraceApp(TraceAPI);

  Proc := 'EnviaMensagemURL';
  Param :=
  // '01.ISPB: ' + InttoStr(ISPB) +
  // '; TpAmbiente: ' + TpAmbiente +
  // '; DominioURL: ' + DominioURL +
  // '; CdLegado: ' + CdLegado +
  // '; CdUsuario: ' + CdUsuario +
  // '; ISPBOrigem: ' + InttoStr(ISPBOrigem) +
  // '; ISPBDestino: ' + InttoStr(ISPBDestino) +
  // '; ConteudoXML: ' + ConteudoXML +
    '; NumCabSeq: ' + NumCabSeq +
    '; CdRetorno: ' + CdRetorno +
    '; DscRetorno: ' + DscRetorno;
  try
    try
      TraceAPI.LogApp(TempoProc, Proc, True, Param);
      // MÉTODO - Início

      EnvNumCabSeq := NumCabSeq;
      EnvCdRetorno := CdRetorno;
      EnvDscRetorno := DscRetorno;

      RetResult := EnviaMensagem_Padrao(TraceAPI, ISPB, TpAmbiente, DominioURL, CdLegado, CdUsuario, Senha, ISPBOrigem, ISPBDestino, ConteudoXML, EnvNumCabSeq, EnvCdRetorno, EnvDscRetorno);

      Result := RetResult;
      StrCopy(NumCabSeq, PAnsiChar(AnsiString(EnvNumCabSeq)));
      StrCopy(CdRetorno, PAnsiChar(AnsiString(EnvCdRetorno)));
      StrCopy(DscRetorno, PAnsiChar(AnsiString(EnvDscRetorno)));
      //
      // // NumCabSeq := PAnsiChar(AnsiString(EnvNumCabSeq));
      // StrCopy(NumCabSeq, PAnsiChar(AnsiString(EnvNumCabSeq)));
      // // StrLCopy(NumCabSeq, PAnsiChar(AnsiString(EnvNumCabSeq)), length(EnvNumCabSeq));
      // // NicksStrPCopy(NumCabSeq, PAnsiChar(EnvNumCabSeq));
      // // EnvNumCabSeq := UTF8String(EnvNumCabSeq);
      // // StrLCopy(@NumCabSeq[0], PAnsiChar(EnvNumCabSeq), Length(NumCabSeq));
      //
      // // CdRetorno := PAnsiChar(AnsiString(EnvCdRetorno));
      // StrCopy(CdRetorno, PAnsiChar(AnsiString(EnvCdRetorno)));
      // // StrLCopy(CdRetorno, PAnsiChar(AnsiString(EnvCdRetorno)), Length(EnvCdRetorno));
      // // NicksStrPCopy(CdRetorno, PAnsiChar(EnvCdRetorno));
      //
      // // DscRetorno := PAnsiChar(AnsiString(EnvDscRetorno));
      // StrCopy(DscRetorno, PAnsiChar(AnsiString(EnvDscRetorno)));
      // // StrLCopy(DscRetorno, PAnsiChar(AnsiString(EnvDscRetorno)), Length(EnvDscRetorno));
      // // NicksStrPCopy(DscRetorno, PAnsiChar(EnvDscRetorno));

      // MÉTODO - Fim
    except
      on E: Exception do
      begin
        Result := 999;
        TraceAPI.LogErro(TempoProc, Proc, E.Message);
        // raise;
      end;
    end;
  finally
    TraceAPI.LogApp(TempoProc, Proc, False,
      'Result: ' + InttoStr(Result) +
      // '; ISPB: ' + InttoStr(ISPB) +
      // '; TpAmbiente: ' + TpAmbiente +
      // '; DominioURL: ' + DominioURL +
      // '; CdLegado: ' + CdLegado +
      // '; CdUsuario: ' + CdUsuario +
      // '; ISPBOrigem: ' + InttoStr(ISPBOrigem) +
      // '; ISPBDestino: ' + InttoStr(ISPBDestino) +
      // '; ConteudoXML: ' + ConteudoXML +
      '; NumCabSeq: ' + NumCabSeq +
      '; CdRetorno: ' + CdRetorno +
      '; DscRetorno: ' + DscRetorno);
  end;
End;

end.
