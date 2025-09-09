--
-- PostgreSQL database cluster dump
--

-- Started on 2025-09-09 18:30:20

\restrict r1v92coCro4CdtsFMzEiWVG5enwKINMCMwRaNUJqjC77VPrxO4VjK1ML9DtkLeZ

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS;

--
-- User Configurations
--








\unrestrict r1v92coCro4CdtsFMzEiWVG5enwKINMCMwRaNUJqjC77VPrxO4VjK1ML9DtkLeZ

--
-- Databases
--

--
-- Database "template1" dump
--

\connect template1

--
-- PostgreSQL database dump
--

\restrict X5nWN8nsVaQAu6DZJ3LRTy9MvoJtDuQJ9MW9HJjkJ72Peh8k2K7mMvNYpXgjCqx

-- Dumped from database version 17.6
-- Dumped by pg_dump version 17.6

-- Started on 2025-09-09 18:30:20

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Completed on 2025-09-09 18:30:20

--
-- PostgreSQL database dump complete
--

\unrestrict X5nWN8nsVaQAu6DZJ3LRTy9MvoJtDuQJ9MW9HJjkJ72Peh8k2K7mMvNYpXgjCqx

--
-- Database "people_lens" dump
--

--
-- PostgreSQL database dump
--

\restrict xIUp9REdg8qQMbCRb0SBBbdBe2Bg5n2Qb35OcsTRkLq453Hh4uUI764p47p5LWI

-- Dumped from database version 17.6
-- Dumped by pg_dump version 17.6

-- Started on 2025-09-09 18:30:20

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5144 (class 1262 OID 16557)
-- Name: people_lens; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE people_lens WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Portuguese_Brazil.1252';


ALTER DATABASE people_lens OWNER TO postgres;

\unrestrict xIUp9REdg8qQMbCRb0SBBbdBe2Bg5n2Qb35OcsTRkLq453Hh4uUI764p47p5LWI
\connect people_lens
\restrict xIUp9REdg8qQMbCRb0SBBbdBe2Bg5n2Qb35OcsTRkLq453Hh4uUI764p47p5LWI

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 254 (class 1259 OID 16860)
-- Name: analise_comparativa; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.analise_comparativa (
    id integer NOT NULL,
    company_id integer NOT NULL,
    titulo character varying(255) NOT NULL,
    pessoas_analisadas jsonb,
    resultados_ia jsonb,
    data_geracao timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.analise_comparativa OWNER TO postgres;

--
-- TOC entry 253 (class 1259 OID 16859)
-- Name: analise_comparativa_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.analise_comparativa_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.analise_comparativa_id_seq OWNER TO postgres;

--
-- TOC entry 5145 (class 0 OID 0)
-- Dependencies: 253
-- Name: analise_comparativa_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.analise_comparativa_id_seq OWNED BY public.analise_comparativa.id;


--
-- TOC entry 224 (class 1259 OID 16586)
-- Name: company; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.company (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    cnpj character varying(15) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    status_id integer NOT NULL
);


ALTER TABLE public.company OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16585)
-- Name: company_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.company_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.company_id_seq OWNER TO postgres;

--
-- TOC entry 5146 (class 0 OID 0)
-- Dependencies: 223
-- Name: company_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.company_id_seq OWNED BY public.company.id;


--
-- TOC entry 234 (class 1259 OID 16679)
-- Name: company_test; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.company_test (
    id integer NOT NULL,
    test_id integer NOT NULL,
    company_id integer NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.company_test OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 16678)
-- Name: company_test_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.company_test_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.company_test_id_seq OWNER TO postgres;

--
-- TOC entry 5147 (class 0 OID 0)
-- Dependencies: 233
-- Name: company_test_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.company_test_id_seq OWNED BY public.company_test.id;


--
-- TOC entry 238 (class 1259 OID 16714)
-- Name: key_configuration_question; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.key_configuration_question (
    id integer NOT NULL,
    key_name character varying(255) NOT NULL,
    description character varying(255),
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.key_configuration_question OWNER TO postgres;

--
-- TOC entry 237 (class 1259 OID 16713)
-- Name: key_configuration_question_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.key_configuration_question_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.key_configuration_question_id_seq OWNER TO postgres;

--
-- TOC entry 5148 (class 0 OID 0)
-- Dependencies: 237
-- Name: key_configuration_question_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.key_configuration_question_id_seq OWNED BY public.key_configuration_question.id;


--
-- TOC entry 226 (class 1259 OID 16604)
-- Name: person; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.person (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    birthday date NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.person OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 16613)
-- Name: person_company; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.person_company (
    id integer NOT NULL,
    person_id integer NOT NULL,
    company_id integer NOT NULL,
    status_id integer NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.person_company OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 16612)
-- Name: person_company_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.person_company_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.person_company_id_seq OWNER TO postgres;

--
-- TOC entry 5149 (class 0 OID 0)
-- Dependencies: 227
-- Name: person_company_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.person_company_id_seq OWNED BY public.person_company.id;


--
-- TOC entry 225 (class 1259 OID 16603)
-- Name: person_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.person_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.person_id_seq OWNER TO postgres;

--
-- TOC entry 5150 (class 0 OID 0)
-- Dependencies: 225
-- Name: person_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.person_id_seq OWNED BY public.person.id;


--
-- TOC entry 236 (class 1259 OID 16698)
-- Name: question; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.question (
    id integer NOT NULL,
    response_type_id integer NOT NULL,
    question_text text NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.question OWNER TO postgres;

--
-- TOC entry 240 (class 1259 OID 16727)
-- Name: question_configuration; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.question_configuration (
    id integer NOT NULL,
    question_id integer NOT NULL,
    key_configuration_question_id integer NOT NULL,
    value text NOT NULL
);


ALTER TABLE public.question_configuration OWNER TO postgres;

--
-- TOC entry 239 (class 1259 OID 16726)
-- Name: question_configuration_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.question_configuration_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.question_configuration_id_seq OWNER TO postgres;

--
-- TOC entry 5151 (class 0 OID 0)
-- Dependencies: 239
-- Name: question_configuration_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.question_configuration_id_seq OWNED BY public.question_configuration.id;


--
-- TOC entry 235 (class 1259 OID 16697)
-- Name: question_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.question_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.question_id_seq OWNER TO postgres;

--
-- TOC entry 5152 (class 0 OID 0)
-- Dependencies: 235
-- Name: question_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.question_id_seq OWNED BY public.question.id;


--
-- TOC entry 242 (class 1259 OID 16746)
-- Name: question_response_option; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.question_response_option (
    id integer NOT NULL,
    question_id integer NOT NULL,
    text text,
    value integer
);


ALTER TABLE public.question_response_option OWNER TO postgres;

--
-- TOC entry 241 (class 1259 OID 16745)
-- Name: question_response_option_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.question_response_option_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.question_response_option_id_seq OWNER TO postgres;

--
-- TOC entry 5153 (class 0 OID 0)
-- Dependencies: 241
-- Name: question_response_option_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.question_response_option_id_seq OWNED BY public.question_response_option.id;


--
-- TOC entry 252 (class 1259 OID 16840)
-- Name: relatorio; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.relatorio (
    id integer NOT NULL,
    company_id integer NOT NULL,
    test_applied_id integer,
    title character varying(255) NOT NULL,
    conteudo_ia jsonb,
    data_geracao timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.relatorio OWNER TO postgres;

--
-- TOC entry 251 (class 1259 OID 16839)
-- Name: relatorio_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.relatorio_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.relatorio_id_seq OWNER TO postgres;

--
-- TOC entry 5154 (class 0 OID 0)
-- Dependencies: 251
-- Name: relatorio_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.relatorio_id_seq OWNED BY public.relatorio.id;


--
-- TOC entry 248 (class 1259 OID 16802)
-- Name: response; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.response (
    id integer NOT NULL,
    test_applied_id integer NOT NULL,
    question_id integer NOT NULL,
    value text,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.response OWNER TO postgres;

--
-- TOC entry 247 (class 1259 OID 16801)
-- Name: response_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.response_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.response_id_seq OWNER TO postgres;

--
-- TOC entry 5155 (class 0 OID 0)
-- Dependencies: 247
-- Name: response_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.response_id_seq OWNED BY public.response.id;


--
-- TOC entry 250 (class 1259 OID 16823)
-- Name: response_option; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.response_option (
    id integer NOT NULL,
    response_id integer NOT NULL,
    question_response_option_id integer NOT NULL
);


ALTER TABLE public.response_option OWNER TO postgres;

--
-- TOC entry 249 (class 1259 OID 16822)
-- Name: response_option_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.response_option_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.response_option_id_seq OWNER TO postgres;

--
-- TOC entry 5156 (class 0 OID 0)
-- Dependencies: 249
-- Name: response_option_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.response_option_id_seq OWNED BY public.response_option.id;


--
-- TOC entry 220 (class 1259 OID 16568)
-- Name: response_type; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.response_type (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE public.response_type OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16567)
-- Name: response_type_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.response_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.response_type_id_seq OWNER TO postgres;

--
-- TOC entry 5157 (class 0 OID 0)
-- Dependencies: 219
-- Name: response_type_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.response_type_id_seq OWNED BY public.response_type.id;


--
-- TOC entry 256 (class 1259 OID 16876)
-- Name: role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.role (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    description text
);


ALTER TABLE public.role OWNER TO postgres;

--
-- TOC entry 255 (class 1259 OID 16875)
-- Name: role_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.role_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.role_id_seq OWNER TO postgres;

--
-- TOC entry 5158 (class 0 OID 0)
-- Dependencies: 255
-- Name: role_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.role_id_seq OWNED BY public.role.id;


--
-- TOC entry 218 (class 1259 OID 16559)
-- Name: status; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.status (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE public.status OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16558)
-- Name: status_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.status_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.status_id_seq OWNER TO postgres;

--
-- TOC entry 5159 (class 0 OID 0)
-- Dependencies: 217
-- Name: status_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.status_id_seq OWNED BY public.status.id;


--
-- TOC entry 232 (class 1259 OID 16663)
-- Name: test; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.test (
    id integer NOT NULL,
    test_type_id integer NOT NULL,
    title character varying(255) NOT NULL,
    description character varying(255),
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.test OWNER TO postgres;

--
-- TOC entry 246 (class 1259 OID 16777)
-- Name: test_applied; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.test_applied (
    id integer NOT NULL,
    status_id integer NOT NULL,
    company_test_id integer NOT NULL,
    person_id integer NOT NULL,
    date_begin_datetime timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    date_end_datetime timestamp without time zone,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.test_applied OWNER TO postgres;

--
-- TOC entry 245 (class 1259 OID 16776)
-- Name: test_applied_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.test_applied_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.test_applied_id_seq OWNER TO postgres;

--
-- TOC entry 5160 (class 0 OID 0)
-- Dependencies: 245
-- Name: test_applied_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.test_applied_id_seq OWNED BY public.test_applied.id;


--
-- TOC entry 231 (class 1259 OID 16662)
-- Name: test_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.test_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.test_id_seq OWNER TO postgres;

--
-- TOC entry 5161 (class 0 OID 0)
-- Dependencies: 231
-- Name: test_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.test_id_seq OWNED BY public.test.id;


--
-- TOC entry 244 (class 1259 OID 16760)
-- Name: test_question; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.test_question (
    id integer NOT NULL,
    test_id integer NOT NULL,
    question_id integer NOT NULL
);


ALTER TABLE public.test_question OWNER TO postgres;

--
-- TOC entry 243 (class 1259 OID 16759)
-- Name: test_question_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.test_question_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.test_question_id_seq OWNER TO postgres;

--
-- TOC entry 5162 (class 0 OID 0)
-- Dependencies: 243
-- Name: test_question_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.test_question_id_seq OWNED BY public.test_question.id;


--
-- TOC entry 222 (class 1259 OID 16577)
-- Name: test_type; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.test_type (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE public.test_type OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16576)
-- Name: test_type_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.test_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.test_type_id_seq OWNER TO postgres;

--
-- TOC entry 5163 (class 0 OID 0)
-- Dependencies: 221
-- Name: test_type_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.test_type_id_seq OWNED BY public.test_type.id;


--
-- TOC entry 230 (class 1259 OID 16637)
-- Name: user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."user" (
    id integer NOT NULL,
    person_id integer NOT NULL,
    status_id integer NOT NULL,
    email character varying(255) NOT NULL,
    password character varying(255) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    role_id integer NOT NULL
);


ALTER TABLE public."user" OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 16636)
-- Name: user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.user_id_seq OWNER TO postgres;

--
-- TOC entry 5164 (class 0 OID 0)
-- Dependencies: 229
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;


--
-- TOC entry 4877 (class 2604 OID 16863)
-- Name: analise_comparativa id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.analise_comparativa ALTER COLUMN id SET DEFAULT nextval('public.analise_comparativa_id_seq'::regclass);


--
-- TOC entry 4840 (class 2604 OID 16589)
-- Name: company id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company ALTER COLUMN id SET DEFAULT nextval('public.company_id_seq'::regclass);


--
-- TOC entry 4855 (class 2604 OID 16682)
-- Name: company_test id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company_test ALTER COLUMN id SET DEFAULT nextval('public.company_test_id_seq'::regclass);


--
-- TOC entry 4861 (class 2604 OID 16717)
-- Name: key_configuration_question id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.key_configuration_question ALTER COLUMN id SET DEFAULT nextval('public.key_configuration_question_id_seq'::regclass);


--
-- TOC entry 4843 (class 2604 OID 16607)
-- Name: person id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person ALTER COLUMN id SET DEFAULT nextval('public.person_id_seq'::regclass);


--
-- TOC entry 4846 (class 2604 OID 16616)
-- Name: person_company id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company ALTER COLUMN id SET DEFAULT nextval('public.person_company_id_seq'::regclass);


--
-- TOC entry 4858 (class 2604 OID 16701)
-- Name: question id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question ALTER COLUMN id SET DEFAULT nextval('public.question_id_seq'::regclass);


--
-- TOC entry 4864 (class 2604 OID 16730)
-- Name: question_configuration id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_configuration ALTER COLUMN id SET DEFAULT nextval('public.question_configuration_id_seq'::regclass);


--
-- TOC entry 4865 (class 2604 OID 16749)
-- Name: question_response_option id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_response_option ALTER COLUMN id SET DEFAULT nextval('public.question_response_option_id_seq'::regclass);


--
-- TOC entry 4875 (class 2604 OID 16843)
-- Name: relatorio id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.relatorio ALTER COLUMN id SET DEFAULT nextval('public.relatorio_id_seq'::regclass);


--
-- TOC entry 4871 (class 2604 OID 16805)
-- Name: response id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response ALTER COLUMN id SET DEFAULT nextval('public.response_id_seq'::regclass);


--
-- TOC entry 4874 (class 2604 OID 16826)
-- Name: response_option id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_option ALTER COLUMN id SET DEFAULT nextval('public.response_option_id_seq'::regclass);


--
-- TOC entry 4838 (class 2604 OID 16571)
-- Name: response_type id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_type ALTER COLUMN id SET DEFAULT nextval('public.response_type_id_seq'::regclass);


--
-- TOC entry 4879 (class 2604 OID 16879)
-- Name: role id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role ALTER COLUMN id SET DEFAULT nextval('public.role_id_seq'::regclass);


--
-- TOC entry 4837 (class 2604 OID 16562)
-- Name: status id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status ALTER COLUMN id SET DEFAULT nextval('public.status_id_seq'::regclass);


--
-- TOC entry 4852 (class 2604 OID 16666)
-- Name: test id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test ALTER COLUMN id SET DEFAULT nextval('public.test_id_seq'::regclass);


--
-- TOC entry 4867 (class 2604 OID 16780)
-- Name: test_applied id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied ALTER COLUMN id SET DEFAULT nextval('public.test_applied_id_seq'::regclass);


--
-- TOC entry 4866 (class 2604 OID 16763)
-- Name: test_question id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_question ALTER COLUMN id SET DEFAULT nextval('public.test_question_id_seq'::regclass);


--
-- TOC entry 4839 (class 2604 OID 16580)
-- Name: test_type id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_type ALTER COLUMN id SET DEFAULT nextval('public.test_type_id_seq'::regclass);


--
-- TOC entry 4849 (class 2604 OID 16640)
-- Name: user id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);


--
-- TOC entry 5136 (class 0 OID 16860)
-- Dependencies: 254
-- Data for Name: analise_comparativa; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.analise_comparativa (id, company_id, titulo, pessoas_analisadas, resultados_ia, data_geracao) FROM stdin;
\.


--
-- TOC entry 5106 (class 0 OID 16586)
-- Dependencies: 224
-- Data for Name: company; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.company (id, name, cnpj, created_at, updated_at, status_id) FROM stdin;
2	teste	11112233342213	2025-09-09 14:05:39.277	2025-09-09 14:39:01.005538	1
1	Empresa teste	11111111111111	2025-09-09 10:03:49.998124	2025-09-09 14:39:59.080455	1
3	string	string	2025-09-09 16:01:26.60138	2025-09-09 16:01:26.60138	1
\.


--
-- TOC entry 5116 (class 0 OID 16679)
-- Dependencies: 234
-- Data for Name: company_test; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.company_test (id, test_id, company_id, created_at, updated_at) FROM stdin;
\.


--
-- TOC entry 5120 (class 0 OID 16714)
-- Dependencies: 238
-- Data for Name: key_configuration_question; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.key_configuration_question (id, key_name, description, created_at, updated_at) FROM stdin;
\.


--
-- TOC entry 5108 (class 0 OID 16604)
-- Dependencies: 226
-- Data for Name: person; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.person (id, name, birthday, created_at, updated_at) FROM stdin;
1	Administrador Padrão	2025-09-09	2025-09-09 10:03:49.998124	2025-09-09 10:03:49.998124
5	Arthur Martins Thomé	1991-04-01	2025-09-09 15:50:33.924925	2025-09-09 15:50:33.924925
6	Arthur Martins Thomé	1991-04-01	2025-09-09 15:50:48.49487	2025-09-09 15:50:48.49487
7	Arthur Martins Thomé	1991-04-01	2025-09-09 15:58:26.606496	2025-09-09 15:58:26.606496
8	Arthur Martins Thomé	1991-04-01	2025-09-09 16:00:00.571027	2025-09-09 16:00:00.571027
\.


--
-- TOC entry 5110 (class 0 OID 16613)
-- Dependencies: 228
-- Data for Name: person_company; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.person_company (id, person_id, company_id, status_id, created_at, updated_at) FROM stdin;
1	1	1	1	2025-09-09 10:03:49.998124	2025-09-09 10:03:49.998124
2	5	1	1	2025-09-09 15:50:34.281673	2025-09-09 15:50:34.281673
3	6	1	1	2025-09-09 15:51:02.64654	2025-09-09 15:51:02.64654
4	7	1	1	2025-09-09 15:58:29.647197	2025-09-09 15:58:29.647197
5	8	1	1	2025-09-09 16:00:00.897287	2025-09-09 16:00:00.897287
\.


--
-- TOC entry 5118 (class 0 OID 16698)
-- Dependencies: 236
-- Data for Name: question; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.question (id, response_type_id, question_text, created_at, updated_at) FROM stdin;
6	1	Quando você está em grupo, qual é o seu papel?	2025-09-09 16:19:03.906426	2025-09-09 16:19:03.906426
7	1	Qual é a sua reação a uma mudança inesperada de planos?	2025-09-09 16:19:03.906426	2025-09-09 16:19:03.906426
8	1	Em um projeto, o que mais te motiva?	2025-09-09 16:19:03.906426	2025-09-09 16:19:03.906426
9	1	Qual destas frases mais te representa?	2025-09-09 16:19:03.906426	2025-09-09 16:19:03.906426
10	1	Como você lida com um conflito?	2025-09-09 16:19:03.906426	2025-09-09 16:19:03.906426
\.


--
-- TOC entry 5122 (class 0 OID 16727)
-- Dependencies: 240
-- Data for Name: question_configuration; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.question_configuration (id, question_id, key_configuration_question_id, value) FROM stdin;
\.


--
-- TOC entry 5124 (class 0 OID 16746)
-- Dependencies: 242
-- Data for Name: question_response_option; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.question_response_option (id, question_id, text, value) FROM stdin;
1	6	Sou o líder, o que toma as decisões.	5
2	6	Sou o que cuida de todos e mantém o grupo unido.	2
3	6	Sou o observador, o que analisa antes de agir.	3
4	6	Sou o executor, o que faz as coisas acontecerem.	4
5	6	Sou o que prefere trabalhar sozinho, mas me adapto.	1
6	7	Fico irritado e quero voltar ao plano original.	4
7	7	Me adapto facilmente e vejo como uma oportunidade.	1
8	7	Analiso os prós e contras antes de decidir.	3
9	7	Assumo o controle e ajudo a redefinir a estratégia.	5
10	7	Busco o consenso do grupo para seguir em frente.	2
11	8	A excelência, a qualidade do resultado final.	3
12	8	O desafio, a superação de obstáculos.	5
13	8	A colaboração, o trabalho em equipe.	2
14	8	A execução, a finalização das tarefas.	4
15	8	A autonomia, poder trabalhar no meu ritmo.	1
16	9	Tento ser discreto, mas quando preciso, defendo meu território.	1
17	9	Gosto de ter meus amigos por perto e criar laços fortes.	2
18	9	A sabedoria é o meu guia, prefiro analisar antes de tomar uma decisão.	3
19	9	Sou o líder nato, a pessoa para quem as outras recorrem.	5
20	9	Sou leal ao meu grupo e faço de tudo para proteger os meus.	4
21	10	Uso a lógica para encontrar a melhor solução para todos.	3
22	10	Sou o primeiro a assumir o controle e resolver a situação.	5
23	10	Não gosto de conflito, prefiro me retirar.	1
24	10	Protejo meu grupo e faço o que for preciso para defender os meus.	4
25	10	Tento mediar a situação e encontrar um consenso.	2
\.


--
-- TOC entry 5134 (class 0 OID 16840)
-- Dependencies: 252
-- Data for Name: relatorio; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.relatorio (id, company_id, test_applied_id, title, conteudo_ia, data_geracao) FROM stdin;
\.


--
-- TOC entry 5130 (class 0 OID 16802)
-- Dependencies: 248
-- Data for Name: response; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.response (id, test_applied_id, question_id, value, created_at, updated_at) FROM stdin;
\.


--
-- TOC entry 5132 (class 0 OID 16823)
-- Dependencies: 250
-- Data for Name: response_option; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.response_option (id, response_id, question_response_option_id) FROM stdin;
\.


--
-- TOC entry 5102 (class 0 OID 16568)
-- Dependencies: 220
-- Data for Name: response_type; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.response_type (id, name, description) FROM stdin;
1	multipla_escolha	Permite ao candidato escolher uma ou mais opções pré-definidas.
2	dissertativa	Permite ao candidato escrever uma resposta em texto livre.
\.


--
-- TOC entry 5138 (class 0 OID 16876)
-- Dependencies: 256
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.role (id, name, description) FROM stdin;
2	Admin	Acesso parcial ao sistema
3	User	Usuário normal
1	SuperAdmin	Acesso total ao sistema
\.


--
-- TOC entry 5100 (class 0 OID 16559)
-- Dependencies: 218
-- Data for Name: status; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.status (id, name, description) FROM stdin;
1	Ativo	Ativo no sistema
2	Inativo	Inativo no sistema
3	Excluido	Excluido do sistema
\.


--
-- TOC entry 5114 (class 0 OID 16663)
-- Dependencies: 232
-- Data for Name: test; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test (id, test_type_id, title, description, created_at, updated_at) FROM stdin;
1	1	Qual bicho você é?	Responda às perguntas e descubra seu perfil comportamental baseado em animais.	2025-09-09 16:16:14.895523	2025-09-09 16:16:14.895523
\.


--
-- TOC entry 5128 (class 0 OID 16777)
-- Dependencies: 246
-- Data for Name: test_applied; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test_applied (id, status_id, company_test_id, person_id, date_begin_datetime, date_end_datetime, created_at, updated_at) FROM stdin;
\.


--
-- TOC entry 5126 (class 0 OID 16760)
-- Dependencies: 244
-- Data for Name: test_question; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test_question (id, test_id, question_id) FROM stdin;
1	1	6
2	1	7
3	1	8
4	1	9
5	1	10
\.


--
-- TOC entry 5104 (class 0 OID 16577)
-- Dependencies: 222
-- Data for Name: test_type; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test_type (id, name, description) FROM stdin;
1	Análise Comportamental	Teste para identificar o perfil comportamental do candidato.
\.


--
-- TOC entry 5112 (class 0 OID 16637)
-- Dependencies: 230
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."user" (id, person_id, status_id, email, password, created_at, updated_at, role_id) FROM stdin;
1	1	1	admin@email.com	senha123	2025-09-09 10:03:49.998124	2025-09-09 10:03:49.998124	1
2	8	1	arthurthome02@gmail.com	123	2025-09-09 16:00:00.9051	2025-09-09 16:00:00.9051	1
\.


--
-- TOC entry 5165 (class 0 OID 0)
-- Dependencies: 253
-- Name: analise_comparativa_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.analise_comparativa_id_seq', 1, false);


--
-- TOC entry 5166 (class 0 OID 0)
-- Dependencies: 223
-- Name: company_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.company_id_seq', 3, true);


--
-- TOC entry 5167 (class 0 OID 0)
-- Dependencies: 233
-- Name: company_test_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.company_test_id_seq', 1, false);


--
-- TOC entry 5168 (class 0 OID 0)
-- Dependencies: 237
-- Name: key_configuration_question_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.key_configuration_question_id_seq', 1, false);


--
-- TOC entry 5169 (class 0 OID 0)
-- Dependencies: 227
-- Name: person_company_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.person_company_id_seq', 5, true);


--
-- TOC entry 5170 (class 0 OID 0)
-- Dependencies: 225
-- Name: person_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.person_id_seq', 8, true);


--
-- TOC entry 5171 (class 0 OID 0)
-- Dependencies: 239
-- Name: question_configuration_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_configuration_id_seq', 1, false);


--
-- TOC entry 5172 (class 0 OID 0)
-- Dependencies: 235
-- Name: question_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_id_seq', 10, true);


--
-- TOC entry 5173 (class 0 OID 0)
-- Dependencies: 241
-- Name: question_response_option_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_response_option_id_seq', 25, true);


--
-- TOC entry 5174 (class 0 OID 0)
-- Dependencies: 251
-- Name: relatorio_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.relatorio_id_seq', 1, false);


--
-- TOC entry 5175 (class 0 OID 0)
-- Dependencies: 247
-- Name: response_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.response_id_seq', 1, false);


--
-- TOC entry 5176 (class 0 OID 0)
-- Dependencies: 249
-- Name: response_option_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.response_option_id_seq', 1, false);


--
-- TOC entry 5177 (class 0 OID 0)
-- Dependencies: 219
-- Name: response_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.response_type_id_seq', 2, true);


--
-- TOC entry 5178 (class 0 OID 0)
-- Dependencies: 255
-- Name: role_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.role_id_seq', 3, true);


--
-- TOC entry 5179 (class 0 OID 0)
-- Dependencies: 217
-- Name: status_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.status_id_seq', 3, true);


--
-- TOC entry 5180 (class 0 OID 0)
-- Dependencies: 245
-- Name: test_applied_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.test_applied_id_seq', 1, false);


--
-- TOC entry 5181 (class 0 OID 0)
-- Dependencies: 231
-- Name: test_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.test_id_seq', 1, true);


--
-- TOC entry 5182 (class 0 OID 0)
-- Dependencies: 243
-- Name: test_question_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.test_question_id_seq', 5, true);


--
-- TOC entry 5183 (class 0 OID 0)
-- Dependencies: 221
-- Name: test_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.test_type_id_seq', 1, true);


--
-- TOC entry 5184 (class 0 OID 0)
-- Dependencies: 229
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_id_seq', 2, true);


--
-- TOC entry 4925 (class 2606 OID 16868)
-- Name: analise_comparativa analise_comparativa_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.analise_comparativa
    ADD CONSTRAINT analise_comparativa_pkey PRIMARY KEY (id);


--
-- TOC entry 4887 (class 2606 OID 16890)
-- Name: company company_cnpj_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company
    ADD CONSTRAINT company_cnpj_key UNIQUE (cnpj);


--
-- TOC entry 4889 (class 2606 OID 16595)
-- Name: company company_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company
    ADD CONSTRAINT company_pkey PRIMARY KEY (id);


--
-- TOC entry 4903 (class 2606 OID 16686)
-- Name: company_test company_test_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company_test
    ADD CONSTRAINT company_test_pkey PRIMARY KEY (id);


--
-- TOC entry 4907 (class 2606 OID 16725)
-- Name: key_configuration_question key_configuration_question_key_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.key_configuration_question
    ADD CONSTRAINT key_configuration_question_key_name_key UNIQUE (key_name);


--
-- TOC entry 4909 (class 2606 OID 16723)
-- Name: key_configuration_question key_configuration_question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.key_configuration_question
    ADD CONSTRAINT key_configuration_question_pkey PRIMARY KEY (id);


--
-- TOC entry 4893 (class 2606 OID 16620)
-- Name: person_company person_company_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company
    ADD CONSTRAINT person_company_pkey PRIMARY KEY (id);


--
-- TOC entry 4891 (class 2606 OID 16611)
-- Name: person person_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person
    ADD CONSTRAINT person_pkey PRIMARY KEY (id);


--
-- TOC entry 4911 (class 2606 OID 16734)
-- Name: question_configuration question_configuration_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_configuration
    ADD CONSTRAINT question_configuration_pkey PRIMARY KEY (id);


--
-- TOC entry 4905 (class 2606 OID 16707)
-- Name: question question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question
    ADD CONSTRAINT question_pkey PRIMARY KEY (id);


--
-- TOC entry 4913 (class 2606 OID 16753)
-- Name: question_response_option question_response_option_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_response_option
    ADD CONSTRAINT question_response_option_pkey PRIMARY KEY (id);


--
-- TOC entry 4923 (class 2606 OID 16848)
-- Name: relatorio relatorio_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.relatorio
    ADD CONSTRAINT relatorio_pkey PRIMARY KEY (id);


--
-- TOC entry 4921 (class 2606 OID 16828)
-- Name: response_option response_option_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_option
    ADD CONSTRAINT response_option_pkey PRIMARY KEY (id);


--
-- TOC entry 4919 (class 2606 OID 16811)
-- Name: response response_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response
    ADD CONSTRAINT response_pkey PRIMARY KEY (id);


--
-- TOC entry 4883 (class 2606 OID 16575)
-- Name: response_type response_type_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_type
    ADD CONSTRAINT response_type_pkey PRIMARY KEY (id);


--
-- TOC entry 4927 (class 2606 OID 16883)
-- Name: role role_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT role_pkey PRIMARY KEY (id);


--
-- TOC entry 4881 (class 2606 OID 16566)
-- Name: status status_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status
    ADD CONSTRAINT status_pkey PRIMARY KEY (id);


--
-- TOC entry 4917 (class 2606 OID 16785)
-- Name: test_applied test_applied_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied
    ADD CONSTRAINT test_applied_pkey PRIMARY KEY (id);


--
-- TOC entry 4901 (class 2606 OID 16672)
-- Name: test test_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test
    ADD CONSTRAINT test_pkey PRIMARY KEY (id);


--
-- TOC entry 4915 (class 2606 OID 16765)
-- Name: test_question test_question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_question
    ADD CONSTRAINT test_question_pkey PRIMARY KEY (id);


--
-- TOC entry 4885 (class 2606 OID 16584)
-- Name: test_type test_type_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_type
    ADD CONSTRAINT test_type_pkey PRIMARY KEY (id);


--
-- TOC entry 4895 (class 2606 OID 16651)
-- Name: user user_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_email_key UNIQUE (email);


--
-- TOC entry 4897 (class 2606 OID 16649)
-- Name: user user_person_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_person_id_key UNIQUE (person_id);


--
-- TOC entry 4899 (class 2606 OID 16647)
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);


--
-- TOC entry 4953 (class 2606 OID 16869)
-- Name: analise_comparativa analise_comparativa_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.analise_comparativa
    ADD CONSTRAINT analise_comparativa_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 4928 (class 2606 OID 16598)
-- Name: company company_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company
    ADD CONSTRAINT company_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.status(id);


--
-- TOC entry 4936 (class 2606 OID 16692)
-- Name: company_test company_test_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company_test
    ADD CONSTRAINT company_test_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 4937 (class 2606 OID 16687)
-- Name: company_test company_test_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company_test
    ADD CONSTRAINT company_test_test_id_fkey FOREIGN KEY (test_id) REFERENCES public.test(id);


--
-- TOC entry 4932 (class 2606 OID 16884)
-- Name: user fk_user_role; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT fk_user_role FOREIGN KEY (role_id) REFERENCES public.role(id);


--
-- TOC entry 4929 (class 2606 OID 16626)
-- Name: person_company person_company_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company
    ADD CONSTRAINT person_company_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 4930 (class 2606 OID 16621)
-- Name: person_company person_company_person_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company
    ADD CONSTRAINT person_company_person_id_fkey FOREIGN KEY (person_id) REFERENCES public.person(id);


--
-- TOC entry 4931 (class 2606 OID 16631)
-- Name: person_company person_company_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company
    ADD CONSTRAINT person_company_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.status(id);


--
-- TOC entry 4939 (class 2606 OID 16740)
-- Name: question_configuration question_configuration_key_configuration_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_configuration
    ADD CONSTRAINT question_configuration_key_configuration_question_id_fkey FOREIGN KEY (key_configuration_question_id) REFERENCES public.key_configuration_question(id);


--
-- TOC entry 4940 (class 2606 OID 16735)
-- Name: question_configuration question_configuration_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_configuration
    ADD CONSTRAINT question_configuration_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.question(id);


--
-- TOC entry 4941 (class 2606 OID 16754)
-- Name: question_response_option question_response_option_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_response_option
    ADD CONSTRAINT question_response_option_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.question(id);


--
-- TOC entry 4938 (class 2606 OID 16708)
-- Name: question question_response_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question
    ADD CONSTRAINT question_response_type_id_fkey FOREIGN KEY (response_type_id) REFERENCES public.response_type(id);


--
-- TOC entry 4951 (class 2606 OID 16849)
-- Name: relatorio relatorio_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.relatorio
    ADD CONSTRAINT relatorio_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 4952 (class 2606 OID 16854)
-- Name: relatorio relatorio_test_applied_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.relatorio
    ADD CONSTRAINT relatorio_test_applied_id_fkey FOREIGN KEY (test_applied_id) REFERENCES public.test_applied(id);


--
-- TOC entry 4949 (class 2606 OID 16834)
-- Name: response_option response_option_question_response_option_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_option
    ADD CONSTRAINT response_option_question_response_option_id_fkey FOREIGN KEY (question_response_option_id) REFERENCES public.question_response_option(id);


--
-- TOC entry 4950 (class 2606 OID 16829)
-- Name: response_option response_option_response_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_option
    ADD CONSTRAINT response_option_response_id_fkey FOREIGN KEY (response_id) REFERENCES public.response(id);


--
-- TOC entry 4947 (class 2606 OID 16817)
-- Name: response response_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response
    ADD CONSTRAINT response_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.question(id);


--
-- TOC entry 4948 (class 2606 OID 16812)
-- Name: response response_test_applied_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response
    ADD CONSTRAINT response_test_applied_id_fkey FOREIGN KEY (test_applied_id) REFERENCES public.test_applied(id);


--
-- TOC entry 4944 (class 2606 OID 16791)
-- Name: test_applied test_applied_company_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied
    ADD CONSTRAINT test_applied_company_test_id_fkey FOREIGN KEY (company_test_id) REFERENCES public.company_test(id);


--
-- TOC entry 4945 (class 2606 OID 16796)
-- Name: test_applied test_applied_person_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied
    ADD CONSTRAINT test_applied_person_id_fkey FOREIGN KEY (person_id) REFERENCES public.person(id);


--
-- TOC entry 4946 (class 2606 OID 16786)
-- Name: test_applied test_applied_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied
    ADD CONSTRAINT test_applied_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.status(id);


--
-- TOC entry 4942 (class 2606 OID 16771)
-- Name: test_question test_question_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_question
    ADD CONSTRAINT test_question_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.question(id);


--
-- TOC entry 4943 (class 2606 OID 16766)
-- Name: test_question test_question_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_question
    ADD CONSTRAINT test_question_test_id_fkey FOREIGN KEY (test_id) REFERENCES public.test(id);


--
-- TOC entry 4935 (class 2606 OID 16673)
-- Name: test test_test_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test
    ADD CONSTRAINT test_test_type_id_fkey FOREIGN KEY (test_type_id) REFERENCES public.test_type(id);


--
-- TOC entry 4933 (class 2606 OID 16652)
-- Name: user user_person_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_person_id_fkey FOREIGN KEY (person_id) REFERENCES public.person(id);


--
-- TOC entry 4934 (class 2606 OID 16657)
-- Name: user user_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.status(id);


-- Completed on 2025-09-09 18:30:20

--
-- PostgreSQL database dump complete
--

\unrestrict xIUp9REdg8qQMbCRb0SBBbdBe2Bg5n2Qb35OcsTRkLq453Hh4uUI764p47p5LWI

--
-- Database "postgres" dump
--

\connect postgres

--
-- PostgreSQL database dump
--

\restrict oZSSNDIAur2nR3SjIscZEazIRZxrbDuFlgbESdpKoqphx4VpLjAzHJylvepyZfX

-- Dumped from database version 17.6
-- Dumped by pg_dump version 17.6

-- Started on 2025-09-09 18:30:20

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 16388)
-- Name: pgagent; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA pgagent;


ALTER SCHEMA pgagent OWNER TO postgres;

--
-- TOC entry 5002 (class 0 OID 0)
-- Dependencies: 7
-- Name: SCHEMA pgagent; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA pgagent IS 'pgAgent system tables';


--
-- TOC entry 2 (class 3079 OID 16389)
-- Name: pgagent; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgagent WITH SCHEMA pgagent;


--
-- TOC entry 5003 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION pgagent; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgagent IS 'A PostgreSQL job scheduler';


--
-- TOC entry 4780 (class 0 OID 16390)
-- Dependencies: 223
-- Data for Name: pga_jobagent; Type: TABLE DATA; Schema: pgagent; Owner: postgres
--

COPY pgagent.pga_jobagent (jagpid, jaglogintime, jagstation) FROM stdin;
9540	2025-09-09 09:00:01.721324-03	ArthurThome
\.


--
-- TOC entry 4781 (class 0 OID 16399)
-- Dependencies: 225
-- Data for Name: pga_jobclass; Type: TABLE DATA; Schema: pgagent; Owner: postgres
--

COPY pgagent.pga_jobclass (jclid, jclname) FROM stdin;
\.


--
-- TOC entry 4782 (class 0 OID 16409)
-- Dependencies: 227
-- Data for Name: pga_job; Type: TABLE DATA; Schema: pgagent; Owner: postgres
--

COPY pgagent.pga_job (jobid, jobjclid, jobname, jobdesc, jobhostagent, jobenabled, jobcreated, jobchanged, jobagentid, jobnextrun, joblastrun) FROM stdin;
\.


--
-- TOC entry 4784 (class 0 OID 16457)
-- Dependencies: 231
-- Data for Name: pga_schedule; Type: TABLE DATA; Schema: pgagent; Owner: postgres
--

COPY pgagent.pga_schedule (jscid, jscjobid, jscname, jscdesc, jscenabled, jscstart, jscend, jscminutes, jschours, jscweekdays, jscmonthdays, jscmonths) FROM stdin;
\.


--
-- TOC entry 4785 (class 0 OID 16485)
-- Dependencies: 233
-- Data for Name: pga_exception; Type: TABLE DATA; Schema: pgagent; Owner: postgres
--

COPY pgagent.pga_exception (jexid, jexscid, jexdate, jextime) FROM stdin;
\.


--
-- TOC entry 4786 (class 0 OID 16499)
-- Dependencies: 235
-- Data for Name: pga_joblog; Type: TABLE DATA; Schema: pgagent; Owner: postgres
--

COPY pgagent.pga_joblog (jlgid, jlgjobid, jlgstatus, jlgstart, jlgduration) FROM stdin;
\.


--
-- TOC entry 4783 (class 0 OID 16433)
-- Dependencies: 229
-- Data for Name: pga_jobstep; Type: TABLE DATA; Schema: pgagent; Owner: postgres
--

COPY pgagent.pga_jobstep (jstid, jstjobid, jstname, jstdesc, jstenabled, jstkind, jstcode, jstconnstr, jstdbname, jstonerror, jscnextrun) FROM stdin;
\.


--
-- TOC entry 4787 (class 0 OID 16515)
-- Dependencies: 237
-- Data for Name: pga_jobsteplog; Type: TABLE DATA; Schema: pgagent; Owner: postgres
--

COPY pgagent.pga_jobsteplog (jslid, jsljlgid, jsljstid, jslstatus, jslresult, jslstart, jslduration, jsloutput) FROM stdin;
\.


-- Completed on 2025-09-09 18:30:20

--
-- PostgreSQL database dump complete
--

\unrestrict oZSSNDIAur2nR3SjIscZEazIRZxrbDuFlgbESdpKoqphx4VpLjAzHJylvepyZfX

-- Completed on 2025-09-09 18:30:20

--
-- PostgreSQL database cluster dump complete
--

