-- Table: public.cvend

-- DROP TABLE IF EXISTS public.cvend;

CREATE TABLE IF NOT EXISTS public.cvend
(
    jncln character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    tycln character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    vennm character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    addvn character varying(200) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    ntelp bigint NOT NULL DEFAULT 0,
    picnm character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    noehp bigint NOT NULL DEFAULT 0,
    email character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    bnknm character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    adbnk character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    norek bigint NOT NULL DEFAULT 0,
    nmrek character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nnpwp bigint NOT NULL DEFAULT 0,
    lsdte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    CONSTRAINT cvend_pkey PRIMARY KEY (jncln, tycln)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.cvend
    OWNER to postgres;


-- FUNCTION: public.cvend_update(character varying, character varying, character varying, character varying, bigint, character varying, bigint, character varying, character varying, character varying, bigint, character varying, bigint)

-- DROP FUNCTION IF EXISTS public.cvend_update(character varying, character varying, character varying, character varying, bigint, character varying, bigint, character varying, character varying, character varying, bigint, character varying, bigint);

CREATE OR REPLACE FUNCTION public.cvend_update(
	i_jncln character varying,
	i_tycln character varying,
	i_vennm character varying,
	i_addvn character varying,
	i_ntelp bigint,
	i_picnm character varying,
	i_noehp bigint,
	i_email character varying,
	i_bnknm character varying,
	i_adbnk character varying,
	i_norek bigint,
	i_nmrek character varying,
	i_nnpwp bigint)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
   DECLARE 
   v_jncln int;
   BEGIN 
	SELECT COUNT (jncln) INTO v_jncln FROM cvend WHERE jncln = i_jncln AND tycln = '*';
	v_jncln:=coalesce(v_jncln,0);
	IF v_jncln = 0 THEN
		INSERT INTO cvend (jncln,tycln, vennm, addvn, ntelp, picnm, noehp, email, bnknm, adbnk, norek, nmrek, nnpwp, lsdte)
		VALUES (i_jncln,'*', i_vennm, i_addvn, i_ntelp, i_picnm, i_noehp, i_email, i_bnknm, i_adbnk, i_norek, i_nmrek, i_nnpwp, current_timestamp);
	END IF;
	DELETE FROM cvend where jncln = i_jncln AND tycln= i_tycln;
	INSERT INTO cvend (jncln,tycln, vennm, addvn, ntelp, picnm, noehp, email, bnknm, adbnk, norek, nmrek, nnpwp, lsdte)
	VALUES (i_jncln,i_tycln, i_vennm, i_addvn, i_ntelp, i_picnm, i_noehp, i_email, i_bnknm, i_adbnk, i_norek, i_nmrek, i_nnpwp, current_timestamp);
   END;
$BODY$;

ALTER FUNCTION public.cvend_update(character varying, character varying, character varying, character varying, bigint, character varying, bigint, character varying, character varying, character varying, bigint, character varying, bigint)
    OWNER TO postgres;


-- FUNCTION: public.cvend_insert(character varying, character varying, character varying, character varying, bigint, character varying, bigint, character varying, character varying, character varying, bigint, character varying, bigint)

-- DROP FUNCTION IF EXISTS public.cvend_insert(character varying, character varying, character varying, character varying, bigint, character varying, bigint, character varying, character varying, character varying, bigint, character varying, bigint);

CREATE OR REPLACE FUNCTION public.cvend_insert(
	i_jncln character varying,
	i_tycln character varying,
	i_vennm character varying,
	i_addvn character varying,
	i_ntelp bigint,
	i_picnm character varying,
	i_noehp bigint,
	i_email character varying,
	i_bnknm character varying,
	i_adbnk character varying,
	i_norek bigint,
	i_nmrek character varying,
	i_nnpwp bigint)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_jncln int;
   BEGIN
	SELECT COUNT (jncln) INTO v_jncln FROM cvend WHERE jncln = i_jncln AND tycln = '*';
	v_jncln:=coalesce(v_jncln,0);
	IF v_jncln = 0 THEN
		INSERT INTO cvend (jncln,tycln, vennm, addvn, ntelp, picnm, noehp, email, bnknm, adbnk, norek, nmrek, nnpwp, lsdte)
		VALUES (i_jncln,'*', i_vennm, i_addvn, i_ntelp, i_picnm, i_noehp, i_email, i_bnknm, i_adbnk, i_norek, i_nmrek, i_nnpwp, current_timestamp);
	END IF;
	SELECT COUNT (jncln) INTO v_jncln FROM cvend WHERE jncln = i_jncln AND tycln = i_tycln;
	v_jncln = coalesce(v_jncln,0);
	IF v_jncln = 0 THEN
		INSERT INTO cvend (jncln,tycln, vennm, addvn, ntelp, picnm, noehp, email, bnknm, adbnk, norek, nmrek, nnpwp, lsdte)
		VALUES (i_jncln,i_tycln, i_vennm, i_addvn, i_ntelp, i_picnm, i_noehp, i_email, i_bnknm, i_adbnk, i_norek, i_nmrek, i_nnpwp, current_timestamp);
	END IF;
   END;
$BODY$;

ALTER FUNCTION public.cvend_insert(character varying, character varying, character varying, character varying, bigint, character varying, bigint, character varying, character varying, character varying, bigint, character varying, bigint)
    OWNER TO postgres;

-- FUNCTION: public.cvend_delete(character varying, character varying)

-- DROP FUNCTION IF EXISTS public.cvend_delete(character varying, character varying);

CREATE OR REPLACE FUNCTION public.cvend_delete(
	i_jncln character varying,
	i_tycln character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
 
	BEGIN
		DELETE FROM cvend WHERE jncln = i_jncln AND tycln = i_tycln;
	END;
$BODY$;

ALTER FUNCTION public.cvend_delete(character varying, character varying)
    OWNER TO postgres;
