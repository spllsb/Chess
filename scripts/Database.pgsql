CREATE TABLE public.tournaments (
	id uuid NOT null primary key,
	"name" varchar(64) NOT NULL,
	max_players numeric NOT NULL,
	updated_at timestamp NOT NULL
);

CREATE TABLE public.players (
	user_id uuid NOT null primary key,
	username varchar(50) NOT NULL
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
	id uuid NOT NULL,
	title varchar(300) NOT NULL,
	"content" text NOT NULL,
	full_name_author varchar(70) NOT NULL,
	created_at timestamp NOT NULL,
	updated_at timestamp NOT NULL,
	img_file_name varchar(100) not null,
	CONSTRAINT articles_pkey PRIMARY KEY (id)
);


CREATE TABLE public.comments (
	id uuid NOT null primary key,
	article_id uuid NOT null,
	content text NOT NULL,
	author varchar(70) not null,
--	created_at timestamp NOT NULL, 
--	updated_at timestamp NOT null
	
    CONSTRAINT fk_article_id FOREIGN KEY (article_id) REFERENCES articles (id)
);

create table player_tournament_participation(
	tournament_id uuid not null,
	player_id uuid not null,
	result varchar(10) not null,
	
	CONSTRAINT fk_tournament_id FOREIGN KEY (tournament_id) REFERENCES tournaments (id),
	CONSTRAINT fk_player_id FOREIGN KEY (player_id) REFERENCES players (user_id)
);


CREATE TABLE public.drills (
	id int4 NOT NULL,
	description varchar(200) NOT NULL,
	"name" varchar(50) NOT NULL,
	category varchar(20) NOT NULL,
	start_position varchar(100) NOT NULL,
	CONSTRAINT drills_pkey PRIMARY KEY (id)
);