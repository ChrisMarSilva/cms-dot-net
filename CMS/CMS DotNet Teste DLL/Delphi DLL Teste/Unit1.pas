unit Unit1;

interface

function EnviaMensagem(
  ISPB: integer;
  TpAmbiente: PAnsiChar;
  CdLegado: PAnsiChar;
  CdUsuario: PAnsiChar;
  Senha: PAnsiChar;
  ISPBOrigem: integer;
  ISPBDestino: integer;
  ConteudoXML: PAnsiChar;
  var NumCabSeq: PAnsiChar;
  var CdRetorno: PAnsiChar;
  var DscRetorno: PAnsiChar
  ): integer; stdcall;

implementation

Uses
  SysUtils;

function EnviaMensagem(
  ISPB: integer;
  TpAmbiente: PAnsiChar;
  CdLegado: PAnsiChar;
  CdUsuario: PAnsiChar;
  Senha: PAnsiChar;
  ISPBOrigem: integer;
  ISPBDestino: integer;
  ConteudoXML: PAnsiChar;
  var NumCabSeq: PAnsiChar;
  var CdRetorno: PAnsiChar;
  var DscRetorno: PAnsiChar
  ): integer; stdcall;
Begin
  Result := 321;
  try
    StrCopy(NumCabSeq, PAnsiChar(AnsiString('123')));
    StrCopy(CdRetorno, PAnsiChar(AnsiString('456')));
    StrCopy(DscRetorno, PAnsiChar(AnsiString('789')));
  Except
    on E: Exception do
    begin
      Result := -999;
      StrCopy(NumCabSeq, PAnsiChar(AnsiString('Erro: ' + E.Message)));
      StrCopy(CdRetorno, PAnsiChar(AnsiString('Erro: ' + E.Message)));
      StrCopy(DscRetorno, PAnsiChar(AnsiString('Erro: ' + E.Message)));
    end;
  end;
End;

end.
