DROP TABLE IF EXISTS nsdbsyst.jrnls CASCADE;
CREATE TABLE IF NOT EXISTS nsdbsyst.jrnls 
(
   idjrn   varchar(20)  NOT NULL DEFAULT '-',
   nompo   varchar(20)  NOT NULL DEFAULT '-',
   brgnm   varchar(20)  NOT NULL DEFAULT '-',
   cmpnm   varchar(20)  NOT NULL DEFAULT '-',
   nocoa   int		NOT NULL DEFAULT 0,
   debit   decimal(10,2) NOT NULL DEFAULT 0,
   krdit   decimal(10,2) NOT NULL DEFAULT 0,  
   lsdte   timestamp    NOT NULL DEFAULT (date '2000,1,1'),
   jrdsk   varchar(100) NOT NULL DEFAULT '-',
   PRIMARY KEY (idjrn, brgnm)
); ALTER TABLE nsdbsyst.jrnls OWNER TO postgres;

CREATE OR REPLACE FUNCTION nsdbsyst.jrnls_insert(
   i_idjrn varchar(20),
   i_nompo varchar(20),
   i_brgnm varchar(20),
   i_cmpnm varchar(20),
   i_nocoa int, 
   i_debit decimal(10,2),
   i_krdit decimal(10,2),
   i_jrdsk varchar(100)
)
RETURNS VOID  -- Tambahkan deklarasi tipe data hasil di sini
AS 
$$
DECLARE 
   v_idjrn int;
BEGIN  
   SELECT idjrn INTO v_idjrn FROM nsdbsyst.jrnls WHERE idjrn = i_idjrn AND brgnm = i_brgnm AND nompo = i_nompo;
   v_idjrn := COALESCE(v_idjrn, 0);
   IF v_idjrn = 0 THEN
      INSERT INTO nsdbsyst.jrnls (idjrn, nompo, brgnm, cmpnm, nocoa, debit, krdit, jrdsk, lsdte) VALUES (i_idjrn, i_nompo, i_brgnm, i_cmpnm, i_nocoa, i_debit, i_krdit, i_jrdsk, LOCALTIMESTAMP);
   END IF;
END;
$$
LANGUAGE plpgsql VOLATILE;

CREATE OR REPLACE FUNCTION nsdbsyst.jrnls_update(
   i_idjrn varchar(20),
   i_nompo varchar(20),
   i_brgnm varchar(20),
   i_cmpnm varchar(20),
   i_nocoa int, 
   i_debit decimal(10,2),
   i_krdit decimal(10,2),
   i_jrdsk varchar(100)
)
RETURNS VOID  -- Tambahkan deklarasi tipe data hasil di sini
AS 
$$
BEGIN  
   DELETE FROM nsdbsyst.jrnls WHERE idjrn = i_idjrn AND brgnm = i_brgnm AND nompo = i_nompo;
    
   INSERT INTO nsdbsyst.jrnls (idjrn, nompo, brgnm, cmpnm, nocoa, debit, krdit, jrdsk, lsdte) VALUES (i_idjrn, i_nompo, i_brgnm, i_cmpnm, i_nocoa, i_debit, i_krdit, i_jrdsk, LOCALTIMESTAMP);
END;
$$
LANGUAGE plpgsql VOLATILE;


CREATE OR REPLACE FUNCTION nsdbsyst.jrnls_delete
(
	
   i_idjrn varchar(20),
   i_nompo varchar(20),
   i_brgnm varchar(20)
) RETURNS VOID
AS 
$$
  BEGIN 
  	   DELETE FROM nsdbsyst.jrnls WHERE idjrn = i_idjrn AND brgnm = i_brgnm AND nompo = i_nompo;

  END;
$$ LANGUAGE plpgsql VOLATILE;

