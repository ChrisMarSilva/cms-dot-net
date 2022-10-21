unit Unit1;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls;

type
  TForm1 = class(TForm)
    Button1: TButton;
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

const
  NOME_DLL = 'ProjTesteDelphiDLL.dll';

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
  ): integer; stdcall; external NOME_DLL name 'EnviaMensagem';

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
Var
  NumCabSeq: PAnsiChar;
  CdRetorno: PAnsiChar;
  DscRetorno: PAnsiChar;
  iRetorno: integer;
begin
  try

    NumCabSeq := PAnsiChar(AnsiString(StringofChar(' ', 20)));
    CdRetorno := PAnsiChar(AnsiString(StringofChar(' ', 5)));
    DscRetorno := PAnsiChar(AnsiString(StringofChar(' ', 200)));

    iRetorno :=
      EnviaMensagem(
      04358798,
      PAnsiChar(AnsiString('PROD')),
      PAnsiChar(AnsiString('INT')),
      PAnsiChar(AnsiString('PILOTO')),
      PAnsiChar(AnsiString('123')),
      04358798,
      04358798,
      PAnsiChar(AnsiString('<XML></XML>')),
      NumCabSeq,
      CdRetorno,
      DscRetorno
      );

    ShowMessage('Retorno: ' + InttoStr(iRetorno) + ' - ' + NumCabSeq + ' - ' + CdRetorno + ' - ' + DscRetorno);

  except
    on E: Exception do
      ShowMessage('Erro: ' + E.Message);
  end;
end;

end.
