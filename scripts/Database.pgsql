CREATE TABLE public.tournaments (
	id uuid NOT null primary key,
	"name" varchar(64) NOT NULL,
	max_players numeric NOT NULL,
	updated_at timestamp NOT NULL
);



CREATE TABLE public.users (
	id uuid NOT null primary key,
	email varchar(50) NOT NULL,
	username varchar(50) NOT NULL,
	"password" varchar(50) NOT NULL,
	salt varchar(50) NOT NULL,
	created_at timestamp NOT NULL,
	updated_at timestamp NOT null
);


CREATE TABLE public.articles (
	id uuid NOT null primary key,
	title varchar(300) NOT NULL,
	content text NOT NULL,
	full_name_author varchar(70) not null,
	created_at timestamp NOT NULL,
	updated_at timestamp NOT null
);