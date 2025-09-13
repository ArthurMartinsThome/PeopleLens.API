--
-- PostgreSQL database cluster dump
--

-- Started on 2025-09-13 17:26:51

\restrict ZVPVzeW20r1VZjjKhZtgbZqzOHpDlKsCcIemtiRoP2JGqqRZ0JkMLq5rvw24sG3

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








\unrestrict ZVPVzeW20r1VZjjKhZtgbZqzOHpDlKsCcIemtiRoP2JGqqRZ0JkMLq5rvw24sG3

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

\restrict 5INJP1UhvPhTB59r9OXROyw5z7Fuz5DrTiY9wtWe6YsXw4brf5FhbIYqQsKxHZA

-- Dumped from database version 17.6
-- Dumped by pg_dump version 17.6

-- Started on 2025-09-13 17:26:51

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

-- Completed on 2025-09-13 17:26:51

--
-- PostgreSQL database dump complete
--

\unrestrict 5INJP1UhvPhTB59r9OXROyw5z7Fuz5DrTiY9wtWe6YsXw4brf5FhbIYqQsKxHZA

--
-- Database "people_lens" dump
--

--
-- PostgreSQL database dump
--

\restrict tv62vwdCINhpu2qGeQTSx84yXXFmeIx4VsbbugeamtjdGh5zMpnCaCVvB72PKyw

-- Dumped from database version 17.6
-- Dumped by pg_dump version 17.6

-- Started on 2025-09-13 17:26:51

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
-- TOC entry 5156 (class 1262 OID 16557)
-- Name: people_lens; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE people_lens WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Portuguese_Brazil.1252';


ALTER DATABASE people_lens OWNER TO postgres;

\unrestrict tv62vwdCINhpu2qGeQTSx84yXXFmeIx4VsbbugeamtjdGh5zMpnCaCVvB72PKyw
\connect people_lens
\restrict tv62vwdCINhpu2qGeQTSx84yXXFmeIx4VsbbugeamtjdGh5zMpnCaCVvB72PKyw

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
-- TOC entry 5157 (class 0 OID 0)
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
-- TOC entry 5158 (class 0 OID 0)
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
-- TOC entry 5159 (class 0 OID 0)
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
-- TOC entry 5160 (class 0 OID 0)
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
-- TOC entry 5161 (class 0 OID 0)
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
-- TOC entry 5162 (class 0 OID 0)
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
-- TOC entry 5163 (class 0 OID 0)
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
-- TOC entry 5164 (class 0 OID 0)
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
    response_type_profile_id integer,
    weight integer DEFAULT 1 NOT NULL
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
-- TOC entry 5165 (class 0 OID 0)
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
-- TOC entry 5166 (class 0 OID 0)
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
-- TOC entry 5167 (class 0 OID 0)
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
-- TOC entry 5168 (class 0 OID 0)
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
-- TOC entry 5169 (class 0 OID 0)
-- Dependencies: 219
-- Name: response_type_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.response_type_id_seq OWNED BY public.response_type.id;


--
-- TOC entry 258 (class 1259 OID 17013)
-- Name: response_type_profile; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.response_type_profile (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    code character varying(10) NOT NULL,
    description text
);


ALTER TABLE public.response_type_profile OWNER TO postgres;

--
-- TOC entry 257 (class 1259 OID 17012)
-- Name: response_type_profile_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.response_type_profile_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.response_type_profile_id_seq OWNER TO postgres;

--
-- TOC entry 5170 (class 0 OID 0)
-- Dependencies: 257
-- Name: response_type_profile_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.response_type_profile_id_seq OWNED BY public.response_type_profile.id;


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
-- TOC entry 5171 (class 0 OID 0)
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
-- TOC entry 5172 (class 0 OID 0)
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
-- TOC entry 5173 (class 0 OID 0)
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
-- TOC entry 5174 (class 0 OID 0)
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
-- TOC entry 5175 (class 0 OID 0)
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
-- TOC entry 5176 (class 0 OID 0)
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
-- TOC entry 5177 (class 0 OID 0)
-- Dependencies: 229
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;


--
-- TOC entry 4883 (class 2604 OID 16863)
-- Name: analise_comparativa id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.analise_comparativa ALTER COLUMN id SET DEFAULT nextval('public.analise_comparativa_id_seq'::regclass);


--
-- TOC entry 4845 (class 2604 OID 16589)
-- Name: company id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company ALTER COLUMN id SET DEFAULT nextval('public.company_id_seq'::regclass);


--
-- TOC entry 4860 (class 2604 OID 16682)
-- Name: company_test id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company_test ALTER COLUMN id SET DEFAULT nextval('public.company_test_id_seq'::regclass);


--
-- TOC entry 4866 (class 2604 OID 16717)
-- Name: key_configuration_question id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.key_configuration_question ALTER COLUMN id SET DEFAULT nextval('public.key_configuration_question_id_seq'::regclass);


--
-- TOC entry 4848 (class 2604 OID 16607)
-- Name: person id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person ALTER COLUMN id SET DEFAULT nextval('public.person_id_seq'::regclass);


--
-- TOC entry 4851 (class 2604 OID 16616)
-- Name: person_company id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company ALTER COLUMN id SET DEFAULT nextval('public.person_company_id_seq'::regclass);


--
-- TOC entry 4863 (class 2604 OID 16701)
-- Name: question id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question ALTER COLUMN id SET DEFAULT nextval('public.question_id_seq'::regclass);


--
-- TOC entry 4869 (class 2604 OID 16730)
-- Name: question_configuration id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_configuration ALTER COLUMN id SET DEFAULT nextval('public.question_configuration_id_seq'::regclass);


--
-- TOC entry 4870 (class 2604 OID 16749)
-- Name: question_response_option id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_response_option ALTER COLUMN id SET DEFAULT nextval('public.question_response_option_id_seq'::regclass);


--
-- TOC entry 4881 (class 2604 OID 16843)
-- Name: relatorio id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.relatorio ALTER COLUMN id SET DEFAULT nextval('public.relatorio_id_seq'::regclass);


--
-- TOC entry 4877 (class 2604 OID 16805)
-- Name: response id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response ALTER COLUMN id SET DEFAULT nextval('public.response_id_seq'::regclass);


--
-- TOC entry 4880 (class 2604 OID 16826)
-- Name: response_option id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_option ALTER COLUMN id SET DEFAULT nextval('public.response_option_id_seq'::regclass);


--
-- TOC entry 4843 (class 2604 OID 16571)
-- Name: response_type id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_type ALTER COLUMN id SET DEFAULT nextval('public.response_type_id_seq'::regclass);


--
-- TOC entry 4886 (class 2604 OID 17016)
-- Name: response_type_profile id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_type_profile ALTER COLUMN id SET DEFAULT nextval('public.response_type_profile_id_seq'::regclass);


--
-- TOC entry 4885 (class 2604 OID 16879)
-- Name: role id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role ALTER COLUMN id SET DEFAULT nextval('public.role_id_seq'::regclass);


--
-- TOC entry 4842 (class 2604 OID 16562)
-- Name: status id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status ALTER COLUMN id SET DEFAULT nextval('public.status_id_seq'::regclass);


--
-- TOC entry 4857 (class 2604 OID 16666)
-- Name: test id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test ALTER COLUMN id SET DEFAULT nextval('public.test_id_seq'::regclass);


--
-- TOC entry 4873 (class 2604 OID 16780)
-- Name: test_applied id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied ALTER COLUMN id SET DEFAULT nextval('public.test_applied_id_seq'::regclass);


--
-- TOC entry 4872 (class 2604 OID 16763)
-- Name: test_question id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_question ALTER COLUMN id SET DEFAULT nextval('public.test_question_id_seq'::regclass);


--
-- TOC entry 4844 (class 2604 OID 16580)
-- Name: test_type id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_type ALTER COLUMN id SET DEFAULT nextval('public.test_type_id_seq'::regclass);


--
-- TOC entry 4854 (class 2604 OID 16640)
-- Name: user id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);


--
-- TOC entry 5146 (class 0 OID 16860)
-- Dependencies: 254
-- Data for Name: analise_comparativa; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.analise_comparativa (id, company_id, titulo, pessoas_analisadas, resultados_ia, data_geracao) FROM stdin;
\.


--
-- TOC entry 5116 (class 0 OID 16586)
-- Dependencies: 224
-- Data for Name: company; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.company (id, name, cnpj, created_at, updated_at, status_id) FROM stdin;
2	teste	11112233342213	2025-09-09 14:05:39.277	2025-09-09 14:39:01.005538	1
1	Empresa teste	11111111111111	2025-09-09 10:03:49.998124	2025-09-09 14:39:59.080455	1
3	string	string	2025-09-09 16:01:26.60138	2025-09-09 16:01:26.60138	1
\.


--
-- TOC entry 5126 (class 0 OID 16679)
-- Dependencies: 234
-- Data for Name: company_test; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.company_test (id, test_id, company_id, created_at, updated_at) FROM stdin;
\.


--
-- TOC entry 5130 (class 0 OID 16714)
-- Dependencies: 238
-- Data for Name: key_configuration_question; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.key_configuration_question (id, key_name, description, created_at, updated_at) FROM stdin;
\.


--
-- TOC entry 5118 (class 0 OID 16604)
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
-- TOC entry 5120 (class 0 OID 16613)
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
-- TOC entry 5128 (class 0 OID 16698)
-- Dependencies: 236
-- Data for Name: question; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.question (id, response_type_id, question_text, created_at, updated_at) FROM stdin;
62	1	Escolha a frase que mais se parece com você:	2025-09-12 18:10:37.926832	2025-09-12 18:10:37.926832
63	1	Em um projeto de equipe, você tende a ser:	2025-09-12 18:10:38.00865	2025-09-12 18:10:38.00865
64	1	Ao enfrentar um problema, você:	2025-09-12 18:10:38.025379	2025-09-12 18:10:38.025379
65	1	Em uma conversa, você prefere:	2025-09-12 18:10:38.039677	2025-09-12 18:10:38.039677
66	1	Diante de uma mudança inesperada, você:	2025-09-12 18:10:38.056037	2025-09-12 18:10:38.056037
67	1	Eu sou uma pessoa que gosta de festas e de estar com pessoas.	2025-09-13 17:02:26.796771	2025-09-13 17:02:26.796771
68	1	Eu me preocupo com as coisas com facilidade.	2025-09-13 17:02:26.827531	2025-09-13 17:02:26.827531
69	1	Eu tenho uma imaginação fértil.	2025-09-13 17:02:26.842083	2025-09-13 17:02:26.842083
70	1	Eu gosto de ajudar as pessoas.	2025-09-13 17:02:26.857374	2025-09-13 17:02:26.857374
71	1	Eu sou sempre organizado e preciso.	2025-09-13 17:02:26.872235	2025-09-13 17:02:26.872235
72	1	Eu sou reservado e quieto.	2025-09-13 17:02:26.890586	2025-09-13 17:02:26.890586
73	1	Eu raramente sinto ansiedade ou medo.	2025-09-13 17:02:26.905496	2025-09-13 17:02:26.905496
74	1	Eu prefiro rotina a novas experiências.	2025-09-13 17:02:26.919681	2025-09-13 17:02:26.919681
75	1	Eu duvido das intenções dos outros.	2025-09-13 17:02:26.934589	2025-09-13 17:02:26.934589
76	1	Eu não gosto de seguir regras e planos.	2025-09-13 17:02:26.948989	2025-09-13 17:02:26.948989
77	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.330077	2025-09-13 17:11:14.330077
78	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.349033	2025-09-13 17:11:14.349033
79	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.360127	2025-09-13 17:11:14.360127
80	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.411688	2025-09-13 17:11:14.411688
81	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.42184	2025-09-13 17:11:14.42184
82	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.434976	2025-09-13 17:11:14.434976
83	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.447594	2025-09-13 17:11:14.447594
84	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.458642	2025-09-13 17:11:14.458642
85	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.469182	2025-09-13 17:11:14.469182
86	2	Escolha a opção que MAIS e a que MENOS se parece com você.	2025-09-13 17:11:14.4817	2025-09-13 17:11:14.4817
\.


--
-- TOC entry 5132 (class 0 OID 16727)
-- Dependencies: 240
-- Data for Name: question_configuration; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.question_configuration (id, question_id, key_configuration_question_id, value) FROM stdin;
\.


--
-- TOC entry 5134 (class 0 OID 16746)
-- Dependencies: 242
-- Data for Name: question_response_option; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.question_response_option (id, question_id, text, response_type_profile_id, weight) FROM stdin;
244	62	Busco resultados e desafios	1	10
245	62	Sou otimista e social	2	10
246	62	Prefiro estabilidade e harmonia	3	10
247	62	Sou detalhista e preciso	4	10
248	63	O líder que direciona as tarefas	1	10
249	63	O que inspira e motiva o grupo	2	10
250	63	O que mantém a paz e a colaboração	3	10
251	63	O que garante a qualidade e os padrões	4	10
252	64	Toma decisões rápidas e diretas	1	10
253	64	Busca a opinião de todos para encontrar uma solução	2	10
254	64	Segue um método comprovado	3	10
255	64	Analisa os dados minuciosamente	4	10
256	65	Ir direto ao ponto	1	10
257	65	Compartilhar histórias e experiências	2	10
258	65	Ouvir atentamente e apoiar	3	10
259	65	Fazer perguntas precisas	4	10
260	66	Se adapta rapidamente e avança	1	10
261	66	Enxerga novas oportunidades	2	10
262	66	Resiste para manter o controle	3	10
263	66	Busca entender todos os detalhes antes de agir	4	10
264	67	Discordo totalmente	5	1
265	67	Discordo	5	2
266	67	Neutro	5	3
267	67	Concordo	5	4
268	67	Concordo totalmente	5	5
269	68	Discordo totalmente	6	5
270	68	Discordo	6	4
271	68	Neutro	6	3
272	68	Concordo	6	2
273	68	Concordo totalmente	6	1
274	69	Discordo totalmente	7	1
275	69	Discordo	7	2
276	69	Neutro	7	3
277	69	Concordo	7	4
278	69	Concordo totalmente	7	5
279	70	Discordo totalmente	8	1
280	70	Discordo	8	2
281	70	Neutro	8	3
282	70	Concordo	8	4
283	70	Concordo totalmente	8	5
284	71	Discordo totalmente	9	1
285	71	Discordo	9	2
286	71	Neutro	9	3
287	71	Concordo	9	4
288	71	Concordo totalmente	9	5
289	72	Discordo totalmente	5	5
290	72	Discordo	5	4
291	72	Neutro	5	3
292	72	Concordo	5	2
293	72	Concordo totalmente	5	1
294	73	Discordo totalmente	6	1
295	73	Discordo	6	2
296	73	Neutro	6	3
297	73	Concordo	6	4
298	73	Concordo totalmente	6	5
299	74	Discordo totalmente	7	5
300	74	Discordo	7	4
301	74	Neutro	7	3
302	74	Concordo	7	2
303	74	Concordo totalmente	7	1
304	75	Discordo totalmente	8	5
305	75	Discordo	8	4
306	75	Neutro	8	3
307	75	Concordo	8	2
308	75	Concordo totalmente	8	1
309	76	Discordo totalmente	9	5
310	76	Discordo	9	4
311	76	Neutro	9	3
312	76	Concordo	9	2
313	76	Concordo totalmente	9	1
314	77	Gosto de ter o controle das situações e tomar decisões rápidas.	10	1
315	77	Sou bom em convencer as pessoas e construir relacionamentos.	12	1
316	77	Prefiro um ambiente de trabalho estável e previsível.	13	1
317	77	Sigo as regras e procedimentos com cuidado e precisão.	11	1
318	78	Sou competitivo e gosto de vencer a qualquer custo.	10	1
319	78	Sou otimista e procuro interações sociais.	12	1
320	78	Sou calmo e consistente, valorizando a cooperação.	13	1
321	78	Sou analítico e busco a perfeição nos detalhes.	11	1
322	79	Gosto de desafios e luto por meus objetivos.	10	1
323	79	Sou bom em expressar minhas ideias e sou carismático.	12	1
324	79	Valorizo a harmonia e o espírito de equipe.	13	1
325	79	Fico satisfeito em entregar um trabalho de alta qualidade.	11	1
326	80	Assumo a liderança em projetos e sou focado em resultados.	10	1
327	80	Tenho um bom relacionamento com todos e sou um bom negociador.	12	1
328	80	Mantenho a calma sob pressão e sou leal.	13	1
329	80	Analiso todos os detalhes e sou metódico antes de agir.	11	1
330	81	Sou determinado e focado em metas.	10	1
331	81	Gosto de ser o centro das atenções e tenho um bom humor.	12	1
332	81	Sou um bom ouvinte e dou apoio aos outros.	13	1
333	81	Sigo as instruções à risca para evitar erros.	11	1
334	82	Prefiro tomar decisões de forma independente.	10	1
335	82	Sou criativo e gosto de novas ideias.	12	1
336	82	Sou paciente e tolerante com os colegas.	13	1
337	82	Questiono a lógica e a razão das coisas.	11	1
338	83	Lido bem com a mudança e busco por novos desafios.	10	1
339	83	Gosto de me adaptar facilmente a diferentes grupos sociais.	12	1
340	83	Busco consistência e rotina.	13	1
341	83	Tento evitar erros a todo custo e busco a perfeição.	11	1
342	84	Prefiro liderar do que seguir.	10	1
343	84	Aprecio a liberdade de expressão e sou inspirador.	12	1
344	84	Ofereço ajuda de bom grado.	13	1
345	84	Sou metódico e preciso na execução das minhas tarefas.	11	1
346	85	Aceito riscos para alcançar uma meta.	10	1
347	85	Posso me comunicar bem e sou persuasivo.	12	1
348	85	Faço as coisas de forma lenta e cuidadosa.	13	1
349	85	Verifico a qualidade do meu trabalho várias vezes.	11	1
350	86	Sou direto e objetivo.	10	1
351	86	Me adapto facilmente a diferentes grupos sociais.	12	1
352	86	Prefiro um ambiente de trabalho que valoriza a cooperação.	13	1
353	86	Gosto de analisar os prós e contras de tudo antes de agir.	11	1
\.


--
-- TOC entry 5144 (class 0 OID 16840)
-- Dependencies: 252
-- Data for Name: relatorio; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.relatorio (id, company_id, test_applied_id, title, conteudo_ia, data_geracao) FROM stdin;
\.


--
-- TOC entry 5140 (class 0 OID 16802)
-- Dependencies: 248
-- Data for Name: response; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.response (id, test_applied_id, question_id, value, created_at, updated_at) FROM stdin;
\.


--
-- TOC entry 5142 (class 0 OID 16823)
-- Dependencies: 250
-- Data for Name: response_option; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.response_option (id, response_id, question_response_option_id) FROM stdin;
\.


--
-- TOC entry 5112 (class 0 OID 16568)
-- Dependencies: 220
-- Data for Name: response_type; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.response_type (id, name, description) FROM stdin;
1	multipla_escolha	Permite ao candidato escolher uma ou mais opções pré-definidas.
2	dissertativa	Permite ao candidato escrever uma resposta em texto livre.
\.


--
-- TOC entry 5150 (class 0 OID 17013)
-- Dependencies: 258
-- Data for Name: response_type_profile; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.response_type_profile (id, name, code, description) FROM stdin;
1	D	D	Dominância 
2	I	I	Influência 
3	S	S	Estabilidade 
4	C	C	Conformidade 
5	Extroversão	E	Avalia o nível de sociabilidade e entusiasmo.
6	Neuroticismo	N	Avalia o nível de instabilidade emocional e ansiedade.
7	Abertura	Ab	Avalia a criatividade, curiosidade e disposição para novas experiências.
8	Agradabilidade	Ag	Avalia a compaixão e cooperação.
9	Conscienciosidade	C	Avalia a organização, disciplina e responsabilidade.
10	Tubarão	T	Dominância
11	Lobo	L	Conformidade
12	Águia	A	Influência
13	Gato	G	Estabilidade
\.


--
-- TOC entry 5148 (class 0 OID 16876)
-- Dependencies: 256
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.role (id, name, description) FROM stdin;
2	Admin	Acesso parcial ao sistema
3	User	Usuário normal
1	SuperAdmin	Acesso total ao sistema
\.


--
-- TOC entry 5110 (class 0 OID 16559)
-- Dependencies: 218
-- Data for Name: status; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.status (id, name, description) FROM stdin;
1	Ativo	Ativo no sistema
2	Inativo	Inativo no sistema
3	Excluido	Excluido do sistema
\.


--
-- TOC entry 5124 (class 0 OID 16663)
-- Dependencies: 232
-- Data for Name: test; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test (id, test_type_id, title, description, created_at, updated_at) FROM stdin;
12	1	Avaliação de Perfil Comportamental DISC - 5 Perguntas	Teste rápido para identificar o perfil comportamental de Dominância, Influência, Estabilidade e Conformidade.	2025-09-12 18:10:34.934122	2025-09-12 18:10:34.934122
14	2	Avaliação de Personalidade Big Five - 10 Perguntas	Teste rápido para avaliar os cinco principais traços de personalidade: Abertura, Conscienciosidade, Extroversão, Agradabilidade e Neuroticismo.	2025-09-13 17:02:26.779207	2025-09-13 17:02:26.779207
15	3	Teste de Perfil Comportamental 'Os Bichos'	Avaliação de perfil comportamental baseada na metodologia DISC, utilizando os arquétipos do Tubarão, Lobo, Águia e Gato.	2025-09-13 17:11:14.295163	2025-09-13 17:11:14.295163
\.


--
-- TOC entry 5138 (class 0 OID 16777)
-- Dependencies: 246
-- Data for Name: test_applied; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test_applied (id, status_id, company_test_id, person_id, date_begin_datetime, date_end_datetime, created_at, updated_at) FROM stdin;
\.


--
-- TOC entry 5136 (class 0 OID 16760)
-- Dependencies: 244
-- Data for Name: test_question; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test_question (id, test_id, question_id) FROM stdin;
65	12	62
66	12	63
67	12	64
68	12	65
69	12	66
70	14	67
71	14	68
72	14	69
73	14	70
74	14	71
75	14	72
76	14	73
77	14	74
78	14	75
79	14	76
80	15	77
81	15	78
82	15	79
83	15	80
84	15	81
85	15	82
86	15	83
87	15	84
88	15	85
89	15	86
\.


--
-- TOC entry 5114 (class 0 OID 16577)
-- Dependencies: 222
-- Data for Name: test_type; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test_type (id, name, description) FROM stdin;
1	DISC	teste DISC
2	Big Five	Teste de personalidade que avalia os cinco grandes traços de personalidade.
3	Os Bichos	Avaliação de perfil comportamental baseada nos arquétipos do Tubarão, Lobo, Águia e Gato.
\.


--
-- TOC entry 5122 (class 0 OID 16637)
-- Dependencies: 230
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."user" (id, person_id, status_id, email, password, created_at, updated_at, role_id) FROM stdin;
1	1	1	admin@email.com	senha123	2025-09-09 10:03:49.998124	2025-09-09 10:03:49.998124	1
2	8	1	arthurthome02@gmail.com	a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3	2025-09-09 16:00:00.9051	2025-09-09 16:00:00.9051	1
\.


--
-- TOC entry 5178 (class 0 OID 0)
-- Dependencies: 253
-- Name: analise_comparativa_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.analise_comparativa_id_seq', 1, false);


--
-- TOC entry 5179 (class 0 OID 0)
-- Dependencies: 223
-- Name: company_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.company_id_seq', 3, true);


--
-- TOC entry 5180 (class 0 OID 0)
-- Dependencies: 233
-- Name: company_test_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.company_test_id_seq', 1, false);


--
-- TOC entry 5181 (class 0 OID 0)
-- Dependencies: 237
-- Name: key_configuration_question_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.key_configuration_question_id_seq', 1, false);


--
-- TOC entry 5182 (class 0 OID 0)
-- Dependencies: 227
-- Name: person_company_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.person_company_id_seq', 5, true);


--
-- TOC entry 5183 (class 0 OID 0)
-- Dependencies: 225
-- Name: person_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.person_id_seq', 8, true);


--
-- TOC entry 5184 (class 0 OID 0)
-- Dependencies: 239
-- Name: question_configuration_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_configuration_id_seq', 1, false);


--
-- TOC entry 5185 (class 0 OID 0)
-- Dependencies: 235
-- Name: question_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_id_seq', 86, true);


--
-- TOC entry 5186 (class 0 OID 0)
-- Dependencies: 241
-- Name: question_response_option_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_response_option_id_seq', 353, true);


--
-- TOC entry 5187 (class 0 OID 0)
-- Dependencies: 251
-- Name: relatorio_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.relatorio_id_seq', 1, false);


--
-- TOC entry 5188 (class 0 OID 0)
-- Dependencies: 247
-- Name: response_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.response_id_seq', 1, false);


--
-- TOC entry 5189 (class 0 OID 0)
-- Dependencies: 249
-- Name: response_option_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.response_option_id_seq', 1, false);


--
-- TOC entry 5190 (class 0 OID 0)
-- Dependencies: 219
-- Name: response_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.response_type_id_seq', 4, true);


--
-- TOC entry 5191 (class 0 OID 0)
-- Dependencies: 257
-- Name: response_type_profile_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.response_type_profile_id_seq', 4, true);


--
-- TOC entry 5192 (class 0 OID 0)
-- Dependencies: 255
-- Name: role_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.role_id_seq', 3, true);


--
-- TOC entry 5193 (class 0 OID 0)
-- Dependencies: 217
-- Name: status_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.status_id_seq', 3, true);


--
-- TOC entry 5194 (class 0 OID 0)
-- Dependencies: 245
-- Name: test_applied_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.test_applied_id_seq', 1, false);


--
-- TOC entry 5195 (class 0 OID 0)
-- Dependencies: 231
-- Name: test_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.test_id_seq', 15, true);


--
-- TOC entry 5196 (class 0 OID 0)
-- Dependencies: 243
-- Name: test_question_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.test_question_id_seq', 89, true);


--
-- TOC entry 5197 (class 0 OID 0)
-- Dependencies: 221
-- Name: test_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.test_type_id_seq', 8, true);


--
-- TOC entry 5198 (class 0 OID 0)
-- Dependencies: 229
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_id_seq', 2, true);


--
-- TOC entry 4932 (class 2606 OID 16868)
-- Name: analise_comparativa analise_comparativa_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.analise_comparativa
    ADD CONSTRAINT analise_comparativa_pkey PRIMARY KEY (id);


--
-- TOC entry 4894 (class 2606 OID 16890)
-- Name: company company_cnpj_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company
    ADD CONSTRAINT company_cnpj_key UNIQUE (cnpj);


--
-- TOC entry 4896 (class 2606 OID 16595)
-- Name: company company_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company
    ADD CONSTRAINT company_pkey PRIMARY KEY (id);


--
-- TOC entry 4910 (class 2606 OID 16686)
-- Name: company_test company_test_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company_test
    ADD CONSTRAINT company_test_pkey PRIMARY KEY (id);


--
-- TOC entry 4914 (class 2606 OID 16725)
-- Name: key_configuration_question key_configuration_question_key_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.key_configuration_question
    ADD CONSTRAINT key_configuration_question_key_name_key UNIQUE (key_name);


--
-- TOC entry 4916 (class 2606 OID 16723)
-- Name: key_configuration_question key_configuration_question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.key_configuration_question
    ADD CONSTRAINT key_configuration_question_pkey PRIMARY KEY (id);


--
-- TOC entry 4900 (class 2606 OID 16620)
-- Name: person_company person_company_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company
    ADD CONSTRAINT person_company_pkey PRIMARY KEY (id);


--
-- TOC entry 4898 (class 2606 OID 16611)
-- Name: person person_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person
    ADD CONSTRAINT person_pkey PRIMARY KEY (id);


--
-- TOC entry 4918 (class 2606 OID 16734)
-- Name: question_configuration question_configuration_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_configuration
    ADD CONSTRAINT question_configuration_pkey PRIMARY KEY (id);


--
-- TOC entry 4912 (class 2606 OID 16707)
-- Name: question question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question
    ADD CONSTRAINT question_pkey PRIMARY KEY (id);


--
-- TOC entry 4920 (class 2606 OID 16753)
-- Name: question_response_option question_response_option_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_response_option
    ADD CONSTRAINT question_response_option_pkey PRIMARY KEY (id);


--
-- TOC entry 4930 (class 2606 OID 16848)
-- Name: relatorio relatorio_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.relatorio
    ADD CONSTRAINT relatorio_pkey PRIMARY KEY (id);


--
-- TOC entry 4928 (class 2606 OID 16828)
-- Name: response_option response_option_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_option
    ADD CONSTRAINT response_option_pkey PRIMARY KEY (id);


--
-- TOC entry 4926 (class 2606 OID 16811)
-- Name: response response_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response
    ADD CONSTRAINT response_pkey PRIMARY KEY (id);


--
-- TOC entry 4890 (class 2606 OID 16575)
-- Name: response_type response_type_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_type
    ADD CONSTRAINT response_type_pkey PRIMARY KEY (id);


--
-- TOC entry 4936 (class 2606 OID 17020)
-- Name: response_type_profile response_type_profile_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_type_profile
    ADD CONSTRAINT response_type_profile_pkey PRIMARY KEY (id);


--
-- TOC entry 4934 (class 2606 OID 16883)
-- Name: role role_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT role_pkey PRIMARY KEY (id);


--
-- TOC entry 4888 (class 2606 OID 16566)
-- Name: status status_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status
    ADD CONSTRAINT status_pkey PRIMARY KEY (id);


--
-- TOC entry 4924 (class 2606 OID 16785)
-- Name: test_applied test_applied_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied
    ADD CONSTRAINT test_applied_pkey PRIMARY KEY (id);


--
-- TOC entry 4908 (class 2606 OID 16672)
-- Name: test test_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test
    ADD CONSTRAINT test_pkey PRIMARY KEY (id);


--
-- TOC entry 4922 (class 2606 OID 16765)
-- Name: test_question test_question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_question
    ADD CONSTRAINT test_question_pkey PRIMARY KEY (id);


--
-- TOC entry 4892 (class 2606 OID 16584)
-- Name: test_type test_type_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_type
    ADD CONSTRAINT test_type_pkey PRIMARY KEY (id);


--
-- TOC entry 4902 (class 2606 OID 16651)
-- Name: user user_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_email_key UNIQUE (email);


--
-- TOC entry 4904 (class 2606 OID 16649)
-- Name: user user_person_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_person_id_key UNIQUE (person_id);


--
-- TOC entry 4906 (class 2606 OID 16647)
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);


--
-- TOC entry 4963 (class 2606 OID 16869)
-- Name: analise_comparativa analise_comparativa_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.analise_comparativa
    ADD CONSTRAINT analise_comparativa_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 4937 (class 2606 OID 16598)
-- Name: company company_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company
    ADD CONSTRAINT company_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.status(id);


--
-- TOC entry 4945 (class 2606 OID 16692)
-- Name: company_test company_test_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company_test
    ADD CONSTRAINT company_test_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 4946 (class 2606 OID 16687)
-- Name: company_test company_test_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.company_test
    ADD CONSTRAINT company_test_test_id_fkey FOREIGN KEY (test_id) REFERENCES public.test(id);


--
-- TOC entry 4950 (class 2606 OID 17021)
-- Name: question_response_option fk_response_type_profile; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_response_option
    ADD CONSTRAINT fk_response_type_profile FOREIGN KEY (response_type_profile_id) REFERENCES public.response_type_profile(id);


--
-- TOC entry 4941 (class 2606 OID 16884)
-- Name: user fk_user_role; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT fk_user_role FOREIGN KEY (role_id) REFERENCES public.role(id);


--
-- TOC entry 4938 (class 2606 OID 16626)
-- Name: person_company person_company_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company
    ADD CONSTRAINT person_company_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 4939 (class 2606 OID 16621)
-- Name: person_company person_company_person_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company
    ADD CONSTRAINT person_company_person_id_fkey FOREIGN KEY (person_id) REFERENCES public.person(id);


--
-- TOC entry 4940 (class 2606 OID 16631)
-- Name: person_company person_company_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.person_company
    ADD CONSTRAINT person_company_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.status(id);


--
-- TOC entry 4948 (class 2606 OID 16740)
-- Name: question_configuration question_configuration_key_configuration_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_configuration
    ADD CONSTRAINT question_configuration_key_configuration_question_id_fkey FOREIGN KEY (key_configuration_question_id) REFERENCES public.key_configuration_question(id);


--
-- TOC entry 4949 (class 2606 OID 16735)
-- Name: question_configuration question_configuration_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_configuration
    ADD CONSTRAINT question_configuration_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.question(id);


--
-- TOC entry 4951 (class 2606 OID 16754)
-- Name: question_response_option question_response_option_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_response_option
    ADD CONSTRAINT question_response_option_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.question(id);


--
-- TOC entry 4947 (class 2606 OID 16708)
-- Name: question question_response_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question
    ADD CONSTRAINT question_response_type_id_fkey FOREIGN KEY (response_type_id) REFERENCES public.response_type(id);


--
-- TOC entry 4961 (class 2606 OID 16849)
-- Name: relatorio relatorio_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.relatorio
    ADD CONSTRAINT relatorio_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.company(id);


--
-- TOC entry 4962 (class 2606 OID 16854)
-- Name: relatorio relatorio_test_applied_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.relatorio
    ADD CONSTRAINT relatorio_test_applied_id_fkey FOREIGN KEY (test_applied_id) REFERENCES public.test_applied(id);


--
-- TOC entry 4959 (class 2606 OID 16834)
-- Name: response_option response_option_question_response_option_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_option
    ADD CONSTRAINT response_option_question_response_option_id_fkey FOREIGN KEY (question_response_option_id) REFERENCES public.question_response_option(id);


--
-- TOC entry 4960 (class 2606 OID 16829)
-- Name: response_option response_option_response_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response_option
    ADD CONSTRAINT response_option_response_id_fkey FOREIGN KEY (response_id) REFERENCES public.response(id);


--
-- TOC entry 4957 (class 2606 OID 16817)
-- Name: response response_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response
    ADD CONSTRAINT response_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.question(id);


--
-- TOC entry 4958 (class 2606 OID 16812)
-- Name: response response_test_applied_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.response
    ADD CONSTRAINT response_test_applied_id_fkey FOREIGN KEY (test_applied_id) REFERENCES public.test_applied(id);


--
-- TOC entry 4954 (class 2606 OID 16791)
-- Name: test_applied test_applied_company_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied
    ADD CONSTRAINT test_applied_company_test_id_fkey FOREIGN KEY (company_test_id) REFERENCES public.company_test(id);


--
-- TOC entry 4955 (class 2606 OID 16796)
-- Name: test_applied test_applied_person_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied
    ADD CONSTRAINT test_applied_person_id_fkey FOREIGN KEY (person_id) REFERENCES public.person(id);


--
-- TOC entry 4956 (class 2606 OID 16786)
-- Name: test_applied test_applied_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_applied
    ADD CONSTRAINT test_applied_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.status(id);


--
-- TOC entry 4952 (class 2606 OID 16771)
-- Name: test_question test_question_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_question
    ADD CONSTRAINT test_question_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.question(id);


--
-- TOC entry 4953 (class 2606 OID 16766)
-- Name: test_question test_question_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_question
    ADD CONSTRAINT test_question_test_id_fkey FOREIGN KEY (test_id) REFERENCES public.test(id);


--
-- TOC entry 4944 (class 2606 OID 16673)
-- Name: test test_test_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test
    ADD CONSTRAINT test_test_type_id_fkey FOREIGN KEY (test_type_id) REFERENCES public.test_type(id);


--
-- TOC entry 4942 (class 2606 OID 16652)
-- Name: user user_person_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_person_id_fkey FOREIGN KEY (person_id) REFERENCES public.person(id);


--
-- TOC entry 4943 (class 2606 OID 16657)
-- Name: user user_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.status(id);


-- Completed on 2025-09-13 17:26:51

--
-- PostgreSQL database dump complete
--

\unrestrict tv62vwdCINhpu2qGeQTSx84yXXFmeIx4VsbbugeamtjdGh5zMpnCaCVvB72PKyw

--
-- Database "postgres" dump
--

\connect postgres

--
-- PostgreSQL database dump
--

\restrict zcdT2eT3UWgwJyNeDdbCQoBuTa2V2bUEFLvVHJFwb8OgHLg5DixlBLjpj3AXVZM

-- Dumped from database version 17.6
-- Dumped by pg_dump version 17.6

-- Started on 2025-09-13 17:26:51

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
7084	2025-09-11 23:32:36.830118-03	ArthurThome
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


-- Completed on 2025-09-13 17:26:52

--
-- PostgreSQL database dump complete
--

\unrestrict zcdT2eT3UWgwJyNeDdbCQoBuTa2V2bUEFLvVHJFwb8OgHLg5DixlBLjpj3AXVZM

-- Completed on 2025-09-13 17:26:52

--
-- PostgreSQL database cluster dump complete
--

