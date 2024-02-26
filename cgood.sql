-- Table: public.cgood

DROP TABLE IF EXISTS public.cgood;

CREATE TABLE IF NOT EXISTS public.cgood
(
    typid character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nomid character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    cmpnm character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    namas character varying(100) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    xnama character varying(200) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    stnbr character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    qtybr integer NOT NULL DEFAULT 0,
    lsdte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    CONSTRAINT cgood_pkey PRIMARY KEY (typid, nomid, cmpnm)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.cgood
    OWNER to postgres;




-- FUNCTION: public.cgood_delete(character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.cgood_delete(character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public.cgood_delete(
	i_typid character varying,
	i_nomid character varying,
	i_cmpnm character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
 
  BEGIN
	DELETE FROM cgood WHERE typid=i_typid AND nomid = i_nomid AND cmpnm = i_cmpnm;
  END
$BODY$;

ALTER FUNCTION public.cgood_delete(character varying, character varying, character varying)
    OWNER TO postgres;



-- FUNCTION: public.cgood_insert(character varying, character varying, character varying, character varying, character varying, character varying, integer)

-- DROP FUNCTION IF EXISTS public.cgood_insert(character varying, character varying, character varying, character varying, character varying, character varying, integer);

CREATE OR REPLACE FUNCTION public.cgood_insert(
	i_typid character varying,
	i_nomid character varying,
	i_namas character varying,
	i_xnama character varying,
	i_cmpnm character varying,
	i_stnbr character varying,
	i_qtybr integer)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
  DECLARE 
  v_typid int;
  BEGIN 
    SELECT COUNT(typid) INTO v_typid FROM cgood WHERE typid = i_typid AND nomid = '*';
	v_typid:=coalesce(v_typid,0);
	IF v_typid = 0 THEN 
		INSERT INTO cgood (typid, nomid,namas,xnama,cmpnm,  stnbr, qtybr,lsdte) VALUES (i_typid, '*', i_namas,i_xnama, i_cmpnm, i_stnbr,i_qtybr, LOCALTIMESTAMP);  
	END IF;
	SELECT COUNT(typid) INTO v_typid FROM cgood WHERE typid = i_typid AND nomid = i_nomid AND cmpnm = i_cmpnm;
	v_typid:=coalesce(v_typid,0);
	IF v_typid = 0 THEN 
		INSERT INTO cgood (typid, nomid,namas,xnama,cmpnm, stnbr, qtybr,lsdte) VALUES (i_typid, i_nomid, i_namas,i_xnama, i_cmpnm, i_stnbr,i_qtybr, LOCALTIMESTAMP);  
	END IF;
   END;
$BODY$;

ALTER FUNCTION public.cgood_insert(character varying, character varying, character varying, character varying, character varying, character varying, integer)
    OWNER TO postgres;



-- FUNCTION: public.cgood_update(character varying, character varying, character varying, character varying, character varying, character varying, integer)

-- DROP FUNCTION IF EXISTS public.cgood_update(character varying, character varying, character varying, character varying, character varying, character varying, integer);

CREATE OR REPLACE FUNCTION public.cgood_update(
	i_typid character varying,
	i_nomid character varying,
	i_namas character varying,
	i_xnama character varying,
	i_cmpnm character varying,
	i_stnbr character varying,
	i_qtybr integer)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
 DECLARE 
  v_typid int;
  BEGIN 
    SELECT COUNT(typid) INTO v_typid FROM cgood WHERE typid = i_typid AND nomid = '*' AND cmpnm = i_cmpnm;
	v_typid:=coalesce(v_typid,0);
	IF v_typid = 0 THEN 
		INSERT INTO cgood (typid, nomid,namas,xnama,cmpnm ,stnbr, qtybr,lsdte) VALUES (i_typid, '*', i_namas,i_xnama, i_cmpnm, i_stnbr,i_qtybr, LOCALTIMESTAMP);  
	END IF;
	DELETE FROM cgood WHERE typid=i_typid AND nomid = i_nomid;
	INSERT INTO cgood (typid, nomid,namas,xnama,cmpnm, stnbr, qtybr,lsdte) VALUES (i_typid, i_nomid, i_namas,i_xnama, i_cmpnm, i_stnbr,i_qtybr, LOCALTIMESTAMP);  
  END;
 
$BODY$;

ALTER FUNCTION public.cgood_update(character varying, character varying, character varying, character varying, character varying, character varying, integer)
    OWNER TO postgres;


-- FUNCTION: public.cgoodhead_insert(character varying, character varying)

-- DROP FUNCTION IF EXISTS public.cgoodhead_insert(character varying, character varying);

CREATE OR REPLACE FUNCTION public.cgoodhead_insert(
	i_typid character varying,
	i_namas character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
  DECLARE 
  v_typid int;
  BEGIN 
    SELECT COUNT(typid) INTO v_typid FROM cgood WHERE typid = i_typid AND nomid = '*';
	v_typid:=coalesce(v_typid,0);
	IF v_typid = 0 THEN 
		INSERT INTO cgood (typid, nomid,namas) VALUES (i_typid, '*', i_namas);  
	END IF;
   END;
$BODY$;

ALTER FUNCTION public.cgoodhead_insert(character varying, character varying)
    OWNER TO postgres;


-- FUNCTION: public.cgoodhead_update(character varying, character varying)

-- DROP FUNCTION IF EXISTS public.cgoodhead_update(character varying, character varying);

CREATE OR REPLACE FUNCTION public.cgoodhead_update(
	i_typid character varying,
	i_namas character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
 DECLARE 
  v_typid int;
  BEGIN 
    SELECT COUNT(typid) INTO v_typid FROM cgood WHERE typid = i_typid AND nomid = '*';
	v_typid:=coalesce(v_typid,0);
	IF v_typid = 0 THEN 
		INSERT INTO cgood (typid, nomid,namas) VALUES (i_typid, '*', i_namas);  
	END IF;
	DELETE FROM cgood WHERE typid= i_typid AND nomid = '*';
	INSERT INTO cgood (typid, nomid,namas) VALUES (i_typid, '*', i_namas); 
  END;
 
$BODY$;

ALTER FUNCTION public.cgoodhead_update(character varying, character varying)
    OWNER TO postgres;
