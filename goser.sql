-- Table: public.goser

-- DROP TABLE IF EXISTS public.goser;

CREATE TABLE IF NOT EXISTS public.goser
(
    idgos integer,
    trsid character varying(20) COLLATE pg_catalog."default",
    noser character varying(20) COLLATE pg_catalog."default"
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.goser
    OWNER to postgres;

-- FUNCTION: public.goser_insert(integer, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.goser_insert(integer, character varying, character varying);

CREATE OR REPLACE FUNCTION public.goser_insert(
	i_idgos integer,
	i_trsid character varying,
	i_noser character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	DECLARE
	v_idgos int;
	BEGIN 
		SELECT max(idgos) INTO v_idgos FROM goser;
		v_idgos:=coalesce(v_idgos,0);v_idgos:=v_idgos+1;
		INSERT INTO goser(idgos,trsid, noser) VALUES (v_idgos,i_trsid, i_noser);
	END;
$BODY$;

ALTER FUNCTION public.goser_insert(integer, character varying, character varying)
    OWNER TO postgres;


-- FUNCTION: public.goser_update(integer, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.goser_update(integer, character varying, character varying);

CREATE OR REPLACE FUNCTION public.goser_update(
	i_idgos integer,
	i_trsid character varying,
	i_noser character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	BEGIN 
		DELETE FROM goser WHERE idgos = i_idgos;
		INSERT INTO goser(idgos,trsid, noser) VALUES (i_idgos,i_trsid, i_noser);
	END;
$BODY$;

ALTER FUNCTION public.goser_update(integer, character varying, character varying)
    OWNER TO postgres;
