
CREATE TABLE account (
  id INTEGER PRIMARY KEY,
  saldo INTEGER NOT NULL,
  limite INTEGER NOT NULL
);
CREATE TABLE account_transaction (
  id SERIAL PRIMARY KEY,
  account_id INTEGER NOT NULL,
  valor INTEGER NOT NULL,
  tipo CHAR(1) NOT NULL,
  descricao VARCHAR(10) NOT NULL,
  realizada_em TIMESTAMP NOT NULL DEFAULT NOW(),
  FOREIGN KEY(account_id) REFERENCES account(id)
);

INSERT INTO account (id, limite, saldo) VALUES (1, 100000, 0);
INSERT INTO account (id, limite, saldo) VALUES (2, 80000, 0);
INSERT INTO account (id, limite, saldo) VALUES (3, 1000000, 0);
INSERT INTO account (id, limite, saldo) VALUES (4, 10000000, 0);
INSERT INTO account (id, limite, saldo) VALUES (5, 500000, 0);



CREATE TABLE accounts (
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	limit_amount INTEGER NOT NULL
);

CREATE TABLE transactions (
	id SERIAL PRIMARY KEY,
	account_id INTEGER NOT NULL,
	amount INTEGER NOT NULL,
	transaction_type CHAR(1) NOT NULL,
	description VARCHAR(10) NOT NULL,
	date TIMESTAMP NOT NULL DEFAULT NOW(),
	CONSTRAINT fk_accounts_transactions_id
		FOREIGN KEY (account_id) REFERENCES accounts(id)
);

CREATE TABLE balances (
	id SERIAL PRIMARY KEY,
	account_id INTEGER NOT NULL,
	amount INTEGER NOT NULL,
	CONSTRAINT fk_accounts_balances_id
		FOREIGN KEY (account_id) REFERENCES accounts(id)
);

DO $$
BEGIN
	INSERT INTO accounts (name, limit_amount)
	VALUES
		('o barato sai caro', 1000 * 100),
		('zan corp ltda', 800 * 100),
		('les cruders', 10000 * 100),
		('padaria joia de cocaia', 100000 * 100),
		('kid mais', 5000 * 100);
	
	INSERT INTO balances (account_id, amount)
		SELECT id, 0 FROM accounts;
END;
$$;





CREATE TABLE clientes (
  id SERIAL PRIMARY KEY,
  nome VARCHAR (50) NOT NULL,
  limite INTEGER NOT NULL
);

CREATE TABLE transacoes (
    id SERIAL PRIMARY KEY,
    cliente_id INTEGER NOT NULL,
    valor INTEGER NOT NULL,
    tipo CHAR(1) NOT NULL,
    descricao VARCHAR(10) NOT NULL,
    realizada_em TIMESTAMP NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_clientes_transacoes_id FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

CREATE INDEX idx_transacoes_id_desc ON transacoes(id desc);

CREATE TABLE saldos (
    id SERIAL PRIMARY KEY,
    cliente_id INTEGER NOT NULL,
    valor INTEGER NOT NULL,
    CONSTRAINT fk_clientes_saldos_id
        FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

CREATE UNIQUE INDEX idx_saldos_cliente_id ON saldos (cliente_id) include (valor);

DO $$
BEGIN
    INSERT INTO clientes (nome, limite)
    VALUES
        ('cliente 1', 1000 * 100),
        ('cliente 2', 800 * 100),
        ('cliente 3', 10000 * 100),
        ('cliente 4', 100000 * 100),
        ('cliente 5', 5000 * 100);
    INSERT INTO saldos (cliente_id, valor)
        SELECT id, 0 FROM clientes;
END;
$$;


CREATE OR REPLACE FUNCTION credit(
    parametro_cliente_id INT,
    parametro_valor INT,
    parametro_tipo CHAR(1),
    parametro_descricao VARCHAR(10)
)
RETURNS INT AS $$
DECLARE
saldo_value INT;
BEGIN
    UPDATE saldos SET valor = valor + parametro_valor WHERE cliente_id = parametro_cliente_id
    RETURNING valor INTO saldo_value;

    INSERT INTO transacoes (cliente_id, valor, tipo, descricao, realizada_em)
    VALUES (parametro_cliente_id, parametro_valor, parametro_tipo, parametro_descricao, now());

    RETURN saldo_value;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION debit(
    parametro_cliente_id INT,
    parametro_limite INT,
    parametro_valor INT,
    parametro_tipo CHAR(1),
    parametro_descricao VARCHAR(10)
)
RETURNS INT AS $$
DECLARE
saldo_value INT;

BEGIN
    UPDATE saldos SET valor = valor - parametro_valor
    WHERE cliente_id = parametro_cliente_id AND valor - parametro_valor > parametro_limite
    RETURNING valor INTO saldo_value;

    IF saldo_value < parametro_limite THEN
       RETURN NULL;
    END IF;

    INSERT INTO transacoes (cliente_id, valor, tipo, descricao, realizada_em)
    VALUES (parametro_cliente_id, parametro_valor, parametro_tipo, parametro_descricao, now());

    RETURN saldo_value;
END;
$$ LANGUAGE plpgsql;



CREATE TABLE clientes (
  id SERIAL PRIMARY KEY,
  nome VARCHAR(255) NOT NULL,
  limite INTEGER NOT NULL,
  saldo INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE transacoes(
    id SERIAL PRIMARY KEY,
    cliente_id INTEGER NOT NULL,
    valor INTEGER NOT NULL,
    tipo CHAR(1) NOT NULL,
    descricao VARCHAR(10) NOT NULL,
    realizada_em TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_cliente_id FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);
-- CREATE UNIQUE INDEX idx_clientes_id ON clientes USING btree (id);
CREATE INDEX idx_transacoes_cliente_id ON transacoes USING btree (cliente_id);

INSERT INTO clientes (nome, limite)
VALUES
  ('o barato sai caro', 1000 * 100),
  ('zan corp ltda', 800 * 100),
  ('les cruders', 10000 * 100),
  ('padaria joia de cocaia', 100000 * 100),
  ('kid mais', 5000 * 100);

