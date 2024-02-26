-- Table: nsdbsyst.usmdl

-- DROP TABLE IF EXISTS nsdbsyst.usmdl;

CREATE TABLE IF NOT EXISTS nsdbsyst.usmdl
(
    idusr integer NOT NULL DEFAULT 0,
    mdusr character varying(10) COLLATE pg_catalog."default" NOT NULL DEFAULT '-'::character varying,
    CONSTRAINT usmdl_pkey PRIMARY KEY (idusr, mdusr)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS nsdbsyst.usmdl
    OWNER to postgres;