-- Table: public.trpblex

-- DROP TABLE IF EXISTS public.trpblex;

CREATE TABLE IF NOT EXISTS public.trpblex
(
    trsid character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    brenm character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    noepo integer NOT NULL DEFAULT 0,
    idven character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nomsj character varying(50) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    noref character varying(50) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    noinv character varying(50) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nomfp character varying(50) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    indte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    jtdte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    CONSTRAINT trpblex_pkey PRIMARY KEY (trsid, brenm)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.trpblex
    OWNER to postgres;


-- Table: public.trpblin

-- DROP TABLE IF EXISTS public.trpblin;

CREATE TABLE IF NOT EXISTS public.trpblin
(
    trsid character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    nomso integer NOT NULL DEFAULT 0,
    nompr integer NOT NULL DEFAULT 0,
    nompo integer NOT NULL DEFAULT 0,
    nocoa integer NOT NULL DEFAULT 0,
    brgnm character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    stnbr character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    cmpnm character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    qtybr integer NOT NULL DEFAULT 0,
    prprc numeric(10,2) NOT NULL DEFAULT 0,
    nombr numeric(10,2) NOT NULL DEFAULT 0,
    nosrv numeric(10,2) NOT NULL DEFAULT 0,
    cstll numeric(10,2) NOT NULL DEFAULT 0,
    dscnt numeric(10,2) NOT NULL DEFAULT 0,
    pphcs numeric(10,2) NOT NULL DEFAULT 0,
    ppncs numeric(10,2) NOT NULL DEFAULT 0,
    ttlhg numeric(10,2) NOT NULL DEFAULT 0,
    pcktr character varying(40) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    podte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    dedte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    pcdte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    lsdte timestamp without time zone NOT NULL DEFAULT '2000-01-01'::date,
    CONSTRAINT trpblin_pkey PRIMARY KEY (trsid, brgnm)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.trpblin
    OWNER to postgres;

-- FUNCTION: public.trpbl_delete(character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.trpbl_delete(character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public.trpbl_delete(
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
  v_cmpnm varchar;
  BEGIN
   	SELECT qtybr,brgnm, cmpnm INTO v_qtybr, v_brgnm, v_cmpnm FROM trpblin WHERE trsid=i_trsid;
   	v_qtybr := COALESCE(v_qtybr, 0);
  	DELETE FROM trpblin WHERE trsid = i_trsid AND brgnm = i_brgnm;
	DELETE FROM trpblex WHERE trsid = i_trsid AND brenm = i_brenm;
	UPDATE cgood SET qtybr =qtybr - v_qtybr WHERE nomid = v_brgnm AND cmpnm = v_cmpnm;
  END;
$BODY$;

ALTER FUNCTION public.trpbl_delete(character varying, character varying, character varying)
    OWNER TO postgres;

-- FUNCTION: public.trpbl_insert(character varying, integer, integer, integer, integer, character varying, character varying, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, numeric, character varying, character varying, integer, character varying, character varying, character varying, character varying, character varying, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION IF EXISTS public.trpbl_insert(character varying, integer, integer, integer, integer, character varying, character varying, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, numeric, character varying, character varying, integer, character varying, character varying, character varying, character varying, character varying, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.trpbl_insert(
	i_trsid character varying,
	i_nomso integer,
	i_nompr integer,
	i_nompo integer,
	i_nocoa integer,
	i_brgnm character varying,
	i_stnbr character varying,
	i_cmpnm character varying,
	i_qtybr integer,
	i_prprc numeric,
	i_nombr numeric,
	i_nosrv numeric,
	i_cstll numeric,
	i_dscnt numeric,
	i_pphcs numeric,
	i_ppncs numeric,
	i_ttlhg numeric,
	i_pcktr character varying,
	i_brenm character varying,
	i_noepo integer,
	i_idven character varying,
	i_nomsj character varying,
	i_noref character varying,
	i_noinv character varying,
	i_nomfp character varying,
	i_indte timestamp without time zone,
	i_jtdte timestamp without time zone,
	i_podte timestamp without time zone,
	i_dedte timestamp without time zone,
	i_pcdte timestamp without time zone)
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
   v_qtybr := COALESCE(v_qtybr, 0) + i_qtybr;
   INSERT INTO trpblin (trsid, nomso, nompr, nompo, nocoa, brgnm, stnbr, cmpnm, qtybr, prprc, nombr, nosrv, cstll, dscnt,pphcs, ppncs, ttlhg, pcktr, podte, dedte, pcdte, lsdte)
   VALUES (i_trsid, i_nomso, i_nompr, i_nompo, i_nocoa, i_brgnm, i_stnbr, i_cmpnm, i_qtybr, i_prprc, i_nombr, i_nosrv, i_cstll, i_dscnt,i_pphcs, i_ppncs, i_ttlhg, i_pcktr, i_podte, i_dedte, i_pcdte, LOCALTIMESTAMP);
   
   INSERT INTO trpblex (trsid, brenm, noepo, idven, nomsj, noref, noinv, nomfp, indte, jtdte)
   VALUES (i_trsid, i_brenm, i_noepo, i_idven, i_nomsj, i_noref, i_noinv, i_nomfp, i_indte, i_jtdte);
   UPDATE cgood SET qtybr = v_qtybr WHERE nomid = i_brgnm AND cmpnm = i_cmpnm;
END;
$BODY$;

ALTER FUNCTION public.trpbl_insert(character varying, integer, integer, integer, integer, character varying, character varying, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, numeric, character varying, character varying, integer, character varying, character varying, character varying, character varying, character varying, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)
    OWNER TO postgres;

-- FUNCTION: public.trpbl_update(character varying, integer, integer, integer, integer, character varying, character varying, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, numeric, character varying, character varying, integer, character varying, character varying, character varying, character varying, character varying, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION IF EXISTS public.trpbl_update(character varying, integer, integer, integer, integer, character varying, character varying, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, numeric, character varying, character varying, integer, character varying, character varying, character varying, character varying, character varying, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.trpbl_update(
	i_trsid character varying,
	i_nomso integer,
	i_nompr integer,
	i_nompo integer,
	i_nocoa integer,
	i_brgnm character varying,
	i_stnbr character varying,
	i_cmpnm character varying,
	i_qtybr integer,
	i_prprc numeric,
	i_nombr numeric,
	i_nosrv numeric,
	i_cstll numeric,
	i_dscnt numeric,
	i_pphcs numeric,
	i_ppncs numeric,
	i_ttlhg numeric,
	i_pcktr character varying,
	i_brenm character varying,
	i_noepo integer,
	i_idven character varying,
	i_nomsj character varying,
	i_noref character varying,
	i_noinv character varying,
	i_nomfp character varying,
	i_indte timestamp without time zone,
	i_jtdte timestamp without time zone,
	i_podte timestamp without time zone,
	i_dedte timestamp without time zone,
	i_pcdte timestamp without time zone)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
   DECLARE 
    v_qtybr int;
    v_cmpnm varchar;
  BEGIN
    SELECT qtybr, cmpnm INTO v_qtybr, v_cmpnm FROM trpblin WHERE trsid = i_trsid AND brgnm = i_brgnm;
    v_qtybr = COALESCE(v_qtybr, 0);
    
    IF v_qtybr > i_qtybr THEN
     	DELETE FROM trpblin WHERE trsid = i_trsid AND brgnm = i_brgnm;
        
		INSERT INTO trpblin (trsid, nomso, nompr, nompo, nocoa, brgnm, stnbr, cmpnm, qtybr, prprc, nombr, nosrv, cstll, dscnt,pphcs, ppncs, ttlhg, pcktr, podte, dedte, pcdte, lsdte)
	   	VALUES (i_trsid, i_nomso, i_nompr, i_nompo, i_nocoa, i_brgnm, i_stnbr, i_cmpnm, i_qtybr, i_prprc, i_nombr, i_nosrv, i_cstll, i_dscnt,i_pphcs, i_ppncs, i_ttlhg, i_pcktr, i_podte, i_dedte, i_pcdte, LOCALTIMESTAMP);

		DELETE FROM trpblex WHERE trsid = i_trsid AND brenm = i_brenm;
		 INSERT INTO trpblex (trsid, brenm, noepo, idven, nomsj, noref, noinv, nomfp, indte, jtdte)
   		VALUES (i_trsid, i_brenm, i_noepo, i_idven, i_nomsj, i_noref, i_noinv, i_nomfp, i_indte, i_jtdte);
		UPDATE cgood SET qtybr = qtybr - (v_qtybr - i_qtybr) WHERE nomid = i_brgnm  AND cmpnm = v_cmpnm;

    ELSEIF v_qtybr < i_qtybr THEN
     	DELETE FROM trpblin WHERE trsid = i_trsid AND brgnm = i_brgnm;
        
		INSERT INTO trpblin (trsid, nomso, nompr, nompo, nocoa, brgnm, stnbr, cmpnm, qtybr, prprc, nombr, nosrv, cstll, dscnt,pphcs, ppncs, ttlhg, pcktr, podte, dedte, pcdte, lsdte)
	   	VALUES (i_trsid, i_nomso, i_nompr, i_nompo, i_nocoa, i_brgnm, i_stnbr, i_cmpnm, i_qtybr, i_prprc, i_nombr, i_nosrv, i_cstll, i_dscnt,i_pphcs, i_ppncs, i_ttlhg, i_pcktr, i_podte, i_dedte, i_pcdte, LOCALTIMESTAMP);

		DELETE FROM trpblex WHERE trsid = i_trsid AND brenm = i_brenm;
		INSERT INTO trpblex (trsid, brenm, noepo, idven, nomsj, noref, noinv, nomfp, indte, jtdte)
   		VALUES (i_trsid, i_brenm, i_noepo, i_idven, i_nomsj, i_noref, i_noinv, i_nomfp, i_indte, i_jtdte);
		UPDATE cgood SET qtybr = qtybr + (i_qtybr - v_qtybr) WHERE nomid = i_brgnm  AND cmpnm = v_cmpnm;

	ELSE -- v_qtybr = i_qtybr
     	DELETE FROM trpblin WHERE trsid = i_trsid AND brgnm = i_brgnm;
        
		INSERT INTO trpblin (trsid, nomso, nompr, nompo, nocoa, brgnm, stnbr, cmpnm, qtybr, prprc, nombr, nosrv, cstll, dscnt,pphcs, ppncs, ttlhg, pcktr, podte, dedte, pcdte, lsdte)
	   	VALUES (i_trsid, i_nomso, i_nompr, i_nompo, i_nocoa, i_brgnm, i_stnbr, i_cmpnm, i_qtybr, i_prprc, i_nombr, i_nosrv, i_cstll, i_dscnt,i_pphcs, i_ppncs, i_ttlhg, i_pcktr, i_podte, i_dedte, i_pcdte, LOCALTIMESTAMP);

		DELETE FROM trpblex WHERE trsid = i_trsid AND brenm = i_brenm;
		INSERT INTO trpblex (trsid, brenm, noepo, idven, nomsj, noref, noinv, nomfp, indte, jtdte)
   		VALUES (i_trsid, i_brenm, i_noepo, i_idven, i_nomsj, i_noref, i_noinv, i_nomfp, i_indte, i_jtdte);
		
    END IF;
  END;
$BODY$;

ALTER FUNCTION public.trpbl_update(character varying, integer, integer, integer, integer, character varying, character varying, character varying, integer, numeric, numeric, numeric, numeric, numeric, numeric, numeric, numeric, character varying, character varying, integer, character varying, character varying, character varying, character varying, character varying, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)
    OWNER TO postgres;
