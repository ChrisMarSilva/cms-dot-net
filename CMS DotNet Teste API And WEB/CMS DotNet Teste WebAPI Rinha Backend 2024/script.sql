
CREATE TABLE cliente (
  id SERIAL PRIMARY KEY,
  nome VARCHAR(50) NOT NULL,
  limite INTEGER NOT NULL,
  saldo INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE cliente_transacao (
  id SERIAL PRIMARY KEY,
  cliente_id INTEGER NOT NULL,
  valor INTEGER NOT NULL,
  tipo CHAR(1) NOT NULL,
  descricao VARCHAR(10) NOT NULL,
  dthrregistro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT fk_transacao_to_cliente FOREIGN KEY(cliente_id) REFERENCES cliente(id)
);

CREATE INDEX idx_transacoes_id_desc ON cliente_transacao(id desc);

CREATE TABLE cliente_saldo  (
	id SERIAL PRIMARY KEY,
	cliente_id INTEGER NOT NULL,
	total INTEGER NOT NULL,
  CONSTRAINT fk_transacao_to_saldo FOREIGN KEY(cliente_id) REFERENCES cliente(id)
);

CREATE UNIQUE INDEX idx_saldo_cliente ON cliente_saldo (cliente_id) include (total);

DO $$
BEGIN
	INSERT INTO cliente (nome, limite)
	VALUES ('cliente 01',   1000 * 100), ('cliente 02',    800 * 100), ('cliente 03',  10000 * 100), ('cliente 04', 100000 * 100), ('cliente 05',   5000 * 100);
	INSERT INTO cliente_saldo (cliente_id, total) SELECT id, 0 FROM cliente;
END;
$$;

/*

CREATE UNLOGGED TABLE "Pessoas" (
    "Id" uuid NOT NULL CONSTRAINT "PK_Pessoas" PRIMARY KEY,
    "Apelido" character varying(32) NOT NULL,
    "Nome" character varying(100) NOT NULL,
    "Nascimento" date NOT NULL,
    "Stack" character varying(32)[],
    "Busca" TEXT
);

CREATE INDEX IF NOT EXISTS IDX_PESSOAS_APELIDO ON "Pessoas" ("Apelido");
CREATE EXTENSION IF NOT EXISTS PG_TRGM;
CREATE INDEX CONCURRENTLY IF NOT EXISTS IDX_PESSOAS_BUSCA ON "Pessoas" USING GIST ("Busca" GIST_TRGM_OPS(SIGLEN=64));

*/