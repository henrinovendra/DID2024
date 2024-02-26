DROP TABLE IF EXISTS nsdbsyst.tblpr; 
CREATE TABLE IF NOT EXISTS nsdbsyst.tblpr 
(
	nompr 		VARCHAR(20)		NOT NULL DEFAULT '-',
	brgnm		VARCHAR(20) 		NOT NULL DEFAULT '-',
	cmpnm 		VARCHAR(20) 		NOT NULL DEFAULT '-',
	nmpmt		VARCHAR(20) 		NOT NULL DEFAULT '-',
	qtybr		INT			NOT NULL DEFAULT 0, 
	lsdte		TIMESTAMP		NOT NULL DEFAULT (date '2000-01-01'),
	PRIMARY KEY(nompr,brgnm)
); ALTER TABLE nsdbsyst.tblpr OWNER TO postgres;

CREATE OR REPLACE FUNCTION nsdbsyst.tblpr_insert
(
	i_nompr 	VARCHAR(20),
	i_brgnm 	VARCHAR(20),
	i_cmpnm 	VARCHAR(20),
	i_nmpmt 	VARCHAR(20),
	i_qtybr		INT
	
) RETURNS VOID
AS 
$$
	DECLARE 
	v_nompr int;
	BEGIN
		SELECT nompr INTO v_nompr FROM nsdbsyst.tblpr WHERE nompr =i_nompr AND cmpnm = i_cmpnm;
		v_nompr:=COALESCE(v_nompr,0);
		IF v_nompr = 0 THEN 
			INSERT INTO nsdbsyst.tblpr(nompr, brgnm, cmpnm,nmpmt, qtybr, lsdte) VALUES (i_nompr, i_brgnm, i_cmpnm,i_nmpmt, i_qtybr, LOCALTIMESTAMP);
		END IF;
	END;
	
$$ LANGUAGE plpgsql VOLATILE;

CREATE OR REPLACE FUNCTION nsdbsyst.tblpr_update 
(
	i_nompr 	VARCHAR(20),
	i_brgnm 	VARCHAR(20),
	i_cmpnm 	VARCHAR(20),
	i_nmpmt 	VARCHAR(20),
	i_qtybr		INT
) RETURNS VOID
AS 
$$
	BEGIN
		DELETE FROM nsdbsyst.tblpr WHERE nompr =i_nompr AND brgnm = i_brgnm AND cmpnm = i_cmpnm;
		INSERT INTO nsdbsyst.tblpr(nompr, brgnm, cmpnm,nmpmt, qtybr, lsdte) VALUES (i_nompr, i_brgnm, i_cmpnm,i_nmpmt, i_qtybr, LOCALTIMESTAMP);
	END;
$$ LANGUAGE plpgsql VOLATILE;

CREATE OR REPLACE FUNCTION nsdbsyst.tblpr_delete
(
	i_nompr VARCHAR, 
	i_brgnm VARCHAR,
	i_cmpnm VARCHAR

)
RETURNS VOID
AS 
$$
	BEGIN 
		DELETE FROM nsdbsyst.tblpr WHERE nompr =i_nompr AND brgnm = i_brgnm AND cmpnm = i_cmpnm;
	END;
$$ LANGUAGE plpgsql VOLATILE;