-- Table: public.cuser

-- DROP TABLE IF EXISTS public.cuser;

CREATE TABLE IF NOT EXISTS public.cuser
(
    idusr integer NOT NULL DEFAULT 0,
    usrnm character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    cmpnm character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    pswdt character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    lsdte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    usrrl character varying(20) COLLATE pg_catalog."default",
    CONSTRAINT cuser_pkey PRIMARY KEY (idusr)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.cuser
    OWNER to postgres;


-- FUNCTION: public.cuser_delete(character varying, character varying)

-- DROP FUNCTION IF EXISTS public.cuser_delete(character varying, character varying);

CREATE OR REPLACE FUNCTION public.cuser_delete(
	i_usrnm character varying,
	i_cmpnm character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
 	BEGIN 
		DELETE FROM cuser WHERE usrnm= i_usrnm AND cmpnm = i_cmpnm;
	END;
 
$BODY$;

ALTER FUNCTION public.cuser_delete(character varying, character varying)
    OWNER TO postgres;

-- FUNCTION: public.cuser_insert(integer, character varying, character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.cuser_insert(integer, character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public.cuser_insert(
	i_idusr integer,
	i_usrnm character varying,
	i_cmpnm character varying,
	i_usrrl character varying,
	i_pswdt character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DECLARE 
		v_idusr int;
	BEGIN 
		SELECT MAX(idusr) INTO v_idusr from cuser;
		v_idusr:=coalesce(v_idusr,0); v_idusr:=v_idusr+1;
		DELETE FROM cuser Where idusr = v_idusr;
		INSERT INTO cuser (idusr, usrnm, cmpnm,usrrl,pswdt, lsdte) VALUES(v_idusr, i_usrnm, i_cmpnm,i_usrrl, i_pswdt, localtimestamp);
	END;
$BODY$;

ALTER FUNCTION public.cuser_insert(integer, character varying, character varying, character varying, character varying)
    OWNER TO postgres;


-- FUNCTION: public.cuser_update(integer, character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.cuser_update(integer, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public.cuser_update(
	i_idusr integer,
	i_usrnm character varying,
	i_cmpnm character varying,
	i_pswdt character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
 
	BEGIN
		DELETE FROM cuser where usrnm = i_usrnm;
		INSERT INTO cuser (idusr, usrnm, cmpnm,pswdt, lsdte) VALUES(i_idusr, i_usrnm, i_cmpnm, i_pswdt, localtimestamp);
	END;
	
$BODY$;

ALTER FUNCTION public.cuser_update(integer, character varying, character varying, character varying)
    OWNER TO postgres;
