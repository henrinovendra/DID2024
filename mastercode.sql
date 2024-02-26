-- Table: public.mastercode

-- DROP TABLE IF EXISTS public.mastercode;

CREATE TABLE IF NOT EXISTS public.mastercode
(
    hcode character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT 0,
    ccode character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT 0,
    namas character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    CONSTRAINT mastercode_pkey PRIMARY KEY (hcode, ccode)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.mastercode
    OWNER to postgres;

-- FUNCTION: public.mastercode_delete(character varying, character varying)

-- DROP FUNCTION IF EXISTS public.mastercode_delete(character varying, character varying);

CREATE OR REPLACE FUNCTION public.mastercode_delete(
	i_hcode character varying,
	i_ccode character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
  BEGIN
    DELETE FROM mastercode WHERE hcode = i_hcode AND ccode = i_ccode;
  END;
$BODY$;

ALTER FUNCTION public.mastercode_delete(character varying, character varying)
    OWNER TO postgres;

-- FUNCTION: public.mastercode_insert(character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.mastercode_insert(character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public.mastercode_insert(
	i_hcode character varying,
	i_ccode character varying,
	i_namas character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_hcode int;
  BEGIN
    SELECT COUNT(hcode) INTO v_hcode FROM mastercode WHERE hcode = i_hcode AND ccode = '*';
    v_hcode:=coalesce(v_hcode,0);
    IF v_hcode = 0 THEN 
    	INSERT INTO mastercode ( hcode, ccode, namas ) VALUES ( i_hcode, '*', i_namas );
    END IF;
    SELECT COUNT(hcode) INTO v_hcode FROM mastercode WHERE hcode = i_hcode AND ccode = i_ccode;
    v_hcode:=coalesce(v_hcode,0);
    IF v_hcode = 0 THEN 
    	INSERT INTO mastercode ( hcode, ccode, namas ) VALUES ( i_hcode, i_ccode, i_namas );
    END IF;
  END;
$BODY$;

ALTER FUNCTION public.mastercode_insert(character varying, character varying, character varying)
    OWNER TO postgres;

-- FUNCTION: public.mastercode_update(character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.mastercode_update(character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public.mastercode_update(
	i_hcode character varying,
	i_ccode character varying,
	i_namas character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
  DECLARE
    v_hcode int;
  BEGIN
    SELECT COUNT(hcode) INTO v_hcode FROM mastercode WHERE hcode = i_hcode AND ccode = '*';
    v_hcode:=coalesce(v_hcode,0);
    IF v_hcode = 0 THEN 
    	INSERT INTO mastercode ( hcode, ccode, namas ) VALUES ( i_hcode, '*', i_namas );
    END IF;
    DELETE FROM mastercode WHERE hcode = i_hcode AND ccode = i_ccode;
    INSERT INTO mastercode ( hcode, ccode, namas ) VALUES ( i_hcode, i_ccode, i_namas );
  END;
$BODY$;

ALTER FUNCTION public.mastercode_update(character varying, character varying, character varying)
    OWNER TO postgres;
