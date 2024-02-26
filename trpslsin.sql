-- Table: public.trslsex

-- DROP TABLE IF EXISTS public.trslsex;

CREATE TABLE IF NOT EXISTS public.trslsex
(
    trsid character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    brenm character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    idven character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nomso character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nocoa integer NOT NULL DEFAULT 0,
    noxpo character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nomdo integer NOT NULL DEFAULT 0,
    noinv integer NOT NULL DEFAULT 0,
    nomfj bigint NOT NULL DEFAULT 0,
    pcktr character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    qtybr integer NOT NULL DEFAULT 0,
    prprc numeric(10,2) NOT NULL DEFAULT 0,
    nombr numeric(10,2) NOT NULL DEFAULT 0,
    nosrv numeric(10,2) NOT NULL DEFAULT 0,
    cstll numeric(10,2) NOT NULL DEFAULT 0,
    pphcs numeric(10,2) NOT NULL DEFAULT 0,
    ppncs numeric(10,2) NOT NULL DEFAULT 0,
    ttlhg numeric(10,2) NOT NULL DEFAULT 0,
    dtepo timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    dtedo timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    dteso timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    dtein timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    dtefj timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    dtedu timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    dtepc timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    CONSTRAINT trslsex_pkey PRIMARY KEY (trsid, brenm)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.trslsex
    OWNER to postgres;-- Table: public.trslsin

-- DROP TABLE IF EXISTS public.trslsin;

CREATE TABLE IF NOT EXISTS public.trslsin
(
    trsid character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    brgnm character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    cmpnm character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nompr integer NOT NULL DEFAULT 0,
    dtepr timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    CONSTRAINT trslsin_pkey PRIMARY KEY (trsid, brgnm)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.trslsin
    OWNER to postgres;


-- FUNCTION: public.trsls_delete(character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.trsls_delete(character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public.trsls_delete(
	i_trsid character varying,
	i_brgnm character varying,
	i_brenm character varying)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
  DECLARE 
  v_qtybr int;
  v_brgnm varchar;
  v_brenm varchar;
  v_cmpnm varchar;
  BEGIN
    SELECT cmpnm INTO v_cmpnm FROM trslsin WHERE trsid = i_trsid AND brgnm =i_brgnm;
   	SELECT qtybr,brenm INTO v_qtybr, v_brenm FROM trslsex WHERE trsid=i_trsid AND brenm = i_brenm;
   	v_qtybr := COALESCE(v_qtybr, 0);
    
  	DELETE FROM trslsin WHERE trsid = i_trsid AND brgnm=i_brgnm;
	DELETE FROM trslsex WHERE trsid = i_trsid AND brenm=i_brenm;
	UPDATE cgood SET qtybr =qtybr - v_qtybr WHERE nomid = v_brgnm AND cmpnm = v_cmpnm;
  END;
$BODY$;

ALTER FUNCTION public.trsls_delete(character varying, character varying, character varying)
    OWNER TO postgres;

-- FUNCTION: public.trsls_insert(character varying, character varying, character varying, integer, timestamp without time zone, character varying, character varying, character varying, integer, character varying, integer, integer, bigint, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION IF EXISTS public.trsls_insert(character varying, character varying, character varying, integer, timestamp without time zone, character varying, character varying, character varying, integer, character varying, integer, integer, bigint, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.trsls_insert(
	i_trsid character varying,
	i_brgnm character varying,
	i_cmpnm character varying,
	i_nompr integer,
	i_dtepr timestamp without time zone,
	i_brenm character varying,
	i_idven character varying,
	i_nomso character varying,
	i_nocoa integer,
	i_noxpo character varying,
	i_nomdo integer,
	i_noinv integer,
	i_nomfj bigint,
	i_pcktr character varying,
	i_qtybr integer,
	i_prprc numeric,
	i_nombr numeric,
	i_nosrv numeric,
	i_cstll numeric,
	i_pphcs numeric,
	i_ppncs numeric,
	i_ttlhg numeric,
	i_dtepo timestamp without time zone,
	i_dtedo timestamp without time zone,
	i_dteso timestamp without time zone,
	i_dtein timestamp without time zone,
	i_dtefj timestamp without time zone,
	i_dtedu timestamp without time zone,
	i_dtepc timestamp without time zone)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    DECLARE
        v_typid varchar(20);
        v_namas varchar(40);
        v_qtybr int;
    BEGIN 
        SELECT typid, namas, qtybr INTO v_typid, v_namas, v_qtybr FROM cgood WHERE nomid = i_brgnm;
        v_qtybr := COALESCE(v_qtybr, 0) - i_qtybr;
        INSERT INTO trslsin (trsid, brgnm, cmpnm, nompr, dtepr) VALUES (i_trsid, i_brgnm, i_cmpnm, i_nompr, i_dtepr);
        INSERT INTO trslsex (trsid, brenm, idven, nomso, nocoa, noxpo, nomdo, noinv, nomfj, pcktr, qtybr, prprc, nombr, nosrv, cstll, pphcs, ppncs, ttlhg, dtepo, dtedo, dteso,dtein, dtefj,dtedu, dtepc)
        VALUES (i_trsid, i_brenm, i_idven, i_nomso, i_nocoa, i_noxpo, i_nomdo, i_noinv, i_nomfj,i_pcktr, i_qtybr, i_prprc, i_nombr, i_nosrv, i_cstll, i_pphcs, i_ppncs, i_ttlhg, i_dtepo, i_dtedo, i_dteso, i_dtein, i_dtefj, i_dtedu, i_dtepc);
        UPDATE cgood SET qtybr = v_qtybr WHERE nomid = i_brgnm AND cmpnm = i_cmpnm;
    END;
$BODY$;

ALTER FUNCTION public.trsls_insert(character varying, character varying, character varying, integer, timestamp without time zone, character varying, character varying, character varying, integer, character varying, integer, integer, bigint, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)
    OWNER TO postgres;
-- FUNCTION: public.trsls_update(character varying, character varying, character varying, integer, timestamp without time zone, character varying, character varying, character varying, integer, character varying, integer, integer, bigint, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION IF EXISTS public.trsls_update(character varying, character varying, character varying, integer, timestamp without time zone, character varying, character varying, character varying, integer, character varying, integer, integer, bigint, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.trsls_update(
	i_trsid character varying,
	i_brgnm character varying,
	i_cmpnm character varying,
	i_nompr integer,
	i_dtepr timestamp without time zone,
	i_brenm character varying,
	i_idven character varying,
	i_nomso character varying,
	i_nocoa integer,
	i_noxpo character varying,
	i_nomdo integer,
	i_noinv integer,
	i_nomfj bigint,
	i_pcktr character varying,
	i_qtybr integer,
	i_prprc numeric,
	i_nombr numeric,
	i_nosrv numeric,
	i_cstll numeric,
	i_pphcs numeric,
	i_ppncs numeric,
	i_ttlhg numeric,
	i_dtepo timestamp without time zone,
	i_dtedo timestamp without time zone,
	i_dteso timestamp without time zone,
	i_dtein timestamp without time zone,
	i_dtefj timestamp without time zone,
	i_dtedu timestamp without time zone,
	i_dtepc timestamp without time zone)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
    DECLARE
        v_qtybr int;
        v_cmpnm varchar; 
    BEGIN
    SELECT qtybr INTO v_qtybr FROM trslsex WHERE trsid = i_trsid AND brenm = i_brenm;
    v_qtybr = COALESCE(v_qtybr,0);

    if v_qtybr <i_qtybr THEN 
        DELETE FROM trslsin WHERE trsid = i_trsid AND brgnm = i_brgnm;
        INSERT INTO trslsin (trsid, brgnm, cmpnm, nompr, dtepr) VALUES (i_trsid, i_brgnm, i_cmpnm, i_nompr, i_dtepr);
        DELETE FROM trslsex WHERE trsid = i_trsid AND brenm = i_brenm;
        INSERT INTO trslsex (trsid, brenm, idven, nomso, nocoa, noxpo, nomdo, noinv, nomfj, pcktr, qtybr, prprc, nombr, nosrv, cstll, pphcs, ppncs, ttlhg, dtepo, dtedo, dteso,dtein, dtefj,dtedu, dtepc)
        VALUES (i_trsid, i_brenm, i_idven, i_nomso, i_nocoa, i_noxpo, i_nomdo, i_noinv, i_nomfj,i_pcktr, i_qtybr, i_prprc, i_nombr, i_nosrv, i_cstll, i_pphcs, i_ppncs, i_ttlhg, i_dtepo, i_dtedo, i_dteso, i_dtein, i_dtefj, i_dtedu, i_dtepc);
        UPDATE cgood SET qtybr = qtybr -(i_qtybr - v_qtybr) WHERE nomid = i_brenm AND cmpnm = i_cmpnm;
    ELSEIF v_qtybr>i_qtybr THEN 
        DELETE FROM trslsin WHERE trsid = i_trsid AND brgnm = i_brgnm;
        INSERT INTO trslsin (trsid, brgnm, cmpnm, nompr, dtepr) VALUES (i_trsid, i_brgnm, i_cmpnm, i_nompr, i_dtepr);
        DELETE FROM trslsex WHERE trsid = i_trsid AND brenm = i_brenm;
        INSERT INTO trslsex (trsid, brenm, idven, nomso, nocoa, noxpo, nomdo, noinv, nomfj, pcktr, qtybr, prprc, nombr, nosrv, cstll, pphcs, ppncs, ttlhg, dtepo, dtedo, dteso,dtein, dtefj,dtedu, dtepc)
        VALUES (i_trsid, i_brenm, i_idven, i_nomso, i_nocoa, i_noxpo, i_nomdo, i_noinv, i_nomfj,i_pcktr, i_qtybr, i_prprc, i_nombr, i_nosrv, i_cstll, i_pphcs, i_ppncs, i_ttlhg, i_dtepo, i_dtedo, i_dteso, i_dtein, i_dtefj, i_dtedu, i_dtepc);
        UPDATE cgood SET qtybr = qtybr + (i_qtybr - v_qtybr) WHERE nomid = i_brenm AND cmpnm = i_cmpnm;
    ELSE --v_qtybr == i_qtybr
        DELETE FROM trslsin WHERE trsid = i_trsid AND brgnm = i_brgnm;
        INSERT INTO trslsin (trsid, brgnm, cmpnm, nompr, dtepr) VALUES (i_trsid, i_brgnm, i_cmpnm, i_nompr, i_dtepr);
        DELETE FROM trslsex WHERE trsid = i_trsid AND brenm = i_brenm;
        INSERT INTO trslsex (trsid, brenm, idven, nomso, nocoa, noxpo, nomdo, noinv, nomfj, pcktr, qtybr, prprc, nombr, nosrv, cstll, pphcs, ppncs, ttlhg, dtepo, dtedo, dteso,dtein, dtefj,dtedu, dtepc)
        VALUES (i_trsid, i_brenm, i_idven, i_nomso, i_nocoa, i_noxpo, i_nomdo, i_noinv, i_nomfj,i_pcktr, i_qtybr, i_prprc, i_nombr, i_nosrv, i_cstll, i_pphcs, i_ppncs, i_ttlhg, i_dtepo, i_dtedo, i_dteso, i_dtein, i_dtefj, i_dtedu, i_dtepc);
    END IF;
    END;
$BODY$;

ALTER FUNCTION public.trsls_update(character varying, character varying, character varying, integer, timestamp without time zone, character varying, character varying, character varying, integer, character varying, integer, integer, bigint, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)
    OWNER TO postgres;






